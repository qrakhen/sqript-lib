﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Qrakhen.Sqript;

namespace Qrakhen.SqriptLib {

	public class ConsoleInterface : Interface {

		private ConsoleColor _consoleColor = ConsoleColor.White;


		public ConsoleInterface() : base("console") { }

		public override void Load() {
			Define(new Call(write, new string[] { "value", "color" }, Sqript.ValueType.Null));
			Define(new Call(writeln, new string[] { "value", "color" }, Sqript.ValueType.Null));
			Define(new Call(readKey, Sqript.ValueType.Integer));
			Define(new Call(read, Sqript.ValueType.String));
			Define(new Call(setColor, new string[] { "color" }, Sqript.ValueType.Null));
		}


		public QValue setColor(Dictionary<string, QValue> parameters) {
			object value = parameters["color"].GetValue();
			if (value is string colorString) {
				_consoleColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), colorString, true);
			} else if (value is int colorInt) {
				_consoleColor = (ConsoleColor) colorInt;
			} else {
				throw new ArgumentException($"The parameter 'color' needs to be an {typeof(int)} or {typeof(string)}!");
			}
			return null;
		}

		public QValue write(Dictionary<string, QValue> parameters) {
			if(parameters.ContainsKey("color") && parameters["color"] != null) {
				ConsoleColor oldColor = Console.ForegroundColor;
				object value = parameters["color"].GetValue();
				if (value is string colorString) {
					Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), colorString, true);
				} else if (value is int colorInt) {
					Console.ForegroundColor = (ConsoleColor) colorInt;
				} else {
					throw new ArgumentException($"The parameter 'color' needs to be an {typeof(int)} or {typeof(string)}!");
				}
				string text = parameters["value"].GetValue().ToString();
				Console.Write(Regex.Unescape(text));
				Console.ForegroundColor = oldColor;
			} else {
				Console.ForegroundColor = _consoleColor;
				string text = parameters["value"].GetValue().ToString();
				Console.Write(Regex.Unescape(text));
			}
			return null;
		}

		public QValue writeln(Dictionary<string, QValue> parameters) {
			if (parameters.ContainsKey("color") && parameters["color"] != null) {
				ConsoleColor oldColor = Console.ForegroundColor;
				object value = parameters["color"].GetValue();
				if (value is string colorString) {
					Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), colorString, true);
				} else if (value is int colorInt) {
					Console.ForegroundColor = (ConsoleColor) colorInt;
				} else {
					throw new ArgumentException($"The parameter 'color' needs to be an {typeof(int)} or {typeof(string)}!");
				}
				string text = parameters["value"].GetValue().ToString();
				Console.Write(Regex.Unescape(text) + Environment.NewLine);
				Console.ForegroundColor = oldColor;
			} else {
				Console.ForegroundColor = _consoleColor;
				string text = parameters["value"].GetValue().ToString();
				Console.Write(Regex.Unescape(text) + Environment.NewLine);
			}
			return null;
		}

		public QValue readKey() {
			var key = Console.ReadKey();
			return new QValue((int) key.KeyChar, Sqript.ValueType.Integer);
		}

		public QValue read() {
			var line = Console.ReadLine();
			return new QValue(line, Sqript.ValueType.String);
		}
	}
}
