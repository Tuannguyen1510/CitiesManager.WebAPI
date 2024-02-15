
using System.ComponentModel.DataAnnotations;

namespace CitiesManager.ClientWeb.Models
{
    public class City
    {
        [Key]
        public  Guid Id { get; set; }

        [Required(ErrorMessage ="City Name can not be blank")]
        public string? Name { get; set; }
    }
}
