using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_usuario_enderecoRepository
    {

        private readonly DataBaseContext dataBaseContext;

        public khf_usuario_enderecoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }
        public IList<khf_usuario_enderecoModel> ListarLocais(int id)
        {
            var lista = new List<khf_usuario_enderecoModel>();
            lista = dataBaseContext.khf_Usuario_Endereco
                .Where(c => c.id_usuario == id)
                .ToList<khf_usuario_enderecoModel>();

            return lista;
        }

        //APELIDOS
        public void Excluir(int id)
        {
            var endereco = new khf_usuario_enderecoModel(id);

            dataBaseContext.khf_Usuario_Endereco.Remove(endereco);
            dataBaseContext.SaveChanges();

        }
    }
}
