using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;
using DotTree.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DotTree.UnitTests
{
    [TestClass]
    public class AdminTests
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
        /// Returns a consistent Family repository object for testing
        /// </summary>
        /// <returns>Returns a consistent Family repository object for testing</returns>
        private IFamilyRepository GetFamilyRepository()
        {
            Mock<IFamilyRepository> mock = new Mock<IFamilyRepository>();
            mock.Setup(m => m.Families).Returns(new List<Family>
                {
                    new Family{Id=1, FamilyName="Doe", Description="The Doe Family"},
                    new Family{Id=2, FamilyName="Deer", Description="The Deer Family"},
                    new Family{Id=3, FamilyName="Doey", Description="The Doey Family"},
                    new Family{Id=4, FamilyName="Deerly", Description="The Deerly Family"}
                });
            return mock.Object;
        }

        /// <summary>
        /// Assert that View model for index method contains all families
        /// </summary>
        [TestMethod]
        public void IndexContainsAllFamilies()
        {
            // Arrange
            AdminController target = new AdminController(GetPersonRepository(), GetFamilyRepository());

            // Act
            Family[] result = ((IEnumerable<Family>)target.Index().ViewData.Model).ToArray();

            // Assertion
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("Doe", result[0].FamilyName);
            Assert.AreEqual("Deer", result[1].FamilyName);
            Assert.AreEqual("Doey", result[2].FamilyName);
            Assert.AreEqual("Deerly", result[3].FamilyName);
        }

        /// <summary>
        /// Assert that AdminController Edit action accesses correct family with valid ID
        /// </summary>
        [TestMethod]
        public void CanEditFamily()
        {
            // Arrange
            AdminController target = new AdminController(GetPersonRepository(), GetFamilyRepository());

            // Act
            Family f1 = target.EditFamily(1).ViewData.Model as Family;
            Family f2 = target.EditFamily(2).ViewData.Model as Family;
            Family f3 = target.EditFamily(3).ViewData.Model as Family;

            // Assertion
            Assert.AreEqual(1, f1.Id);
            Assert.AreEqual(2, f2.Id);
            Assert.AreEqual(3, f3.Id);
        }

        /// <summary>
        /// Assert that AdminController Edit action accesses nothing when given an invalid ID
        /// </summary>
        [TestMethod]
        public void CannotEditInvalidFamily()
        {
            // Arrange
            AdminController target = new AdminController(GetPersonRepository(), GetFamilyRepository());

            // Act
            Family result = (Family)target.EditFamily(5).ViewData.Model;

            // Assertion
            Assert.IsNull(result);
        }

        /// <summary>
        /// Assert that AdminController POST EditFamily action passes valid models to be updated
        /// </summary>
        [TestMethod]
        public void CanSaveValidFamilyChanges()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo, but this isn't dependant on content anyway
            Mock<IFamilyRepository> mock = new Mock<IFamilyRepository>();
            AdminController target = new AdminController(GetPersonRepository(), mock.Object);
            Family family = new Family { FamilyName = "Test", Description = "The Test Family" };

            // Act
            ActionResult result = target.EditFamily(family);

            // Assertion - save method was called, and user was not sent back to the family view
            mock.Verify(m => m.SaveFamily(family));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Assert that AdminController POST EditFamily action does not pass invalid models to be updated
        /// </summary>
        [TestMethod]
        public void CannotSaveInvalidFamilyChanges()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo, but this isn't dependant on content anyway
            Mock<IFamilyRepository> mock = new Mock<IFamilyRepository>();
            AdminController target = new AdminController(GetPersonRepository(), mock.Object);
            Family family = new Family { FamilyName = "Test", Description = "The Test Family" };

            // add a deliberate error to the model
            target.ModelState.AddModelError("error", "error");

            // Act
            ActionResult result = target.EditFamily(family);

            // Assertion - save method was NOT called, and user WAS sent back to the family view
            mock.Verify(m => m.SaveFamily(It.IsAny<Family>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Assert that AdminController POST EditPerson action passes valid models to be updated
        /// </summary>
        [TestMethod]
        public void CanSaveValidPersonChanges()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo, but this isn't dependant on content anyway
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            AdminController target = new AdminController(mock.Object,GetFamilyRepository());
            Person person = new Person { FirstName = "Testable", LastName = "Test" };

            // Act
            ActionResult result = target.EditPerson(person);

            // Assertion - save method was called, and user was not sent back to the family view
            mock.Verify(m => m.SavePerson(person));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Assert that AdminController POST EditPerson action does not pass invalid models to be updated
        /// </summary>
        [TestMethod]
        public void CannotSaveInvalidPersonChanges()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo, but this isn't dependant on content anyway
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            AdminController target = new AdminController(mock.Object, GetFamilyRepository());
            Person person = new Person { FirstName = "Testable", LastName = "Test" };

            // add a deliberate error to the model
            target.ModelState.AddModelError("error", "error");

            // Act
            ActionResult result = target.EditPerson(person);

            // Assertion - save method was NOT called, and user WAS sent back to the family view
            mock.Verify(m => m.SavePerson(It.IsAny<Person>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Assert that valid family IDs get deleted
        /// </summary>
        [TestMethod]
        public void CanDeleteFamilyId()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo
            Family family = new Family{Id=2, FamilyName="Deer", Description="The Deer Family"};
            Mock<IFamilyRepository> mock = new Mock<IFamilyRepository>();
            mock.Setup(m => m.Families).Returns(new List<Family>
                {
                    new Family{Id=1, FamilyName="Doe", Description="The Doe Family"},
                    family,
                    new Family{Id=3, FamilyName="Doey", Description="The Doey Family"}
                });

            AdminController target = new AdminController(GetPersonRepository(), mock.Object);

            // Act
            target.DeleteFamily(family.Id);

            // Assertion
            // assert that the delete functionality was called with the correct ID
            mock.Verify(m => m.DeleteFamily(family.Id));
        }

        /// <summary>
        /// Assert that valid person IDs get deleted
        /// </summary>
        [TestMethod]
        public void CanDeletePersonId()
        {
            // Arrange
            //  need extra Moq functionality so can't use the normal mock repo
            Person person = new Person{Id=2, FamilyId=1, ParentId=0, ParentId2=0, FirstName="Jane", LastName="Doe"};
            Mock < IPersonRepository > mock = new Mock<IPersonRepository>();
            mock.Setup(m => m.People).Returns(new List<Person>
                {
                    new Person{Id=1, FamilyId=1, ParentId=0, ParentId2=0, FirstName="John", LastName="Doe"},
                    person,
                    new Person{Id=3, FamilyId=1, ParentId=1, ParentId2=2, FirstName="Jennifer", LastName="Deer"}
                });

            AdminController target = new AdminController(mock.Object, GetFamilyRepository());

            // Act
            target.DeletePerson(person.Id);

            // Assertion
            // assert that the delete functionality was called with the correct ID
            mock.Verify(m => m.DeletePerson(person.Id));
        }

        /// <summary>
        /// Assert that AdminController EditFamily displays complete list of family members
        /// Not yet implemented
        /// </summary>
        [TestMethod]
        public void EditFamilyLoadsAllMembers()
        {
            // Arrange
            // TODO:
            // Not quite sure how to mock up the many-to-many relationship yet, will have to come back to this
            //  Don't want to unit test the EF context, but need to ensure the AdminController uses eager loading
            //  from the context in order to get the FamilyMembers loaded. Loading type is implemented in the rEFFamilyRepository
            //  based on the LoadMethod member variable
            throw new NotImplementedException();

            // Act


            // Assertion

        }
    }
}
