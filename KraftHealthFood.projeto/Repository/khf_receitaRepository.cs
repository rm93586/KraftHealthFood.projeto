using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_receitaRepository
    {

        private readonly DataBaseContext dataBaseContext;

        public khf_receitaRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<khf_receitaModel> Listar()
        {
            var lista = new List<khf_receitaModel>();
            lista = dataBaseContext.khf_Receitas
                .ToList<khf_receitaModel>();
            return lista;
        }

        public IList<khf_receitaModel> ListarAleatorio()
        {
            var lista = new List<khf_receitaModel>();
            lista = dataBaseContext.khf_Receitas
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList<khf_receitaModel>();
            return lista;
        }

        public khf_receitaModel Consultar(int id)
        {
            var receita = dataBaseContext.khf_Receitas
                .Where(c => c.id_receita == id)
                .Include(c => c.dica)
                .FirstOrDefault();

            return receita;
        }

        public void Inserir(khf_receitaModel receita)
        {
            dataBaseContext.khf_Receitas.Add(receita);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(khf_receitaModel receita)
        {
            dataBaseContext.khf_Receitas.Update(receita);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(khf_receitaModel receita)
        {

            dataBaseContext.khf_Receitas.Remove(receita);
            dataBaseContext.SaveChanges();
        }
    }
}
