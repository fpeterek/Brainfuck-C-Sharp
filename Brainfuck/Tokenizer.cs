using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Brainfuck
{
    public class Tokenizer
    {

        private static readonly char[] ValidTokens = { '[', ']', '+', '-', '.', ',', '<', '>' };
        private static readonly char   CommentChar = '#';

        private readonly StreamReader Stream;
        private readonly List<char> Tokens;

        public static bool IsToken(char c) 
        {
            foreach(char token in Tokenizer.ValidTokens) 
            {
                if (c == token) 
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsComment(char c)
        {
            return c == Tokenizer.CommentChar;
        }

        public Tokenizer(string filename)
        {
            Stream = new StreamReader(filename);
            Tokens = new List<char>();
        }

        public Tokenizer()
        {
            Tokens = new List<char>();
        }

        public List<char> Tokenize(string s)
        {
			foreach (char c in s)
			{
				if (IsComment(c))
				{
					break;
				}
				if (IsToken(c))
				{
					Tokens.Add(c);
				}
			}

            return Tokens;
        }

        public List<char> Tokenize()
        {
            string line;

            while ( (line = Stream.ReadLine()) != null ) 
            {
                foreach(char c in line)
                {
                    if (IsComment(c))
                    {
                        break;
                    }
                    if (IsToken(c))
                    {
                        Tokens.Add(c);
                    }
                }
            }

            return Tokens;

        }

        public void DeleteTokens()
        {
            Tokens.Clear();
        }

        public void CloseStream()
        {
            if (Stream != null)
            {
                Stream.Close();
            }
        }

        public void CleanUp()
        {
            DeleteTokens();
            CloseStream();
        }
    }
}
