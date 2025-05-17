const {
  EmbedBuilder,
  ButtonBuilder,
  StringSelectMenuBuilder,
  StringSelectMenuOptionBuilder,
  ButtonStyle,
  ActionRowBuilder,
  ModalBuilder,
  TextInputBuilder,
  TextInputStyle,
} = require("discord.js");
const axios = require("axios");

const criarCronogramaInteraction = require("./interactions/criarCronograma.js");
const buscarCronogramasInteraction = require("./interactions/buscarCronogramas.js");
const editarCronogramaInteraction = require("./interactions/editarCronograma.js");
let mensagem = null;

async function handlerCronogramaInteraction(interaction) {
  if (interaction.isButton() && interaction.customId === "cronogramaId") {
    let embedCronograma = new EmbedBuilder()
      .setColor("Random")
      .setDescription(
        ` # **📌 ・ STUDY BOT - CRONOGRAMA MENU**\n\n` +
          `### 📌 **Selecione a opção abaixo!**\n` +
          `### **ATENÇÃO:**\n` +
          `Fique atento ao que é pedido nas informações!`
      )
      .setTimestamp()
      .setFooter({
        text: " @StudyBot - 2025 | Todos os direitos reservados!",
      });

    const row = new ActionRowBuilder().addComponents(
      new ButtonBuilder()
        .setCustomId("criarCronogramaId")
        .setLabel("CRIAR CRONOGRAMA")
        .setEmoji({ name: "✅" })
        .setStyle(ButtonStyle.Primary),

      new ButtonBuilder()
        .setCustomId("editarCronogramaId")
        .setLabel("EDITAR CRONOGRAMA")
        .setEmoji({ name: "🔄" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("buscarTodosCronogramasId")
        .setLabel("BUSCAR TODOS CRONOGRAMAS")
        .setEmoji({ name: "🔍" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("buscarPorIdCronogramasId")
        .setLabel("BUSCAR CRONOGRAMA POR ID")
        .setEmoji({ name: "🔍" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("apagarCronogramasId")
        .setLabel("APAGAR CRONOGRAMA")
        .setEmoji({ name: "❌" })
        .setStyle(ButtonStyle.Danger)
    );

    // Responde com a embed de opções
    mensagem = await interaction.reply({
      embeds: [embedCronograma],
      components: [row],
      flags: 64,
    });
  }

  if (
    (interaction.isButton() && interaction.customId === "criarCronogramaId") ||
    (interaction.isModalSubmit() &&
      interaction.customId === "modal-criar-cronograma")
  ) {
    await criarCronogramaInteraction(interaction, mensagem);
  }

  if (
    (interaction.isButton() &&
      interaction.customId === "buscarTodosCronogramasId") ||
    (interaction.isStringSelectMenu() &&
      interaction.customId === "selectCronogramaId")
  ) {
    await buscarCronogramasInteraction(interaction, mensagem);
  }

  if (
    (interaction.isButton() && interaction.customId === "editarCronogramaId") ||
    (interaction.isStringSelectMenu() &&
      interaction.customId === "editarCronogramaSelectId")||
      (interaction.isStringSelectMenu() &&
      interaction.customId === "editarCronogramaSelectIdDia")
  ) {
    await editarCronogramaInteraction(interaction, mensagem);
  }

  if (
    interaction.isButton() &&
    interaction.customId === "apagarCronogramasId"
  ) {
    try {
      const res = await axios.get(`${process.env.API_URL}/Cronograma`);

      console.log(`[STUDY BOT - CRONOGRAMA] Resposta da API:`, res.data);

      let embed = new EmbedBuilder()
        .setColor("Random")
        .setDescription("a")
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

      const cronogramas = res.data.resultado;

      if (!cronogramas.length) {
        embed.setDescription(
          `# 🚨・STUDY BOT - CRONOGRAMA\n\n・ERRO AO BUSCAR OS CRONOGRAMAS!\n\n` +
            `Nenhum cronograma encontrado.`
        );

        return mensagem.edit({
          components: [],
          flags: 64,
        });
      }

      const menu = new StringSelectMenuBuilder()
        .setCustomId("apagarCronogramaSelectId")
        .setPlaceholder("Selecione um cronograma para apagar")
        .setMinValues(1)
        .setMaxValues(1)
        .addOptions(
          cronogramas.map((cronograma) =>
            new StringSelectMenuOptionBuilder()
              .setLabel(cronograma.nome)
              .setValue(cronograma.id.toString())
              .setEmoji("📘")
          )
        );

      const row = new ActionRowBuilder().addComponents(menu);

      embed.setDescription(
        `# ✅・STUDY BOT - CRONOGRAMA\n\n・SELECIONE O CRONOGRAMA ABAIXO!\n`
      );

      await mensagem.edit({
        embeds: [embed],
        components: [row],
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
          `# 🚨・STUDY BOT - CRONOGRAMA\n\n・ERRO AO BUSCAR OS CRONOGRAMAS!\n\n` +
            `Ocorreu um erro ao buscar os cronogramas. T
            tente novamente mais tarde.`
        )
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });
      await mensagem.edit({
        embeds: [embed],
        components: [],
        flags: 64,
      });
    }
  }

  if (
    interaction.isStringSelectMenu() &&
    interaction.customId === "apagarCronogramaSelectId"
  ) {
    const cronogramaId = interaction.values[0];

    try {
      const res = await axios.delete(
        `${process.env.API_URL}/Cronograma/${cronogramaId}`
      );
      console.log(`[STUDY BOT - CRONOGRAMA] Resposta da API:`, res.data);

      let embed = new EmbedBuilder()
        .setColor("Random")
        .setDescription(
          `# ✅・STUDY BOT - CRONOGRAMA・CRONOGRAMA APAGADO!\n\n` +
            `O cronograma **${res.data.resultado.nome}** foi apagado com sucesso!`
        )
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

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
        .setColor("Red")
        .setDescription(`🚨 Erro ao apagar cronograma.`);

      await mensagem.edit({
        embeds: [embed],
        flags: 64,
      });
    }
  }
}

module.exports = handlerCronogramaInteraction;
