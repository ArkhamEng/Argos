using Argos.Models;
using Argos.Models.Business;
using Argos.Models.Config;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    public class PersonSearchVieModel<T> where T :Person
    {
        public SelectList States { get; set; }

        public SelectList Cities { get; set; }

        public ICollection<PersonViewModel<T>> Entities { get; set; }


        public PersonSearchVieModel()
        {
            this.Entities = new List<PersonViewModel<T>>();
            this.States = new List<State>().ToSelectList();
            this.Cities = new List<Town>().ToSelectList();
        }
    }

}