using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CountryDemo.Models
{
    public class stateviewmodel
    {
        public int CountryId { get; set; }
        public string Country { get; set; }

        public int stateID { get; set; }

        public string stateName { get; set; }
    }
}
