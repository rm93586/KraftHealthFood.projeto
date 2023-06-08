using KraftHealthFood.projeto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KraftHealthFood.projeto.DTO.Receitas
{
    public class ReceitaDTO
    {

        public string? nm_receita { get; set; }


        public string? ds_receita { get; set; }


        public string? ds_imagem { get; set; }

        public string? ds_link_receita { get; set; }
    }
}
