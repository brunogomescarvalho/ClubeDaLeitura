namespace ClubeDaLeitura.ConsoleApp.Domain
{
    public class Emprestimo
    {
        private static int contador = 1;
        public int Id { get; private set; }
        public Revista Revista { get; set; }
        public Amigo Amigo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        private DateTime? DataDevolucao { get; set; }
        public bool Finalizado { get => DataDevolucao != null; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            this.Amigo = amigo;
            this.Revista = revista;
            this.DataEmprestimo = DateTime.Now;
            this.Revista.AlterarStatusRevista();
            this.Id = contador++;
        }

        public void Devolver()
        {
            this.DataDevolucao = DateTime.Now;
            this.Revista.AlterarStatusRevista();
        }

        public override string ToString()
        {
            return $"{Id,-5} | {DataEmprestimo,-15:d} | {Amigo.Nome,-10} {Amigo.Id,5} | {Revista.TipoColecao,-25} {Revista.NrEdicao,-6} {Revista.Id,5} | {(Finalizado ? "Sim" : "Não"),-10} | {DataDevolucao,-10:d} ";
        }

    }
}