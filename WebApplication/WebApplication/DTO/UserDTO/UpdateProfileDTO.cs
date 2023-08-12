namespace WebApplication.DTO.UserDTO
{
    public class UpdateProfileDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        //public byte[] Image { get; set; }
    }
}
