using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura
{
    public class Revista : Entidade
    {
        public string TipoColecao;
        public int NrEdicao;
        public int AnoEdicao;
        public Caixa _Caixa;
        public bool Locada { get; private set; }

        public Revista(string tipo, int numeroEdicao, int anoEdicao, Caixa caixa)
        {
            this.TipoColecao = tipo;
            this.NrEdicao = numeroEdicao;
            this.AnoEdicao = anoEdicao;
            this._Caixa = caixa;
            this.Locada = false;
        }

        public void AlterarStatusRevista()
        {
            this.Locada = !this.Locada;
        }

        public override string ToString()
        {
            return $"{Id,-5} | {TipoColecao,-25} | {NrEdicao,-6} | {AnoEdicao,-5} | {_Caixa.Cor,-8} | {_Caixa.Etiqueta,-20} | {(Locada ? "Sim" : "Não")}";
        }

        public override void Editar(Entidade revistaEditada)
        {
            Revista revista = (Revista)revistaEditada;
            this.TipoColecao = revista.TipoColecao;
            this.NrEdicao = revista.NrEdicao;
            this.AnoEdicao = revista.AnoEdicao;
            this._Caixa = revista._Caixa;
        }
    }
}