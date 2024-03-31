using Employee_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

       
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

       

        [HttpGet("GetDepartments")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartments()
        {
            var departments = await _context.Departments
                                  .Select(d => new { Id = d.Id, Name = d.Name })
                                  .ToListAsync();

            return departments;
        }

        [HttpGet("GetDesignation")]

        public async Task<ActionResult<IEnumerable<object>>> GetDesignation()
        {
            var Designation = await _context.Designation
                                  .Select(des => new { Id = des.Id, Name = des.Name })
                                  .ToListAsync();

            return Designation;
        }
        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Designation)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                EmployeeCode = employee.EmployeeCode,
                JoiningDate = employee.JoiningDate,
                Birthdate = employee.Birthdate,
                Address = employee.Address,
                Gender = employee.Gender,
                Designation = employee.Designation.Name,
                Department = employee.Department.Name,
                IsActive = employee.IsActive
            };

            return employeeViewModel;
        }

        

        // GET: api/employees/GetEmployeesWithBasicInfo
        [HttpGet("GetEmployeesWithBasicInfo")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployeesWithBasicInfo()
        {
            var employees = await _context.Employees
        .Include(e => e.Designation)
        .Include(e => e.Department)
        .Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.Name,
            EmployeeCode = e.EmployeeCode,
            JoiningDate = e.JoiningDate,
            Birthdate = e.Birthdate,
            Address = e.Address,
            Gender = e.Gender,
            Designation = e.Designation.Name,
            Department = e.Department.Name,
            IsActive = e.IsActive
        })
        .ToListAsync();

            return employees;
        }



        
       
        // POST: api/employees/AddEmployee
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddEmployee(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Designation designation = await _context.Designation.FirstOrDefaultAsync(d => d.Name == model.Designation);
            if (designation == null)
            {
                // Designation not found, return error
                ModelState.AddModelError(nameof(model.Designation), "Invalid Designation");
                return BadRequest(ModelState);
            }

            Department department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == model.Department);
            if (department == null)
            {
                // Department not found, return error
                ModelState.AddModelError(nameof(model.Department), "Invalid Department");
                return BadRequest(ModelState);
            }

            Employee employee = new Employee
            {
                Name = model.Name,
                EmployeeCode = model.EmployeeCode,
                Gender = model.Gender,
                Birthdate = model.Birthdate,
                JoiningDate = model.JoiningDate,
                DesignationId = designation.Id,
                DepartmentId = department.Id,
                Address = model.Address,
                IsActive = model.IsActive
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }




        // PUT: api/employees/5
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel model)
        {
            //if (id != model.Id)
            //{
            //    return BadRequest();
            //}

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Update properties of the employee
            employee.Name = model.Name;
            employee.EmployeeCode = model.EmployeeCode;
            employee.Gender = model.Gender;
            employee.Birthdate = model.Birthdate;
            employee.JoiningDate = model.JoiningDate;

            // Retrieve Designation and Department from the database
            var designation = await _context.Designation.FirstOrDefaultAsync(d => d.Name == model.Designation);
            if (designation == null)
            {
                ModelState.AddModelError(nameof(model.Designation), "Invalid Designation");
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == model.Department);
            if (department == null)
            {
                ModelState.AddModelError(nameof(model.Department), "Invalid Department");
                return BadRequest(ModelState);
            }

            // Update DesignationId and DepartmentId
            employee.DesignationId = designation.Id;
            employee.DepartmentId = department.Id;

            employee.Address = model.Address;
            employee.IsActive = model.IsActive;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

         
        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
