const {
    SlashCommandBuilder,
    ModalBuilder,
    TextInputBuilder,
    TextInputStyle,
    ActionRowBuilder,
  } = require("discord.js");
  
  module.exports = {
    data: new SlashCommandBuilder()
      .setName("criarconteudo")
      .setDescription("Cria um novo conteúdo"),
  
    async execute(interaction) {
      
    },
  };
  