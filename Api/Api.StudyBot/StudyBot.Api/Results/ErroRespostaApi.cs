using StudyBot.Domain.Erros;
using StudyBot.Domain.Extensions;

namespace StudyBot.Api.Results
{
    public class ErroRespostaApi
    {
        public ErroEntidade Codigo { get; set; }
        public string Mensagem => Codigo.GetDescription();
    }
}