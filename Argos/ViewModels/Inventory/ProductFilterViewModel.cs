using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.ViewModels.Inventory
{
    public class ProductFilterViewModel
    {
        public int? ProductId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public string Text { get; set; }

        public bool IsGrid { get; set; }

        #region Autoparts Filter
        public int? CarMakeId { get; set; }

        public int? CarModelId { get; set; }

        public int? CarYearId { get; set; }
        #endregion
    }
}