namespace ClubeDaLeitura
{
    public class Caixa
    {
        public Cores Cor;
        public string Etiqueta;
        public int Numero { get; private set; }
        private static int Contador = 1;

        public Caixa(Cores cor, string etiqueta)
        {
            this.Cor = cor;
            this.Etiqueta = etiqueta;
            this.Numero = Contador++;
        }

        public override string ToString()
        {
            return $"{this.Numero,-4} | {this.Cor,-8} | {this.Etiqueta,-20}";
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