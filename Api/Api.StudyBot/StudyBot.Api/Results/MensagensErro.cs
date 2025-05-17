using StudyBot.Domain.Erros;

namespace StudyBot.Api.Results
{
    public static class MensagensErro
    {
        private static readonly Dictionary<ErroEntidade, string> erros = new()
        {
            { ErroEntidade.ALGO_INESPERADO_OCORREU, "Algo inesperado ocorreu." }
        };
    }
}