using StudyBot.Domain.Conteudos;
using StudyBot.Domain.DiasSemanas.Enum;
using StudyBot.Domain.Erros;

namespace StudyBot.Domain.DiasSemanas
{
    public class DiasSemana : Entity<DiasSemana>
    {

        protected DiasSemana()
        {
        }
        
       public DiasEnum DiaEnum { get; protected set; }
       public DateTime Data { get; protected set; }
       public virtual List<Guid> Conteudos { get; protected set; } = [];
       
       public override bool Equals(DiasSemana? other)
       {
              return other is not null && 
                    DiaEnum == other.DiaEnum &&
                    Conteudos.SequenceEqual(other.Conteudos);
       }
       
         public static Resultado<DiasSemana> Criar(DiasEnum dia, DateTime data, List<Guid> conteudos)
         {
              var erros = Validar(dia, data);
              
              if (erros.Count > 0)
                return erros;
              
              var diasSemana = new DiasSemana
              {
                DiaEnum = dia,
                Data = data.ToUniversalTime(),
                Conteudos = conteudos
              };
              
              return diasSemana;
         }
         
            public Resultado<DiasSemana> Editar(DiasEnum dia, DateTime data)
            {
                var erros = Validar(dia,data);
                
                if (erros.Count > 0)
                    return erros;
                
                this.DiaEnum = dia;
                this.Data = data.ToUniversalTime();
                
                return this;
            }
            
            public Resultado<List<Guid>> AdicionarConteudo(Guid conteudo)
            {
                var erros = new List<ErroEntidade>();
                
                if (conteudo == Guid.Empty || conteudo == null)
                    erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
                
                if (erros.Count > 0)
                    return erros;
                
                this.Conteudos.Add(conteudo);
                
                return this.Conteudos;
            }
            
            private static List<ErroEntidade> Validar(DiasEnum dia, DateTime data)
            {
                var erros = new List<ErroEntidade>();
                
                if(dia < DiasEnum.Domingo || dia > DiasEnum.Sabado)
                    erros.Add(ErroEntidade.DIA_SEMANA_INVALIDO);
                
                if(data < DateTime.Now)
                    erros.Add(ErroEntidade.DATA_INVALIDA);
                return erros;
            }
    }
}