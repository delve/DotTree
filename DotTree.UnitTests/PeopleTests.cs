using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;
using DotTree.WebUI.Controllers;
using DotTree.WebUI.HtmlHelpers;
using DotTree.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DotTree.UnitTests
{
    [TestClass]
    public class PeopleTests
    {
        /// <summary>
        /// Returns a consistent Person repository object for testing
        /// </summary>
        /// <returns>Returns a consistent Person repository object for testing</returns>
        private IPersonRepository GetPersonRepository()
        {
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            mock.Setup(m => m.People).Returns(new List<Person>
                {
                    new Person{Id=1, FamilyId=1, ParentId=0, ParentId2=0, FirstName="John", LastName="Doe"},
                    new Person{Id=2, FamilyId=1, ParentId=0, ParentId2=0, FirstName="Jane", LastName="Doe"},
                    new Person{Id=3, FamilyId=1, ParentId=1, ParentId2=2, FirstName="Jennifer", LastName="Deer"},
                    new Person{Id=4, FamilyId=1, ParentId=0, ParentId2=0, FirstName="Joseph", LastName="Deer"},
                    new Person{Id=5, FamilyId=1, ParentId=1, ParentId2=2, FirstName="Jonathan", LastName="Doe"},
                    new Person{Id=6, FamilyId=1, ParentId=0, ParentId2=0, FirstName="Jessica", LastName="Doe"},
                    new Person{Id=7, FamilyId=2, ParentId=1, ParentId2=2, FirstName="Johnny", LastName="Deerly"},
                    new Person{Id=8, FamilyId=3, ParentId=0, ParentId2=0, FirstName="Jessie", LastName="Deary"},
                    new Person{Id=9, FamilyId=2, ParentId=1, ParentId2=2, FirstName="Joshy", LastName="Deerly"}
                });
            return mock.Object;
        }

        /// <summary>
        /// Assert that people list controller paginates
        /// </summary>
        [TestMethod]
        public void CanPaginate()
        {
            // Arrange
            IPersonRepository mock = GetPersonRepository();
            PersonController controller = new PersonController(mock);
            controller.PageSize = 3;

            // Act
            IEnumerable<Person> result = ((PeopleListViewModel)controller.List(1, 2).Model).People;

            // Assertion
            Person[] resultArray = result.ToArray();
            Assert.IsTrue(3 == resultArray.Length);
            Assert.AreEqual("Joseph", resultArray[0].FirstName);
            Assert.AreEqual("Jessica", resultArray[2].FirstName);
        }

        /// <summary>
        /// Assert that HTML paging helper can generate paginated page links
        /// </summary>
        [TestMethod]
        public void CanGeneratePageLinks()
        {
            // Arrange
            //  have to instanciate an HtmlHelper in order to apply the extension method
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };
            // delegate
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assertion
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                            @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                            @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        /// <summary>
        /// Assert that controller puts correct pagination information into ViewModel
        /// </summary>
        [TestMethod]
        public void CanSendPaginationToViewModel()
        {
            // Arrange
            PersonController controller = new PersonController(GetPersonRepository());
            controller.PageSize = 3;

            // Act
            PeopleListViewModel result = (PeopleListViewModel)controller.List(1, 2).Model;
            PagingInfo pageInfo = result.PagingInfo;

            // Assertion
            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(3, pageInfo.ItemsPerPage);
            Assert.AreEqual(6, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }

        /// <summary>
        /// Assert that list view properly filters by selected family
        /// </summary>
        [TestMethod]
        public void CanFilterFamilies()
        {
            // Arrange
            PersonController controller = new PersonController(GetPersonRepository());
            controller.PageSize = 3;

            // Act
            Person[] result = ((PeopleListViewModel)controller.List(2, 1).Model).People.ToArray();

            // Assertion
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].FirstName == "Johnny" && result[1].FirstName=="Joshy");
            Assert.IsTrue(result[0].FamilyId == 2 && result[1].FamilyId == 2);
        }

        /// <summary>
        /// Assert that Navcontroller can create familID list
        /// </summary>
        [TestMethod]
        public void CanCreateCategories()
        {
            // Arrange
            NavController target = new NavController(GetPersonRepository());

            // Act
            int[] results = ((IEnumerable<int>)target.Menu().Model).ToArray();

            // Assertion
            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(2, results[1]);
            Assert.AreEqual(3, results[2]);

        }

        /// <summary>
        /// Assert that Menu action of NavController correctly assigns the selected family
        /// </summary>
        [TestMethod]
        public void IndicatesSelectedFamily()
        {
            // Arrange
            NavController target = new NavController(GetPersonRepository());
            int familyToSelect = 2;

            // Act
            int result = target.Menu(familyToSelect).ViewBag.SelectedFamily;

            // Assertion
            Assert.AreEqual(2, result);
        }

        /// <summary>
        /// Assert that PagingInfo gets correct TotalItems count from controller for family specific listing
        /// </summary>
        [TestMethod]
        public void GenerateFamilySpecificPersonCount()
        {
            // Arrange
            PersonController target = new PersonController(GetPersonRepository());
            target.PageSize = 3;

            // Act
            int result1 = ((PeopleListViewModel)target.List(1).Model).PagingInfo.TotalItems;
            int result2 = ((PeopleListViewModel)target.List(2).Model).PagingInfo.TotalItems;
            int result3 = ((PeopleListViewModel)target.List(3).Model).PagingInfo.TotalItems;
            int resultAll = ((PeopleListViewModel)target.List(0).Model).PagingInfo.TotalItems;

            // Assertion
            Assert.AreEqual(6, result1);
            Assert.AreEqual(2, result2);
            Assert.AreEqual(1, result3);
            Assert.AreEqual(9, resultAll);
        }
    }
}
