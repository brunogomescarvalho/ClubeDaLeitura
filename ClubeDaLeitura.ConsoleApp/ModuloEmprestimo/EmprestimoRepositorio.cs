using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class EmprestimoRepositorio : RepositorioMae
    {


        public override void Adicionar(Entidade entidade)
        {
            Emprestimo emprestimo = (Emprestimo)entidade;
            emprestimo.Registrar();
            base.Adicionar(emprestimo);
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

    }
}