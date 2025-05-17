using StudyBot.Application.Dtos;
using StudyBot.Application.Services.CronogramaService.Dtos;
using StudyBot.Domain.DiasSemanas.Enum;
using StudyBot.Domain.Erros;

namespace StudyBot.Application.Services.CronogramaService;

public interface ICronogramaService
{
    Task<Resultado<CronogramaDto>> CriarCronograma(CriarCronogramaDto input);
    Task<Resultado<CronogramaDto>> EditarCronograma(Guid id, EditarCronogramaDto dto);
    Task<Resultado<CronogramaDto>> BuscarCronogramaPorId(Guid id);
    Task<Resultado<List<CronogramaDto>>> BuscarTodosCronogramas();
    Task<Resultado<string>> DeletarCronograma(Guid id);
    Task<Resultado<CronogramaDto>> AdicionarConteudoAoCronograma(Guid cronogramaId, Guid conteudoId, DiasEnum diaEnum);
    
}