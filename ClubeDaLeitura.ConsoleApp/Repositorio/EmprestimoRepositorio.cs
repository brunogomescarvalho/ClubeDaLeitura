using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Repositorio
{
    public class EmprestimoRepositorio : RepositorioMae
    {

        public void AdicionarEmprestimo(Emprestimo emprestimo)
        {
            registros.Add(emprestimo);
        }

        public ArrayList BuscarTodos()
        {
            return registros;
        }

        public Emprestimo ObterEmprestimoPorID(int id)
        {
            foreach (Emprestimo item in registros)
            {
                if (item.Id == id)
                    return item;
            }
            return null!;
        }

        public ArrayList ObterEmprestimosMensal()
        {
            ArrayList emprestimosUltimoMes = new ArrayList();

            foreach (Emprestimo item in registros)
            {
                int dias = item.DataEmprestimo.CompareTo(DateTime.Now);
                if (dias <= 30)
                    emprestimosUltimoMes.Add(item);
            }
            return emprestimosUltimoMes;
        }

        public ArrayList ObterEmprestimosEmAberto()
        {
            ArrayList emprestimosEmAberto = new ArrayList();

            foreach (Emprestimo item in registros)
            {
                if (item.Finalizado == false)
                    emprestimosEmAberto.Add(item);
            }

            return emprestimosEmAberto;
        }

         public void Editar(Emprestimo emprestimo, ArrayList dados)
        {
            emprestimo.Revista.AlterarStatusRevista();
            emprestimo.Amigo = (Amigo)dados[0]!;
            emprestimo.Revista = (Revista)dados[1]!;
            emprestimo.Revista.AlterarStatusRevista();
        }

        public void RemoverEmprestimo(Emprestimo emprestimo)
        {
            registros.Remove(emprestimo);
        }
    }
}