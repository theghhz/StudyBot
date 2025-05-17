const {
  EmbedBuilder,
  ActionRowBuilder,
  ModalBuilder,
  TextInputBuilder,
  TextInputStyle,
} = require("discord.js");

const axios = require("axios");

async function criarCronogramaInteraction(interaction, mensagem) {
  if (interaction.isButton() && interaction.customId === "criarCronogramaId") {
    const modal = new ModalBuilder()
      .setCustomId("modal-criar-cronograma")
      .setTitle("📌 CRIAR CRONOGRAMA");

    const inputNome = new TextInputBuilder()
      .setCustomId("nome")
      .setLabel("Nome do cronograma")
      .setStyle(TextInputStyle.Short)
      .setPlaceholder("Digite o nome do cronograma")
      .setRequired(true);

    const row1 = new ActionRowBuilder().addComponents(inputNome);

    modal.addComponents(row1, row2);

    await interaction.showModal(modal);
  }

  if (
    interaction.isModalSubmit() &&
    interaction.customId === "modal-criar-cronograma"
  ) {
    const nome = interaction.fields.getTextInputValue("nome");

    console.log("[STUDY BOT - INTERACTIONS] Recebido modal:", nome);

    try {
      const res = await axios.post(`${process.env.API_URL}/Cronograma`, {
        nome,
      });

      console.log(`[STUDY BOT - CRONOGRAMA] Resposta da API:`, res.data);
      let embed = new EmbedBuilder()
        .setColor("Random")
        .setDescription(
          `# ✅・STUDY BOT - CRONOGRAMA・CRONOGRAMA CRIADO!\n\n` +
            `O cronograma **${res.data.resultado.nome}** foi criado com sucesso!`
        )
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

      await interaction.deferUpdate();

      await mensagem.edit({
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

      let embed = new EmbedBuilder()
        .setColor("Random")
        .setDescription(
          `# 🚨・STUDY BOT - CRONOGRAMA・ERRO AO CRIAR O CRONOGRAMA!\n\n` +
            `Ocorreu um erro ao criar o cronograma. Tente novamente mais tarde.`
        )
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

      await mensagem.edit({
        embeds: [embed],
        flags: 64,
      });
    }
  }
}

module.exports = criarCronogramaInteraction;
