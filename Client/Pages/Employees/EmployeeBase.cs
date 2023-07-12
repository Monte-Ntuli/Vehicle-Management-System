using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections;

namespace BlazorApp1.Client.Pages.Employees
{
    public class EmployeeBase : ComponentBase
    {
        [Parameter]
        public string company { get; set; }

        [Parameter]
        public int ID { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager navMan { get; set; }
        public LoginDTO login { get; set; } = new LoginDTO();
        public IEnumerable<EmployeeDTO> Employee { get; set; } = new List<EmployeeDTO>();

        public List<EmployeeDTO> employee { get; set; } = new List<EmployeeDTO>();

        public CreateEmployeeDTO createEmployeeDTO = new CreateEmployeeDTO();

        public UpdateEmployeeDTO updateEmployeeDTO = new UpdateEmployeeDTO(); 
        protected override async Task OnInitializedAsync()
        {
            login.Email = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "UserName");
            var username = login.Email.Replace("\"", string.Empty).Trim();
            var email = username.Replace("\'", string.Empty).Trim(new char[] { (char)39 });
            company = email.Replace("\'", string.Empty).Trim();
            Employee = await EmployeeService.GetEmployeesByCompany(company);
        }
        public async Task AddEmployee()
        {
            createEmployeeDTO.Company = company;
            createEmployeeDTO.UserName = createEmployeeDTO.Email;
            await EmployeeService.Create(createEmployeeDTO);
            await JSRuntime.InvokeVoidAsync("alert", "Employee Added Successfully");
        }

        public async Task EditEmployee()
        {
            updateEmployeeDTO.UserID = ID;
            updateEmployeeDTO.Company = company;
            updateEmployeeDTO.Password = company;

            checkForNull();

            await EmployeeService.Update(updateEmployeeDTO);

        }

        public async Task checkForNull()
        {
            foreach(var employee in Employee)
            {
                if (employee.UserID == ID)
                {
                    if (updateEmployeeDTO.FirstName == null)
                    {
                        updateEmployeeDTO.FirstName = employee.FirstName;
                    }
                    if (updateEmployeeDTO.LastName == null)
                    {
                        updateEmployeeDTO.LastName = employee.LastName;
                    }
                    if (updateEmployeeDTO.UserName == null)
                    {
                        updateEmployeeDTO.UserName = employee.UserName;
                    }
                    if (updateEmployeeDTO.Role == null)
                    {
                        updateEmployeeDTO.Role = employee.Role;
                    }
                    if (updateEmployeeDTO.PhoneNum == null)
                    {
                        updateEmployeeDTO.PhoneNum = employee.PhoneNum;
                    }
                    if (updateEmployeeDTO.Email == null)
                    {
                        updateEmployeeDTO.Email = employee.Email;
                    }
                }
            }
        }
    }
}
