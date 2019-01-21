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
        public async Task addDesc(params string[] inputs ) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(inputs[0]);
                if (desc != null) {
                    List<string> str = inputs.ToList();
                    str.RemoveAt(0);
                    desc.descr += String.Join(" ", str);
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

        [Command("desc")]
        public async Task showDesc(string name) {
            description desc = description.get_description(name);
                if (desc != null) {
                    await Context.Channel.SendMessageAsync("", false, desc.toEmbed(Context.Guild));
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
        }

        [Command("help")]
        public async Task helpAsync()
        {
            //await Context.User.SendMessageAsync("Please check out this google document for my commands: <https://docs.google.com/document/d/1pWfIToswRCDVpqTK1Bj5Uv6s-n7zpOaqgZHQjW3SNzU/edit?usp=sharing>");
        }

        [Command("lootcrate")]
        public async Task lootCrate() {
            var emote = Context.Client.Guilds.FirstOrDefault(e=>e.Id == 493151648894681108).Emotes.ToList();
            var em = emote.ElementAt(Program.rand.Next(emote.Count));
            await ReplyAsync(Context.User.Mention + " YOU RECEIVE " + em + "    `" + em.Name + "`");
        }
    }
}