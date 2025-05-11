namespace NotasUnivoDev.Models.ViewModels
{
    public class CareersSubjectsViewModel
    {
        public int CareerSubjectId { get; set; }
        public int Cycle { get; set; }
        public int SubjectId { get; set; }
        public int CareersId { get; set; }
        public bool IsActive { get; set; }
        public List<SubjectsModel> SubjectsList { get; set; }
        public List<CareersModel> CareersList { get; set; }

        public int CareerId { get; set; }
    }
}
