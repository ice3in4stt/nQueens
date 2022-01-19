using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Queens2
{
    class Queen 
    {

        public int X { get;  set; }
        public int Y { get;  set; }
        public Board InBoard { get; }

        public Queen(int x, int y, Board board)
        {
            X = x;
            Y = y;
            InBoard = board;
        }


        /// <summary>
        /// Checks if it is possible to move queen to a given cordinates. Returns boolean, if true will move queen to a given cordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool tryMove(int x, int y) 
        {
            int newX = X + x;
            int newY = Y + y;


            if (newX <= InBoard.Size && 
                newX > 0  && 
                newY <= InBoard.Size && 
                newY > 0)
            {
                X += x;
                Y += y;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if this queen is positioned diagonally or laterally in comparison to other queen on the board, 
        /// if it is then the queens threathen one another and will return true, otherwise returns false. 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Threatens(Queen other)
        {
            if ((this.X == other.X ||
                this.Y == other.Y ||
                Math.Abs(this.X - other.X) == Math.Abs(this.Y - other.Y)) &&
                !this.Equals(other)
                )
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"({X},{Y})"; 
        }


    }
}
