using System;
using System.Collections;
using System.Collections.Generic;


namespace Brainfuck
{
    public class BfRuntime
    {

        public AST Ast { get; set; }
        readonly byte[] bytes;
        ushort iterator;

        static readonly ushort ArraySize = 30000;

        public BfRuntime(AST param)
        {
            Ast = param;
            bytes = new byte[BfRuntime.ArraySize];
            iterator = 0;
        }

        public BfRuntime()
        {
            bytes = new byte[BfRuntime.ArraySize];
            iterator = 0;
        }

        public void RunProgram()
        {
            RunBody(Ast.MainBody);
            /*
            foreach(Operator o in Ast.MainBody.Operators)
            {
                Console.WriteLine(o.Type);
            }
            */
        }

        void RunLoop()
        {
            while (bytes[iterator] > 0)
            {
                
            }
        }

        void RunBody(Body body)
        {
            foreach(Operator op in body.Operators)
            {
                InterpretOperator(op);
            }
        }

        void InterpretOperator(Operator op)
        {
            switch (op.Type) 
            {
                case OperatorType.DecPtr:
                    DecPtr();
                    break;
                case OperatorType.IncPtr:
                    IncPtr();
                    break;
                case OperatorType.DecVal:
                    DecVal();
                    break;
                case OperatorType.IncVal:
                    IncVal();
                    break;
                case OperatorType.Loop:
                    Loop(op as Loop);
                    break;
                case OperatorType.Read:
                    Read();
                    break;
                case OperatorType.Write:
                    Write();
                    break;
            }
        }

        void IncPtr()
        {
            if (iterator < BfRuntime.ArraySize - 1)
            {
                ++iterator;
            }
        }

        void DecPtr()
        {
            if (iterator > 0)
            {
                --iterator;
            }
        }

        void IncVal() 
        {
            ++bytes[iterator];
        }

        void DecVal()
        {
            --bytes[iterator];
        }

        void Read()
        {
            char input = Console.ReadKey().KeyChar;
            bytes[iterator] = (byte)input;
        }

        void Write()
        {
            Console.Write( (char)bytes[iterator] );
        }

        void Loop(Loop loop)
        {
            while (bytes[iterator] != 0)
            {
                RunBody(loop.LoopBody);
            }
        }

    }
}
