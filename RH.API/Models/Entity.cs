namespace RH.API.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? DeletadoEm { get; set; } = null;
        public bool Ativo { get; set; } = true;
        
        public void Desativar() {
            Ativo = false;
            DeletadoEm = DateTime.Now;
        }
    }
}
