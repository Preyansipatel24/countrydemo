using System.ComponentModel.DataAnnotations;

namespace CountryDemo.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
