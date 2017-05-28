using System;

namespace Brainfuck
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0) 
            {
                Brainfuck brainfuck = new Brainfuck();
            }

            for (int iter = 0; iter < args.Length; ++iter)
            {
                Console.WriteLine(("Program " + iter));
                Brainfuck brainfuck = new Brainfuck(args[iter]);
            }
        }
    }
}
