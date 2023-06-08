using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_mensagemRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public khf_mensagemRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<khf_mensagemModel> Listar()
        {
            var lista = new List<khf_mensagemModel>();
            lista = dataBaseContext.khf_Mensagens
                .Include(c => c.usuario)
                .ToList<khf_mensagemModel>();
            return lista;
        }

        public IList<khf_mensagemModel> ListarRecentes()
        {
            var ultimosItens = dataBaseContext.khf_Mensagens
                .Include(c => c.usuario)
                .OrderBy(m => m.dt_mensagem)
                .ToList<khf_mensagemModel>();

            if((ultimosItens.Count - 5) < 5 )
            {
                return ultimosItens;
            }

            return ultimosItens.GetRange(ultimosItens.Count - 5, 5);
        }

        public void Inserir(khf_mensagemModel mensagem)
        {
            dataBaseContext.khf_Mensagens.Add(mensagem);
            dataBaseContext.SaveChanges();
        }


        public List<khf_mensagemModel> ConsultarPorIdUsuario(int id)
        {
            var mensagem = dataBaseContext.khf_Mensagens
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.id_usuario == id)
                .Include(c => c.usuario).ToList();

            return mensagem;
        }

        public khf_mensagemModel ConsultarPoridMensagem(int id)
        {
            var mensagem = dataBaseContext.khf_Mensagens
                .Where(c => c.id_mensagem == id)
                .Include(c => c.usuario).FirstOrDefault();

            return mensagem;
        }
        public void Excluir(khf_mensagemModel mensagem)
        {
            dataBaseContext.khf_Mensagens.Remove(mensagem);
            dataBaseContext.SaveChanges();
        }

        public void ExcluirRange(List<khf_mensagemModel> mensagem)
        {
            dataBaseContext.khf_Mensagens.RemoveRange(mensagem);
            dataBaseContext.SaveChanges();
        }
    }
}
