using System.ComponentModel.DataAnnotations;

namespace EFCore5WebApp.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
