using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace KraftHealthFood.projeto.Repository
{
    public class khf_usuarioRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public khf_usuarioRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<khf_usuarioModel> Listar()
        {
            var lista = new List<khf_usuarioModel>();
            lista = dataBaseContext.khf_Usuario
                .Include(c => c.endereco)
                .ToList<khf_usuarioModel>();
            return lista;
        }


        public khf_usuarioModel Consultar(int id)
        {
            var cliente = dataBaseContext.khf_Usuario
                .Where(c => c.id_usuario == id)
                .Include(c => c.endereco).FirstOrDefault();

            return cliente;
        }

        public khf_usuarioModel logar(string email, string senha)
        {
            var cliente = dataBaseContext.khf_Usuario
                .Where(c => c.ds_email.ToLower() == email.ToLower() && c.ds_senha == senha)
                .Include(c => c.endereco).FirstOrDefault();

            return cliente;
        }

        public khf_usuarioModel ConsultarPorParteNome(string nomeParcial)
        {
            var usuario = dataBaseContext.khf_Usuario.Where(e => e.nm_usuario.ToLower().Contains(nomeParcial)).FirstOrDefault<khf_usuarioModel>();
            return usuario;
        }

        public khf_usuarioModel ConsultarPorEmail(string email)
        {
            var usuario = dataBaseContext.khf_Usuario.Where(e => e.ds_email.ToLower().Contains(email)).FirstOrDefault<khf_usuarioModel>();
            return usuario;
        }

        public khf_usuarioModel ConsultarPorCPF(string cpf)
        {
            var usuario = dataBaseContext.khf_Usuario.Where(e => e.nr_cpf.ToLower().Contains(cpf)).FirstOrDefault<khf_usuarioModel>();
            return usuario;
        }

        public void Inserir(khf_usuarioModel cliente)
        {
            dataBaseContext.khf_Usuario.Add(cliente);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(khf_usuarioModel cliente)
        {

            dataBaseContext.khf_Usuario.Update(cliente);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(khf_usuarioModel usuario)
        {
            dataBaseContext.ChangeTracker.Clear();
            dataBaseContext.khf_Usuario.Remove(usuario);
            dataBaseContext.SaveChanges();
        }

    }
}
