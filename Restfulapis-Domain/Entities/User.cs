namespace Restfulapis_Domain.Entities
{
    public sealed class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
    }
}
