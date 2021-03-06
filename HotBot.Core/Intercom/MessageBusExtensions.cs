﻿using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	public static class MessageBusExtensions
	{
		/// <summary>
		/// Extension for using generics with the dataType argument.
		/// </summary>
		/// <typeparam name="TEvent">The type of data to publish.</typeparam>
		/// <param name="bus"></param>
		/// <param name="instance">The instance to be published.</param>
		public static void PublishSpecific<TEvent>(this MessageBus bus, TEvent instance)
		{
			Verify.NotNull(bus, "bus");
			Verify.NotNull(instance, "instance");
			bus.Publish(instance);
		}
	}
}