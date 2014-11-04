using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotTree.Domain.Entities;

namespace DotTree.WebUI.Models
{
    public class PeopleListViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? CurrentFamily { get; set; }
    }
}