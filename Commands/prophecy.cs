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
                string prophecyString = "";

                for (int i = 0; i < inputs.Length; i++)
                {
                    prophecyString += inputs[i];

                    if(i < inputs.Length - 1)
                    {
                        prophecyString += " ";
                    }
                }

                prophecy.add_prophecy(new prophecy { Text = prophecyString });

                await Context.Channel.SendMessageAsync("Added the prophecy.");
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

        [Command("listSpeakers")]
        public async Task listSpeakersAsync()
        {
            if(description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                List<speaker> allSpeakers = speaker.get_all_speakers();

                if (allSpeakers.Count == 0)
                {
                    await Context.Channel.SendMessageAsync("No speakers found.");
                }
                else
                {
                    string workingMessage = "";

                    for (int i = 0; i < allSpeakers.Count; i++)
                    {
                        workingMessage += string.Format("%s - %s \n", allSpeakers[i].ID, allSpeakers[i].Name);

                        if((i + 1) % 20 == 0)
                        {
                            await Context.Channel.SendMessageAsync(workingMessage);
                            workingMessage = "";
                        }
                    }

                    if(!workingMessage.Equals(""))
                    {
                        await Context.Channel.SendMessageAsync(workingMessage);
                    }
                }
            }
        }

        [Command("listProphecies")]
        public async Task listPropheciesAsync()
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                List<prophecy> allProphecies = prophecy.get_all_prophecies();

                if (allProphecies.Count == 0)
                {
                    await Context.Channel.SendMessageAsync("No prophecies found.");
                }
                else
                {
                    string workingMessage = "";

                    for (int i = 0; i < allProphecies.Count; i++)
                    {

                        string prophecyPart = "";
                        if (allProphecies[i].Text.Length > 32)
                        {
                            prophecyPart = allProphecies[i].Text.Substring(0, 32) + "...";
                        }
                        else
                        {
                            prophecyPart = allProphecies[i].Text;
                        }

                        workingMessage += string.Format("%s - %s \n", allProphecies[i].ID, prophecyPart);

                        if ((i + 1) % 20 == 0)
                        {
                            await Context.Channel.SendMessageAsync(workingMessage);
                            workingMessage = "";
                        }
                    }

                    if (!workingMessage.Equals(""))
                    {
                        await Context.Channel.SendMessageAsync(workingMessage);
                    }
                }
            }
        }

        [Command("listSpeakerIntros")]
        public async Task listSpeakerIntrosAsync(int id)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker speaker = speaker.get_speaker(id);

                if(speaker != null)
                {
                    if (speaker.Intros.Count == 0)
                    {
                        await Context.Channel.SendMessageAsync("No speaker intros found.");
                    }
                    else
                    {
                        string workingMessage = "";

                        for (int i = 0; i < speaker.Intros.Count; i++)
                        {
                            string introPart = "";
                            if(speaker.Intros[i].Length > 32)
                            {
                                introPart = speaker.Intros[i].Substring(0, 32) + "...";
                            }
                            else
                            {
                                introPart = speaker.Intros[i];
                            }

                            workingMessage += string.Format("%s - %s... \n", i, introPart);

                            if ((i + 1) % 20 == 0)
                            {
                                await Context.Channel.SendMessageAsync(workingMessage);
                                workingMessage = "";
                            }
                        }

                        if (!workingMessage.Equals(""))
                        {
                            await Context.Channel.SendMessageAsync(workingMessage);
                        }
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + id + " does not exist.");
                }
            }
        }

        [Command("listSpeakerOutros")]
        public async Task listSpeakerOutrosAsync(int id)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker speaker = speaker.get_speaker(id);

                if (speaker != null)
                {
                    if (speaker.Outros.Count == 0)
                    {
                        await Context.Channel.SendMessageAsync("No speaker outros found.");
                    }
                    else
                    {
                        string workingMessage = "";

                        for (int i = 0; i < speaker.Outros.Count; i++)
                        {
                            string outroPart = "";
                            if (speaker.Outros[i].Length > 32)
                            {
                                outroPart = speaker.Outros[i].Substring(0, 32) + "...";
                            }
                            else
                            {
                                outroPart = speaker.Outros[i];
                            }

                            workingMessage += string.Format("%s - %s... \n", i, outroPart);

                            if ((i + 1) % 20 == 0)
                            {
                                await Context.Channel.SendMessageAsync(workingMessage);
                                workingMessage = "";
                            }
                        }

                        if (!workingMessage.Equals(""))
                        {
                            await Context.Channel.SendMessageAsync(workingMessage);
                        }
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + id + " does not exist.");
                }
            }
        }

        [Command("printSpeakerIntro")]
        public async Task printSpeakerIntroAsync(int speakerId, int introIndex)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker scryer = speaker.get_speaker(speakerId);

                if (scryer != null)
                {
                    if (scryer.Intros.Count > introIndex && introIndex >= 0)
                    {
                        await Context.Channel.SendMessageAsync(scryer.Intros[introIndex]);
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("Index of intro is out of bounds.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("printSpeakerOutro")]
        public async Task printSpeakerOutroAsync(int speakerId, int outroIndex)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker scryer = speaker.get_speaker(speakerId);

                if (scryer != null)
                {
                    if (scryer.Outros.Count > outroIndex && outroIndex >= 0)
                    {
                        await Context.Channel.SendMessageAsync(scryer.Outros[outroIndex]);
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("Index of outro is out of bounds.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("printProphecy")]
        public async Task printProphecy(int prophecyId)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                prophecy aProphecy = prophecy.get_prophecy(prophecyId);

                if (aProphecy != null)
                {
                    await Context.Channel.SendMessageAsync(aProphecy.Text);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Prophecy with ID: " + prophecyId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("updateSpeakerName")]
        public async Task updateSpeakerNameAsync(int speakerId, params string[] inputs)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                string newName = "";

                for (int i = 0; i < inputs.Length; i++)
                {
                    newName += inputs[i];

                    if (i < inputs.Length - 1)
                    {
                        newName += " ";
                    }
                }

                speaker scryer = speaker.get_speaker(speakerId);

                if(scryer != null)
                {
                    scryer.Name = newName;

                    speaker.update_speaker(scryer);

                    await Context.Channel.SendMessageAsync("Updated speaker's name.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("deleteSpeaker")]
        public async Task deleteSpeakerAsync(int speakerId)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {

                speaker scryer = speaker.get_speaker(speakerId);

                if (scryer != null)
                {
                    speaker.delete_speaker(scryer);
                    await Context.Channel.SendMessageAsync("Speaker deleted.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("deleteProphecy")]
        public async Task deleteProphecyAsync(int prophecyId)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {

                prophecy prop = prophecy.get_prophecy(prophecyId);

                if (prop != null)
                {
                    prophecy.delete_prophecy(prop);
                    await Context.Channel.SendMessageAsync("Prophecy deleted.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Prophecy with ID: " + prophecyId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("deleteSpeakerIntro")]
        public async Task deleteSpeakerIntroAsync(int speakerId, int introIndex)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker scryer = speaker.get_speaker(speakerId);

                if (scryer != null)
                {
                    if (scryer.Intros.Count > introIndex && introIndex >= 0)
                    {
                        scryer.Intros.RemoveAt(introIndex);
                        speaker.update_speaker(scryer);
                        await Context.Channel.SendMessageAsync("Intro deleted.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("Index of intro is out of bounds.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
                }
            }
            else
            {
                //ReplyAsync(Context.User.Mention + ", you don't have access to add a description");
            }
        }

        [Command("deleteSpeakerOutro")]
        public async Task deleteSpeakerOutroAsync(int speakerId, int outroIndex)
        {
            if (description.isDescriptionManager(Context.Guild.GetUser(Context.User.Id)))
            {
                speaker scryer = speaker.get_speaker(speakerId);

                if (scryer != null)
                {
                    if (scryer.Outros.Count > outroIndex && outroIndex >= 0)
                    {
                        scryer.Outros.RemoveAt(outroIndex);
                        speaker.update_speaker(scryer);
                        await Context.Channel.SendMessageAsync("Outro deleted.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("Index of outro is out of bounds.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Speaker with ID: " + speakerId + " does not exist.");
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
