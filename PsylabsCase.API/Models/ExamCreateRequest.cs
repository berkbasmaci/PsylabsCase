using System.ComponentModel.DataAnnotations;

namespace PsylabsCase.API.Models
{
    public class ExamCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
