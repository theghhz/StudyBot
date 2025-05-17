
const axios = require("axios");

const {
    EmbedBuilder,
    ActionRowBuilder,
    ModalBuilder,
    TextInputBuilder,
    TextInputStyle,
  } = require("discord.js");

async function criarConteudoInteraction(interaction, mensagem){

    if (interaction.isButton() && interaction.customId === "criarConteudoId") {
        // Cria o modal de criação de conteúdo
        const modalCriarConteudo = new ModalBuilder()
          .setCustomId("modal-criar-conteudo")
          .setTitle("Criar Conteúdo");
    
        const nomeInput = new TextInputBuilder()
          .setCustomId("nome")
          .setLabel("Nome do conteúdo")
          .setStyle(TextInputStyle.Short)
          .setRequired(true);
    
        const descricaoInput = new TextInputBuilder()
          .setCustomId("descricao")
          .setLabel("Descrição")
          .setStyle(TextInputStyle.Paragraph)
          .setRequired(true);
    
        modalCriarConteudo.addComponents(
          new ActionRowBuilder().addComponents(nomeInput),
          new ActionRowBuilder().addComponents(descricaoInput)
        );
    
        // Abre o modal
        await interaction.showModal(modalCriarConteudo);
      }
    
      // Lida com a submissão do modal de criação de conteúdo
      if (
        interaction.isModalSubmit() &&
        interaction.customId === "modal-criar-conteudo"
      ) {
        const nome = interaction.fields.getTextInputValue("nome");
        const descricao = interaction.fields.getTextInputValue("descricao");
    
        console.log("[STUDY BOT - INTERACTIONS] Recebido modal:", nome, descricao);
    
        try {
          const res = await axios.post(`${process.env.API_URL}/Conteudo`, {
            nome,
            descricao,
          });
    
          console.log("[STUDY BOT - API] Resposta da API:", res.data);
    
          let embedResposta = new EmbedBuilder()
            .setColor("Random")
            .setDescription(
              `# ✅ Conteúdo criado com sucesso!\n\n` +
                `**Nome:** ${res.data.resultado.nome}\n**Descrição:** ${res.data.resultado.descricao}`
            )
            .setTimestamp();
    
          await interaction.deferUpdate(); //fecha o modal da tela
    
          return await mensagem.edit({
            embeds: [embedResposta],
            components: [],
            flags: 64,
          });
        } catch (error) {
          console.error("Erro Api:", {
            message: error.message,
            response: error.response?.data,
            status: error.response?.status,
          });
    
          return await mensagem.edit({
            content: "```❌ Ocorreu um erro ao criar o conteúdo.```",
            components: [],
            flags: 64,
          });
        }
      }
}

module.exports = criarConteudoInteraction;