using System.ComponentModel.DataAnnotations;

namespace CountryDemo.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }
        public int StateId { get; set; }
        public int CoutryId { get; set; }
    }
}
