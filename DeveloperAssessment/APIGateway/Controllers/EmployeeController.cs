using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.DBModels;

namespace APIGateway.Controllers
{
    //[Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeManager;
        public EmployeeController(IEmployeeService employeeManager)
        {
            _employeeManager = employeeManager;
        }
        /// <summary>
        /// Web action used to insert new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeInputModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeManager.InsertEmployee(employee);
            return Ok(employee);
        }

        /// <summary>
        /// Web action used to update an old employee,phone number must be UAE number
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeInputModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeManager.UpdateEmployee(employee);
            return Ok(employee);
        }

        /// <summary>
        /// Web action used to get all employees 
        /// </summary>
        /// <returns>All employees (No pagenating)</returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeManager.GetEmployees());
        }

        /// <summary>
        /// Web action used to delete an existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            var result = await _employeeManager.DeleteEmployee(id);
            return Ok(result);
        }

        /// <summary>
        /// Web action used to get employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets a single employee by its id</returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            var result = await _employeeManager.GetEmployeeByID(id);
            return Ok(result);
        }

    }
}
