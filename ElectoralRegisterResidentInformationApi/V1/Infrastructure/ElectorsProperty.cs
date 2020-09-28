using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectoralRegisterResidentInformationApi.V1.Infrastructure
{
    [Table("r20Property", Schema = "dbo")]
    public class ElectorsProperty
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("PropertyURN")]
        [MaxLength(25)]
        public string Uprn { get; set; }
    }
}
