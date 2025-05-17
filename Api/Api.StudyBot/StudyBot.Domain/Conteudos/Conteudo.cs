using StudyBot.Domain.Erros;

namespace StudyBot.Domain.Conteudos
{
    public class Conteudo : Entity<Conteudo>
    {
        protected Conteudo()
        {
        }
        
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }

        public static Resultado<Conteudo> Criar(string nome, string descricao)
        {
            var erros = Validar(nome, descricao);
            if (erros.Count > 0)
                return erros;
            
            var conteudo = new Conteudo
            {   
                Id = Guid.NewGuid(),
                Nome = nome,
                Descricao = descricao,
            };

            return conteudo;
        }

        public Resultado<Conteudo> Editar(string nome, string descricao)
        {
            var erros = Validar(nome, descricao);
            if (erros.Count > 0)
                return erros;
            
            Nome = nome;
            Descricao = descricao;
            
            return this;
        }

        private static List<ErroEntidade> Validar(string nome, string descricao)
        {
            var erros = new List<ErroEntidade>();
            
            if (string.IsNullOrWhiteSpace(nome))
                erros.Add(ErroEntidade.NOME_INVALIDO);
            
            if (string.IsNullOrWhiteSpace(descricao))
                erros.Add(ErroEntidade.DESCRICAO_INVALIDA);
            
            return erros;
        }
        public override bool Equals(Conteudo? outro)
        {
            return outro is not null &&
                   Nome == outro.Nome &&
                   Descricao == outro.Descricao;
        }
    }
}