using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.DTOs
{
    public class PersonResponse
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your display name")]
        public string DisplayName { get; set; }
        public virtual ICollection<SkillResponse> Skills { get; set; }
    }
}
