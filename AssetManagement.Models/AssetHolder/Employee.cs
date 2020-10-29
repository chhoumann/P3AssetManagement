using AssetManagement.Core;

namespace AssetManagement.Models
{
    /// <summary>
    /// An employee of type IEmployee which derrives IAssetHolder.
    /// </summary>
    public sealed class Employee : IEmployee
    {
        public string Username { get; }

        public string Department { get; }

        public string Label { get; }

        public Employee(string name, string username, string department)
        {
            Label = name;
            Username = username;
            Department = department;
        }
    }
}