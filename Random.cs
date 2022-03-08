using System;
using System.Collections.Generic;
using Qrakhen.Sqript;

namespace Qrakhen.SqriptLib {

	public class RandomInterface : Interface {

		private Random _random;

		public RandomInterface() : base("random") {
			_random = new Random();
		}

		public QValue set_seed(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("seed")) {
				throw new ArgumentException("The needed parameter 'seed' is missing!");
			}
			_random = new Random(ToInt(parameters, "seed"));
			return null;
		}

		public QValue range(Dictionary<string, QValue> parameters) {
			if(!parameters.ContainsKey("min")) {
				throw new ArgumentException("The needed parameter 'min' is missing!");
			}
			if(!parameters.ContainsKey("max")) {
				throw new ArgumentException("The needed parameter 'max' is missing!");
			}
			int min = ToInt(parameters, "min");
			int max = ToInt(parameters, "max");
			return new QValue(
				_random.Next(min, max + 1),
				Sqript.ValueType.Integer
			);
		}

		public QValue rangeD() {
			return new QValue(
				_random.NextDouble(),
				Sqript.ValueType.Decimal
			);
		}

		public override void Load() {
			Define(new Call(set_seed, new string[] { "seed" }, Sqript.ValueType.Null));
			Define(new Call(range, new string[] { "min", "max" }, Sqript.ValueType.Integer));
			Define(new Call(rangeD, Sqript.ValueType.Decimal));
		}

		private int ToInt(Dictionary<string, QValue> parameters, string name) {
			if(parameters[name].Type != Sqript.ValueType.Integer
				&& parameters[name].Type != Sqript.ValueType.Decimal
				&& parameters[name].Type != Sqript.ValueType.Number
				&& parameters[name].Type != Sqript.ValueType.Any
				&& parameters[name].Type != Sqript.ValueType.String){
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Integer' but it is: " + parameters[name].Type);
			}
			try {
				return int.Parse(parameters[name].Value.ToString());
			} catch(FormatException) {
				throw new ArgumentException("The parameter '" + name + "' should have they type 'Integer' but it is: " + parameters[name].Type);
			}
		}

		private decimal ToDecimal(Dictionary<string, QValue> parameters, string name) {
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
	}
}
