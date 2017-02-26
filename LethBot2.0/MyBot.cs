﻿using System;
using Discord;
using Discord.Commands;
namespace LethBot2._0
{
    public class MyBot
    {
        private readonly DiscordClient _discord;
        private CommandsLibrary cm;
        public MyBot()
        {
            _discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            cm = new CommandsLibrary(_discord);
            cm.CreateCommandService(); //start command service

            _discord.ExecuteAndWait(async () =>
            {
                await _discord.Connect("Mjg0Njc2MzM2ODc3MDQzNzEy.C5HHZQ.PQuBiJ0OMnp4QVC3pt1SIwZelFc", TokenType.Bot);
            });
            

        }//ctor end

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}