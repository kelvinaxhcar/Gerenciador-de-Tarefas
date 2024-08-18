namespace GerenciadorDeTarefas.EquipeApi.Dominio.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Task> Tarefas { get; set; } = new List<Task>();
    }

    public class Task
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Description { get; set; }
        public bool Concluida { get; set; }
        public int TeamId { get; set; }


        public Team Team { get; set; } = new Team();
    }
}
