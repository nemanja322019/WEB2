using WebApplication.Enums;

namespace WebApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public UserTypes UserType { get; set; }
        public List<Item> Items { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public bool IsVerified { get; set; }

        //public byte[] Image { get; set; }
    }
}
