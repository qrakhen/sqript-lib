using System;
using Qrakhen.Sqript;
using System.Collections.Generic;
using System.IO;

namespace Qrakhen.SqriptLib {

	public class FileInterface : Interface {

		public FileInterface() : base("file") { }

		public QValue Exists(Dictionary<string, QValue> parameters) {
			return new QValue(File.Exists(parameters["file"].Str()), Qrakhen.Sqript.ValueType.Boolean);
		}

		public QValue read(Dictionary<string, QValue> parameters) {
			if (!File.Exists(parameters["file"].Str())) {
				throw new Sqript.Exception("could not find file '" + parameters["file"] + "'");
			} else {
				return new QValue(File.ReadAllText(parameters["file"].Str()), Qrakhen.Sqript.ValueType.String);
			}
		}

		public QValue write(Dictionary<string, QValue> parameters) {
			string content = parameters["content"].GetValue() == null ? "" : parameters["content"].Str();
			File.WriteAllText(parameters["file"].Str(), content);
			return QValue.True;
		}

		public override void Load() {
			Define(new Call(read, new string[] { "file" }, Sqript.ValueType.String, "read"));
			Define(new Call(write, new string[] { "file", "content" }, Sqript.ValueType.Boolean, "write"));
			Define(new Call(Exists, new string[] { "file" }, Sqript.ValueType.Boolean, "exists"));
		}
	}
}
