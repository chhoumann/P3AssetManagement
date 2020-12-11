using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public class ComputerModel
    {

        public ComputerModel(string name) => Name = name;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }

        [Required]
        public Computer Computer { get; private set; }

        public override string ToString() => Name;
    }
}
