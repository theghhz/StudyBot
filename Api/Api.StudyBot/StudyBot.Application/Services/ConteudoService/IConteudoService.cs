using StudyBot.Application.Dtos;
using StudyBot.Application.Services.ConteudoService.Dtos;
using StudyBot.Domain.Erros;

namespace StudyBot.Application.Services.ConteudoService;

public interface IConteudoService
{
    Task<Resultado<ConteudoDto>> CriarConteudo(CriarConteudoDto input);
    Task<Resultado<ConteudoDto>> AtualizarConteudo(Guid id,AtualizarConteudoDto input);
    Task<Resultado<List<ConteudoDto>>> BuscarTodosConteudos();
    Task<Resultado<ConteudoDto>> BuscarConteudoPorId(Guid id);
    Task<Resultado<string>> RemoverConteudo(Guid id);
}