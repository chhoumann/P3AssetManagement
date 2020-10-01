namespace AssetManagement.Models
{
    public sealed class Employee : AssetHolder
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}