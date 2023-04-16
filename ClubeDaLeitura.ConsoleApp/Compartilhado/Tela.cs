using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public abstract class Tela
    {
        protected static string? OpcaoMenu;
        protected bool Continuar = true;
        public abstract void MostrarMenu();


        protected void MostrarMensagemStatus(ConsoleColor cor, string msg)
        {
            Console.Clear();
            Console.ForegroundColor = cor;
            Console.WriteLine(msg);
            Console.ResetColor();
            Console.ReadKey();
        }

        protected void MostrarTexto(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
        }

        protected void MostrarLista(ArrayList lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
        }

        protected bool VerificarListaContemItens(ArrayList lista, string item)
        {
            if (lista.Count == 0)
            {
                MostrarMensagemStatus(ConsoleColor.Yellow, $"Lista de {item} sem registros até o momento");
                return false;
            }
            return true;
        }

        protected bool VerificarItemEncontrado(Object obj, string item)
        {
            if (obj == null)
            {
                MostrarMensagemStatus(ConsoleColor.Red, item);
                return false;
            }
            return true;

        }

        protected bool OpcaoValida(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in id)
                {
                    if (item < (char)48 || item > (char)57)
                    {
                        MostrarMensagemStatus(ConsoleColor.Red, "Opção Inválida");
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

    }
}