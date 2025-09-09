

namespace ProfileBook.API.DTOs
{
    public class AuthResponseDto
    {

        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpiration { get; set; }



        //public string Token { get; set; }
        public UserDto User { get; set; }  // Make sure this exists


        public class UserDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}