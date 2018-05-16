using Argos.Models.Config;
using Argos.Models.HumanResources;
using Argos.Models.Production;
using Argos.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    public class PersonFilterViewModel<T>
    {
        public SelectList States { get; set; }

        public SelectList Cities { get; set; }

        public ICollection<T> Entities { get; set; }


        public PersonFilterViewModel()
        {
            this.Entities = new List<T>();
            this.States = new List<State>().ToSelectList();
            this.Cities = new List<City>().ToSelectList();
        }
    }

  
    public class PersonViewModel<T>
    {
        public SelectList States { get; set; }

        public SelectList Cities { get; set; }

        public SelectList JobPositions { get; set; }

        public T Entity { get; set; }

    }

   
}