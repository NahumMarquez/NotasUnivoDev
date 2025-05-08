using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;

namespace NotasUnivoDev.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly AppDbContext DbContext;

        public SubjectsController(AppDbContext Db)
        {
            DbContext = Db;
        }

        public IActionResult Index()
        {
            //Eager loading
            List<SubjectsModel> List = DbContext.Subjects.ToList();
            return View(List);
        }

        [HttpGet]
        public IActionResult UpSert(int id)
        {
            SubjectsModel model = new();

            if (id > 0)
            {
                model = DbContext.Subjects.FirstOrDefault(x => x.SubjectId == id) ?? new();
            }

            return View(model); 
        }

        [HttpPost]
        public IActionResult UpSert(SubjectsModel model)
        {
            if (model.SubjectId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Subjects.Add(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
               if (ModelState.IsValid)
               {
                    model.EditeBy = "ADMIN EDITED";
                    model.Edited = DateTime.Now;
                    DbContext.Subjects.Update(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
               }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            SubjectsModel career = DbContext.Subjects.FirstOrDefault(row => row.SubjectId == id) ?? new();
            career.IsActive = !career.IsActive;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            SubjectsModel career = DbContext.Subjects.FirstOrDefault(row => row.SubjectId == id) ?? new();
            if (career is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(career);
            }
        }

    }
}

