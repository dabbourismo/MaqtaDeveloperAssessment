using Models.CustomModels;
using Models.DBModels;
using Models.Utilities;

namespace BusinessLayer
{
    public interface IEmployeeService
    {
        Task<ResultEnums.Result> DeleteEmployee(int EmployeeID);
        Task<EmployeeViewModel> GetEmployeeByID(int id);
        Task<ICollection<EmployeeViewModel>> GetEmployees();
        Task<ResultEnums.Result> InsertEmployee(EmployeeInputModel Employee);
        Task<ResultEnums.Result> UpdateEmployee(EmployeeInputModel Employee);
    }
}