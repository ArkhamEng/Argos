using Argos.Models.Interfaces;
using System.Collections.Generic;

namespace Argos.Common.Interfaces
{
    public interface IAppCache
    {
        IEnumerable<ISelectable> CreditStatus { get; }

        IEnumerable<ISelectable> Categories { get; }

        IEnumerable<ISelectable> Makers { get; }

        IEnumerable<ISelectable> MeasureUnits { get; }

        IEnumerable<ISelectable> EmailTypes { get; }

        IEnumerable<ISelectable> FlowDirections { get; }

        double Iva { get; }

        IEnumerable<ISelectable> PayForms { get; }

        IEnumerable<ISelectable> PayMethods { get; }

        IEnumerable<ISelectable> PhoneTypes { get; }

        IEnumerable<ISelectable> AddressTypes { get; }

        IEnumerable<ISelectable> PurchaseStatus { get; }

        IEnumerable<ISelectable> SaleStatus { get; }

        IEnumerable<ISelectable> States { get; }


        void LoadCache();

        void Reload(string name);
    }
}
