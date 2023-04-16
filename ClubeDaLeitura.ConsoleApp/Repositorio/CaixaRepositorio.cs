using System.Collections;
using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.Repositorio
{
    public class CaixaRepositorio : RepositorioMae
    {
        public ArrayList ListarCaixas()
        {
            return registros;
        }

        public void CadastrarCaixa(Caixa caixa)
        {
            registros.Add(caixa);
        }

        public Caixa BuscarPorNumero(int numero)
        {
            foreach (Caixa item in registros)
            {
                if (item.Numero == numero)
                    return item;
            }

            return null!;
        }

        public void ExcluirCaixa(Caixa caixa)
        {
            registros.Remove(caixa);
        }

    }
}