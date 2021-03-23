using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using People.web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using People.data;

namespace People.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
            "Data Source=.\\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllPeople()
        {
            DbManager db = new DbManager(_connectionString);
            List<Person> people = db.GetAllPeople();
            return Json(people);
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            DbManager db = new DbManager(_connectionString);
            db.AddPerson(person);
            return Json(person);
        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            DbManager db = new DbManager(_connectionString);
            db.EditPerson(person);
            return Json(person);
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            DbManager db = new DbManager(_connectionString);
            db.DeletePerson(id);
            return Json(id);
        }

    }
}
