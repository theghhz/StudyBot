const { REST } = require("@discordjs/rest");
const { Routes } = require("discord-api-types/v10");
const { ActivityType, EmbedBuilder, ActionRowBuilder, ButtonBuilder, ButtonStyle } = require("discord.js");
const axios = require('axios');
const color = require('colors');
const dotenv = require('dotenv');
const fs = require("fs");
const path = require("path");
dotenv.config();

module.exports = {
  name: "ready",
  once: true,
  async execute(client) {
    // Global
    const globalCommands = Array.from(
      client.commands.filter((cmd) => cmd.global === true).values()
    ).map((m) => m.data.toJSON());

    // Guild
    const guildCommands = Array.from(
      client.commands.filter((cmd) => cmd.global === false).values()
    ).map((m) => m.data.toJSON());

    const rest = new REST({ version: "10" }).setToken(process.env.DISCORD_TOKEN);

    // Global
    try {
      await rest.put(Routes.applicationCommands(client.user.id), {
        body: globalCommands,
      });
      console.log(`${color.bold.green(`[STUDY BOT - READY]`)} Global commands registered successfully!`.yellow);
    } catch (error) {
        console.error("[STUDY BOT] Failed to register global commands:", error);
    }

    // Guild
    try {
      const commands = [];
      const commandsPath = path.join(__dirname, "../commands");
      const commandFiles = fs.readdirSync(commandsPath).filter(file => file.endsWith(".js"));
      
      for (const file of commandFiles) {
          const command = require(`../commands/${file}`);
          commands.push(command.data.toJSON());
      }
      
      const rest = new REST().setToken(process.env.DISCORD_TOKEN);
      
      (async () => {
          try {
              console.log("[STUDY BOT] Registrando slashcommands...");
      
              await rest.put(
                  Routes.applicationCommands(process.env.CLIENT_ID),
                  { body: commands }
              );
      
              console.log("[STUDY BOT]Comandos registrados com sucesso!");
          } catch (error) {
              console.error(error);
          }
      })();

    } catch (error) {
      console.error("[STUDY BOT] Failed to register guild commands:", error);
    }

    // Rich Presence
    // let status = [
    //   {
    //     name: "Study Bot!",
    //     type: ActivityType.Competing
    //   }
    // ];
    client.user.setActivity("Study Bot!", { type: ActivityType.Competing });

    // setInterval(() => {
    //   let random = Math.floor(Math.random() * status.length);
    //   client.user.setActivity(status[random]);
    // }, 7000);

    //--------------- CONTROLE DE CARGOS -----------------//

    let statusApi = await axios.get(`${process.env.API_URL}/Status`);

    let statusMessage = statusApi.status === 200 ? ":green_circle: API Online" : ":red_circle: API Offline";

    let embed = new EmbedBuilder()
      .setColor("Random")
      .setDescription(`# 📌 ・ STUDY BOT\n\n` +
        `### [Study - API]: ${statusMessage} \n` + 
        `### **ATENÇÃO:**\n` + `Selecione a opção a baixo!`)
      .setTimestamp()
      .setFooter({
        text: ' @StudyBot - 2025 | Todos os direitos reservados!'
      });

    const row = new ActionRowBuilder()
      .addComponents(

        new ButtonBuilder()
          .setCustomId('conteudoId')
          .setLabel('CONTEÚDOS')
          .setStyle(ButtonStyle.Secondary),
        
          new ButtonBuilder()
          .setCustomId('cronogramaId')
          .setLabel('CRONOGRAMAS')
          .setStyle(ButtonStyle.Secondary),

          new ButtonBuilder()
          .setCustomId('cronogramaHojeId')
          .setLabel('CRONOGRAMA DE HOJE')
          .setStyle(ButtonStyle.Primary),

      );

    const channel = client.channels.cache.get(process.env.CANAL_PRINCIPAL_ID);

    try {
      const fetchedMessages = await channel.messages.fetch();
      await channel.bulkDelete(fetchedMessages);
    } catch (error) {
      console.error('Erro ao excluir mensagens:', error);
    }

    if (channel) {
      await channel.send({ embeds: [embed], components: [row] });
    } else {
      console.error(`${color.bold.red(`[CANAL DE CARGOS NÃO ENCONTRADO]`)}`);
    }
 // ---------------------------------------------------------------------------------//
    console.log(`${color.bold.green(`[STUDY BOT - READY]`)} Logging into Discord...`.yellow);
    console.table({
      "Name": client.user.tag, 
      "Author": `@theghhz`
    });

    console.log(`${color.bold.green(`[READY]`)} ${client.user.tag} is online!`.yellow);

  }
};
