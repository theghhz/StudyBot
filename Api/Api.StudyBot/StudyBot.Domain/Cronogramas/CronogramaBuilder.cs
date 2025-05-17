using StudyBot.Domain.DiasSemanas;
using StudyBot.Domain.Erros;

namespace StudyBot.Domain.Cronogramas
{
    public class CronogramaBuilder
    {
        private string Nome;
        private List<DiasSemana> DiasSemana;
        
        public CronogramaBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }
        
        public CronogramaBuilder ComDiasSemana(List<DiasSemana> diasSemana)
        {
            DiasSemana = diasSemana;
            return this;
        }
        
        public Resultado<Cronograma> Builder()
        {
            return Cronograma.Criar(Nome, DiasSemana);
        }
    }
}