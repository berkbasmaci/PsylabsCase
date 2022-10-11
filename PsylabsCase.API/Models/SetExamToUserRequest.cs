using System.ComponentModel.DataAnnotations;

namespace PsylabsCase.API.Models
{
    public class SetExamToUserRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ExamId { get; set; }
    }
}
