using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public class AssetHolder : IEquatable<AssetHolder>
    {
        public AssetHolder(string name, string username)
        {
            Name = name;
            Username = username;
        }

        public AssetHolder(string name, string username, string department)
        {
            Name = name;
            Username = username;
            Department = department;
        }

        #region EFCore stuff
        public AssetHolder()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required] public string Name { get; private set; }

        [Required] public string Username { get; private set; }

        public string Department { get; private set; }
        #endregion

        public bool Equals(AssetHolder other) => Username == other?.Username;

        public override string ToString() => Name;
    }
}