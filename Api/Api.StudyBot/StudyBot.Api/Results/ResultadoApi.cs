using StudyBot.Domain.Erros;

namespace StudyBot.Api.Results
{
    public class ResultadoApi<T>
    {
        public bool Sucesso { get; set; }
        public T? Resultado { get; set; }
        public List<ErroRespostaApi> Erros { get; set; } = [];
        
        public static ResultadoApi<T> CriarSucesso(T resultado)
        {
            return new ResultadoApi<T>
            {
                Sucesso = true,
                Resultado = resultado
            };
        }

        public static ResultadoApi<T> CriarFracasso(List<ErroEntidade> erros)
        {
            var err = erros.Select(x => new ErroRespostaApi { Codigo = x }).ToList();
            return new ResultadoApi<T> 
            {
                Sucesso = false,
                Erros = err
            };
        }
    }
}