using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_dicaRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public khf_dicaRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<khf_dicaModel> Listar()
        {
            var lista = new List<khf_dicaModel>();
            lista = dataBaseContext.khf_Dicas
                .Include(c => c.receitas)
                .ToList<khf_dicaModel>();
            return lista;
        }

        public IList<khf_dicaModel> ListarAleatorio()
        {
            var lista = new List<khf_dicaModel>();
            lista = dataBaseContext.khf_Dicas
                .Include(c => c.receitas)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList<khf_dicaModel>();
            return lista;
        }

        public khf_dicaModel Consultar(int id)
        {
            var dica = dataBaseContext.khf_Dicas
                .Where(c => c.id_dica == id)
                .Include(c => c.receitas).FirstOrDefault();

            return dica;
        }

        public void Inserir(khf_dicaModel dica)
        {
            dataBaseContext.khf_Dicas.Add(dica);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(khf_dicaModel dica)
        {
            dataBaseContext.khf_Dicas.Update(dica);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(khf_dicaModel dica)
        {

            dataBaseContext.khf_Dicas.Remove(dica);
            dataBaseContext.SaveChanges();
        }
    }
}
