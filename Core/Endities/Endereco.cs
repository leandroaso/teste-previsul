using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Endities
{
    public class Endereco
    {
        public int? Id { get; set; }
        [JsonIgnore]
        public int? IdCliente { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Logradouro { get; set; }

        [Required]
        [StringLength(maximumLength: 8)]
        public string Cep { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(maximumLength: 60)]
        public string Bairro { get; set; }

        [Required]
        public short Status { get; set; }
        public DateTime? DatInclusao { get; set; }

    }
}
