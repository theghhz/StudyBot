const {
  EmbedBuilder,
  StringSelectMenuBuilder,
  StringSelectMenuOptionBuilder,
  ActionRowBuilder,
  ButtonBuilder,
  ButtonStyle,
} = require("discord.js");

const axios = require("axios");

async function editarCronogramaInteraction(interaction, mensagem) {
  if (interaction.isButton() && interaction.customId === "editarCronogramaId") {
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
          flasg: 64,
        });
      }

      const menu = new StringSelectMenuBuilder()
        .setCustomId(`editarCronogramaSelectId`)
        .setPlaceholder("Selecione um cronograma para editar")
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
      await interaction.deferUpdate();
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
            `Ocorreu um erro ao buscar os cronogramas. Tente novamente mais tarde.`
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
    interaction.customId === "editarCronogramaSelectId"
  ) {
    let cronogramaId = interaction.values[0];
    console.log("Cronograma ID " + cronogramaId);
    try {
      const res = await axios.get(
        `${process.env.API_URL}/Cronograma/${cronogramaId}`
      );

      console.log(`[STUDY BOT - CRONOGRAMA] Resposta da API:`, res.data);

      const cronograma = res.data.resultado;

      const embed = new EmbedBuilder()
        .setDescription(`# ✅・STUDY BOT - CRONOGRAMA・\n\n`)
        .setColor("Random")
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

      // Garante que dias é sempre um array
      const dias = Array.isArray(cronograma.dias)
        ? cronograma.dias
        : [cronograma.dias];

      for (const dia of dias) {
        const dataFormatada = new Date(dia.data).toLocaleDateString("pt-BR");
        const nomeDia = [
          "Domingo",
          "Segunda",
          "Terça",
          "Quarta",
          "Quinta",
          "Sexta",
          "Sábado",
        ][dia.diaEnum];
        const conteudos =
          dia.conteudos && dia.conteudos.length
            ? dia.conteudos
                .map((c) => `• **${c.nome}**: ${c.descricao}`)
                .join("\n")
            : "Nenhum conteúdo.";

        embed.addFields({
          name: `🗓️ ${nomeDia} (${dataFormatada})`,
          value: conteudos,
        });
      }

      embed.addFields({
        name: `:arrow_down_small: SELECIONE O DIA ABAIXO! :arrow_down_small:`,
        value: "\u200B",
      });

      const menu = new StringSelectMenuBuilder()
        .setCustomId("editarCronogramaSelectIdDia")
        .setPlaceholder("Selecione um cronograma para editar")
        .setMinValues(1)
        .setMaxValues(1)
        .addOptions(
          dias.map((dia) =>
            new StringSelectMenuOptionBuilder()
              .setLabel(
                `${
                  [
                    "Domingo",
                    "Segunda-Feira",
                    "Terça-Feira",
                    "Quarta-Feira",
                    "Quinta-Feira",
                    "Sexta-Feira",
                    "Sábado",
                  ][dia.diaEnum]
                } (${new Date(dia.data).toLocaleDateString("pt-BR")})`
              )
              .setValue(dia.id.toString())
              .setEmoji("📘")
          )
        );

      const row = new ActionRowBuilder().addComponents(menu);

      await interaction.deferUpdate();
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
        .setColor("Red")
        .setDescription(`🚨 Erro ao buscar cronograma por ID.`);

      await mensagem.edit({
        embeds: [embed],
        components: [],
        flags: 64,
      });
    }
  }
  if (
    interaction.isStringSelectMenu() &&
    interaction.customId === "editarCronogramaSelectIdDia"
  ) {
    const diaId = interaction.values[0];
    console.log("[DEBUG] Interação recebida:", {
      type: interaction.type,
      isSelectMenu: interaction.isStringSelectMenu(),
      customId: interaction.customId,
      values: interaction.values,
    });

    try {
      console.log("Dia ID " + diaId);
      const res = await axios.get(`${process.env.API_URL}/Cronograma`);
      const cronogramas = res.data.resultado;

      const cronograma = cronogramas.find((c) =>
        c.dias.some((d) => d.id.toString() === diaId)
      );

      const dia = cronograma.dias.find((d) => d.id.toString() === diaId);

      if (!dia) {
        const embed = new EmbedBuilder()
          .setColor("Red")
          .setDescription(`🚨 Dia não encontrado dentro do cronograma.`);

        return await mensagem.edit({
          embeds: [embed],
          components: [],
          flags: 64,
        });
      }

      const dataFormatada = new Date(dia.data).toLocaleDateString("pt-BR");
      const nomeDia = [
        "Domingo",
        "Segunda-feira",
        "Terça-feira",
        "Quarta-feira",
        "Quinta-feira",
        "Sexta-feira",
        "Sábado",
      ][dia.diaEnum];

      const conteudos = dia.conteudos.length
        ? dia.conteudos
            .map((c, i) => `**${i + 1}. ${c.nome}**\n${c.descricao}`)
            .join("\n")
        : "Nenhum conteúdo disponível para este dia.";

      const embed = new EmbedBuilder()
        .setTitle(`📚 Conteúdos de ${nomeDia} (${dataFormatada})`)
        .setDescription(conteudos)
        .setColor("Blue")
        .setTimestamp()
        .setFooter({
          text: " @StudyBot - 2025 | Todos os direitos reservados!",
        });

      const row = new ActionRowBuilder().addComponents(
        new ButtonBuilder()
          .setCustomId("addConteudoEmEditarConteudo")
          .setLabel("ADICIONAR CONTEÚDO")
          .setEmoji({ name: "✅" })
          .setStyle(ButtonStyle.Primary),

        new ButtonBuilder()
          .setCustomId("deletarConteudoEmEditarConteudo")
          .setLabel("DELETAR CONTEÚDO")
          .setEmoji({ name: "🔄" })
          .setStyle(ButtonStyle.Danger)
      );

      await interaction.deferUpdate();

      await mensagem.edit({
        embeds: [embed],
        components: [row],
        flags: 64,
      });
    } catch (error) {
      console.error("Erro ao buscar cronogramas:", {
        message: error.message,
        response: error.response?.data,
        status: error.response?.status,
      });

      const embed = new EmbedBuilder()
        .setColor("Red")
        .setDescription(`🚨 Erro ao buscar os conteúdos do dia.`);

      await mensagem.edit({
        embeds: [embed],
        components: [],
        flags: 64,
      });
    }
  }

  if (
    interaction.isButton() &&
    interaction.customId === "addConteudoEmEditarConteudo"
  ) {
  }

  if (
    interaction.isButton() &&
    interaction.customId === "deletarConteudoEmEditarConteudo"
  ) {
  }
}

module.exports = editarCronogramaInteraction;
