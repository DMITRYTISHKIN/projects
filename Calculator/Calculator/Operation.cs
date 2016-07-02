using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
        public enum Operation
        {
            None,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Sqrt,
            Clear
        }
        public enum Menus
        {
            Exit,
            Copy,
            Paste
        }
        public enum NumberNotation{
            A = 10,
            B,
            C,
            D,
            E,
            F
        }
        public enum MemoryOperation
        {
            AdditionMemory,
            SubtractionMemory,
            OutMemory,
            RemoveMemory
        }
}
