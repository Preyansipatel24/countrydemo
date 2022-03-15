using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryDemo.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        public int CoutryId { get; set; }
  
    }
}
