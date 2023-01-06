using DataAccess.EmployeeMockRepository;
using Models.CustomModels;
using Models.DBModels;

namespace UnitTests.EmployeeRepoTests
{
    public class EmployeeUnitTests
    {
        private readonly EmployeeMockRepository _employeeMockRepository;
        public EmployeeUnitTests()
        {
            _employeeMockRepository = new EmployeeMockRepository();
        }
        [Fact]
        public void InsertEmployee_ShouldReturn_Success()
        {
            //Arrange
            var employee = new Employee()
            {
                Id = 5,
                Name = "Nader",
                Phone = "01149219212"
            };
            //Act
            var result = _employeeMockRepository.InsertEmployee(employee);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Success, result.Result);
        }
        [Fact]
        public void DeleteEmployee_ShouldReturn_Success()
        {
            //Arrange
            var employeeid = 1;
            //Act
            var result = _employeeMockRepository.DeleteEmployee(employeeid);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Success, result.Result);
        }

        [Fact]
        public void DeleteEmployee_EmployeeDoesntExists_ShouldReturn_Fail()
        {
            //Arrange
            var employeeid = 999;
            //Act
            var result = _employeeMockRepository.DeleteEmployee(employeeid);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Failure, result.Result);
        }
        [Fact]
        public void UpdateEmployee_ShouldReturn_Success()
        {
            //Arrange
            var employee = new Employee()
            {
                Id = 1,
                Name = "Samir",
                Phone = "01123458789"
            };
            //Act
            var result = _employeeMockRepository.UpdateEmployee(employee);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Success, result.Result);
        }
        [Fact]
        public void UpdateEmployee_EmployeeIsNull_ShouldReturn_Fail()
        {
            //Arrange

            //Act
            var result = _employeeMockRepository.UpdateEmployee(null);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Failure, result.Result);
        }
        [Fact]
        public void UpdateEmployee_EmployeeDoesntExists_ShouldReturn_Fail()
        {
            //Arrange
            var employee = new Employee()
            {
                Id = 500,
                Name = "Samir",
                Phone = "01123458789"
            };
            //Act
            var result = _employeeMockRepository.UpdateEmployee(employee);
            //Assert
            Assert.Equal(Models.Utilities.ResultEnums.Result.Failure, result.Result);
        }
        [Fact]
        public async void GetEmployeeByID_EmployeeExists_ShouldReturn_EmployeeTask()
        {
            //Arrange
            var employeeId = 1;
            //Act
            var result = await _employeeMockRepository.GetEmployeeByID(employeeId);
            //Assert
            Assert.IsType<Employee>(result);
        }
        [Fact]
        public async void GetEmployeeByID_EmployeedoesntExists_ShouldReturn_Null()
        {
            //Arrange
            var employeeId = 500;
            //Act
            var result = await _employeeMockRepository.GetEmployeeByID(employeeId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetEmployees_ShouldReturn_ListOfEmployeesViewModel()
        {
            //Arrange

            //Act
            var result = await _employeeMockRepository.GetEmployees();
            //Assert
            Assert.IsType<List<EmployeeViewModel>>(result);
        }

       
    }
}