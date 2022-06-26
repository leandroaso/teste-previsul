namespace Core.DTOs
{
    public class FiltroPaginated
    {
        public string? Nome { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string? Logradouro { get; set; }
        public string? Cep { get; set; }
        public string? Uf { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }

        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 0;
    }
}
