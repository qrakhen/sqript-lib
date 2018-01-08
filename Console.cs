using System;
using System.Collections.Generic;
using Qrakhen.Sqript;

namespace Qrakhen.SqriptLib
{
    public class ConsoleInterface : Interface
    {
        public ConsoleInterface() : base("console") {

        }

        public Value write(Dictionary<string, Value> parameters) {
            Console.Write(parameters["value"].getValue());
            return null;
        }

        public Value read(Dictionary<string, Value> parameters) {
            var key = Console.ReadKey();
            return new Value((int) key.KeyChar, Sqript.ValueType.INTEGER);
        }

        public Value readLine(Dictionary<string, Value> parameters) {
            var line = Console.ReadLine();
            return new Value(line, Sqript.ValueType.STRING);
        }

        public override void load() {
            define(new Call("write", new string[] { "value" }, write, Sqript.ValueType.NULL));
            define(new Call("read", new string[] { }, read, Sqript.ValueType.INTEGER));
            define(new Call("readLine", new string[] { }, readLine, Sqript.ValueType.STRING));
        }
    }
}
