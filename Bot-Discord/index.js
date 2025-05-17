const cron = require("node-cron");
const crypto = require('crypto');
const axios = require('axios');
const fs = require("node:fs");
const path = require("node:path");
const color = require('colors');
const { Client, Collection, Partials, GatewayIntentBits } = require("discord.js");
require("dotenv").config();

const client = new Client({
  intents: [
    GatewayIntentBits.DirectMessages,
    GatewayIntentBits.Guilds,
    GatewayIntentBits.GuildMembers,
    GatewayIntentBits.GuildMessageReactions,
    GatewayIntentBits.GuildModeration,
    GatewayIntentBits.GuildInvites,
    GatewayIntentBits.GuildPresences,
    GatewayIntentBits.GuildMessages,
    GatewayIntentBits.MessageContent,
  ],
  partials: [
    Partials.Message,
    Partials.Channel,
    Partials.Reaction,
    Partials.GuildMember,
  ],
});

// Collections
client.commands = new Collection();
client.interactions = new Collection();

//Paths
const commandPath = path.join(__dirname, 'commands');
const eventPath = path.join(__dirname, "events");
const foldersPath = path.join(__dirname, "commands");
const commandFiles = fs.readdirSync(foldersPath).filter(file => file.endsWith(".js"));
const interactionPath = path.join(__dirname, "interactions");

//------- COMMANDS HANDLER ------------
console.log(`${color.bold.green('[STUDY BOT]')} Started! Refreshing application commands...`.yellow);

let commandCount = 0;
fs.readdirSync(commandPath)
  .filter(file => file.endsWith('.js'))
  .forEach(file => {
    try {
      const command = require(path.join(commandPath, file));
      if (command.data && command.data.name) {
        client.commands.set(command.data.name, command);
        commandCount++;
        console.log(`Loaded command: ${command.data.name}`);
      } else {
        console.error(`Command file ${file} does not export a valid command object.`);
      }
    } catch (error) {
      console.error(`Failed to load command ${file}:`, error);
    }
  });

console.log(`${color.bold.green('[STUDY BOT - GLOBAL COMMANDS]')} [${commandCount}] commands successfully loaded.`.yellow);

// ---------------- INTERACTION HANDLER -----------------------
console.log(`${color.bold.green('[STUDY BOT - INTERACTIONS]')} Started! Refreshing interactions...`.yellow);

let interactionCount = 0;
fs.readdirSync(interactionPath)
  .filter(file => file.endsWith('.js'))
  .forEach(file => {
    try {
      const interaction = require(path.join(interactionPath, file));
      if (interaction.data && interaction.data.id) {
        client.interactions.set(interaction.data.id, interaction);
        interactionCount++;
        console.log(`Loaded interaction: ${interaction.data.id}`);
      } else {
        console.error(`Interaction file ${file} does not export a valid interaction object.`);
      }
    } catch (error) {
      console.error(`Failed to load interaction ${file}:`, error);
    }
  });

console.log(`${color.bold.green('[STUDY BOT - INTERACTIONS]')} [${interactionCount}] interactions successfully loaded.`.yellow);

//----------------EVENT HANDLER--------------------

console.log(`${color.bold.green('[EVENTS]')} Started refreshing application events...`.yellow);

let eventCount = 0;
fs.readdirSync(eventPath)
  .filter(file => file.endsWith('.js'))
  .forEach(file => {
    try {
      const event = require(path.join(eventPath, file));
      if (event.name) {
        if (event.once) {
          client.once(event.name, (...args) => event.execute(...args));
        } else {
          client.on(event.name, (...args) => event.execute(...args));
        }
        eventCount++;
        console.log(`Loaded event: ${event.name}`);
      } else {
        console.error(`Event file ${file} does not export a valid event object.`);
      }
    } catch (error) {
      console.error(`Failed to load event ${file}:`, error);
    }
  });

console.log(`${color.bold.green('[STUDY BOT - EVENTS]')} [${eventCount}] events successfully loaded.`.yellow);


//------------------ CRON JOB -------------------//
// Exemplo de ID do canal e do usuário
const CHANNEL_ID = "1262838408447787095";
const USER_ID = "171074480691478528";

// Tarefa que roda às 6:30 da manhã
// cron.schedule("30 6 * * *", async () => {
//     const hoje = new Date();
//     const hojeISO = hoje.toISOString().split("T")[0]; // só a parte da data

//     try {
//         const res = await axios.get(`${process.env.API_URL}/Cronograma`);
//         const cronograma = res.data.resultado[0]; // pegue o desejado

//         const diaHoje = cronograma.dias.find(dia =>
//             dia.data.startsWith(hojeISO)
//         );

//         const canal = await client.channels.fetch(CHANNEL_ID);

//         if (!diaHoje) {
//             await canal.send(`<@${USER_ID}> Nenhum conteúdo para hoje.`);
//             return;
//         }

//         const conteudos = diaHoje.conteudos;

//         if (!conteudos.length) {
//             await canal.send(`<@${USER_ID}> Nenhum conteúdo atribuído para hoje.`);
//             return;
//         }

//         const lista = conteudos.map(c => `• ${c.nome} - ${c.descricao}`).join("\n");

//         await canal.send(`<@${USER_ID}> 📚 Conteúdos para hoje:\n${lista}`);
//     } catch (err) {
//         console.error(err.response?.data || err.message);
//     }
// });

client.login(process.env.DISCORD_TOKEN);