namespace GtechDesktop.WPF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
