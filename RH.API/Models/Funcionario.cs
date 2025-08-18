namespace RH.API.Models
{
    public class Funcionario : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; } = string.Empty;
        public decimal Salario { get; set; }
        public string Documento { get; set; } = string.Empty;
        public Guid Area { get; set; } 
        public Funcionario() { }
        public Funcionario(string nome, DateTime dataNascimento, string cargo, decimal salario, string documento, Guid area)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cargo = cargo;
            Salario = salario;
            Documento = documento;
            Area = area;
        }
    }
}
