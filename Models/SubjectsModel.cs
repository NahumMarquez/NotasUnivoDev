using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotasUnivoDev.Models
{
    public class SubjectsModel : BaseModel
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int VU { get; set; }

        public SubjectsModel()
        {
            IsActive = true;
            Created = DateTime.Now;
            CreatedBy = "ADMIN";    
        }
    }
}
