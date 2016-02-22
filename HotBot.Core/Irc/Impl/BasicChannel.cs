﻿using HotBot.Core.Intercom;
using HotBot.Core.Unity;
using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	[RegisterFor(typeof(Channel))]
	public sealed class BasicChannel : Channel
	{
		private List<User> _activeUsers = new List<User>();

		public IrcConnection Connection { get; }
		public TwitchConnector Connector { get; }
		public MessageBus Bus { get; }
		public string Name { get; }

		public IReadOnlyCollection<User> ActiveUsers { get; }

		public BasicChannel(TwitchConnector connector, IrcConnection connection, MessageBus bus, string channelName)
		{
			Verify.NotNull(connector, "connector");
			Verify.NotNull(connection, "connection");
			Verify.NotNull(bus, "bus");
			Verify.NotNull(channelName, "channelName");
			ActiveUsers = new ReadOnlyCollection<User>(_activeUsers);
			Connector = connector;
			Connector.Decoder.ChatReceived += Decoder_ChatReceived;
			Connection = connection;
			Name = channelName;
			Bus = bus;
		}

		private void Decoder_ChatReceived(object sender, ChatEventArgs e)
		{
			if (e.Channel == this)
			{
				ChatReceived?.Invoke(this, e);
			}
		}

		public event EventHandler<ChatEventArgs> ChatReceived;

		public event EventHandler<UserChannelEventArgs> UserJoined;

		public event EventHandler<UserChannelEventArgs> UserLeft;

		public void Join()
		{
			Connection.SendCommand($"JOIN #{Name}");
		}

		public void Leave()
		{
			Connection.SendCommand($"PART #{Name}");
		}

		public void Say(string message)
		{
			Connection.SendCommand($"PRIVMSG #{Name} :{message}");
		}
	}
}