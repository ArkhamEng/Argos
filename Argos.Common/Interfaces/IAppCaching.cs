using Argos.Models.Interfaces;
using System.Collections.Generic;

namespace Argos.Common.Interfaces
{
    public interface IAppCache
    {
        IEnumerable<ISelectable> AccountTypes { get; }

        IEnumerable<ISelectable> PaymentPeriodes { get; }

        IEnumerable<ISelectable> MaintPeriodes { get; }


        IEnumerable<ISelectable> AccountStatuses { get; }

        IEnumerable<ISelectable> ServiceTypes { get; }

        IEnumerable<ISelectable> CreditStatus { get; }

        IEnumerable<ISelectable> Categories { get; }

        IEnumerable<ISelectable> Makers { get; }

        IEnumerable<ISelectable> MeasureUnits { get; }

        IEnumerable<ISelectable> EmailTypes { get; }

        IEnumerable<ISelectable> FlowDirections { get; }

        IEnumerable<ISelectable> PayForms { get; }

        IEnumerable<ISelectable> PayMethods { get; }

        IEnumerable<ISelectable> PhoneTypes { get; }

        IEnumerable<ISelectable> AddressTypes { get; }

        IEnumerable<ISelectable> PurchaseStatus { get; }

        IEnumerable<ISelectable> SaleStatus { get; }

        IEnumerable<ISelectable> States { get; }

        void Reload(string name);

        IConfig GetConfig();

    }

    public interface IConfig
    {
        int DaysToPay { get; }

        /// <summary>
        /// Lock session duration in minutes
        /// </summary>
        int LockDuration { get; }

         /// <summary>
         /// Sessón expiration time in minutes
         /// </summary>
         int SessionExpTime  { get; }
    }

}
