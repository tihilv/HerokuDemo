using System.ComponentModel.DataAnnotations;

namespace RegistrationService.Controllers
{
    public class RegistrationInfo
    {
        [Required]
        public string CompanyName { get; set; } 
        
        [Required]
        public string ContactPerson { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string LicenseKey { get; set; }
    }
}