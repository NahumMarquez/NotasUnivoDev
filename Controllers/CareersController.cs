﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;
using NotasUnivoDev.Models.ViewModels;
using System.Linq;

namespace NotasUnivoDev.Controllers
{
    public class CareersController : Controller
    {
        private readonly AppDbContext DbContext;

        public CareersController(AppDbContext Db)
        {
            DbContext = Db;
        }

        public IActionResult Index()
        {
            //Eager loading
            ViewData["Title"] = "Careers";
            List<CareersModel> List = DbContext.Careers.Include(x=> x.Faculty).ToList();
            return View(List);
        }

        [HttpGet]
        public IActionResult UpSert(int id)
        {
            CareersViewModel model = new();

            if (id > 0)
            {
                CareersModel career = DbContext.Careers.FirstOrDefault(x => x.CareerId == id) ?? new();
                model.CareerId = career.CareerId;
                model.CareerName = career.CareerName;
                model.FacultyId = career.FacultyId;
                model.IsActive = career.IsActive;

            }
            model.FacultiesList = DbContext.Faculties.ToList();

            return View(model); 
        }

        [HttpPost]
        public IActionResult UpSert(CareersViewModel model)
        {
            CareersModel careersModel = new(); // Asegúrate de usar el mismo nombre en ambas líneas
            careersModel.CareerName = model.CareerName;
            careersModel.FacultyId = model.FacultyId;
            careersModel.CareerId = model.CareerId;

            ModelState.Remove("FacultiesList");
            ModelState.Remove("SubjectsList");


            if(careersModel.CareerId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Careers.Add(careersModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
               if (ModelState.IsValid)
               {
                    careersModel.EditeBy = "ADMIN EDITED";
                    careersModel.Edited = DateTime.Now;
                    DbContext.Careers.Update(careersModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
               }
            }
            model.FacultiesList = DbContext.Faculties.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CareersModel career = DbContext.Careers.FirstOrDefault(row => row.FacultyId == id) ?? new();
            career.IsActive = !career.IsActive;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            CareersModel career = DbContext.Careers.FirstOrDefault(row => row.CareerId == id) ?? new();
            if (career is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CareersViewModel vModel = new();
                vModel.CareerId = career.CareerId;
                vModel.CareerName = career.CareerName;
                vModel.IsActive = career.IsActive;

                vModel.FacultiesList = DbContext.Faculties.Where(x => career.FacultyId == x.FacultyId).ToList();
                //Listados de materias de las cuales poseen CareerId en el registro
                var listSubjectsId = DbContext.CareersSubjects.Where(x => x.CareerId == id).Select(x => x.SubjectId).Distinct().ToList();


                vModel.SubjectsList = DbContext.Subjects.Where(x => listSubjectsId.Contains(x.SubjectId)).ToList();
                return View(vModel); 
            }
        }

    }
}

