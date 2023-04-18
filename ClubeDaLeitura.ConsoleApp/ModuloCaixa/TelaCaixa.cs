using static ClubeDaLeitura.Caixa;
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura
{
    public class TelaCaixa : Tela
    {
        private readonly CaixaRepositorio repositorio;

        public TelaCaixa(CaixaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public override void MostrarMenu()
        {
            while (Continuar)
            {
                MostrarTexto("-- Caixas --");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Visualizar");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("9 - Voltar");

                OpcaoMenu = Console.ReadLine()!;

                switch (OpcaoMenu)
                {
                    case "1": CadastrarCaixa(); break;
                    case "2": VisualizarCaixas(); break;
                    case "3": EditarCaixa(); break;
                    case "4": ExcluirCaixa(); break;
                    case "9": Continuar = false; continue;
                    default: continue;
                }
            }

        }
        public void CadastrarCaixa()
        {
            Cores cor = SolicitarCorDaCaixa();

            if (!ValidarCorCaixa(cor))
                return;

            MostrarTexto("Informe uma descrição para a etiqueta:");
            var etiqueta = Console.ReadLine();

            var caixa = new Caixa(cor!, etiqueta!);

            repositorio!.Adicionar(caixa);

            MostrarMensagemStatus(ConsoleColor.Green, "Caixa cadatrada com sucesso");
        }

        public void VisualizarCaixas()
        {
            MostrarTexto("-- Caixas Cadastradas --\n");

            var caixas = repositorio.BuscarTodos();

            if (!VerificarListaContemItens(caixas, "caixa"))
                return;

            RenderizarTabela(caixas, true);
        }

        private void EditarCaixa()
        {
            MostrarTexto("--- Editar Caixa ---\n");

            var caixas = repositorio.BuscarTodos();

            if (!VerificarListaContemItens(caixas, "caixas"))
                return;

            RenderizarTabela(caixas, false);

            Console.Write("\nInforme o id da caixa para editar\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Caixa caixa = (Caixa)repositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(caixa, "Caixa  não cadatrada"))
                return;

            Cores cor = SolicitarCorDaCaixa();

            if (!ValidarCorCaixa(cor))
                return;

            MostrarTexto("Informe uma descrição para a etiqueta:");
            var etiqueta = Console.ReadLine();

            caixa.Cor = cor;
            caixa.Etiqueta = etiqueta!;

            MostrarMensagemStatus(ConsoleColor.Green, "Caixa editada com sucesso");

        }

        private void ExcluirCaixa()
        {
            MostrarTexto("--- Excluir Caixa ---\n");

            ArrayList caixas = repositorio.BuscarTodos();

            if (!VerificarListaContemItens(caixas, "caixas"))
                return;

            RenderizarTabela(caixas, false);

            Console.Write("\nInforme o id da caixa para excluir\n=> ");
            string id = Console.ReadLine()!;

            if (!OpcaoValida(id))
                return;

            Caixa caixa = (Caixa)repositorio.BuscarPorId(int.Parse(id));

            if (!VerificarItemEncontrado(caixa, "Caixa  não cadatrada"))
                return;

            repositorio.Remover(caixa);
            MostrarMensagemStatus(ConsoleColor.Green, "Caixa excluída com sucesso");
        }

        private Cores SolicitarCorDaCaixa()
        {
            MostrarTexto("Informe a cor da caixa:");
            Console.WriteLine("0 - Azul");
            Console.WriteLine("1 - Amarelo");
            Console.WriteLine("2 - Vermelho");
            Console.WriteLine("3 - Verde");
            Console.WriteLine("4 - Turquesa");
            Console.WriteLine("5 - Ambar");
            Console.WriteLine("6 - Violeta");
            Console.WriteLine("7 - Branca");  
            Console.Write("8 - Cinza\n=> ");
            

           return (Cores)int.Parse(Console.ReadLine()!);
        }

        private bool ValidarCorCaixa(Cores cor)
        {
            foreach (Cores item in Enum.GetValues((typeof(Cores))))
            {
                if (cor == item)
                    return true;
            }
            MostrarMensagemStatus(ConsoleColor.DarkRed, "Opção inválida...Tecle para continuar!");
            CadastrarCaixa();
            return false;
        }

        private void RenderizarTabela(ArrayList caixas, bool esperarTecla)
        {
            MostrarCabecalho();
            MostrarLista(caixas);
            if (esperarTecla)
                Console.ReadKey();
        }

        private static void MostrarCabecalho()
        {
            Console.WriteLine("{0,-4} | {1,-8} | {2}", "ID", "COR", "ETIQUETA");
            Console.WriteLine("-----|----------|-----------------------------");
        }

    }
}