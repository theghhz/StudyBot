
const axios = require("axios");

const {
    EmbedBuilder,
  } = require("discord.js");

async function buscarConteudoInteraction(interaction, mensagem) {
  if (
    interaction.isButton() &&
    interaction.customId === "buscarTodosConteudosId"
  ) {
    try {
      const res = await axios.get(`${process.env.API_URL}/Conteudo`);
      const conteudos = res.data.resultado;

      const embed = new EmbedBuilder()
        .setColor("Blue")
        .setTitle("📚 Conteúdos cadastrados")
        .setDescription(
          conteudos.length > 0
            ? conteudos
                .map(
                  (c) =>
                    `**ID:** ${c.id}\n**Nome:** ${c.nome}\n**Descrição:** ${c.descricao}`
                )
                .join("\n\n")
            : `# 📚 Conteúdos cadastrados` + ` ###Nenhum conteúdo encontrado.`
        )
        .setTimestamp();

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
        content: "`❌ Erro ao buscar conteúdos.`",
        components: [],
        flags: 64,
      });
    }
  }
}

module.exports = buscarConteudoInteraction;
