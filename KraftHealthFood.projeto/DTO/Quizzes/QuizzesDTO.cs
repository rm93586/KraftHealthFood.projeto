using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KraftHealthFood.projeto.DTO.Quizzes
{
    public class QuizzesDTO
    {

        public string? nm_quiz { get; set; }

        public string? tx_quiz { get; set; }

        public string? ds_pergunta { get; set; }

        public string? ds_resposta_1 { get; set; }

        public string? ds_resposta_2 { get; set; }

        public string? ds_resposta_3 { get; set; }

        public string? ds_resposta_4 { get; set; }

        public string? ds_resposta_certa { get; set; }

    }
}
