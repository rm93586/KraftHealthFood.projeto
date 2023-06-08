using KraftHealthFood.projeto.Models;
using Microsoft.EntityFrameworkCore;

namespace KraftHealthFood.projeto.Repository.Context
{
    public class DataBaseContext : DbContext
    {
        //Propriedade que será responsável pelo acesso a tabela khf_Cliente
        public DbSet<khf_usuarioModel> khf_Usuario { get; set; }

        //Propriedade que será responsável pelo acesso a tabela khf_Cliente_Endereco
        public DbSet<khf_usuario_enderecoModel> khf_Usuario_Endereco { get; set; }

        //Propriedade que será responsável pelo acesso a tabela khf_mensagem
        public DbSet<khf_mensagemModel> khf_Mensagens { get; set; }

        //Propriedade que será responsável pelo acesso a tabela khf_quizzes
        public DbSet<khf_quizzesModel> khf_Quizzes { get; set; }

        //Propriedade que será responsável pelo acesso a tabela khf_dicas
        public DbSet<khf_dicaModel> khf_Dicas { get; set; }

        //Propriedade que será responsável pelo acesso a tabela khf_receitas
        public DbSet<khf_receitaModel> khf_Receitas { get; set; }



        // Este campo sobrescreve o ModelBuilder para a construção das tabelas e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<khf_usuario_enderecoModel>()
                .HasKey(ac => new { ac.id_usuario });

            //Relação de um para um ou 0 entre CLIENTE e ENDEREÇO
            modelBuilder.Entity<khf_usuarioModel>()
                .HasOne(c => c.endereco)
                .WithOne(e => e.cliente)
                .HasForeignKey<khf_usuario_enderecoModel>(e => e.id_usuario);


            modelBuilder.Entity<khf_mensagemModel>()
                .HasOne(m => m.usuario)
                .WithMany(c => c.mensagens)
                .HasForeignKey(m => m.id_usuario);

            modelBuilder.Entity<khf_receitaModel>()
                .HasOne(r => r.dica)
                .WithMany(d => d.receitas)
                .HasForeignKey(r => r.id_dica)
                .IsRequired(false);

        }

        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        protected DataBaseContext()
        {
        }

    }


}
