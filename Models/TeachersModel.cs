using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotasUnivoDev.Models
{
    public class TeachersModel : BaseModel
    {
        [Key]
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public TeachersModel()
        {
            IsActive = true;
            Created = DateTime.Now;
            CreatedBy = "ADMIN";    
        }
    }
}
