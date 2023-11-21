using System.ComponentModel.DataAnnotations;

namespace EtiqaFreelancerApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(13)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? SkillSets   { get; set; }

        [Required]
        public string? Hobby { get; set; }

    }
}
