using System.Collections;
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.Repositorio;

namespace ClubeDaLeitura.ConsoleApp.Telas
{
    public class TelaRevista : Tela
    {
        private readonly RevistaRepositorio revistaRepositorio;
        private readonly CaixaRepositorio caixaRepositorio;

        public TelaRevista(RevistaRepositorio revistaRepositorio, CaixaRepositorio caixaRepositorio)
        {
            this.caixaRepositorio = caixaRepositorio;
            this.revistaRepositorio = revistaRepositorio;
        }

        public override void MostrarMenu()
        {
            while (Continuar)
            {
                MostrarTexto("-- Revistas --");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Visualizar");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("9 - Voltar");

                OpcaoMenu = Console.ReadLine()!;

                if (OpcaoValida(OpcaoMenu))
                    switch (OpcaoMenu)
                    {
                        case "1": CadastrarRevista(); break;
                        case "2": ListarRevistas(); break;
                        case "3": EditarRevista(); break;
                        case "4": ExcluirRevista(); break;
                        case "9": Continuar = false; continue;
                        default: continue;
                    }
            }
        }
        private void CadastrarRevista()
        {
            ArrayList revista = ExecutarFormulario();

            revistaRepositorio.CadastrarRevista(new Revista((string)revista[0]!, (int)revista[1]!, (int)revista[2]!, (Caixa)revista[3]!));

            MostrarMensagemStatus(ConsoleColor.Green, "Revista Cadastrada Com Sucesso");
        }

        private void ListarRevistas()
        {
            MostrarTexto(" -- Revistas Cadastradas --\n");
            var revistas = revistaRepositorio.ListarRevistas();

            if (!VerificarListaContemItens(revistas, "revistas"))
                return;

            RenderizarTabelaRevistas(revistas, true);
        }

        public ArrayList ExecutarFormulario()
        {
            ArrayList caixas = caixaRepositorio.ListarCaixas();

            if (!VerificarListaContemItens(caixas, "caixas"))
                return null!;

            MostrarTexto("Informe a coleção");
            var colecao = Console.ReadLine();

            MostrarTexto("Informe o número da edição");
            var nrEdicao = int.Parse(Console.ReadLine()!);

            MostrarTexto("Informe o ano da edição");
            var anoEdicao = int.Parse(Console.ReadLine()!);

            Console.Clear();
            VisualizarCaixas(caixas);

            Console.Write("\nInforme o nr da caixa\n=> ");
            string nrCaixa = Console.ReadLine()!;

            if (!OpcaoValida(nrCaixa))
                return null!;

            Caixa caixa = BuscarCaixa(int.Parse(nrCaixa));

            if (!VerificarItemEncontrado(caixa, $"Caixa nº {nrCaixa} não cadastrada"))
                return null!;

            return new ArrayList() { colecao, nrEdicao, anoEdicao, caixa };
        }

        private void EditarRevista()
        {
            var revistas = revistaRepositorio.ListarRevistas();

            if (!VerificarListaContemItens(revistas, "revistas"))
                return;

            RenderizarTabelaRevistas(revistas, false);

            Console.Write("\nInforme o id da revista para editar\n=> ");
            string idRevista = Console.ReadLine()!;

            if (!OpcaoValida(idRevista))
                return;

            Revista revista = revistaRepositorio.BuscarPorId(int.Parse(idRevista));

            if (!VerificarItemEncontrado(revista, $"Revista id {idRevista} não cadastrada."))
                return;

            ArrayList revistaEditada = ExecutarFormulario();

            revistaRepositorio.Editar(revista, revistaEditada);

            MostrarMensagemStatus(ConsoleColor.Green, "Revista Editada Com Sucesso");
        }


        private void ExcluirRevista()
        {
            var revistas = revistaRepositorio.ListarRevistas();

            if (!VerificarListaContemItens(revistas, "revistas"))
                return;

            RenderizarTabelaRevistas(revistas, false);

            Console.Write("Informe o id da revista\n=> ");
            string idRevista = Console.ReadLine()!;

            if (!OpcaoValida(idRevista))
                return;

            Revista revista = revistaRepositorio.BuscarPorId(int.Parse(idRevista));

            if (!VerificarItemEncontrado(revista, $"Revista id {idRevista} não cadastrada."))
                return;

            if (revista.Locada == true)
            {
                MostrarMensagemStatus(ConsoleColor.DarkYellow, $"Não é possivel excluir a revista: {revista.Id} - {revista.TipoColecao} pois está locada.");
                return;
            }

            revistaRepositorio.RemoverRevista(revista);
            MostrarMensagemStatus(ConsoleColor.Green, "Revista Excluída Com Sucesso");
        }
        private void VisualizarCaixas(ArrayList caixas)
        {
            MostrarTexto(" -- Caixas Cadastradas --");
            MostrarLista(caixas);
        }

        private void CabecalhoRevistas()
        {
            Console.WriteLine("\n{0,-5} | {1,-25} | {2,-6} | {3,-5} | {4,-8} | {5,-20} | {6}", "ID", "COLEÇÃO", "EDIÇÃO", "ANO", "CX: Cor", "Etiqueta", "LOCADA");
            Console.WriteLine("------|---------------------------|--------|-------|----------|----------------------|---------");
        }

        private void RenderizarTabelaRevistas(ArrayList revistas, bool esperarTecla)
        {
            CabecalhoRevistas();
            MostrarLista(revistas);
            if (esperarTecla)
                Console.ReadKey();
        }

        private Caixa BuscarCaixa(int numero)
        {
            return caixaRepositorio.BuscarPorNumero(numero);
        }
    }
}