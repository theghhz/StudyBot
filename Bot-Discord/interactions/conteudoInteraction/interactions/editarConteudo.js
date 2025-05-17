const axios = require("axios");

const {
    EmbedBuilder,
    ActionRowBuilder,
    ModalBuilder,
    TextInputBuilder,
    TextInputStyle,
  } = require("discord.js");

async function editarConteudoInteraction(interaction, mensagem) {
    if (interaction.isButton() && interaction.customId === "editarConteudoId") {
        const modalId = new ModalBuilder()
          .setCustomId("modal-editar-conteudo-id")
          .setTitle("Editar Conteúdo - Passo 1");
    
        const idInput = new TextInputBuilder()
          .setCustomId("conteudoId")
          .setLabel("Informe o ID do conteúdo")
          .setStyle(TextInputStyle.Short)
          .setRequired(true);
    
        modalId.addComponents(new ActionRowBuilder().addComponents(idInput));
    
        await interaction.showModal(modalId);
      }
    
      if (
        interaction.isModalSubmit() &&
        interaction.customId === "modal-editar-conteudo-id"
      ) {
        const id = interaction.fields.getTextInputValue("conteudoId");
    
        try {
          const res = await axios.get(`${process.env.API_URL}/Conteudo/${id}`);
          const conteudo = res.data.resultado;
    
          const modalEditar = new ModalBuilder()
            .setCustomId(`modal-editar-conteudo-final-${id}`)
            .setTitle("Editar Conteúdo");
    
          const nomeInput = new TextInputBuilder()
            .setCustomId("nome")
            .setLabel("Nome do conteúdo")
            .setStyle(TextInputStyle.Short)
            .setRequired(true)
            .setValue(conteudo.nome);
    
          const descricaoInput = new TextInputBuilder()
            .setCustomId("descricao")
            .setLabel("Descrição")
            .setStyle(TextInputStyle.Paragraph)
            .setRequired(true)
            .setValue(conteudo.descricao);
    
          modalEditar.addComponents(
            new ActionRowBuilder().addComponents(nomeInput),
            new ActionRowBuilder().addComponents(descricaoInput)
          );
    
          await interaction.showModal(modalEditar);
        } catch (error) {
          console.error("Erro Api:", {
            message: error.message,
            response: error.response?.data,
            status: error.response?.status,
          });
    
          await mensagem.edit({
            content: "❌ Conteúdo não encontrado com esse ID.",
            components: [],
            flags: 64,
          });
        }
      }
    
      if (
        interaction.isModalSubmit() &&
        interaction.customId.startsWith("modal-editar-conteudo-final-")
      ) {
        const id = interaction.customId.split("modal-editar-conteudo-final-")[1];
        const nome = interaction.fields.getTextInputValue("nome");
        const descricao = interaction.fields.getTextInputValue("descricao");
    
        try {
          const res = await axios.put(`${process.env.API_URL}/Conteudo/${id}`, {
            nome,
            descricao,
          });
    
          let embedConteudoEditado = new EmbedBuilder()
            .setColor("Green")
            .setDescription(
              `# ✅ Conteúdo atualizado com sucesso!\n\n` +
                `**Nome:** ${res.data.resultado.nome}\n**Descrição:** ${res.data.resultado.descricao}`
            )
            .setTimestamp();
    
          await interaction.deferUpdate();
    
          await interaction.reply({
            embeds: [embedConteudoEditado],
            flags: 64,
          });
        } catch (error) {
          console.error("Erro Api:", {
            message: error.message,
            response: error.response?.data,
            status: error.response?.status,
          });
    
          await interaction.reply({
            content: "❌ Erro ao atualizar o conteúdo.",
            components: [],
            flags: 64,
          });
        }
      }
}

module.exports = editarConteudoInteraction;