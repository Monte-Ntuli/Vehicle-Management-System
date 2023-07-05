using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Models
{
    public class EmployeeModel
    {
        [Key] public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public bool isDeleted { get; set; }
    }
}
