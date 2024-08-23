namespace ifxNetworks.Core.DTOs.Response
{
    public class UserAuthResponseDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public long IdUser { get; set; } = 0;
        public int IdRole { get; set; } = 0;
    }
}
