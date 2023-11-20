using System.Text.Json.Serialization;

namespace proekt.Models
{
    public class StudSession
    {
        public int GradeId { get; set; }

        public int? GradeNumber { get; set; }

        public DateTime? GradeDate { get; set; }

        public int? StudentId { get; set; }

        public Student? Student { get; set; }
    }
}
