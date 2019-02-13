using Argos.Common.Constants;
using Argos.Common.Interfaces;
using Argos.Data.Context;
using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Argos.Support
{
    [System.ComponentModel.DataObject]
    public class AppCache : IAppCache
    {
        private static object objLck = new object();
        private static AppCache instance = null;
        public static AppCache Instance
        {
            get
            {
                lock (objLck)
                {
                    if (instance == null)
                        instance = new AppCache();

                    return instance;
                }
            }
        }

        public IEnumerable<ISelectable> MaintPeriodes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(MaintPeriodes));
            }
        }

        public IEnumerable<ISelectable> PaymentPeriodes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(PaymentPeriodes));
            }
        }


        public IEnumerable<ISelectable> AccountTypes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(AccountTypes));
            }
        }

        public IEnumerable<ISelectable> AccountStatuses
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(AccountStatuses));
            }
        }

        public IEnumerable<ISelectable> ServiceTypes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(ServiceTypes));
            }
        }

        public IEnumerable<ISelectable> States
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(States));
            }
        }

        public IEnumerable<ISelectable> CreditStatus
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(CreditStatus));
            }
        }

        public IEnumerable<ISelectable> PayForms
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(PayForms));
            }
        }

        public IEnumerable<ISelectable> PayMethods
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(PayMethods));
            }
        }

        public IEnumerable<ISelectable> PurchaseStatus
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(PurchaseStatus));
            }
        }

        public IEnumerable<ISelectable> SaleStatus
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(SaleStatus));
            }
        }


        public IEnumerable<ISelectable> AddressTypes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(AddressTypes));
            }
        }

        public IEnumerable<ISelectable> PhoneTypes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(PhoneTypes));
            }
        }

        public IEnumerable<ISelectable> EmailTypes
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(EmailTypes));
            }
        }

        public IEnumerable<ISelectable> FlowDirections
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(FlowDirections));
            }
        }

        public IEnumerable<ISelectable> Categories
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(Categories));
            }
        }

        public IEnumerable<ISelectable> Makers
        {
            get
            {
                return (IEnumerable<ISelectable>)GetCollection(nameof(Makers));
            }
        }

        public IEnumerable<ISelectable> MeasureUnits
        {
            get
            {
               return (IEnumerable<ISelectable>)GetCollection(nameof(MeasureUnits));
            }
        }

        IEnumerable<Configuration> Config
        {
            get { return (IEnumerable<Configuration>)HttpRuntime.Cache[nameof(Config)]; }
        }



        private object GetCollection(string name)
        {
            lock (objLck)
            {
                var collection = HttpRuntime.Cache.Get(name);

                if (collection == null)
                {
                    using (var db = new ApplicationDbContext())
                    {

                        var dbSet = db.GetType().GetProperty(name).GetValue(db);

                        if (dbSet is IEnumerable<ActivableAudit>)
                        {
                            //busco los activos
                            var temp = ((IEnumerable<ActivableAudit>)dbSet).Where(a=> a.IsActive).ToList();

                            var coll = new List<ISelectable>();

                            //los ordeno por nombre
                            temp.ForEach(t => 
                            {
                                coll.Add((ISelectable)t);
                            });

                           collection = coll.OrderBy(c => c.Text).ToList();
                        }
                        else if(dbSet is IEnumerable<Configuration>)
                        {
                            
                        }
                        else
                            collection = ((IEnumerable<ISelectable>)dbSet).OrderBy(s => s.Text).ToList();


                        HttpRuntime.Cache.Add(nameof(name), collection,
                          null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                    }
                }

                return collection;
            }
        }

        public void ClearCache()
        {
            lock (objLck)
            {
                var properties = this.GetType().GetProperties();

                foreach (var p in properties)
                {
                    HttpRuntime.Cache.Remove(p.Name);
                }
            }
        }

        public void Reload(string name)
        {
            throw new NotImplementedException();
        }

        public IConfig GetConfig()
        {
            throw new NotImplementedException();
        }
    }
}