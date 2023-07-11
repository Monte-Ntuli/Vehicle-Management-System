namespace BlazorApp1.Server.Models
{
    public class AddressModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string ComplexName { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
    }
}
