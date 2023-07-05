namespace BlazorApp1.Shared.RequestModels
{
    public class UpdateEmployeeRM
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
    }
}
