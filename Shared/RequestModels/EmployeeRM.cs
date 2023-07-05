using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Shared.RequestModels
{
    public class EmployeeRM
    {
        public EmployeeRM(string firstName, string lastName, string email, string userName, string company, DateTime dateCreated)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Company = company;
            DateCreated = dateCreated;
        }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
