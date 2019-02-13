using Argos.Common.Constants;
using Argos.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Argos.Models.Production;
using Argos.Common.Enums;
using Argos.Models;
using System.Threading;

namespace Argos.Web.Support
{
    public class TaskManager : IDisposable
    {
        #region Singleton
        private static TaskManager instance;

        private static object objLck = new object();

        public static TaskManager Instance
        {
            get
            {
                lock (objLck)
                {
                    if (instance == null)
                        instance = new TaskManager();

                    return instance;
                }
            }
        }

        #endregion

        System.Timers.Timer timer;
        ApplicationDbContext db;
        static bool IsBusy = false;


        static DateTime? lastRun = null;

        private TaskManager()
        {
            timer = new System.Timers.Timer(Numbers.MilisecPerMinute);
            this.timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //si ya hay una ejecución exitosa de hoy, no hago nada
            if (lastRun != null && lastRun.Value == DateTime.Now.TodayLocal())
                return;

            //entre 0 y 2 hrs
            if (DateTime.Now.ToLocal().Hour >= Numbers.Zero && DateTime.Now.ToLocal().Hour <= Numbers.Two)
            {
                new Thread(() =>
                {
                    ProgramServices();

                }).Start();
            }
        }

        public void StartMonitor()
        {
            this.timer.Start();
        }

        private void ProgramServices()
        {

            timer.Stop();

            try
            {
                lock (objLck)
                {
                    IsBusy = true;

                    var begin = DateTime.Now.ToLocal();

                    using (db = new ApplicationDbContext())
                    {
                        var sCount = 0; var cCount = 0; var aCount = 0;

                        var month = DateTime.Now.ToLocal().Month;
                        var year = DateTime.Now.ToLocal().Year;

                        var now = DateTime.Now.ToLocal();

                        //busco las cuentas activas que incluyan poliza o mantenimientos, y que sus
                        //fechas de ultima programación sean menores que el día de hoy
                        var accounts = (from a in db.Accounts
                                    where a.IsActive && a.AccountStatusId == StatusAccount.Active &&
                                    (a.HasMaintenance && (a.LastMaintenanceDate == null || a.LastMaintenanceDate <= now)) ||
                                    (a.HasPolicy && (a.LastPaymentDate == null || a.LastPaymentDate <= now))

                                    select a).ToList();

                        foreach (var account in accounts)
                        {
                            var occ = CreateOccerrences(account);

                            sCount += occ.OfType<Service>().Count();
                            cCount += occ.OfType<Service>().Count();

                            if (occ.Count > Numbers.Zero)
                            {
                                db.Occurences.AddRange(occ);
                                db.Entry(account).Property(a => a.LastMaintenanceDate).IsModified = true;
                                db.Entry(account).Property(a => a.LastMaintenanceDate).IsModified = true;
                                db.Entry(account).Property(a => a.CutOffDate).IsModified = true;
                                db.SaveChanges();
                            }
                            aCount++;
                        }

                        var end = DateTime.Now.ToLocal();

                        var taskLog = new Models.Analytic.ExecutionTaskLog
                        {
                            Comment = string.Format("Scheduled Services {0} | Scheduled Charges {1} | Accounts Analized {2}", sCount, cCount, aCount),
                            Duration = (end - begin).Milliseconds,
                            HasSucced = true,
                            TaskName = "Schedule Charges and Services",
                            InsDate = DateTime.Now.ToLocal(),
                        };

                        db.ExecutionTaskLogs.Add(taskLog);
                        db.SaveChanges();

                        lastRun = DateTime.Now.TodayLocal();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                timer.Start();
            }
        }


        public List<Occurence> CreateOccerrences(Account account)
        {
            try
            {
                List<Occurence> list = new List<Occurence>();

                //la fecha actual mas un mes
                var nextMonth = DateTime.Now.ToLocal().AddMonths(Numbers.One);

                //dias del mes siguiente
                var daysInMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);

                //obtengo la fecha límite (final del próximo mes)
                var limitDate = new DateTime(nextMonth.Year, nextMonth.Month, daysInMonth);

                //obtengo la fecha de referencia
                var maintRefDate = account.LastMaintenanceDate ?? account.BeginDate;

                if (account.HasMaintenance && maintRefDate < DateTime.Now.ToLocal())
                {
                    //obtengo la fecha estimada sumando los meses configurados a la fecha de referencia
                    var eDate = maintRefDate.AddMonths(account.MaintPeriodId);

                    //mientras la fecha sea menor que el final del siguiente mes
                    while (eDate < limitDate)
                    {
                        var service = new Service
                        {
                            ServiceTypeId = ServiceTypes.Preventive,
                            ScheduleDate = new DateTime(eDate.Year, eDate.Month, Numbers.One),
                            DueDate = new DateTime(eDate.Year, eDate.Month, DateTime.DaysInMonth(eDate.Year, eDate.Month)),
                            AccountId = account.AccountId
                        };

                        account.LastMaintenanceDate = service.ScheduleDate;

                        list.Add(service);

                        eDate = eDate.AddMonths(account.MaintPeriodId);
                    }
                }

                //obtengo la fecha de referencia
                var payRefDate = account.LastPaymentDate ?? account.BeginDate;

                if (account.HasPolicy && payRefDate < DateTime.Now.ToLocal())
                {
                    //obtengo la fecha estimada sumando los meses configurados a la fecha de referencia
                    var eDate = payRefDate.AddMonths(account.PaymentPeriodId);

                    //mientras la fecha sea menor que el final del siguiente mes
                    while (eDate < limitDate)
                    {
                        var charge = new Charge
                        {
                            ScheduleDate = new DateTime(eDate.Year, eDate.Month, Numbers.One),
                            DueDate = new DateTime(eDate.Year, eDate.Month, Numbers.Config.DaysToPay),
                            Amount = account.PolicyCost,
                            AccountId = account.AccountId
                        };

                        account.LastPaymentDate = charge.ScheduleDate;

                        list.Add(charge);

                        eDate = eDate.AddMonths(account.PaymentPeriodId);
                    }
                }

                return list;
            }
            catch (Exception)
            {
                return new List<Occurence>();
            }
        }

        public void Dispose()
        {
            if (timer != null)
            {
                this.timer.Elapsed -= Timer_Elapsed;

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }


    }
}