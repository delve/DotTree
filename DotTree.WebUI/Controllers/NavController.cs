using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotTree.Domain.Abstract;

namespace DotTree.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IPersonRepository repository;

        public NavController(IPersonRepository repo)
        {
            this.repository = repo;
        }

        public PartialViewResult Menu(int family = 0)
        {
            ViewBag.SelectedFamily = family;
            IEnumerable<int> families = repository.People
                .Select(x => x.FamilyId)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(families);
        }
    }
}