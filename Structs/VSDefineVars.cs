﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividScript.VStructs
{
    public class VSDefineVars : VStruct
    {
        public VarType Type = VarType.Bool;
        public List<VSVar> Vars = new List<VSVar>();
        public VSDefineVars(VTokenStream s) : base(s)
        {

        }
        public override void SetupParser()
        {
            PreParser = (t) =>
            {
                //Console.WriteLine("T=" + t.ToString());
                switch (t.Token)
                {
                    case Token.Int:
                        Type = VarType.Int;
                        break;
                    case Token.Byte:
                        Type = VarType.Byte;
                        break;
                    case Token.Short:
                        Type = VarType.Short;
                        break;
                    case Token.Long:
                        Type = VarType.Long;
                        break;
                    case Token.Float:
                        Type = VarType.Float;
                        break;
                    case Token.Double:
                        Type = VarType.Double;
                        break;
                    case Token.String:
                        Type = VarType.String;
                        break;
                }
                Console.WriteLine("VarType:" + Type.ToString());
            };
            Parser = (t) =>
            {
                //Console.WriteLine("T==" + t.Text+" Tok:"+t.Token);
                switch (t.Token)
                {
                    case Token.Id:

                        var v = new VSVar();
                        v.Name = t.Text;
                        v.Type = Type;
                        Console.WriteLine("VAR:" + v.Name + " TYPE:" + v.Type);
                        
                        break;
                    case Token.EndLine:
                        Done = true;
                        break;
                }
            };
        }
    }
}
