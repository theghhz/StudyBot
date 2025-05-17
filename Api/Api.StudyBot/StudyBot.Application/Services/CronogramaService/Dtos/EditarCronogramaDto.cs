using StudyBot.Domain.DiasSemanas.Enum;

namespace StudyBot.Application.Services.CronogramaService.Dtos;

public record EditarCronogramaDto(
    string? Nome,
    List<EditarDiaSemanaDto>? DiasSemana 
);

public record EditarDiaSemanaDto(
    DiasEnum Dia,
    List<Guid> ConteudosIds 
);