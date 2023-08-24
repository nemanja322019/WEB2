namespace WebApplication.DTO.UserDTO
{
    public class DisplayProfileDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }   
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        //public byte[] Image { get; set; }
        public bool IsVerified { get; set; }
        public string VerificationStatus { get; set; }
        public string UserType { get; set; }
        public string Image { get; set; }
    }
}
