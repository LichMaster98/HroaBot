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

namespace trilhroalbot.Commands
{
    public class hroa : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task addDesc(params string[] inputs ) {
            if (description.isDescriptionManager(Context.User)) {
                description desc = description.get_description(inputs[0]);
                if (desc != null) {
                    List<string> str = inputs.ToList().RemoveAt(0);
                    desc.description += String.Join(" ", str);
                } else {
                    desc = new description();
                    desc.title = inputs[0];
                    desc.author = Context.User.Id;
                    List<string> str = inputs.ToList().RemoveAt(0);
                    desc.description = String.Join(" ", str);
                }
                Context.Channel.SendMessageAsync();
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("img")]

        [Command("help")]
        public async Task helpAsync()
        {
            await Context.User.SendMessageAsync("Please check out this google document for my commands: <https://docs.google.com/document/d/1pWfIToswRCDVpqTK1Bj5Uv6s-n7zpOaqgZHQjW3SNzU/edit?usp=sharing>");
        }

        [Command("lootcrate")]
        public async Task lootCrate() {
            var emote = Context.Client.Guilds.FirstOrDefault(e=>e.Id == 493151648894681108).Emotes.ToList();
            var em = emote.ElementAt(Program.rand.Next(emote.Count));
            await ReplyAsync(Context.User.Mention + " YOU RECEIVE " + em + "    `" + em.Name + "`");
        }
    }
}