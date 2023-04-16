namespace ClubeDaLeitura
{
    public class Amigo
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        private static int contador = 1;

        public Amigo(string nome, string nomeResponsavel, string telefone, Endereco endereco)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.NomeResponsavel = nomeResponsavel;
            this.Endereco = endereco;
            this.Id = contador++;
        }


        public override string ToString()
        {
            return $"{Id,-3} | {Nome,-10} | {NomeResponsavel,-11} | {Telefone,-12} | {Endereco}";
        }
    }
}