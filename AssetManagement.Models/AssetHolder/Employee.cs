namespace AssetManagement.Models
{
    public sealed class Employee : AssetHolder
    {
        public string Email { get; }
        public int Id { get; }

        public Employee(string name, string email) : base(name) => Email = email;
    }
}