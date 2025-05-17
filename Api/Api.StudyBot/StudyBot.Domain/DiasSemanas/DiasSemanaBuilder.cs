using StudyBot.Domain.DiasSemanas.Enum;
using StudyBot.Domain.Erros;

namespace StudyBot.Domain.DiasSemanas;

public class DiasSemanaBuilder
{
    private DiasEnum Dia;
    private DateTime Data;
    private List<Conteudos.Conteudo> Conteudos = new List<Conteudos.Conteudo>();
    
    public DiasSemanaBuilder ComDia(DiasEnum dia)
    {
        Dia = dia;
        return this;
    }
    
    public DiasSemanaBuilder ComData(DateTime data)
    {
        Data = data;
        return this;
    }
    
    public DiasSemanaBuilder ComConteudos(List<Conteudos.Conteudo> conteudos)
    {
        Conteudos = conteudos;
        return this;
    }
    
    public Resultado<DiasSemana> Builder()
    {
        return DiasSemana.Criar(Dia, Data, Conteudos);
    }
}