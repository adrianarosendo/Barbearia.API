namespace Barbearia.API.DTO
{
    public class UserTokenDto
    {

        public bool IsAuthenticated { get; set; }

        public DateTime AuthenticatedAt { get; set; }


        public string? Token { get; set; }


        public string? Message { get; set; }
    }
}
