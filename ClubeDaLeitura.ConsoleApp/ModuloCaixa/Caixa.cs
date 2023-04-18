using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura
{
    public class Caixa : Entidade
    {
        public Cores Cor;
        public string Etiqueta;
      

        public Caixa(Cores cor, string etiqueta)
        {
            this.Cor = cor;
            this.Etiqueta = etiqueta;
        }

        public override void Editar(Entidade entidade)
        {
            Caixa caixa = (Caixa)entidade;
            Etiqueta = caixa.Etiqueta;
            Cor = caixa.Cor;
        }
        
        public override string ToString()
        {
            return $"{this.Id,-4} | {this.Cor,-8} | {this.Etiqueta,-20}";
        }

        public enum Cores
        {
            AZUL,
            AMARELA,
            VERMELHA,
            VERDE,
            TURQUESA,
            AMBAR,
            VIOLETA,
            BRANCA,
            CINZA,

        }
    }
}