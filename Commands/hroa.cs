using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using hroabot.Classes;
using RestSharp;

namespace hroabot.Commands
{
    public class defaultCommands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task helpAsync(params string[] inputs)
        {
            if (inputs.Length == 0) {
                await ReplyAsync("Hi! I'm the " + Context.Client.CurrentUser.Username + " for more information use one of the following phrases after the help command: ```desc, list, edit, thumbnail, author, authImg, authUrl, footer, footerImg, preview, color, delete, img, wiki, add```");
            } else {
                switch(inputs[0].ToLower()) {
                case "desc":
                    await ReplyAsync("Usage: `hs!desc [Location]` | Gives the description in a embed of the given location. Fails silently if the location doesn't exist.");
                break;
                case "list":
                    await ReplyAsync("Usage: `hs!list` | Gives a list of all available locations along with the first 50 characters of the description.");
                break;
                /*
                case "audio":
                    await ReplyAsync("Usage: `hs!audio [Location]` | Joins the VC of the user and plays the audio for a given location.");
                break; 
                // */
                case "color":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!color [Location] [0-255] [0-255] [0-255]` | Sets the embed color of the given location in red, blue, green 0-255 values.");
                break;
                case "delete":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!delete [Location]` | Removes the location from the database.");
                break;
                case "img":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!img [Location] [Img Link]` | Adds the img link to the given location.");
                break;
                case "add":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!add [Location] [Multi Word Description]` | If the location doesn't exist a new location is added. If the location exists the text is appended to the end after adding 2 line breaks");
                break;
                case "wiki":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!wiki [Location] [wikiLink]` | Adds a wiki link to the title field of the embed");
                break;
                case "edit":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!edit [Location] [Multi Word Description]` | Replaces the description text of given location");
                break;
                case "thumbnail":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!thumbnail [Location] [link]` | Adds or replaces the thumbnail img. Please provide a URL");
                break;
                case "author":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!edit [Location] \"Author Name\"` | Set the Author name");
                break;
                case "authorimg":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!authorImg [Location] [link]` | Adds or replaces the author img. Please provide a URL");
                break;
                case "authorurl":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!authorUrl [Location] [link]` | Adds or replaces the author url destination. Please provide a URL");
                break;
                case "footer":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!footer [Location] \"Footer in Multiwords\"` | Adds a footer message to the description");
                break;
                case "footerimg":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!footerimg [Location] [link]` | Adds or replaces the footer img. Please provide a URL");
                break;
                case "preview":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!preview` | Toggles the preview mode");
                break;
                case "audiotoggle":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!audioToggle` | Toggles the audio availability");
                break;
                }
            }
        }

        [Command("lootcrate")]
        public async Task lootCrate() {
            var emote = Context.Client.Guilds.FirstOrDefault(e=>e.Id == 493151648894681108).Emotes.ToList();
            var em = emote.ElementAt(Program.rand.Next(emote.Count));
            await ReplyAsync(Context.User.Mention + " YOU RECEIVE " + em + "    `" + em.Name + "`");
        }
    }
}