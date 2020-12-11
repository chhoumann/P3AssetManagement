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
            : this(name, username)
        {
            Department = department;
        }

        public bool Equals(AssetHolder other) => Username == other?.Username;

        public override string ToString() => Name;

        #region EFCore stuff
        public AssetHolder()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; private set; }

        [MaxLength(50)]
        public string Department { get; private set; }
        #endregion
    }
}