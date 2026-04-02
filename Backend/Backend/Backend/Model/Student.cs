using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Backend.Model
{
    /*
    Student Table
    - uses GUID / UUID for 
        - security purposes
        - privacy for the student
        - registers the timeframe and mac add of the device
    */

    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Student_ID { get; set; }

        [Required]
        public int Program_ID { get; set; }

        [Required]
        public required int Department_ID { get; set; }

        [Required]
        public required int Year_Level { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        [MaxLength(210)]
        public string? CreatedBy { get; set; }

        [MaxLength(210)]
        public string? LastUpdatedBy { get; set; }
    }
}