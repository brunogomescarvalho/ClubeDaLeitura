using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura
{
    public class Amigo : Entidade
    {
        public string Nome { get; private set; }
        public string NomeResponsavel { get; private set; }
        public string Telefone { get; private set; }
        public Endereco Endereco { get; private set; }

        public Amigo(string nome, string nomeResponsavel, string telefone, Endereco endereco)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.NomeResponsavel = nomeResponsavel;
            this.Endereco = endereco;
        }

        public override string ToString()
        {
            return $"{Id,-3} | {Nome,-10} | {NomeResponsavel,-11} | {Telefone,-12} | {Endereco}";
        }

        public override void Editar(Entidade amigoEditado)
        {
            Amigo amigo = (Amigo)amigoEditado;
            this.Nome = amigo.Nome;
            this.Telefone = amigo.Telefone;
            this.NomeResponsavel = amigo.NomeResponsavel;
            this.Endereco = amigo.Endereco;
        }
    }
}