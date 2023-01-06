using DataAccess.EmployeeRepository;
using Microsoft.EntityFrameworkCore;
using Models.CustomModels;
using Models.DBModels;
using Models.Utilities;

namespace BusinessLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<ICollection<EmployeeViewModel>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }
        public async Task<EmployeeViewModel> GetEmployeeByID(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeByID(id);
                if (result != null)
                {
                    return MapFromEmployeeDBObj(result);
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }

        }
        public async Task<ResultEnums.Result> InsertEmployee(EmployeeInputModel Employee)
        {
            try
            {
                var dbEmployee = MapToEmployeeDBObj(Employee);
                await _employeeRepository.InsertEmployee(dbEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }
            return ResultEnums.Result.Success;
        }
        public async Task<ResultEnums.Result> UpdateEmployee(EmployeeInputModel Employee)
        {
            try
            {
                if (Employee == null)
                {
                    return ResultEnums.Result.Failure;
                }
                var dbEmployee = MapToEmployeeDBObj(Employee);
                await _employeeRepository.UpdateEmployee(dbEmployee);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }
            return ResultEnums.Result.Success;
        }
        public async Task<ResultEnums.Result> DeleteEmployee(int EmployeeID)
        {
            try
            {
                await _employeeRepository.DeleteEmployee(EmployeeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }
            return ResultEnums.Result.Success;
        }



        private Employee MapToEmployeeDBObj(EmployeeInputModel employeeInputModel)
        {
            var employee = new Employee()
            {
                Id = employeeInputModel.Id,
                Name = employeeInputModel.Name,
                Phone = employeeInputModel.Phone,
            };
            return employee;
        }
        private EmployeeViewModel MapFromEmployeeDBObj(Employee employeeDbModel)
        {
            var employee = new EmployeeViewModel()
            {
                Id = employeeDbModel.Id,
                Name = employeeDbModel.Name,
                Phone = employeeDbModel.Phone,
            };
            return employee;
        }
    }
}