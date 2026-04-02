using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Backend.Model
{
    /*
    Teacher Table
    - uses GUID / UUID for 
        - security purposes
        - privacy for the teacher
        - registers the timeframe and mac add of the device
    */

    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Teacher_ID { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Department { get; set; }

        public DateTime CreatedAt {get; set;}

        public DateTime LastUpdatedAt {get; set;}

        [MaxLength(50)]
        public string? CreatedBy {get; set; }

        [MaxLength(50)]
        public string? LastUpdatedBy {get; set; }
    }
}
