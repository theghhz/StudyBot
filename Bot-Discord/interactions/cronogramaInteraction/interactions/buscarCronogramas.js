const {
    EmbedBuilder,
    StringSelectMenuBuilder,
    StringSelectMenuOptionBuilder,
    ActionRowBuilder
  } = require("discord.js");
  
  const axios = require("axios");

async function buscarCronogramasInteraction(interaction, mensagem){
    if (
        interaction.isButton() &&
        interaction.customId === "buscarTodosCronogramasId"
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
              flasg: 64,
            });
          }
    
          const menu = new StringSelectMenuBuilder()
            .setCustomId("selectCronogramaId")
            .setPlaceholder("Selecione um cronograma")
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
        interaction.customId === "selectCronogramaId"
      ) {
        const cronogramaId = interaction.values[0];
    
        try {
          const res = await axios.get(
            `${process.env.API_URL}/Cronograma/${cronogramaId}`
          );
          const cronograma = res.data.resultado;
    
          const embed = new EmbedBuilder()
            .setDescription(`# ✅・STUDY BOT - CRONOGRAMA・\n\n`)
            .setColor("Random")
            .setTimestamp()
            .setFooter({
              text: " @StudyBot - 2025 | Todos os direitos reservados!",
            });
    
          for (const dia of cronograma.dias) {
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
            const conteudos = dia.conteudos.length
              ? dia.conteudos
                  .map((c) => `• **${c.nome}**: ${c.descricao}`)
                  .join("\n")
              : "Nenhum conteúdo.";
    
            embed.addFields({
              name: `🗓️ ${nomeDia} (${dataFormatada})`,
              value: conteudos,
            });
          }
    
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
            .setDescription(`🚨 Erro ao buscar cronograma por ID.`);
    
          await mensagem.edit({
            embeds: [embed],
            flags: 64,
          });
        }
      }
}

module.exports = buscarCronogramasInteraction;