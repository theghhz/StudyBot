using StudyBot.Application.Services.CronogramaService.Dtos;
using StudyBot.Domain.DiasSemanas;

namespace StudyBot.Application.Dtos;

public class CronogramaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<DiasSemanaDto> Dias { get; set; } = [];
}