using Discord.Commands;
using hroabot.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hroabot.Commands
{
    public class ProphecyCommands : ModuleBase<SocketCommandContext>
    {
        // Administration Commands

        [Command("addProphecy")]
        public async Task addProphecyAsync(params string[] inputs)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                prophecy prop = prophecy.get_prophecy(inputs[0]);

                if (prop == null)
                {
                    prop = new prophecy();
                }

                string prophecyString = "";

                for (int i = prop == null ? 0:1; i < inputs.Length; i++)
                {
                    prophecyString += inputs[i];

                    if(i < inputs.Length - 1)
                    {
                        prophecyString += " ";
                    }
                }

                prophecy.add_prophecy(new prophecy { Text = prophecyString });

                await Context.Channel.SendMessageAsync("Added and/or updated the prophecy.");
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("addSpeaker")]
        public async Task addSpeakerAsync(params string[] body)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                string totalBody = "";

                for (int i = 0; i < body.Length; i++)
                {
                    totalBody += body[i];

                    if(i < body.Length - 1)
                    {
                        totalBody += " ";
                    }
                }

                speaker.add_speaker(new speaker { Name = totalBody });

                await Context.Channel.SendMessageAsync("Created new speaker.");
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("addSpeakerIntro")]
        public async Task addSpeakerIntroAsync(int id, params string[] body)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                string totalBody = "";

                for (int i = 0; i < body.Length; i++)
                {
                    totalBody += body[i];

                    if (i < body.Length - 1)
                    {
                        totalBody += " ";
                    }
                }

                speaker scryer = speaker.get_speaker(id);

                if (scryer != null)
                {
                    scryer.Intros.Add(totalBody);

                    speaker.update_speaker(scryer);

                    await Context.Channel.SendMessageAsync("Added intro to speaker.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + id + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("addSpeakerOutro")]
        public async Task addSpeakerOutroAsync(int id, params string[] body)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {

                string totalBody = "";

                for (int i = 0; i < body.Length; i++)
                {
                    totalBody += body[i];

                    if (i < body.Length - 1)
                    {
                        totalBody += " ";
                    }
                }

                speaker scryer = speaker.get_speaker(id);

                if (scryer != null)
                {
                    scryer.Outros.Add(totalBody);

                    speaker.update_speaker(scryer);

                    await Context.Channel.SendMessageAsync("Added outro to speaker.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + id + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        // User Commands
    }
}
