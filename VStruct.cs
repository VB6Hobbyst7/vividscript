﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividScript
{
    public enum VStructType
    {
        Entry,Module,Method,Function,Exit,Unknown
    }
    public delegate void ParseStructToken(VToken t);
    public class VStruct
    {
        public VStructType Type = VStructType.Unknown;
        public string Name = "";
        public List<VStruct> Structs = new List<VStruct>();
        public int At = 0;
        public bool Ran = false;
        public long RunCount = 0;
        public ParseStructToken PreParser = null;
        public ParseStructToken Parser = null;
        public bool Done = false;
        public VTokenStream TokStream = null;
        public VStruct(VTokenStream toks)
        {
            this.TokStream = toks;
            Parse();
        }
        public virtual void BackOne()
        {
            TokStream.Pos--;
            if (TokStream.Pos < 0) TokStream.Pos = 0;
        }
        public virtual VToken PeekNext()
        {
            if (TokStream.Pos >= TokStream.Len) return null;
            return TokStream.Tokes[TokStream.Pos];
        }
        public virtual VToken Peek(int c)
        {
            c = c - 1;
            if (TokStream.Pos + c >= TokStream.Len) return null;
            return TokStream.Tokes[TokStream.Pos + c];
        }
        public virtual void Parse()
        {
            SetupParser();
            if (PreParser != null) PreParser(TokStream.GetNext());
            while (TokStream.Pos < TokStream.Len)
            {

                var nt = PeekNext();
             //   Console.WriteLine("VS:" + nt.Text + " T:" + nt.Token);
                Parser(TokStream.GetNext());
                if (Done) return;
            }

        }
        public virtual void SetupParser()
        {

        }
    }
}
