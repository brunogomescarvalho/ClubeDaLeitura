using System.Collections;
using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Repositorio
{
    public class AmigoRepositorio : RepositorioMae
    {
        public void AdicionarAmigo(Amigo amigo)
        {
            registros.Add(amigo);
        }

        public ArrayList ListarAmigos()
        {
            return registros;
        }

        public Amigo ObterAmigoPorID(int id)
        {
            foreach (Amigo item in registros)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null!;
        }

        public void ExcluirAmigo(Amigo amigo)
        {
            registros.Remove(amigo);
        }

        public void EditarAmigo(int id, ArrayList amigoEditado)
        {
            Amigo amigoSolicitado = ObterAmigoPorID(id);

            amigoSolicitado.Nome = (string)amigoEditado[0]!;
            amigoSolicitado.NomeResponsavel = (string)amigoEditado[1]!;
            amigoSolicitado.Telefone = (string)amigoEditado[2]!;
            amigoSolicitado.Endereco = (Endereco)amigoEditado[3]!;
        }

    }
}