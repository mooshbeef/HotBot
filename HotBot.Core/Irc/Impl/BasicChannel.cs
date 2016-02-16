﻿using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannel : Channel
	{
		//TODO: Store this somewhere safe.
		public const string ChannelPrefix = "#";

		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(25)]
		public string Name { get; private set; }

		public BasicChannel(string name) : this()
		{
			try
			{
				Verify.ChannelName(name);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "name", ex);
			}
			Name = name;
		}

		private BasicChannel()
		{
			Id = Guid.NewGuid();
		}

		public override string ToString()
		{
			return ChannelPrefix + Name;
		}
	}
}
