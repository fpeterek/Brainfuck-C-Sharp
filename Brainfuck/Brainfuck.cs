using System;

namespace Brainfuck
{
    public class Brainfuck
    {

        readonly Tokenizer Tokenizer;
        readonly AST Ast;
        readonly BfRuntime Runtime;

        public Brainfuck(string filename)
        {
            Tokenizer = new Tokenizer(filename);
            Ast = new AST(Tokenizer.Tokenize());

            /* Delete tokens and close stream to save memory */
            Tokenizer.CleanUp();
            /* Probably won't need the tokenizer anymore after the AST is created */
            Tokenizer = null;

            Runtime = new BfRuntime(Ast);

            Runtime.RunProgram();

        }

        public Brainfuck()
        {
            /* Interactive interpreter (REPL) */
            Tokenizer = new Tokenizer();
            Runtime = new BfRuntime();
            string input;

            while (true)
            {
                /* Read Input */
                Console.Write("$ ");
                input = Console.ReadLine();

                if (input.Equals("reset", StringComparison.OrdinalIgnoreCase))
                {
                    /* Reset runtime environment */
                    Runtime = new BfRuntime();
                }
                else if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    /* Exit program */
                    return;
                }

                /* Tokenize and parse */
                Ast = new AST(Tokenizer.Tokenize(input));
                /* Delete existing tokens */
                Tokenizer.CleanUp();
                Runtime.Ast = Ast;
                /* Run */
                Runtime.RunProgram();
            }
        }

    }
}
