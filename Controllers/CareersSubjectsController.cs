using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;
using NotasUnivoDev.Models.ViewModels;

namespace NotasUnivoDev.Controllers
{
    public class CareersSubjectsController : Controller
    {
        private readonly AppDbContext DbContext;

        public CareersSubjectsController(AppDbContext context)
        {
            DbContext = context;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Careers Subjects";

            List<CareersSubjetcsModel> List = DbContext.CareersSubjects
                .Include(x => x.Subject)
                .Include(x => x.Career)
                .ToList();
            return View(List);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            CareersSubjectsViewModel model = new();
            if (id > 0)
            {
                CareersSubjetcsModel item = DbContext.CareersSubjects.FirstOrDefault(x => x.CareersSubjetcId == id) ?? new();
                model.CareerSubjectId = item.CareersSubjetcId;
                model.Cycle = item.Cycle;
                model.CareersId = item.CareerId;
                model.SubjectId = item.SubjectId;

            }

            model.CareersList = DbContext.Careers.ToList();
            model.SubjectsList = DbContext.Subjects.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpSert(CareersSubjectsViewModel model)
        {
            CareersSubjetcsModel careersSubjectModel = new(); // Asegúrate de usar el mismo nombre en ambas líneas
            careersSubjectModel.CareerId = model.CareerId;
            careersSubjectModel.SubjectId = model.SubjectId;
            careersSubjectModel.Cycle = model.Cycle;
            careersSubjectModel.IsActive = model.IsActive;
            careersSubjectModel.CareersSubjetcId = model.CareerSubjectId;


            ModelState.Remove("SubjectsList");
            ModelState.Remove("CareersList");


            if (careersSubjectModel.CareersSubjetcId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.CareersSubjects.Add(careersSubjectModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    careersSubjectModel.EditeBy = "ADMIN EDITED";
                    careersSubjectModel.Edited = DateTime.Now;
                    DbContext.CareersSubjects.Update(careersSubjectModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            model.SubjectsList = DbContext.Subjects.ToList();
            model.CareersList = DbContext.Careers.ToList();
            return View(model);
        }
    }
}
