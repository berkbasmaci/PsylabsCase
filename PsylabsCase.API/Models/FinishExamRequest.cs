using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsylabsCase.API.Models
{
    public class FinishExamRequest
    {
        [Required]
        public int ExamId { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
    }

    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

    public class FinishExamResponse
    {
        public double Score { get; set; }
    }
}
