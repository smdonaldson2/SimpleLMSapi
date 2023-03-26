using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebApi
{
    [TestFixture]
    public class courseTests
    {
        private CourseController _courseController;

        [SetUp]
        public void SetUp()
        {
            _courseController = new CourseController();
        }

        [Test]
        public void Get_ReturnsAllCourses()
        {
            // Arrange

            // Act
            var result = _courseController.Get();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Course>>(result.Value);
            Assert.AreEqual(3, result.Value.Count());
        }

        [Test]
        public void Get_WithValidId_ReturnsCorrectCourse()
        {
            // Arrange
            var expectedCourse = _courseController.Get(2).Value;

            // Act
            var result = _courseController.Get(2).Value;

            // Assert
            Assert.AreEqual(expectedCourse.ID, result.ID);
            Assert.AreEqual(expectedCourse.Name, result.Name);
            Assert.AreEqual(expectedCourse.Modules.Count(), result.Modules.Count());
        }

        [Test]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = _courseController.Get(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Post_AddsNewCourse()
        {
            // Arrange
            var courseToAdd = new Course { ID = 4, Name = "Course 4", Modules = new List<Module>() };

            // Act
            var result = _courseController.Post(courseToAdd);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            Assert.AreEqual(4, _courseController.Get().Value.Count());
            Assert.IsTrue(_courseController.Get().Value.Contains(courseToAdd));
        }

        [Test]
        public void Put_WithValidId_UpdatesExistingCourse()
        {
            // Arrange
            var courseToUpdate = new Course { ID = 2, Name = "Updated Course 2", Modules = new List<Module>() };

            // Act
            var result = _courseController.Put(2, courseToUpdate);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.AreEqual(courseToUpdate.Name, _courseController.Get(2).Value.Name);
            Assert.AreEqual(courseToUpdate.Modules.Count(), _courseController.Get(2).Value.Modules.Count());
        }

        [Test]
        public void Put_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var courseToUpdate = new Course { ID = 99, Name = "Updated Course 99", Modules = new List<Module>() };

            // Act
            var result = _courseController.Put(99, courseToUpdate);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_WithValidId_RemovesCourse()
        {
            // Arrange

            // Act
            var result = _courseController.Delete(2);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.AreEqual(2, _courseController.Get().Value.Count());
            Assert.IsNull(_courseController.Get(2).Value);
        }

        [Test]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = _courseController.Delete(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}