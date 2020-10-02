namespace AssetManagement.Models
{
    public sealed class Employee : AssetHolder
    {
        public string Email { get; private set; }

        public Employee(string name, string email) : base(name) => Email = email;
    }
}