using StudyBot.Domain.Erros;

namespace StudyBot.Domain.Conteudo
{
    public class ConteudoBuilder
    {
        private string Nome;
        private string Descricao;
        
        public ConteudoBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }
        
        public ConteudoBuilder ComDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }
        
        public Resultado<Conteudos.Conteudo> Builder()
        {
            return Conteudos.Conteudo.Criar(Nome, Descricao);
        }
    }
}