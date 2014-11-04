using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotTree.Domain.Entities;
using DotTree.Domain.Abstract;
using DotTree.WebUI.Models;

namespace DotTree.WebUI.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository repository;
        public int PageSize = 4;

        public PersonController(IPersonRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(int family, int page = 1)
        {
            IEnumerable<Person> modelPeople = repository.People
                    .Where(p => p.FamilyId == family || family == 0)
                    .OrderBy(p => p.Id);
            PeopleListViewModel model = new PeopleListViewModel
            {
                People = modelPeople
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = modelPeople.Count()
                    },
                CurrentFamily = family
            };
            return View(model);
        }
    }
}