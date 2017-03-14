using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Commands;

namespace LethBot2._0
{
    public class CommandsLibrary
    {
        private DiscordClient discord;
        private List<string> categoriesList;
        private bool nextRound;
        private bool gameWon;
        private Dictionary<string, int> userScores;
        private IEnumerable<User> users;
        public CommandsLibrary(DiscordClient discord)
        {
            this.discord = discord;
            categoriesList = new List<string>
            {
                "2537 : Brand Names",
                "16290 : Finctional Females",
                "223 : Word Origins",
                "10068 : Colorful Films",
                "1024 : Historical Quotes"
            };
            users = new List<User>();
            userScores = new Dictionary<string, int>();
    }

        public void CreateCommandService()
        {
            var commands = discord.GetService<CommandService>();
            
                 commands.CreateCommand("hello") //test command
                .Alias(new string[] { "hi", "hey" }) //add 2 aliases
                .Description("Greets a person.") //add description, it will be shown when !help is used
                .Parameter("GreetedPerson", ParameterType.Required) //as an argument, we have a person we want to greet
                .Do(async e =>
                {
                    await e.Channel.SendMessage($"{e.User.Name} greets {e.GetArg("GreetedPerson")}");
                    //sends a message to channel with the given text
                });
            /*commands.CreateCommand("newUser")
                .Description("Create a new user for the points DB")
                .Parameter("username", ParameterType.Required)
                .Do(async e =>
                {
                    DbConnect db = new DbConnect();
                    db.Insert(e.GetArg(0).ToString());
                    await e.Channel.SendMessage("User succesfully added to DB");
                });*/

            commands.CreateCommand("categories")
                .Description("Provides a list of categories with their ID.") //jeopardy
                .Do(async e =>
                    {
                    string categories = "";
                    foreach (var variable in categoriesList)
                    {
                        categories += variable + "\n";
                    }
                        await e.Channel.SendMessage(categories);
                    });
            commands.CreateCommand("cahReset")
                .Description("Resets the scores for CaH.")
                .Do(async e =>
                {
                    gameWon = false;
                    userScores.Clear();
                    await e.Channel.SendMessage("User scores for the current game of CaH has been reset.");
                });
            commands.CreateCommand("cahScores")
                .Description("Shows the user scores for the current game.")
                .Do(async e =>
                {
                    string listOfScores = "";
                    foreach (var user in userScores)
                    {
                        listOfScores += user.Key + ": " + user.Value + " points\n";
                    }
                    await e.Channel.SendMessage(listOfScores);
                });

            commands.CreateCommand("cah")
                    .Description("Returns a random black card from CaH")
                    .Do(async e =>
                    {
                        nextRound = false;
                        BlackCard bc = new BlackCard();
                        bc.LoadJson();
                        await e.Channel.SendMessage(bc.GetBlackCard().text);

                        discord.MessageReceived += async (s, f) => {
                            if (f.Message.Text.Contains("!win") && nextRound == false)
                            {
                                IEnumerable<User> winner = f.Message.MentionedUsers;
                                string userName = winner.ToArray()[0].Name;

                                if(userScores.ContainsKey(userName)) //if user exists in list
                                {
                                    userScores[userName] += 1; //increment score for user
                                }
                                else
                                {
                                    userScores.Add(userName, 1); //add new user with default score
                                }

                                foreach (var variable in userScores)
                                {
                                    if (variable.Value >= 7) //when user has 7 points
                                    {
                                        await e.Channel.SendMessage("Congratulations! " + variable.Key +
                                                                    " won the game!\nUse !cahReset to reset the game.\n");

                                        string listOfScores = "";
                                        foreach (var user in userScores)
                                        {
                                            listOfScores += user.Key + ": " + user.Value + " points\n";
                                        }
                                        await e.Channel.SendMessage(listOfScores);

                                        gameWon = true; //avoid printing current round score
                                        break; //stop loop when reaching user with 7 points
                                    }
                                }
                                nextRound = true; //avoid multiple event triggers during same round
                                if (gameWon == false)
                                {
                                    await f.Channel.SendMessage("Congratulations! " + winner.ToArray()[0].Name + " won this round.\n"
                                    + userName + " now has " + userScores[userName] + " points.");
                                }
                            }
                        };
                    });

            commands.CreateCommand("trivia")
                    .Alias(new string[] { "question"}) 
                    .Description("Provides a random question.")
                    .Parameter("categoryId", ParameterType.Required)
                    .Parameter("value", ParameterType.Required) //100,200,400 etc
                    .Do(async e =>
                    {
                        JsonQuestion question = new JsonQuestion(e.GetArg(0), Convert.ToInt32(e.GetArg(1)));
                        DataContainer container = question.GetQuestions();
                        int listLength = container.Questions.Count;
                        Random r = new Random();
                        int rand = r.Next(0, listLength); //get random question
                        await e.Channel.SendMessage(container.Questions[rand].question);

                        discord.MessageReceived += async (s, f) => {
                            // container.Questions[rand].answer
                            if (f.Message.Text == container.Questions[rand].answer) //if right answer
                                await f.Channel.SendMessage("Congratulations " + f.Message.User + "!");
                        };
                    });
        }
    }
}
