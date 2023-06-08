using KraftHealthFood.projeto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KraftHealthFood.projeto.DTO.Usuario
{
    public class UsuarioAlterarDTO
    {
        public int id_usuario { get; set; }

        public string? nm_usuario { get; set; }

        public string? nr_cpf { get; set; }

        public DateTime? dt_nascimento { get; set; }

        public string? ds_genero { get; set; }

        public string? ds_telefone { get; set; }

        public string? ds_email { get; set; }

        public string? ds_senha { get; set; }
    }
}
