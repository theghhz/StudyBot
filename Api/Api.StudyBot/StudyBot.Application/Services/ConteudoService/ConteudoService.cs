using StudyBot.Application.Dtos;
using StudyBot.Application.Services.ConteudoService.Dtos;
using StudyBot.Domain.Conteudo;
using StudyBot.Domain.Erros;
using StudyBot.Infrastructure.Repositorios.ConteudoRepo;

namespace StudyBot.Application.Services.ConteudoService;

public class ConteudoService(IConteudoRepositorio conteudoRepositorio) : IConteudoService
{
    
    public async Task<Resultado<ConteudoDto>> CriarConteudo(CriarConteudoDto input)
    {
        var erros = new List<ErroEntidade>();

        var conteudoCriado = new ConteudoBuilder()
            .ComNome(input.Nome)
            .ComDescricao(input.Descricao)
            .Builder();
        
        if (!conteudoCriado.FoiSucesso)
            erros.AddRange(conteudoCriado.Erros);

        if (erros.Count > 0)
            return erros;

        await conteudoRepositorio.Salvar(conteudoCriado.Valor!);
        
        return Mapper.Conteudo(conteudoCriado.Valor);
    }
    
    public async Task<Resultado<ConteudoDto>> AtualizarConteudo(Guid id,AtualizarConteudoDto input)
    {
        var erros = new List<ErroEntidade>();
        
        var conteudoExistente = await conteudoRepositorio.BuscarPorId(id);

        if (conteudoExistente == null)
        {
            erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
            return erros;
        }

        var conteudoEditado = conteudoExistente.Editar(
            input.Nome,
            input.Descricao
        );
        
        if(!conteudoEditado.FoiSucesso)
            erros.AddRange(conteudoEditado.Erros);

        if (erros.Count > 0)
            return erros;

        var conteudoAtt = conteudoEditado.Valor;

        await conteudoRepositorio.Atualizar(conteudoAtt);
        
        return Mapper.Conteudo(conteudoAtt);
    }

    public async Task<Resultado<List<ConteudoDto>>> BuscarTodosConteudos()
    {
        var erros = new List<ErroEntidade>();
        
        var conteudos = await conteudoRepositorio.BuscarTodos();

        if (conteudos == null)
        {
            erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
            return erros;
        }

        var conteudosEncontrados = conteudos.Select(c => new ConteudoDto()
        {
            Id = c.Id,
            Nome = c.Nome,
            Descricao = c.Descricao,
        }).ToList();
        
        return conteudosEncontrados;
    }
    
    public async Task<Resultado<ConteudoDto>> BuscarConteudoPorId(Guid id)
    {
        var erros = new List<ErroEntidade>();
        
        var conteudoExistente = await conteudoRepositorio.BuscarPorId(id);

        if (conteudoExistente == null)
        {
            erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
            return erros;
        }

        return Mapper.Conteudo(conteudoExistente);
    }
    
    public async Task<Resultado<string>> RemoverConteudo(Guid id)
    {
        var erros = new List<ErroEntidade>();
        
        var conteudoExistente = await conteudoRepositorio.BuscarPorId(id);

        if (conteudoExistente == null)
        {
            erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
            return erros;
        }

        await conteudoRepositorio.Remover(conteudoExistente);

        return "Conteúdo removido com sucesso!";
    }
}