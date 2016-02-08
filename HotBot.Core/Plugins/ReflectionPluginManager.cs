﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public class ReflectionPluginManager : PluginManager
	{
		private Dictionary<string, Plugin> _namedPlugins = new Dictionary<string, Plugin>();
		private Dictionary<Type, Plugin> _typedPlugins = new Dictionary<Type, Plugin>();
		private object _stateLock = new object();

		public IUnityContainer Container { get; private set; }

		public ReflectionPluginManager(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			Container = container;
		}

		public void AddPlugin(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			if (plugin.Description == null)
			{
				throw new ArgumentException("Plugin.Description cannot be null", "plugin");
			}
			Type pluginType = plugin.GetType();
			string pluginName = plugin.Description.Name;
			if (_typedPlugins.ContainsKey(pluginType))
			{
				throw new InvalidOperationException($"A plugin by the type '{pluginType.FullName}' has already been added");
			}
			if (_namedPlugins.ContainsKey(pluginName))
			{
				throw new InvalidOperationException($"Another plugin by the name '{pluginName}' has already been added. It is of the type '{pluginType.FullName}");
			}
			_typedPlugins.Add(pluginType, plugin);
			_namedPlugins.Add(pluginName, plugin);
		}

		public void RemovePlugin(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			if (plugin.Description == null)
			{
				throw new ArgumentException("Plugin.Description cannot be null", "plugin");
			}
			Type pluginType = plugin.GetType();
			string pluginName = plugin.Description.Name;
			if (_typedPlugins.ContainsKey(pluginType))
			{
				_typedPlugins.Remove(pluginType);
				_namedPlugins.Remove(pluginName);
			}
		}

		public Plugin GetPlugin(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("cannot be empty", "name");
			}
			if (!_namedPlugins.ContainsKey(name))
			{
				return null;
			}
			return _namedPlugins[name];
		}

		public Plugin GetPlugin(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!_typedPlugins.ContainsKey(type))
			{
				return null;
			}
			return _typedPlugins[type];
		}

		public void LoadAll()
		{
			foreach (Plugin pl in _typedPlugins.Values)
			{
				pl.Load(Container);//TODO: try-catch loading of plugins
			}
		}

		public void UnloadAll()
		{
			foreach (Plugin pl in _typedPlugins.Values)
			{
				pl.Unload();//TODO: try-catch unloading of plugins
			}
		}

		public void Reload()
		{
			LoadAll();
			UnloadAll();
		}
	}
}