using System.Runtime.InteropServices.JavaScript;

namespace StudyBot.Application.Dtos
{
    public class ConteudoDto
    {   
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}

