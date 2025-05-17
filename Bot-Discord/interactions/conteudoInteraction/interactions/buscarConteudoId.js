
const axios = require("axios");

const {
    EmbedBuilder,
    ActionRowBuilder,
    ModalBuilder,
    TextInputBuilder,
    TextInputStyle,
  } = require("discord.js");

async function buscarConteudoIdInteraction(interaction, mensagem) {
    if (
        interaction.isButton() &&
        interaction.customId === "buscarPorIdConteudosId"
      ) {
        const modal = new ModalBuilder()
          .setCustomId("modal-buscar-por-id")
          .setTitle("Buscar Conteúdo por ID");
    
        modal.addComponents(
          new ActionRowBuilder().addComponents(
            new TextInputBuilder()
              .setCustomId("id")
              .setLabel("ID do conteúdo")
              .setStyle(TextInputStyle.Short)
              .setRequired(true)
          )
        );
    
        return await interaction.showModal(modal);
      }
    
      if (
        interaction.isModalSubmit() &&
        interaction.customId === "modal-buscar-por-id"
      ) {
        const id = interaction.fields.getTextInputValue("id");
    
        try {
          const res = await axios.get(`${process.env.API_URL}/Conteudo/${id}`);
          const c = res.data.resultado;
    
          const embed = new EmbedBuilder()
            .setColor("Blue")
            .setDescription(
              `# 🔍 Conteúdo encontrado\n\n` +
                `**ID:** ${c.id}\n**Nome:** ${c.nome}\n**Descrição:** ${c.descricao}`
            )
            .setTimestamp();
    
          await interaction.deferUpdate();
    
          return await mensagem.edit({
            embeds: [embed],
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
            content: "❌ Conteúdo não encontrado.",
            components: [],
            flags: 64,
          });
        }
      }
}

module.exports = buscarConteudoIdInteraction;