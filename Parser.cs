using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Qrakhen.Sqript;

namespace Qrakhen.SqriptLib {

	public class ParserInterface : Interface {

		public ParserInterface() : base("parser") { }

		public override void Load() {
			Define(new Call(toNumber, new string[] { "value" }, Sqript.ValueType.Number));
			Define(new Call(toInt, new string[] { "value" }, Sqript.ValueType.Number));
			Define(new Call(toDecimal, new string[] { "value" }, Sqript.ValueType.Number));
			Define(new Call(toBool, new string[] { "value" }, Sqript.ValueType.Number));
		}


		public QValue toNumber(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("value")) {
				throw new ArgumentException("The needed parameter 'min' is missing!");
			}
			decimal number = toDecimal(parameters, "value");
			return new QValue(
				number,
				Sqript.ValueType.Number
			);
		}

		public QValue toInt(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("value")) {
				throw new ArgumentException("The needed parameter 'min' is missing!");
			}
			int number = toInt(parameters, "value");
			return new QValue(
				number,
				Sqript.ValueType.Integer
			);
		}

		public QValue toDecimal(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("value")) {
				throw new ArgumentException("The needed parameter 'min' is missing!");
			}
			double number = toDouble(parameters, "value");
			return new QValue(
				number,
				Sqript.ValueType.Decimal
			);
		}

		public QValue toBool(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("value")) {
				throw new ArgumentException("The needed parameter 'min' is missing!");
			}
			bool number = toBool(parameters, "value");
			return new QValue(
				number,
				Sqript.ValueType.Boolean
			);
		}

		#region Helper

		private decimal toDecimal(Dictionary<string, QValue> parameters, string name) {
			if(parameters[name].Type != Sqript.ValueType.Integer
				&& parameters[name].Type != Sqript.ValueType.Decimal
				&& parameters[name].Type != Sqript.ValueType.Number
				&& parameters[name].Type != Sqript.ValueType.Any
				&& parameters[name].Type != Sqript.ValueType.String) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Decimal' but it is: " + parameters[name].Type);
			}
			try {
				return decimal.Parse(parameters[name].Value.ToString());
			} catch(FormatException) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Decimal' but it is: " + parameters[name].Type);
			}
		}

		private int toInt(Dictionary<string, QValue> parameters, string name) {
			if(parameters[name].Type != Sqript.ValueType.Integer
				&& parameters[name].Type != Sqript.ValueType.Decimal
				&& parameters[name].Type != Sqript.ValueType.Number
				&& parameters[name].Type != Sqript.ValueType.Any
				&& parameters[name].Type != Sqript.ValueType.String) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Integer' but it is: " + parameters[name].Type);
			}
			try {
				return int.Parse(parameters[name].Value.ToString());
			} catch(FormatException) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Integer' but it is: " + parameters[name].Type);
			}
		}

		private double toDouble(Dictionary<string, QValue> parameters, string name) {
			if(parameters[name].Type != Sqript.ValueType.Integer
				&& parameters[name].Type != Sqript.ValueType.Decimal
				&& parameters[name].Type != Sqript.ValueType.Number
				&& parameters[name].Type != Sqript.ValueType.Any
				&& parameters[name].Type != Sqript.ValueType.String) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Double' but it is: " + parameters[name].Type);
			}
			try {
				return double.Parse(parameters[name].Value.ToString());
			} catch(FormatException) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Double' but it is: " + parameters[name].Type);
			}
		}

		private bool toBool(Dictionary<string, QValue> parameters, string name) {
			if(parameters[name].Type != Sqript.ValueType.Integer
				&& parameters[name].Type != Sqript.ValueType.Decimal
				&& parameters[name].Type != Sqript.ValueType.Number
				&& parameters[name].Type != Sqript.ValueType.Any
				&& parameters[name].Type != Sqript.ValueType.String) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Boolean' but it is: " + parameters[name].Type);
			}
			try {
				return bool.Parse(parameters[name].Value.ToString());
			} catch(FormatException) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Boolean' but it is: " + parameters[name].Type);
			}
		}

		#endregion
	}
}
