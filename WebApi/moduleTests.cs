using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Tests
{
    public class ModuleControllerTests
    {
        private ModuleController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ModuleController();
        }

        [Test]
        public void GetAllModules_ReturnsAllModules()
        {
            // Arrange

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<IEnumerable<Module>>(okResult.Value);
            var modules = (IEnumerable<Module>)okResult.Value;
            Assert.AreEqual(3, modules.Count());
        }

        [Test]
        public void GetModuleById_ReturnsCorrectModule()
        {
            // Arrange
            var moduleId = 1;

            // Act
            var result = _controller.Get(moduleId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<Module>(okResult.Value);
            var module = (Module)okResult.Value;
            Assert.AreEqual(moduleId, module.ID);
        }

        [Test]
        public void GetModuleById_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var invalidId = 4;

            // Act
            var result = _controller.Get(invalidId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void CreateModule_ReturnsNewModule()
        {
            // Arrange
            var newModule = new Module { ID = 4, Name = "Module 4", Assignments = new List<Assignment>() };

            // Act
            var result = _controller.Post(newModule);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual(nameof(_controller.Get), createdResult.ActionName);
            Assert.AreEqual(newModule.ID, createdResult.RouteValues["id"]);
            Assert.AreEqual(newModule, createdResult.Value);
        }

        [Test]
        public void UpdateModule_ReturnsNoContentForExistingModule()
        {
            // Arrange
            var moduleId = 1;
            var updatedModule = new Module { ID = moduleId, Name = "Updated Module 1", Assignments = new List<Assignment>() };

            // Act
            var result = _controller.Put(moduleId, updatedModule);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            var module = _controller.Get(moduleId).Value;
            Assert.AreEqual(updatedModule.Name, module.Name);
            Assert.AreEqual(updatedModule.Assignments, module.Assignments);
        }

        [Test]
        public void UpdateModule_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var invalidId = 4;
            var updatedModule = new Module { ID = invalidId, Name = "Updated Module 4", Assignments = new List<Assignment>() };

            // Act
            var result = _controller.Put(invalidId, updatedModule);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteModule_ReturnsNoContentForExistingModule()
        {
            // Arrange
            var moduleId = 1;

            // Act
            var result = _controller.Delete(moduleId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            var module = _controller.Get(moduleId).Value;
            Assert.IsNull(module);
        }

                [Test]
        public void DeleteModule_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var invalidId = 4;

            // Act
            var result = _controller.Delete(invalidId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}

