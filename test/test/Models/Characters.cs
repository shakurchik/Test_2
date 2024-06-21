using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;


    [Table("Characters")]
    public class Characters
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(120)]
        public string LastName { get; set; }

        public int CurrentWeight { get; set; }

        public int MaxWeight { get; set; }

        public ICollection<Backpacks> BackpackItems { get; set; } = new List<Backpacks>();

        public ICollection<CharacterTitle> Titles { get; set; } = new List<CharacterTitle>();
    }
