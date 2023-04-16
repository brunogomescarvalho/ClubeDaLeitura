using System.Collections;
using ClubeDaLeitura.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.Repositorio
{
    public class RevistaRepositorio : RepositorioMae
    {

        public ArrayList ListarRevistas()
        {
            return registros;
        }

        public void CadastrarRevista(Revista revista)
        {
            registros.Add(revista);
        }

        public Revista BuscarPorId(int id)
        {
            foreach (Revista item in registros)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null!;
        }


        public void Editar(Revista revista, ArrayList revistaEditada)
        {
            revista.TipoColecao = (string)revistaEditada[0]!;
            revista.NrEdicao = (int)revistaEditada[1]!;
            revista.AnoEdicao = (int)revistaEditada[2]!;
            revista._Caixa = (Caixa)revistaEditada[3]!;
        }

        public void RemoverRevista(Revista revista)
        {
            registros.Remove(revista);
        }

    }
}