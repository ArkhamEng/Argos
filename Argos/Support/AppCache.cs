using Argos.Models;
using Argos.Models.Config;
using Argos.Models.Business;
using System.Collections.Generic;
using System.Web;
using Argos.Models.Inventory;
using System.Linq;
using System.Web.Caching;

namespace Argos.Support
{
    [System.ComponentModel.DataObject]
    public class AppCache
    {
        public static List<State> States
        {
            get { return (List<State>)HttpRuntime.Cache["States"]; }
        }

        public static List<CreditStatus> CreditStatus
        {
            get { return (List<CreditStatus>)HttpRuntime.Cache["CreditStatus"]; }
        }

        public static List<PayForm> PayForms
        {
            get { return (List<PayForm>)HttpRuntime.Cache["PayForms"]; }
        }

        public static List<PayMethod> PayMethods
        {
            get { return (List<PayMethod>)HttpRuntime.Cache["PayMethods"]; }
        }

        public static List<Models.Purchasing.PurchaseStatus> PurchaseStatus
        {
            get { return (List<Models.Purchasing.PurchaseStatus>)HttpRuntime.Cache["PurchaseStatus"]; }
        }

        public static List<Models.Sales.SaleStatus> SaleStatus
        {
            get { return (List<Models.Sales.SaleStatus>)HttpRuntime.Cache["SaleStatus"]; }
        }


        public static List<AddressType> AddressTypes
        {
            get { return (List<AddressType>)HttpRuntime.Cache["AddressTypes"]; }
        }

        public static List<PhoneType> PhoneTypes
        {
            get { return (List<PhoneType>)HttpRuntime.Cache["PhoneTypes"]; }
        }

        public static List<EmailType> EmailTypes
        {
            get { return (List<EmailType>)HttpRuntime.Cache["EmailTypes"]; }
        }

        public static List<FlowDirection> FlowDirections
        {
            get { return (List<FlowDirection>)HttpRuntime.Cache["FlowDirection"]; }
        }

      


        public static void LoadApplicationCache()
        {
            using (var db = new ApplicationDbContext())
            {
                HttpRuntime.Cache.Add("States", db.States.OrderBy(s => s.Name).ToList(),
                    null,Cache.NoAbsoluteExpiration,Cache.NoSlidingExpiration,CacheItemPriority.NotRemovable,null);

                HttpRuntime.Cache.Add("CreditStatus", db.CreditStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("PayForms", db.PayForms.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("PayMethods", db.PayMethods.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("PurchaseStatus", db.PurchaseStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("SaleStatus", db.SaleStatus.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("AddressTypes", db.AddressTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("EmailTypes", db.EmailTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("PhoneTypes", db.PhoneTypes.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                HttpRuntime.Cache.Add("FlowDirections", db.FlowDirections.OrderBy(s => s.Name).ToList(),
                    null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            }
        }

        public static void DisposeChace()
        {
            using (var db = new ApplicationDbContext())
            {

            }
        }

    }
}