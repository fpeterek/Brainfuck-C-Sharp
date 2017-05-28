using System;
using System.Collections;
using System.Collections.Generic;

namespace Brainfuck
{

    public enum OperatorType 
    {
        IncPtr    = '>',
        DecPtr    = '<',
        IncVal    = '+',
        DecVal    = '-',
        Loop      = '[',
        LoopEnd   = ']',
        Read      = ',',
        Write     = '.',
    }

    public class Body
    {
        public List<Operator> Operators { get; }
        public Body Parent { get; set; }

        public Body()
        {
            Operators = new List<Operator>();
        }

        public void AddOperator(Operator o)
        {
            Operators.Add(o);
        }

        public Operator LastOperator() 
        {
            return Operators[Operators.Count - 1];
        }

    }

    public class Operator 
    {
        public OperatorType Type { get; }

        public Operator(char op)
        {
            Type = (OperatorType)op;
        }

        public Operator(Operator op)
        {
            Type = op.Type;
        }

    }

    public class Loop : Operator
    {

        public Body LoopBody { get; }

        public Loop(char op) : base(op)
        {
            LoopBody = new Body();
        }

        public Loop(Operator op) : base(op)
        {
            LoopBody = new Body();
        }

    }

    public class AST
    {

        public Body MainBody { get; }

        private Body CurrentBody;

        public AST(List<char> tokens)
        {
            MainBody = new Body()
            {
                Parent = null
            };
            CurrentBody = MainBody;

            CreateAST(tokens);
        }

        private void CreateAST(List<char> tokens)
        {
            foreach (char token in tokens)
            {
                Operator op = new Operator(token);
                AddOperator(op);
            }
        }

        private void AddOperator(Operator c) 
        {

            if (c.Type == OperatorType.LoopEnd)
            {
                ExitLoop();
                return;
            }

            if (c.Type == OperatorType.Loop)
            {
                Loop loop = new Loop(c);
                loop.LoopBody.Parent = CurrentBody;

                CurrentBody.AddOperator(loop);
                CurrentBody = loop.LoopBody;

                return;
            }

            CurrentBody.AddOperator(c);

        }

        private void ExitLoop() 
        {
            if (CurrentBody.Parent == null) 
            {
                return;
            }
            CurrentBody = CurrentBody.Parent;
        }

    }
}
