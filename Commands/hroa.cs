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
    public class DescCommands : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task addDesc(params string[] inputs) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(inputs[0]);
                if (desc != null) {
                    List<string> str = inputs.ToList();
                    str.RemoveAt(0);
                    desc.descr += System.Environment.NewLine + System.Environment.NewLine + String.Join(" ", str);
                    description.update_description(desc);
                } else {
                    desc = new description();
                    desc.title = inputs[0];
                    desc.author = Context.User.Id;
                    List<string> str = inputs.ToList();
                    str.RemoveAt(0);
                    desc.descr = String.Join(" ", str);
                    description.insert_description(desc);
                }
                await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("img")]
        public async Task addImg(string name, string link) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.img = link;
                    description.update_description(desc);
                    await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                } else {
                    await ReplyAsync(Context.User.Mention + ", you have to `hs!add 'location' 'Multi word description'` before adding an image link to it.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("delete")]
        public async Task deleteDesc(string name) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    description.delete_description(desc);
                    await ReplyAsync(Context.User.Mention + ", description deleted");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("color")]
        public async Task colorDesc(string name, int red, int green, int blue) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    if ( 0 > red || red > 255 ) {
                        await ReplyAsync(Context.User.Mention + ", the red paramater needs to be between 0 and 255.");
                        return;
                    } else if (0 > green || green > 255 ) {
                        await ReplyAsync(Context.User.Mention + ", the green paramater needs to be between 0 and 255.");
                        return;
                    } else if (0 > blue || blue > 255 ) {
                        await ReplyAsync(Context.User.Mention + ", the blue paramater needs to be between 0 and 255.");
                        return;
                    }
                    desc.rgb[0] = red;
                    desc.rgb[1] = green;
                    desc.rgb[2] = blue;
                    description.update_description(desc);
                    await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("wiki")]
        public async Task wikiDesc(string name, string wiki) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.wikiLink = wiki;
                    description.update_description(desc);
                    await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("desc")]
        public async Task showDesc(string name) {
            description desc = description.get_description(name);
                if (desc != null) {
                    await Context.Channel.SendMessageAsync("", false, desc.toEmbed(Context.Guild));
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
        }

        [Command("list")]
        public async Task showList() {
            List<description> descs = description.get_description();
            List<string> str = new List<string>();
            int count = 8;
            str.Add("```");
            foreach(description d in descs) {
                string toAdd = d.title;
                if (d.descr.Length > 50 ) {
                    toAdd += " - " + d.descr.Substring(0,49) + "...";
                } else {
                    toAdd += " - " + d.descr;
                }
                count += toAdd.Length + 1;
                if ( count >= 2000) { // Handling output condition of 2000 max characters
                    str.Add("```");
                    await ReplyAsync(String.Join(System.Environment.NewLine,str));
                    str = new List<string>();
                    str.Add("```");
                    count = 9 + toAdd.Length;
                }
                str.Add(toAdd);
            }
            str.Add("```");
            await ReplyAsync(String.Join(System.Environment.NewLine,str));
        }

        [Command("help")]
        public async Task helpAsync(params string[] inputs)
        {
            if (inputs.Length == 0) {
                await ReplyAsync("Hi! I'm the " + Context.Client.CurrentUser.Username + " for more information use one of the following phrases after the help command: ```desc , list , color , delete , img , wiki , add```");
            } else {
                switch(inputs[0].ToLower()) {
                case "desc":
                    await ReplyAsync("Usage: `hs!desc [Location]` | Gives the description in a embed of the given location. Fails silently if the location doesn't exist.");
                break;
                case "list":
                    await ReplyAsync("Usage: `hs!list` | Gives a list of all available locations along with the first 50 characters of the description.");
                break;
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
                    await ReplyAsync("**Requires Admin** | Usage: `hs!add [Location] [Multi Word Description] `| If the location doesn't exist a new location is added. If the location exists the text is appended to the end after adding 2 line breaks");
                break;
                case "wiki":
                    await ReplyAsync("**Requires Admin** | Usage: `hs!wiki [Location] [wikiLink] `| Adds a wiki link to the title field of the embed");
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