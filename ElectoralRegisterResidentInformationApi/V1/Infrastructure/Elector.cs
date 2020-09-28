using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectoralRegisterResidentInformationApi.V1.Infrastructure
{
    [Table("r20Elector", Schema = "dbo")]
    public class Elector
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("ElectorForename")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("ElectorSurname")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Column("ElectorTitle")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Column("ElectorNationality")]
        [MaxLength(20)]
        public string Nationality { get; set; }

        [Column("ElectorEmail")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("sysPropertyId")]
        public int? PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public ElectorsProperty ElectorsProperty { get; set; }

    }
}
