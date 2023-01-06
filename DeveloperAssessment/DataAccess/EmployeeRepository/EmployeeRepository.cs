using Microsoft.EntityFrameworkCore;
using Models;
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
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets All Employees
        /// </summary>
        /// <returns>All Employees as IQueryable</returns>
        public async Task<ICollection<EmployeeViewModel>> GetEmployees()
        {
            return await context.Employees.Select(x => new EmployeeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
            }).ToListAsync();
        }
        /// <summary>
        /// Gets a single employee by its id
        /// </summary>
        /// <param name="id">id of the employee</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByID(int id)
        {
            try
            {
                return await context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return default;
            }
        }
        /// <summary>
        /// Inserts a new employee to the database
        /// </summary>
        /// <param name="Employee">Model of the employee</param>
        /// <returns></returns>
        public async Task<ResultEnums.Result> InsertEmployee(Employee Employee)
        {
            try
            {
                await context.Employees.AddAsync(Employee);
                await SaveAsync();
                return ResultEnums.Result.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }

        }
        /// <summary>
        /// Deletes an employee from the db using its id
        /// </summary>
        /// <param name="EmployeeID">Id of the employee</param>
        /// <returns></returns>
        public async Task<ResultEnums.Result> DeleteEmployee(int EmployeeID)
        {
            try
            {
                Employee Employee = await context.Employees.FindAsync(EmployeeID);
                if (Employee != null)
                {
                    context.Employees.Remove(Employee);
                    await SaveAsync();
                    return ResultEnums.Result.Success;
                }
                else
                {
                    return ResultEnums.Result.Failure;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }

        }
        /// <summary>
        /// Updates the employee with new data
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public async Task<ResultEnums.Result> UpdateEmployee(Employee Employee)
        {
            try
            {
                context.Entry(Employee).State = EntityState.Modified;
                await SaveAsync();
                return ResultEnums.Result.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResultEnums.Result.Failure;
            }

        }
        /// <summary>
        /// Saves data to database async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }




    }
}
