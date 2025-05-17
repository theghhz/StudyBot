namespace StudyBot.Domain.Erros
{
    public class Resultado<T>
    {
        private readonly Either<T, List<ErroEntidade>> result;

        private Resultado(T success)
        {
            result = success;
        }

        private Resultado(List<ErroEntidade> failure)
        {
            result = failure;
        }

        public T? Valor => result.Left;
        public List<ErroEntidade>? Erros => result.Right;
        public bool FoiSucesso => result.IsLeft;
        public bool PossuiErros => result.IsRight;


        public static implicit operator Resultado<T>(T success)
        {
            return new Resultado<T>(success);
        }

        public static implicit operator Resultado<T>(List<ErroEntidade> errors)
        {
            return new Resultado<T>(errors);
        }
    }
}