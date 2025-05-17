using StudyBot.Application.Dtos;
using StudyBot.Domain.Conteudos;
using StudyBot.Domain.Cronogramas;
using StudyBot.Domain.DiasSemanas; 

namespace StudyBot.Application
{
    public static class Mapper
    {
        public static ConteudoDto? Conteudo(Conteudo conteudo)
        {
            return new ConteudoDto()
            {   
                Nome = conteudo.Nome,
                Descricao = conteudo.Descricao
            };
        }
        
        public static List<ConteudoDto> Conteudos(List<Conteudo> conteudos)
        {
            return conteudos.Select(c => Conteudo(c)).ToList();
        }
        
        public static CronogramaDto Cronograma(Cronograma cronograma)
        {
            return new CronogramaDto
            {
                Id = cronograma.Id,
                Nome = cronograma.Nome,
                Dias = cronograma.Dias
                    .OrderBy(d => d.DiaEnum)
                    .Select(d => new DiasSemanaDto()
                    {
                        Id = d.Id,
                        DiaEnum = d.DiaEnum,
                        Data = d.Data,
                        Conteudos = d.Conteudos.Select(c => new ConteudoDto
                        {
                            Id = c.Id,
                            Nome = c.Nome,
                            Descricao = c.Descricao,
                        }).ToList()
                    }).ToList()
            };
        }
    
        public static List<CronogramaDto> Cronogramas(List<Cronograma> cronogramas)
        {
            return cronogramas.Select(c => Cronograma(c)).ToList();
        }
    }
}