﻿using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Core
{
	public class DbContextDataStore : DbContext, DataStore, MessageHandler<SaveChangesNotificationArgs>
	{
		public MessageBus Bus { get; }

		public DbSet<User> Users { get; set; }

		public DbSet<Channel> Channels { get; set; }

		static DbContextDataStore()
		{
			var strategy = new DropCreateDatabaseIfModelChanges<DbContextDataStore>();
			Database.SetInitializer(strategy);
		}

		public DbContextDataStore(MessageBus bus) : base()
		{
			//TODO: Do this in a different thread
			Database.Initialize(false);
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			Bus.Subscribe(this);
		}

		public void Initialize()
		{
			Database.Connection.Open();
		}

		void MessageHandler<SaveChangesNotificationArgs>.HandleMessage(SaveChangesNotificationArgs message)
		{
			SaveChanges();
		}
	}
}