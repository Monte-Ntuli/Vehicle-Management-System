using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Server.Models;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EmployeeController(IUnitOfWork unitOfWork, ILogger<EmployeeController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
            
        }

        #region Create new employee
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateEmployeeDTO employee)
        {
            CreatePasswordHash(employee.Password, out byte[] passwordHash, out byte[] passwordSalt);

            EmployeeModel employeeModel = new EmployeeModel();

            employeeModel.FirstName = employee.FirstName;
            employeeModel.LastName = employee.LastName;
            employeeModel.Email = employee.Email;
            employeeModel.UserName = employee.UserName;
            employeeModel.PhoneNum = employee.PhoneNum;
            employeeModel.PasswordHash = passwordHash;
            employeeModel.PasswordSalt = passwordSalt;
            employeeModel.Company = employee.Company;
            employeeModel.Role = employee.Role;

            var NewEmployee = await _unitOfWork.Employee.AddAsync(_mapper.Map<EmployeeEntity>(employeeModel));

            if (NewEmployee == null)
            {
                return Accepted(employee);
            }
            if (NewEmployee != null)
            {
                return BadRequest(NewEmployee);
            }
            else return BadRequest();
        }
        #endregion

        #region Employee PasswordHash 
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
          
        }
        #endregion

        #region Employee Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO employee)
        {
                var user = await _unitOfWork.Employee.Login(employee);
                return Accepted(user);
        }
        #endregion

        #region Update Employee by id
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeDTO employee)
        {
            await _unitOfWork.Employee.UpdateAsync(_mapper.Map<EmployeeEntity>(employee));
                return Accepted(employee);
        }
        #endregion
        
        #region Delete Employee by id
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int employee)
        {
                await _unitOfWork.Employee.DeleteEmployeeAsync(employee);
                return Accepted(employee);
        }
        #endregion

        #region Restore Employee by id
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int employee)
        {
               await _unitOfWork.Employee.RestoreEmployeeAsync(employee);
               return Accepted(employee);
        }
        #endregion

        #region Get Employee by ID
        [HttpGet("GetEmployeeByID/{id}")]
        public async Task<IActionResult> GetEmployeeByID(int employee)
        {
                var user = await _unitOfWork.Employee.GetEmployeeByIDAsync(employee);
                if (user == null)
                {
                    return BadRequest(user);
                }
                return Accepted(user);
        }
        #endregion

        #region Get Employee by Company
        [HttpGet("GetEmployeeByCompany/{company}")]
        public async Task<IActionResult> GetEmployeeeByCompany(string company)
        {
                var user = await _unitOfWork.Employee.GetEmployeeByCompanyAsync(company);
                if (user == null)
                {
                    return BadRequest(user);
                }
                return Accepted(user);
        }
        #endregion

        #region Get Employee by role
        [HttpGet("GetEmployeeByRole/{id}")]
        public async Task<IActionResult> GetEmployeeByRole(string Role)
        {
            var user = await _unitOfWork.Employee.GetEmployeeByRoleAsync(Role);
                if (user == null)
                {
                    return BadRequest(user);
                }
                return Accepted(user);
        }
        #endregion

        #region Get employee by email
        [HttpGet("GetEmployeeByEmail/{email}")]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            var user = await _unitOfWork.Employee.GetEmployeeByEmailAsync(email);
            if (user == null) { return BadRequest(user); }
            return Accepted(user);
        }
        #endregion

    }
}
