using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectoralRegisterResidentInformationApi.V1.Infrastructure
{
    [Table("r20ElectorExtension", Schema = "dbo")]
    public class ElectorExtension
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("sysElectorId")]
        public int ElectorId { get; set; }

        [ForeignKey("ElectorId")]
        public Elector Elector { get; set; }

        [Column("MiddleName")]
        [MaxLength(200)]
        public string MiddleName { get; set; }

        [Column("DOB")]
        public DateTime DateOfBirth { get; set; }
    }
}
