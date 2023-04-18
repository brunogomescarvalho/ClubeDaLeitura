using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.MenuPrincipal
{
    public class Menu
    {
        public static void MostrarInicio()
        {
            bool Continuar = true;

            Tela tela;

            RevistaRepositorio repositorioRevista = new RevistaRepositorio();
            CaixaRepositorio repositorioCaixa = new CaixaRepositorio();
            AmigoRepositorio repositorioAmigo = new AmigoRepositorio();
            EmprestimoRepositorio repositorioEmprestimo = new EmprestimoRepositorio();

            TelaEmprestimo.CadastrarAlgunsItens(repositorioAmigo, repositorioCaixa, repositorioRevista, repositorioEmprestimo);

            while (Continuar)
            {
                Console.Clear();
                Console.WriteLine("---- Clube da Leitura ---");
                Console.WriteLine("[1] Caixas");
                Console.WriteLine("[2] Amigos");
                Console.WriteLine("[3] Revistas ");
                Console.WriteLine("[4] Empréstimos");
                Console.WriteLine("[9] Sair");

                int opcaoMenu = int.Parse(Console.ReadLine()!);

                switch (opcaoMenu)
                {
                    case 1:
                        tela = new TelaCaixa(repositorioCaixa);
                        break;
                    case 2:
                        tela = new TelaAmigo(repositorioAmigo);
                        break;
                    case 3:
                        tela = new TelaRevista(repositorioRevista, repositorioCaixa);
                        break;
                    case 4:
                        tela = new TelaEmprestimo(repositorioAmigo, repositorioRevista, repositorioEmprestimo);
                        break;
                    case 9:
                        Continuar = false;
                        continue;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida");
                        Console.ReadKey();
                        continue;
                }

                tela.MostrarMenu();
            }
        }
    }
}