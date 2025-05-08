using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;

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
            List<CareersSubjetcsModel> List = DbContext.CareersSubjects
                
                .Include(x => x.Subject)
                .Include(x => x.Career)
                .ToList();

            return View(List);
        }
    }
}
