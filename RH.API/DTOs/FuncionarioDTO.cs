namespace RH.API.DTOs
{
    public class FuncionarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; } = string.Empty;
        public decimal Salario { get; set; } 
        public string Documento { get; set;} = string.Empty;
        public Guid Area { get; set; }
    }
}
