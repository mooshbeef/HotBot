﻿using System;
using System.Linq;
using HotBot.Core.Services.DataStorage;

namespace HotBot.Core.Services.Irc
{
	public class SendChatMessage : SendIrcMessage
	{
		public string Text { get; }
		public Channel Channel { get; }

		public SendChatMessage(Channel channel, string chatMessage) : base(GenerateCommand(channel, chatMessage))
		{
			Text = chatMessage;
			Channel = channel;
		}

		private static string GenerateCommand(Channel channel, string chatMessage)
		{
			return "PRIVMSG #" + channel.Name + " :" + chatMessage;
		}
	}
}