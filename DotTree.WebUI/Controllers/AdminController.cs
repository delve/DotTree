using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;

namespace DotTree.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IPersonRepository people;
        private IFamilyRepository families;

        public AdminController(IPersonRepository prepo, IFamilyRepository frepo)
        {
            people = prepo;
            families = frepo;
        }

        public ViewResult Index()
        {
            return View(families.Families);
        }

        public ViewResult EditFamily(int familyId)
        {
            families.LoadMethod = 1;
            Family family = families.Families
                .FirstOrDefault(f => f.Id == familyId);
            return View(family);
        }

        [HttpPost]
        public ActionResult EditFamily(Family family)
        {
            if (ModelState.IsValid)
            {
                families.SaveFamily(family);
                TempData["message"] = string.Format("The {0} family has been saved.", family.FamilyName);
                return RedirectToAction("Index");
            }
            else
            {
                // model is invalid, some input data errors exist
                return View(family);
            }
        }

        public ViewResult EditPerson(int personId)
        {
            Person person = people.People
                .FirstOrDefault(p => p.Id == personId);
            ViewBag.PersonSimpleName = String.Format("{0} {1}", person.FirstName, person.LastName);
            return View(person);
        }

        [HttpPost]
        public ActionResult EditPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                people.SavePerson(person);
                TempData["message"] = string.Format("{0} has been saved.", person.FirstName);
                return RedirectToAction("Index");
            }
            else
            {
                // model is invalid, some input data errors exist
                return View(person);
            }
        }

        public ViewResult CreateFamily()
        {
            return View("EditFamily", new Family());
        }

        public ViewResult CreatePerson()
        {
            return View("EditPerson", new Person());
        }

        [HttpPost]
        public ActionResult DeleteFamily(int familyId)
        {
            Family deletedFamily = this.families.DeleteFamily(familyId);
            if (null != deletedFamily)
            {
                TempData["message"] = string.Format("The {0} family was deleted", deletedFamily.FamilyName);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePerson(int personId)
        {
            Person deletedPerson = this.people.DeletePerson(personId);
            if (null != deletedPerson)
            {
                TempData["message"] = string.Format("{0} {1} was deleted", deletedPerson.FirstName, deletedPerson.LastName);
            }

            return RedirectToAction("Index");
        }
    }
}