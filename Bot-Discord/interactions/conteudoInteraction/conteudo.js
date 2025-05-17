const {
  EmbedBuilder,
  ButtonBuilder,
  ButtonStyle,
  ActionRowBuilder
} = require("discord.js");

const criarConteudoInteraction = require("./interactions/criarConteudo.js");
const buscarConteudoInteraction = require("./interactions/buscarConteudo.js");
const buscarConteudoIdInteraction = require("./interactions/buscarConteudoId.js");
const apagarConteudoInteraction = require("./interactions/apagarConteudo.js");
const editarConteudoInteraction = require("./interactions/editarConteudo.js");

let mensagem = null;

async function handlerConteudoInteraction(interaction) {
  
  if (interaction.isButton() && interaction.customId === "conteudoId") {
    let embedConteudo = new EmbedBuilder()
      .setColor("Random")
      .setDescription(
        ` # **📌 ・ STUDY BOT - CONTEUDO MENU**\n\n` +
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
        .setCustomId("criarConteudoId")
        .setLabel("CRIAR CONTEÚDO")
        .setEmoji({ name: "✅" })
        .setStyle(ButtonStyle.Primary),

      new ButtonBuilder()
        .setCustomId("editarConteudoId")
        .setLabel("EDITAR CONTEÚDO")
        .setEmoji({ name: "🔄" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("buscarTodosConteudosId")
        .setLabel("BUSCAR TODOS CONTEÚDOS")
        .setEmoji({ name: "🔍" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("buscarPorIdConteudosId")
        .setLabel("BUSCAR CONTEÚDO POR ID")
        .setEmoji({ name: "🔍" })
        .setStyle(ButtonStyle.Secondary),

      new ButtonBuilder()
        .setCustomId("ApagarConteudosId")
        .setLabel("APAGAR CONTEÚDO")
        .setEmoji({ name: "❌" })
        .setStyle(ButtonStyle.Danger)
    );

    // Responde com a embed de opções
    mensagem = await interaction.reply({
      embeds: [embedConteudo],
      components: [row],
      flags: 64,
    });
  }

  // CRIAR CONTEÚDO
  if (
    (interaction.isButton() && interaction.customId === "criarConteudoId") ||
    (interaction.isModalSubmit() &&
      interaction.customId === "modal-criar-conteudo")
  ) {
    await criarConteudoInteraction(interaction, mensagem);
  }

  // BUSCAR TODOS OS CONTEUDOS
  if (
    interaction.isButton() &&
    interaction.customId === "buscarTodosConteudosId"
  ) {
    await buscarConteudoInteraction(interaction, mensagem);
  }

  if (
    (interaction.isButton() &&
      interaction.customId === "buscarPorIdConteudosId") ||
    (interaction.isModalSubmit() &&
      interaction.customId === "modal-buscar-por-id")
  ) {
    await buscarConteudoIdInteraction(interaction, mensagem);
  }

  // APAGAR CONTEÚDO
  if (
    (interaction.isButton() && interaction.customId === "ApagarConteudosId") ||
    (interaction.isModalSubmit() &&
      interaction.customId === "modal-apagar-conteudo")
  ) {
    await apagarConteudoInteraction(interaction, mensagem);
  }

  if (
    (interaction.isButton() && interaction.customId === "editarConteudoId") ||
    (interaction.isModalSubmit() &&
      interaction.customId === "modal-editar-conteudo-id") ||
    (interaction.isModalSubmit() &&
      interaction.customId.startsWith("modal-editar-conteudo-final-"))
  ) {
    await editarConteudoInteraction(interaction, mensagem);
  }
}

module.exports = handlerConteudoInteraction;
