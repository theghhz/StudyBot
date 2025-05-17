const { InteractionType, EmbedBuilder } = require("discord.js");
const handlerConteudoInteraction = require("../interactions/conteudoInteraction/conteudo.js");
const handlerCronogramaInteraction = require("../interactions/cronogramaInteraction/cronograma.js");

module.exports = {
  name: "interactionCreate",
  async execute(interaction) {
    try {
      console.log(
        `[STUDY BOT - INTERACTIONS] Interaction received: ${interaction.type}`
      );

      if (interaction.type === InteractionType.ApplicationCommand) {
        console.log(
          `[STUDY BOT - INTERACTIONS] Command: ${interaction.commandName}`
        );

        const command = interaction.client.commands.get(
          interaction.commandName
        );

        if (!command) {
          console.error(
            `[STUDY BOT - INTERACTIONS] Comando não encontrado: ${interaction.commandName}`
          );
          return;
        }

        await command.execute(interaction);
      } else if (
        interaction.type === InteractionType.MessageComponent ||
        interaction.type === InteractionType.ModalSubmit
      ) {
        console.log(`[STUDY BOT - INTERACTIONS] Component/Modal: ${interaction.customId}`);
      
        if (
          interaction.customId === "conteudoId" ||
          interaction.customId === "criarConteudoId" ||
          interaction.customId === "editarConteudoId" ||
          interaction.customId === "buscarTodosConteudosId" ||
          interaction.customId === "buscarPorIdConteudosId" ||
          interaction.customId === "ApagarConteudosId" ||
          interaction.customId === "modal-criar-conteudo" ||
          interaction.customId === "modal-buscar-por-id" ||
          interaction.customId === "modal-apagar-conteudo" ||
          interaction.customId === "modal-editar-conteudo-id" ||
          interaction.customId === "modal-editar-conteudo-final-"
        ) {
          return await handlerConteudoInteraction(interaction);
        }

        if(interaction.customId === "cronogramaId" ||
          interaction.customId === "criarCronogramaId" ||
          interaction.customId === "editarCronogramaId" ||
          interaction.customId === "buscarTodosCronogramasId" ||
          interaction.customId === "buscarPorIdCronogramasId" ||
          interaction.customId === "ApagarCronogramasId" ||
          interaction.customId === "modal-criar-cronograma" ||
          interaction.customId === "modal-buscar-por-id-cronograma" ||
          interaction.customId === "modal-apagar-cronograma" ||
          interaction.customId === "modal-editar-cronograma-id" ||
          interaction.customId === "selectCronogramaId" ||
          interaction.customId === "modal-editar-cronograma-final-" ||
          interaction.customId === "editarCronogramaSelectId" ||
          interaction.customId === "editarCronogramaSelectIdDia"
        ) {
          return await handlerCronogramaInteraction(interaction);
        }

      
        const component = interaction.client.interactions.get(interaction.customId);
      
        if (!component) {
          console.error(`[STUDY BOT - INTERACTIONS] Componente não encontrado: ${interaction.customId}`);
          return;
        }
      
        await component.execute(interaction);
      
      } else {
        console.error(
          `[STUDY BOT - INTERACTIONS] Tipo de interação não suportado: ${interaction.type}`
        );
      }
    } catch (error) {
      console.error(
        `[STUDY BOT - INTERACTIONS] Erro ao processar interação: ${error}`
      );

      let embed = new EmbedBuilder()
        .setColor("Random")
        .setTitle(`🚨[STUDY BOT - INTERACTIONS]・Erro ao processar a interação`)
        .setDescription(
          `${
            interaction.type === InteractionType.ApplicationCommand
              ? "[ERROR] -> O COMANDO NÃO PODE SER EXECUTADO!"
              : interaction.type === InteractionType.MessageComponent
              ? "[ERROR2] -> O COMANDO NÃO PODE SER EXECUTADO!"
              : "[ERROR3] -> O COMANDO NÃO PODE SER EXECUTADO!"
          }`
        );

      await interaction.reply({
        embeds: [embed],
        flags: 64,
      });
    }
  },
};
//
