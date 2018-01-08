using System;
using Qrakhen.Sqript;
using System.Collections.Generic;
using System.IO;

namespace Qrakhen.SqriptLib
{
    public class FileInterface : Interface
    {
        public FileInterface() : base("file") {

        }

        public Value exists(Dictionary<string, Value> parameters) {
            return new Value(File.Exists(parameters["file"].str()), Qrakhen.Sqript.ValueType.BOOLEAN);
        }

        public Value read(Dictionary<string, Value> parameters) {
            if (!File.Exists(parameters["file"].str())) throw new Qrakhen.Sqript.Exception("could not find file '" + parameters["file"] + "'");
            else return new Value(File.ReadAllText(parameters["file"].str()), Qrakhen.Sqript.ValueType.STRING);
        }

        public Value write(Dictionary<string, Value> parameters) {
            string content;
            if (parameters["content"].getValue() == null) content = "";
            else content = parameters["content"].str();
            File.WriteAllText(parameters["file"].str(), content);
            return Value.TRUE;
        }

        public override void load() {
            define(new Call("read", new string[] { "file" }, read, Sqript.ValueType.STRING));
            define(new Call("write", new string[] { "file", "content" }, write, Sqript.ValueType.BOOLEAN));
            define(new Call("exists", new string[] { "file" }, exists, Sqript.ValueType.BOOLEAN));
        }
    }
}
