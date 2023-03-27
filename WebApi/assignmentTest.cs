
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [TestFixture]
    public class assignmentTest
    {
        private AssignmentsController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new AssignmentsController();
        }

        [Test]
        public void Get_ReturnsListOfAssignments()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { ID = 1, Name = "Assignment 1", Grade = 95, DueDate = DateTime.Today.AddDays(7) },
                new Assignment { ID = 2, Name = "Assignment 2", Grade = 85, DueDate = DateTime.Today.AddDays(14) },
                new Assignment { ID = 3, Name = "Assignment 3", Grade = 90, DueDate = DateTime.Today.AddDays(21) },
            };
            _controller = new assignmentTest(assignments);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Assignment>>(result.Value);
            Assert.AreEqual(assignments.Count, result.Value.Count());
        }

        [Test]
        public void Get_ReturnsSingleAssignment_WhenGivenValidId()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { ID = 1, Name = "Assignment 1", Grade = 95, DueDate = DateTime.Today.AddDays(7) },
                new Assignment { ID = 2, Name = "Assignment 2", Grade = 85, DueDate = DateTime.Today.AddDays(14) },
                new Assignment { ID = 3, Name = "Assignment 3", Grade = 90, DueDate = DateTime.Today.AddDays(21) },
            };
            _controller = new assignmentTest(assignments);

            // Act
            var result = _controller.Get(2);

            // Assert
            Assert.IsInstanceOf<Assignment>(result.Value);
            Assert.AreEqual(2, result.Value.ID);
        }

        [Test]
        public void Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            // Arrange
            var assignments = new List<Assignment>();
            _controller = new assignmentTest(assignments);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Post_CreatesNewAssignment()
        {
            // Arrange
            var assignment = new Assignment { Name = "New Assignment", Grade = 80, DueDate = DateTime.Today.AddDays(7) };

            // Act
            var result = _controller.Post(assignment);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            Assert.AreEqual(1, _controller.Get().Value.Count());
        }

        [Test]
        public void Put_UpdatesExistingAssignment_WhenGivenValidId()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { ID = 1, Name = "Assignment 1", Grade = 95, DueDate = DateTime.Today.AddDays(7) },
                new Assignment { ID = 2, Name = "Assignment 2", Grade = 85, DueDate = DateTime.Today.AddDays(14) },
                new Assignment { ID = 3, Name = "Assignment 3", Grade = 90, DueDate = DateTime.Today.AddDays(21) },
            };
            _controller = new assignmentTest(assignments);

            var updatedAssignment = new Assignment { ID = 2, Name = "Updated Assignment", Grade = 90, DueDate = DateTime.Today.AddDays(10) };

            // Act
            var result = _controller.Put(2, updatedAssignment);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.AreEqual("Updated Assignment", assignments[1].Name);
        }

        [Test]
        public void Put_ReturnsNotFound_WhenGivenInvalidId()
        {
            // Arrange
            var assignments = new List<Assignment>();
            _controller = new assignmentTest(assignments);

            var updatedAssignment = new Assignment { ID = 1, Name = "Updated Assignment", Grade = 90, DueDate = DateTime.Today.AddDays(10) };

            // Act
            var result = _controller.Put(1, updatedAssignment);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_RemovesAssignment_WhenGivenValidId()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { ID = 1, Name = "Assignment 1", Grade = 95, DueDate = DateTime.Today.AddDays(7) },
                new Assignment { ID = 2, Name = "Assignment 2", Grade = 85, DueDate = DateTime.Today.AddDays(14) },
                new Assignment { ID = 3, Name = "Assignment 3", Grade = 90, DueDate = DateTime.Today.AddDays(21) },
            };
            _controller = new assignmentTest(assignments);

            // Act
            var result = _controller.Delete(2);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.AreEqual(2, assignments.Count());
        }

        [Test]
        public void Delete_ReturnsNotFound_WhenGivenInvalidId()
        {
            // Arrange
            var assignments = new List<Assignment>();
            _controller = new assignmentTest(assignments);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
