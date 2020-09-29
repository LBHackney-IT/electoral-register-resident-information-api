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

        [Column("MiddleName")]
        [MaxLength(200)]
        public string MiddleName { get; set; }

        [Column("DOB")]
        public DateTime DateOfBirth { get; set; }

        [Column("NINOPassword")]
        [MaxLength(512)]
        public string NinoPassword { get; set; }

        [Column("ITRSourceID")]
        public int ItrSourceId { get; set; }

        [Column("RAG")]
        [MaxLength(1)]
        public string Rag { get; set; }

        [Column("DeceasedEvidenceAcknowledgement")]
        public bool DeceasedEvidenceAcknowledgement { get; set; }

        [Column("UnsubscribeEmail")]
        public bool UnsubscribeEmail { get; set; }
    }
}
