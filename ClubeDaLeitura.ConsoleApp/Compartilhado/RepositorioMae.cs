using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class RepositorioMae
    {
        protected ArrayList registros = new ArrayList();

        private int contadorId = 1;

        public virtual void Adicionar(Entidade entidade)
        {
            entidade.Id = contadorId++;
            registros.Add(entidade);
        }

        public void Remover(Entidade entidade)
        {
            registros.Remove(entidade);
        }

        public ArrayList BuscarTodos()
        {
            return registros;
        }

        public Entidade BuscarPorId(int id)
        {
            foreach (Entidade item in registros)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null!;
        }
    }
}
