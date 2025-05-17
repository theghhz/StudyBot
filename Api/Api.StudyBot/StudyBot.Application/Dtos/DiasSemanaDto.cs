using StudyBot.Domain.DiasSemanas.Enum;

namespace StudyBot.Application.Dtos;

public class DiasSemanaDto
{   
    public Guid Id { get; set; }
    public DiasEnum DiaEnum { get; set; }
    public DateTime Data { get; set; }
    public List<ConteudoDto> Conteudos { get; set; }
}