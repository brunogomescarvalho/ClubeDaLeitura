using System.Collections;
using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaAmigo : Tela
    {
        private readonly AmigoRepositorio amigoRepositorio;

        public TelaAmigo(AmigoRepositorio amigoRepositorio)
        {
            this.amigoRepositorio = amigoRepositorio;
        }

        public override void MostrarMenu()
        {
            while (Continuar)
            {
                Console.Clear();
                Console.WriteLine("-- Amigos --");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Visualizar");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("9 - Voltar");

                OpcaoMenu = Console.ReadLine()!;

                if (OpcaoValida(OpcaoMenu))
                    switch (OpcaoMenu)
                    {
                        case "1": CadastrarAmigo(); break;
                        case "2": ListarAmigos(); break;
                        case "3": EditarAmigo(); break;
                        case "4": ExcluirAmigo(); break;
                        case "9": Continuar = false; continue;
                        default: continue;
                    }
            }
        }

        private void CadastrarAmigo()
        {
            Amigo amigo = ExecutarFormulario();
            amigoRepositorio.Adicionar(amigo);
        }

        private void ListarAmigos()
        {
            MostrarTexto("-- Amigos Cadastrados -- ");

            ArrayList amigos = amigoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(amigos, "amigos"))
                return;

            RenderizarTabela(amigos, true);
        }


        private void EditarAmigo()
        {
            MostrarTexto("-- Editar Amigo -- ");

            ArrayList amigos = amigoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(amigos, "amigos"))
                return;

            RenderizarTabela(amigos, false);

            Console.Write("\nInforme o id do amigo\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Amigo amigo = (Amigo)amigoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(amigo, "Amigo não encontrado"))
            {
                EditarAmigo();
                return;
            }

            Amigo amigoEditado = ExecutarFormulario();
            amigo.Editar(amigoEditado);


            MostrarMensagemStatus(ConsoleColor.Green, $"Amigo editado com sucesso");
        }

        private void ExcluirAmigo()
        {
            MostrarTexto("-- Excluir Amigo -- ");

            ArrayList amigos = amigoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(amigos, "amigos"))
                return;

            RenderizarTabela(amigos, false);

            Console.Write("\nInforme o id do amigo\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Amigo amigoEncontrado = (Amigo)amigoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(amigoEncontrado, "Amigo não encontrado"))
            {
                EditarAmigo();
                return;
            }

            amigoRepositorio.Remover(amigoEncontrado);
            MostrarMensagemStatus(ConsoleColor.Green, "Cadastro Excluído com sucesso");
        }

        private Amigo ExecutarFormulario()
        {
            MostrarTexto("Informe o nome");
            string nome = Console.ReadLine()!;

            MostrarTexto("Informe o nome do responsável");
            string nomeResponsavel = Console.ReadLine()!;

            MostrarTexto("Informe o telefone");
            string telefone = Console.ReadLine()!;

            MostrarTexto("--- Cadastro Endereço ---\nInforme a rua:");
            string rua = Console.ReadLine()!;

            MostrarTexto("Informe o número:");
            int numero = int.Parse(Console.ReadLine()!);

            MostrarTexto("Informe o bairro:");
            string bairro = Console.ReadLine()!;

            MostrarTexto("Informe o cep:");
            string cep = Console.ReadLine()!;

            MostrarTexto("Informe o complemento:");
            string complemento = Console.ReadLine()!;

            Endereco endereco = new Endereco(rua, numero, bairro, cep, complemento);

            return new Amigo(nome, nomeResponsavel, telefone, endereco);
        }

        private void RenderizarTabela(ArrayList amigos, bool esperarTecla)
        {
            ExibirCabecalhoTabela();
            MostrarLista(amigos);
            if (esperarTecla)
                Console.ReadLine();
        }

        private void ExibirCabecalhoTabela()
        {
            Console.WriteLine("\n{0,-3} | {1,-10} | {2,-11} | {3, -10}   | {4,-20} | {5,-5} | {6,-20} | {7,-10} | {8}", "ID", "NOME", "RESPONSÁVEL", "TELEFONE", "LOGRADOURO", "NRº", "BAIRRO", "CEP", "COMPLEMENTO");
            Console.WriteLine("----|------------|-------------|--------------|----------------------|-------|----------------------|------------|---------------");
        }

    }
}