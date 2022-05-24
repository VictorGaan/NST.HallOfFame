using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.DTOs
{
    public class SkillRequest
    {
        [Required(ErrorMessage = "Enter skill name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter level skill")]
        [Range(1, 10, ErrorMessage = "The level must be in the range 1-10")]
        public byte Level { get; set; }
    }
}
