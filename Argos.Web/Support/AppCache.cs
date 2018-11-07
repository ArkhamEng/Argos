using Argos.Common.Constants;
using Argos.Common.Interfaces;
using Argos.Data.Context;
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

        public double Iva { get { return (double)HttpRuntime.Cache[Labels.IVA]; } }

        public IEnumerable<ISelectable> States
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(States)]; }
        }

        public IEnumerable<ISelectable> CreditStatus
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(CreditStatus)]; }
        }

        public IEnumerable<ISelectable> PayForms
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(PayForms)]; }
        }

        public IEnumerable<ISelectable> PayMethods
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(PayMethods)]; }
        }

        public IEnumerable<ISelectable> PurchaseStatus
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(PurchaseStatus)]; }
        }

        public IEnumerable<ISelectable> SaleStatus
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(SaleStatus)]; }
        }


        public IEnumerable<ISelectable> AddressTypes
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(AddressTypes)]; }
        }

        public IEnumerable<ISelectable> PhoneTypes
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(PhoneTypes)]; }
        }

        public IEnumerable<ISelectable> EmailTypes
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(EmailTypes)]; }
        }

        public IEnumerable<ISelectable> FlowDirections
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(FlowDirections)]; }
        }

        public IEnumerable<ISelectable> Categories
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(Categories)]; }
        }

        public IEnumerable<ISelectable> Makers
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(Makers)]; }
        }

        public IEnumerable<ISelectable> MeasureUnits
        {
            get { return (IEnumerable<ISelectable>)HttpRuntime.Cache[nameof(MeasureUnits)]; }
        }

        public  void LoadCache()
        {

            using (var db = new ApplicationDbContext())
            {
                //Cargo el cache de la aplicación usando reflection para obtener el nombre de la pripiedad 
                //lo cual sera el key de cada registro

                var record = db.Configurations.FirstOrDefault(s => s.Name == Labels.IVA);
                var type = Type.GetType(record.Type);
                var iva = Convert.ChangeType(record.Value, type);

                HttpRuntime.Cache.Add(Labels.IVA, iva,
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                
                HttpRuntime.Cache.Add(nameof(States), db.States.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(CreditStatus), db.CreditStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(PayForms), db.PayForms.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(PayMethods), db.PayMethods.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(PurchaseStatus), db.PurchaseStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(SaleStatus), db.SaleStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(AddressTypes), db.AddressTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(EmailTypes), db.EmailTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(PhoneTypes), db.PhoneTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(FlowDirections), db.FlowDirections.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(Makers), db.Makers.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);


                HttpRuntime.Cache.Add(nameof(Categories), db.Categories.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add(nameof(MeasureUnits), db.MeasureUnits.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);


            }
        }

        /// <summary>
        /// Realoads one cache property using its name as parameter
        /// </summary>
        /// <param name="name">property name to reload</param>
        public void Reload(string name)
        {
            lock (objLck)
            {
                using (var db = new ApplicationDbContext())
                {
                    var set = (DbSet<ISelectable>)db.GetType().GetProperty(name).GetValue(db);
                    var lst = set.OrderBy(s => s.Text).ToList();

                    if (HttpRuntime.Cache[name] != null)
                        HttpRuntime.Cache[name] = lst;
                    else
                        HttpRuntime.Cache.Add(nameof(MeasureUnits), lst,
                          null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                }
            }
        }

        public void ClearCache()
        {
            lock(objLck)
            {
                var properties = this.GetType().GetProperties();

                foreach(var p in properties)
                {
                    HttpRuntime.Cache.Remove(p.Name);
                }
            }
        }

    }
}