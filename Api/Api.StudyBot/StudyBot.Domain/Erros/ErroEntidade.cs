namespace StudyBot.Domain.Erros;
using System.ComponentModel;

public enum ErroEntidade
{
    [Description("Nome inválido")]
    NOME_INVALIDO,
    
    [Description("Descrição inválida")]
    DESCRICAO_INVALIDA,
    
    [Description("Conteúdo não encontrado")]
    CONTEUDO_NAO_ENCONTRADO,
    
    [Description("Erro ao salvar conteúdo")]
    ERRO_SALVAR_CONTEUDO,
    
    [Description("Erro ao atualizar conteúdo")]
    ERRO_ATUALIZAR_CONTEUDO,
    
    [Description("CORINGA")]
    CORINGA,
    
    [Description("Data inválida")]
    DATA_INVALIDA,
    
    [Description("Data inválida")]
    DIA_SEMANA_INVALIDO,
    
    [Description("Algo inesperado ocorreu")]
    ALGO_INESPERADO_OCORREU,
    
    [Description("Cronograma não econtrado")]
    CRONOGRAMA_NAO_ENCONTRADO,
    
    [Description("Dia da semana não encontrado")]
    DIA_SEMANA_NAO_ENCONTRADO,
    
    [Description("Conteudo já adicionado")]
    CONTEUDO_JA_ADICIONADO
}