const axios = require("axios");

const {
    EmbedBuilder,
    ActionRowBuilder,
    ModalBuilder,
    TextInputBuilder,
    TextInputStyle,
  } = require("discord.js");

async function apagarConteudoInteraction(interaction, mensagem){
    if (interaction.isButton() && interaction.customId === "ApagarConteudosId") {
        const modal = new ModalBuilder()
          .setCustomId("modal-apagar-conteudo")
          .setTitle("Apagar Conteúdo");
    
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
        interaction.customId === "modal-apagar-conteudo"
      ) {
        const id = interaction.fields.getTextInputValue("id");
    
        try {
          await axios.delete(`${process.env.API_URL}/Conteudo/${id}`);
    
          const embed = new EmbedBuilder()
            .setColor("Red")
            .setDescription(
              `# 🗑️ Conteúdo deletado com sucesso!\n\n` +
                `O conteúdo com ID ${id} foi removido.`
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
            content: "❌ Erro ao apagar conteúdo.",
            components: [],
            flags: 64,
          });
        }
      }
}

module.exports = apagarConteudoInteraction;