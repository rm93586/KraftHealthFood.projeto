using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_quizzesRepository
    {

        private readonly DataBaseContext dataBaseContext;

        public khf_quizzesRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<khf_quizzesModel> Listar()
        {
            var lista = new List<khf_quizzesModel>();
            lista = dataBaseContext.khf_Quizzes
                .ToList<khf_quizzesModel>();
            return lista;
        }

        public IList<khf_quizzesModel> ListarAleatorio()
        {
            var lista = new List<khf_quizzesModel>();
            lista = dataBaseContext.khf_Quizzes
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList<khf_quizzesModel>();
            return lista;
        }

        public khf_quizzesModel Consultar(int id)
        {
            var quiz = dataBaseContext.khf_Quizzes
                .Where(c => c.id_quiz == id).FirstOrDefault();

            return quiz;
        }

        public khf_quizzesModel ConsultarPorParteNome(string nomeParcial)
        {
            var quiz = dataBaseContext.khf_Quizzes.Where(e => e.nm_quiz.ToLower().Contains(nomeParcial)).FirstOrDefault<khf_quizzesModel>();
            return quiz;
        }

        public void Inserir(khf_quizzesModel quiz)
        {
            dataBaseContext.khf_Quizzes.Add(quiz);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(khf_quizzesModel quiz)
        {
            dataBaseContext.khf_Quizzes.Update(quiz);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(khf_quizzesModel quiz)
        {

            dataBaseContext.khf_Quizzes.Remove(quiz);
            dataBaseContext.SaveChanges();
        }
    }
}
