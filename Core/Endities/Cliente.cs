using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Endities
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string? Nome { get; set; }

        [Required]
        [Column("DT_NASCIMENTO")]
        public DateTime? DtNascimento { get; set; }

        [Required]
        public short status { get; set; }
        [Column("DAT_INCLUSAO")]
        public DateTime? DatInclusao { get; set; }

        public IEnumerable<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}
