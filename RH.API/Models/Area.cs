namespace RH.API.Models
{
    public class Area : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public Guid? Gestor { get; set; } = null;
        public Area() { }
        public Area(string nome, Guid? gestor = null)
        {
            Nome = nome;
            Gestor = gestor;
        }
    }
}
