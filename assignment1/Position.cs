using System;
using System.Collections.Generic;
using System.Text;

namespace assignment1
{
    class Position
    {
        public int row;
        public int column;
        public Position String2Position(string pos)
        {
            this.column = pos[0] - 'a';
            this.row = 8 - int.Parse(pos[1].ToString());
            if (column < 8 && column >= 0 && row < 8 && row >= 0)
            {
                return this;
            }
            else
            {
                throw new Exception($"Invalid position: {pos}");
            }
        }
    }
}
