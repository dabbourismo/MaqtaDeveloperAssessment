using APIGateway.Controllers;
using BusinessLayer;
using DataAccess.EmployeeMockRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Validators;

namespace UnitTests.EmployeeControllerTests
{
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _employeeController;
        private readonly EmployeeService _employeeService;
        private readonly EmployeeMockRepository _employeeMockRepository;
        private readonly ModelMockValidator _modelMockValidator;

        public EmployeeControllerTest()
        {
            _employeeMockRepository = new EmployeeMockRepository();
            _employeeService = new EmployeeService(_employeeMockRepository);
            _employeeController = new EmployeeController(_employeeService);
            _modelMockValidator = new ModelMockValidator();
        }

        [Fact]
        public async void InsertEmployee_ShouldReturn_Ok()
        {
            //Arrange
            var employee = new EmployeeInputModel()
            {
                Id = 5,
                Name = "Nader",
                Phone = "01149219212"
            };
            //Act
            _modelMockValidator.ValidateModel(employee, _employeeController);
            var result = await _employeeController.InsertEmployee(employee) as OkObjectResult;
            //Assert
            Assert.Equal(employee, result.Value);
        }

        [Fact]
        public async void InsertEmployee_NameIsLessThan3Chars_ShouldReturn_BadRequest()
        {
            //Arrange
            var employee = new EmployeeInputModel()
            {
                Id = 5,
                Name = "N",
                Phone = "1"
            };
            //Act
            _modelMockValidator.ValidateModel(employee, _employeeController);
            var result = await _employeeController.InsertEmployee(employee);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public async void InsertEmployee_NameIsMissing_ShouldReturn_BadRequest()
        {
            //Arrange
            var employee = new EmployeeInputModel()
            {
                Id = 5,
                Phone = "1"
            };
            //Act
            _modelMockValidator.ValidateModel(employee, _employeeController);
            var result = await _employeeController.InsertEmployee(employee);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }

        [Fact]
        public async void UpdateEmployee_ShouldReturn_Success()
        {
            //Arrange
            var employee = new EmployeeInputModel()
            {
                Id = 1,
                Name = "Samir",
                Phone = "01123458789"
            };
            //Act
            _modelMockValidator.ValidateModel(employee, _employeeController);
            var result = await _employeeController.UpdateEmployee(employee) as OkObjectResult;
            //Assert
            Assert.Equal(employee, result.Value);
        }

        [Fact]
        public async void UpdateEmployee_EmployeeDoesntExists_ShouldReturn_BadRequest()
        {
            //Arrange
            var employee = new EmployeeInputModel()
            {
                Id = 5111,
                Phone = "1"
            };
            //Act
            _modelMockValidator.ValidateModel(employee, _employeeController);
            var result = await _employeeController.UpdateEmployee(employee);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }


        [Fact]
        public async void DeleteEmployee_EmployeeDoesntExists_ShouldReturn_Ok()
        {
            //Arrange
            var employeeId = 1;
            //Act
            var result = await _employeeController.DeleteEmployee(1) as OkObjectResult; 
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
