using Microsoft.AspNetCore.Mvc;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;

namespace NotasUnivoDev.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext DbContext;

        public TeachersController(AppDbContext Db)
        {
            DbContext = Db;
        }

        public IActionResult Index()
        {
            //Eager loading
            ViewData["Title"] = "Teachers";
            List<TeachersModel> List = DbContext.Teachers.ToList();
            return View(List);
        }

        [HttpGet]
        public IActionResult UpSert(int id)
        {
            TeachersModel model = new();

            if (id > 0)
            {
                model = DbContext.Teachers.FirstOrDefault(x => x.TeacherId == id) ?? new();

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UpSert(TeachersModel model)
        {
            if (model.TeacherId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Teachers.Add(model);
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
                    DbContext.Teachers.Update(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TeachersModel teacher = DbContext.Teachers.FirstOrDefault(row => row.TeacherId == id) ?? new();
            teacher.IsActive = !teacher.IsActive;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            TeachersModel teacher = DbContext.Teachers.FirstOrDefault(row => row.TeacherId == id) ?? new();
            if (teacher is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(teacher);
            }
        }

    }
}
