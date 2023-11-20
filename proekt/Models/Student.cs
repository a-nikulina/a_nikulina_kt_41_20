using System.Diagnostics;

namespace proekt.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SecName { get; set; }
        public ICollection<StudSession>? StudSession { get; set; }
    }
}
