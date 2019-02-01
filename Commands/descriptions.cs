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
        static Boolean showPreview = true;

        [Command("add")]
        public async Task addDesc(params string[] inputs) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(inputs[0]);
                if (desc != null) {
                    List<string> str = inputs.ToList();
                    foreach (string s in str) {
                        if (s.Equals("[b]")) {
                            desc.descr += System.Environment.NewLine;
                        } else {
                            desc.descr += s + " ";
                        }
                    }
                    description.update_description(desc);
                } else {
                    desc = new description();
                    desc.title = inputs[0];
                    List<string> str = inputs.ToList();
                    str.RemoveAt(0);
                    foreach (string s in str) {
                        if (s.Equals("[b]")) {
                            desc.descr += System.Environment.NewLine;
                        } else {
                            desc.descr += s + " ";
                        }
                    }
                    description.insert_description(desc);
                }
                if (showPreview) { await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild)); }
                else await Context.Channel.SendMessageAsync("Description either updated or created.");
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("edit")]
        public async Task doEdit(params string[] inputs) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(inputs[0]);
                if (desc != null) {
                    desc.descr = "";
                    List<string> str = inputs.ToList();
                    foreach (string s in str) {
                        if (s.Equals("[b]")) {
                            desc.descr += System.Environment.NewLine;
                        } else {
                            desc.descr += s + " ";
                        }
                    }
                    description.update_description(desc);
                }
                if (showPreview) { await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild)); }
                else await Context.Channel.SendMessageAsync("Description either updated or created.");
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
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Image added to description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", you have to `hs!add 'location' 'Multi word description'` before adding an image link to it.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("thumbnail")]
        public async Task addThumbnail(string name, string link) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.thumbnail = link;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Image added to description");
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
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Color changed on the description");
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
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Wiki link added to description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of '" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("author")]
        public async Task addAuthor(string name, string author) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.author = author;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Author information updated on description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of `" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("authImg")]
        public async Task addAuthImg(string name, string authImg) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.authImg = authImg;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Author Img information updated on description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of `" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("authUrl")]
        public async Task addAuthUrl(string name, string authUrl) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.authUrl = authUrl;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Author Url information updated on description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of `" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("footer")]
        public async Task addfooter(string name, string footer) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.footer = footer;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Footer information updated on description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of `" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("footerImg")]
        public async Task addfooterImg(string name, string footerImg) {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                description desc = description.get_description(name);
                if (desc != null) {
                    desc.footerImg = footerImg;
                    description.update_description(desc);
                    if (showPreview) await Context.Channel.SendMessageAsync("Here is the description you made: ", false, desc.toEmbed(Context.Guild));
                    else await Context.Channel.SendMessageAsync("Footer Img information updated on description");
                } else {
                    await ReplyAsync(Context.User.Mention + ", a description under the title of `" + name +"` does not exist.");
                }
            } else {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("preview")]
        public async Task togglePreview() {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id))) {
                showPreview = !showPreview;
                await Context.Channel.SendMessageAsync("Preview toggled to: " + showPreview);
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
                    toAdd += " - " + d.descr.Substring(0,47) + "...";
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

    }
}