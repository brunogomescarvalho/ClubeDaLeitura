using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo : Tela
    {
        private readonly AmigoRepositorio amigoRepositorio;
        private readonly EmprestimoRepositorio emprestimoRepositorio;
        private readonly RevistaRepositorio revistaRepositorio;
        public TelaEmprestimo(AmigoRepositorio amigoRepositorio, RevistaRepositorio revistaRepositorio, EmprestimoRepositorio emprestimoRepositorio)
        {
            this.amigoRepositorio = amigoRepositorio;
            this.emprestimoRepositorio = emprestimoRepositorio;
            this.revistaRepositorio = revistaRepositorio;
        }

        public override void MostrarMenu()
        {
            while (Continuar)
            {
                Console.Clear();
                Console.WriteLine("-- Empréstimos --");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar Todos");
                Console.WriteLine("3 - Finalizar");
                Console.WriteLine("4 - Listar histórico 30 dias");
                Console.WriteLine("5 - Listar empréstimos em aberto");
                Console.WriteLine("6 - Editar");
                Console.WriteLine("7 - Excluir");
                Console.WriteLine("9 - Voltar");

                OpcaoMenu = Console.ReadLine()!;
                if (OpcaoValida(OpcaoMenu))
                    switch (OpcaoMenu)
                    {
                        case "1": CriarNovoEmprestimo(); break;
                        case "2": ListarEmprestimos(); break;
                        case "3": FinalizarEmprestimo(); break;
                        case "4": MostrarEmprestimosUltimoMes(); break;
                        case "5": MostrarEmprestimosEmAberto(); break;
                        case "6": Editar(); break;
                        case "7": ExcluirEmprestimo(); break;
                        case "9": Continuar = false; continue;
                        default: continue;
                    }
            }
        }

        private void CriarNovoEmprestimo()
        {
            MostrarTexto("-- Empréstimo --\n");
            Emprestimo emprestimo = ExecutarFormulario();

            if (emprestimo == null)
                return;

            emprestimoRepositorio.Adicionar(emprestimo);

            MostrarMensagemStatus(ConsoleColor.Green, "Empréstimo Cadastrado Com Sucesso");

        }

        private void ListarEmprestimos()
        {
            MostrarTexto("-- Empréstimos --\n");

            ArrayList emprestimos = emprestimoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(emprestimos, "empréstimos"))
                return;

            RenderizarTabelaEmprestimo(emprestimos, true);
        }

        private void FinalizarEmprestimo()
        {
            MostrarTexto("-- Devolução de Empréstimos --\n");

            ArrayList emprestimos = emprestimoRepositorio.ObterEmprestimosEmAberto();

            if (!VerificarListaContemItens(emprestimos, "empréstimos"))
                return;

            RenderizarTabelaEmprestimo(emprestimos, false);

            Console.Write("\nInforme o id do empréstimo para efetuar a devolução:\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Emprestimo emprestimo = (Emprestimo)emprestimoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(emprestimo, "Empréstimo não cadastrado"))
                return;

            emprestimo.Devolver();

            MostrarMensagemStatus(ConsoleColor.Green, "Devolução efetuada com sucesso");
        }

        private void MostrarEmprestimosUltimoMes()
        {
            MostrarTexto("-- Histórico de Empréstimos 30 dias --\n");

            ArrayList emprestimos = emprestimoRepositorio.ObterEmprestimosMensal();

            if (!VerificarListaContemItens(emprestimos, "empréstimos"))
                return;

            RenderizarTabelaEmprestimo(emprestimos, true);
        }

        private void MostrarEmprestimosEmAberto()
        {
            MostrarTexto("-- Empréstimos em aberto --\n");

            ArrayList emprestimosEmAberto = emprestimoRepositorio.ObterEmprestimosEmAberto();

            if (!VerificarListaContemItens(emprestimosEmAberto, "empréstimos em aberto"))
                return;

            RenderizarTabelaEmprestimo(emprestimosEmAberto, true);
        }

        private Emprestimo ExecutarFormulario()
        {
            ArrayList amigos = amigoRepositorio.BuscarTodos();
            ArrayList revistas = revistaRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(amigos, "amigos") || !VerificarListaContemItens(revistas, "revistas"))
                return null!;

            ExibirTabelaAmigo();
            MostrarLista(amigos);

            Console.Write("\nInforme o id do Amigo\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return null!;

            Amigo amigo = (Amigo)amigoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(amigo, $"Amigo id {id} não cadastrado."))
                return null!;

            Console.Clear();
            ExibirCabecalhoRevistas();
            MostrarLista(revistas);

            Console.Write("\nInforme o id da revista\n=> ");
            string idRevista = Console.ReadLine()!;

            if (!OpcaoValida(idRevista))
                return null!;

            Revista revista = (Revista)revistaRepositorio.BuscarPorId(int.Parse(idRevista));

            if (!VerificarItemEncontrado(revista, $"Revista id {idRevista} não cadastrada."))
                return null!;

            if (revista.Locada == true)
            {
                MostrarMensagemStatus(ConsoleColor.DarkYellow, $"Revista id {revista.Id} - {revista.TipoColecao} já está locada.");
                return null!;
            }

            return new Emprestimo(amigo, revista);

        }

        private void Editar()
        {
            MostrarTexto("--- Editar Emprestimo ---");

            ArrayList emprestimos = emprestimoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(emprestimos, "empréstimos"))
                return;

            RenderizarTabelaEmprestimo(emprestimos, false);

            Console.Write("\nInforme o id do empréstimo para efetuar a edição:\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Emprestimo emprestimo = (Emprestimo)emprestimoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(emprestimo, "Empréstimo não cadastrado"))
                return;

            Emprestimo emprestimoEditado = ExecutarFormulario();

            if (emprestimoEditado == null)
                return;


            MostrarMensagemStatus(ConsoleColor.Green, "Edição efetuada com sucesso");

        }


        private void ExcluirEmprestimo()
        {
            MostrarTexto("--- Excluir Empréstimo ---");

            ArrayList emprestimos = emprestimoRepositorio.BuscarTodos();

            if (!VerificarListaContemItens(emprestimos, "empréstimos"))
                return;

            RenderizarTabelaEmprestimo(emprestimos, false);

            Console.Write("\nInforme o id do empréstimo para efetuar a exclusão:\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Emprestimo emprestimo = (Emprestimo)emprestimoRepositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(emprestimo, "Empréstimo não cadastrado"))
                return;

            if (emprestimo.Finalizado == false)
            {
                MostrarMensagemStatus(ConsoleColor.DarkYellow, $"Não é possivel excluir o empréstimo: {emprestimo.Id} - {emprestimo.Amigo.Nome} - {emprestimo.DataEmprestimo} pois está em aberto.");
                return;
            }

            emprestimoRepositorio.Remover(emprestimo);
            MostrarMensagemStatus(ConsoleColor.Green, "Exclusão efetuada com sucesso");

        }
        private void ExibirTabelaAmigo()
        {
            Console.WriteLine("\n{0,-3} | {1,-10} | {2,-11} | {3, -10}   | {4,-20} | {5,-5} | {6,-20} | {7,-10} | {8}", "ID", "NOME", "RESPONSÁVEL", "TELEFONE", "LOGRADOURO", "NRº", "BAIRRO", "CEP", "COMPLEMENTO");
            Console.WriteLine("----|------------|-------------|--------------|----------------------|-------|----------------------|------------|---------------");

        }

        private void ExibirCabecalhoEmprestimo()
        {
            Console.WriteLine("{0,-5} | {1,-10} | {2,-10} {3,5} | {4,-25} {5,-5} {6,5} | {7,-5} | {8,-10}", "ID", "DATA EMPRÉSTIMO", "AMIGO", "ID", "COLEÇÃO", "EDIÇÃO", "ID", "FINALIZADO", "DATA DEVOLUÇÃO");
            Console.WriteLine("------|-----------------|------------------|----------------------------------------|------------|--------------------");
        }
        private void ExibirCabecalhoRevistas()
        {
            Console.WriteLine("\n{0,-5} | {1,-25} | {2,-6} | {3,-5} | {4,-8} | {5,-20} | {6}", "ID", "COLEÇÃO", "EDIÇÃO", "ANO", "CX: Cor", "Etiqueta", "LOCADA");
            Console.WriteLine("------|---------------------------|--------|-------|----------|----------------------|---------");
        }
        private void RenderizarTabelaEmprestimo(ArrayList emprestimos, bool esperarTecla)
        {
            ExibirCabecalhoEmprestimo();
            MostrarLista(emprestimos);
            if (esperarTecla)
                Console.ReadKey();
        }











        public static void CadastrarAlgunsItens(AmigoRepositorio amigoRep, CaixaRepositorio caixaRep, RevistaRepositorio revistaRep, EmprestimoRepositorio emprestimorep)
        {
            AmigoRepositorio repositorio = amigoRep;
            CaixaRepositorio repositorioCx = caixaRep;
            RevistaRepositorio repositorioRv = revistaRep;
            EmprestimoRepositorio repositorioEp = emprestimorep;

            var endereco = new Endereco("Pará", 45, "Centro", "88508-222", "casa");
            var endereco1 = new Endereco("Av Brasil", 1500, "São Cristóvão", "88508-120", "Ap 202");
            var endereco2 = new Endereco("Caetano V Costa", 145, "Centro", "88508-002", "Ap 101");

            var amigo = new Amigo("JowJow", "Nane", "32232374", endereco);

            repositorio.Adicionar(amigo);
            repositorio.Adicionar(new Amigo("Dagmar", "Leslie", "32252340", endereco1));
            repositorio.Adicionar(new Amigo("Honofre", "Elba", "9992374", endereco2));

            var cx1 = new Caixa(Caixa.Cores.AMARELA, "Quadrinhos");
            var cx2 = new Caixa(Caixa.Cores.AZUL, "Teoria Musical");
            var cx3 = new Caixa(Caixa.Cores.VERDE, "Programação");
            var cx4 = new Caixa(Caixa.Cores.VERMELHA, "Antiga Diversos");

            repositorioCx.Adicionar(cx1);
            repositorioCx.Adicionar(cx2);
            repositorioCx.Adicionar(cx3);
            repositorioCx.Adicionar(cx4);

            var revista = new Revista("História da Arte", 123, 1985, cx4);
            var revista1 = new Revista("A geometria da Música", 021, 1996, cx2);
            var revista2 = new Revista("A Casa do código", 54, 2007, cx3);
            var revista3 = new Revista("As Aventuras de Patrick", 23, 2011, cx1);

            repositorioRv.Adicionar(revista);
            repositorioRv.Adicionar(revista1);
            repositorioRv.Adicionar(revista2);
            repositorioRv.Adicionar(revista3);

            var emprestimo = new Emprestimo(amigo, revista);
        
            repositorioEp.Adicionar(emprestimo);

        }

    }
}

