using StudyBot.Domain.DiasSemanas;
using StudyBot.Domain.Erros;

namespace StudyBot.Domain.Cronogramas
{
    public class Cronograma : Entity<Cronograma>
    {
        protected Cronograma()
        {
        }

        public string Nome { get; protected set; }
        public virtual ICollection<DiasSemana> Dias { get; protected set; } 

        public override bool Equals(Cronograma? outro)
        {
            return outro is not null 
                   && Nome == outro.Nome
                   && Dias.SequenceEqual(outro.Dias);
        }

        public static Resultado<Cronograma> Criar(string nome, List<DiasSemana> dias)
        {
            var erros = Validar(nome);

            if (erros.Count > 0)
                return erros;

            var cronograma = new Cronograma()
            {
                Nome = nome,
                Dias = dias
            };

            return cronograma;
        }
        
        public Resultado<Cronograma> Editar(string nome, List<DiasSemana> dias)
        {
            var erros = Validar(nome);

            this.Nome = nome;
            this.Dias = dias;

            return this;
        }

        private static List<ErroEntidade> Validar(string nome)
        {
            var erros = new List<ErroEntidade>();
            
            if(string.IsNullOrWhiteSpace(nome))
                erros.Add(ErroEntidade.NOME_INVALIDO);
            
            return erros;

        }
    }
}