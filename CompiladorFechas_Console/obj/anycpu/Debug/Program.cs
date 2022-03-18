using System;
using System.IO;
using Scanner;

namespace CompiladorFechas_Console
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var contenido = File.ReadAllText("test.txt");
            var logger = new Logger();
            var scanner = new Scanner.Scanner(new Input(contenido), logger);
            var parser = new Parser.Parser(scanner, logger);
            parser.Parse();
        }
    }
}
