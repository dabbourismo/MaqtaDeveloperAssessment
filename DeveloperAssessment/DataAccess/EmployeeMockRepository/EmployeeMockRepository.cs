using DataAccess.EmployeeRepository;
using Models.CustomModels;
using Models.DBModels;
using Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EmployeeMockRepository
{
    public class EmployeeMockRepository : IEmployeeRepository
    {
        private IList<Employee> _mockEmployees;
        public EmployeeMockRepository()
        {
            _mockEmployees = new List<Employee>()
            {
                 new Employee()
                 {
                     Id = 1,
                     Name= "Nabil",
                     Phone = "01020925337"
                 },
                new Employee()
                 {
                     Id = 2,
                     Name= "Yousry",
                     Phone = "01149219212"
                 },
                 new Employee()
                 {
                     Id = 3,
                     Name= "Mourad",
                     Phone = "01123458789"
                 },
            };

        }
        public async Task<ResultEnums.Result> InsertEmployee(Employee employee)
        {
            await SimulateNetworkDelay(1000);
            _mockEmployees.Add(employee);
            return ResultEnums.Result.Success;

        }
        public async Task<ResultEnums.Result> DeleteEmployee(int employeeId)
        {
            await SimulateNetworkDelay(1500);
            var employeeToBeDeleted = _mockEmployees.FirstOrDefault(x => x.Id == employeeId);
            if (employeeToBeDeleted != null)
            {
                _mockEmployees.Remove(employeeToBeDeleted);
                return ResultEnums.Result.Success;
            }
            return ResultEnums.Result.Failure;
        }

        public async Task<ResultEnums.Result> UpdateEmployee(Employee employee)
        {
            await SimulateNetworkDelay(1000);
            if (employee == null)
            {
                return ResultEnums.Result.Failure;
            }
            var employeeToBeUpdated = _mockEmployees.FirstOrDefault(x => x.Id == employee.Id);
            if (employeeToBeUpdated == null)
            {
                return ResultEnums.Result.Failure;
            }
            employeeToBeUpdated.Name = employee.Name;
            employeeToBeUpdated.Phone = employee.Phone;
            return ResultEnums.Result.Success;
        }

        public async Task<Employee> GetEmployeeByID(int employeeId)
        {
            var employee = _mockEmployees.FirstOrDefault(x => x.Id == employeeId);
            if (employee != null)
            {
                return employee;
            }
            return default;
            
        }

        public async Task<ICollection<EmployeeViewModel>> GetEmployees()
        {
            await SimulateNetworkDelay(1000);
            return _mockEmployees.Select(x=> new EmployeeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
            }).ToList();
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }


        private async Task SimulateNetworkDelay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
