using Models.CustomModels;
using Models.DBModels;
using Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EmployeeRepository
{
    public interface IEmployeeRepository : IDisposable
    {
        /// <summary>
        /// Gets All Employees
        /// </summary>
        /// <returns>All Employees as IQueryable</returns>
        Task<ICollection<EmployeeViewModel>> GetEmployees();
        /// <summary>
        /// Gets a single employee by its id
        /// </summary>
        /// <param name="id">id of the employee</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByID(int employeeId);
        /// <summary>
        /// Inserts a new employee to the database
        /// </summary>
        /// <param name="Employee">Model of the employee</param>
        /// <returns></returns>
        Task<ResultEnums.Result> InsertEmployee(Employee employee);
        /// <summary>
        /// Deletes an employee from the db using its id
        /// </summary>
        /// <param name="EmployeeID">Id of the employee</param>
        /// <returns></returns>
        Task<ResultEnums.Result> DeleteEmployee(int employeeId);
        /// <summary>
        /// Updates the employee with new data
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        Task<ResultEnums.Result> UpdateEmployee(Employee employee);
        /// <summary>
        /// Saves data to database async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
