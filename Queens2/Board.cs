using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens2
{
    class Board : IEquatable<Board>, ICloneable
    {
        public int Size { get; }
        public List<Queen> Queens { get; set; }

        public Board(int size)
        {
            Size = size;

            Queens = new List<Queen>();
        }

        /// <summary>
        /// Add a queen to the board at the given cordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddQueen(int x, int y)
        {
            Queens.Add(new Queen(x, y, this));
        }


        /// <summary>
        /// Checks if any queens threaten eachother on the board.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            foreach (var item in Queens)
            {
                foreach (var item2 in Queens)
                {
                    if (item.Threatens(item2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns mirrored copy of the board.
        /// </summary>
        /// <returns></returns>
        public Board MirroredBoard()
        {
            Board board = new Board(this.Size);

            foreach (var item in this.Queens)
            {
                board.AddQueen(this.Size - item.X + 1, item.Y);
            }
            return board;
        }

        /// <summary>
        /// Returns 90 degrees rotated copy of the board.
        /// </summary>
        /// <returns></returns>
        public Board rotatedBoard()
        {
            Board board = new Board(this.Size);

            foreach (var item in this.Queens)
            {
                board.AddQueen(item.Y, (this.Size - item.X) + 1);

            }
            return board;
        }

        /// <summary>
        /// Returns Board for a given size with randomly placed queen on each row.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Board randomBoard(int size)
        {
            Board board = new Board(size);
            Random random = new Random();


            board.AddQueen(random.Next(size) + 1, 1);

            for (int i = 2; i < size + 1; i++)
            {
                /*int x;
                do
                {
                    x = random.Next(size + 1);
                    foreach (var item in board.Queens)
                    {
                        if (x == item.X)
                        {
                            x = 0;
                        }
                    }
                } while (x == 0);*/

                int x = Helpers.randomNumber(size, board.Queens.Select(q => q.X).ToList());

                board.AddQueen(x, i);
            }

            return board;
        }



        /// <summary>
        /// Returns list of valid Board solutions for the given board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<Board> ValidBoards(Board board)
        {
            List<Board> boards = new List<Board>();

            Board board1 = new Board(board.Size);

            board1.AddQueen(1, 1);
            board1.AddQueen(1, 2);

            do
            {
                if (!board1.IsValid())
                {
                    while (!board1.Queens.Last().tryMove(1, 0))
                    {
                        board.Queens.RemoveAt(board1.Queens.Count - 1);
                    }

                }
                else
                {
                    board.AddQueen(1, board.Queens.Last().Y + 1);
                }

            } while (!board.IsValid() || board.Queens.Count != board.Size);

            boards.Add(board1);

            return boards;
        }

        /// <summary>
        /// Draws the board in the console.
        /// </summary>

        public void Print()
        {
            bool found = false;
            for (int i = this.Size; i > 0; i--)
            {
                for (int j = 1; j < this.Size + 1; j++)
                {
                    foreach (var item in this.Queens)
                    {
                        if (item.Y == i && item.X == j)
                        {
                            found = true;
                            break;
                        }

                    }
                    if (found)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("o");
                    }
                    found = false;
                    /*if (this.Queens.Select(q => new { X = q.X, Y = q.Y }).
                                        Contains(new { X = i, Y = j }))
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("O");
                    }
                    */
                }
                Console.WriteLine();
            }
            
        }
        /// <summary>
        /// Compares this board to other board by comparing Queen coordinates and board size. 
        /// Same Queens coordinates and board size will return true (regardless of Queens list order)
        /// </summary>
        /// <param name="other">The other board</param>
        /// <returns>Are the Board equal</returns>
        public bool Equals(Board other)
        {
            //If the Board hashes are same the board must be the same board
            if (this.GetHashCode() == other.GetHashCode()) return true;

            //If the size differs they can't be similar
            if (this.Size != other.Size) return false;

            //If the amount of queens differ they can't be similar
            if (this.Queens.Count() != other.Queens.Count()) return false;

            //If the method has reached this point we must check all the queens coordinates for equality
            
            //We first select the two board's Queens coordinates into two lists of anonymous objects containing the X and Y coordinate 
            var thisCoords = this.Queens.Select(q => new { X = q.X, Y = q.Y }).ToList();
            var thatCoords = other.Queens.Select(q => new { X = q.X, Y = q.Y }).ToList();

            //Then use the ScrambledEquals method to check if the two lists have exactly the same coordinate pairs (regarless of order)
            return (Helpers.ScrambledEquals<object>(thisCoords, thatCoords));
        }

        /// <summary>
        /// Compares to find out if other is any rotation or mirrored rotation of this
        /// </summary>
        /// <param name="other">The Board which this is being compared to </param>
        /// <returns>Result of the comparison</returns>
        public bool isRotationMirror(Board other)
        {
            if (this.Equals(other) ||
                this.Equals(other.rotatedBoard()) ||
                this.Equals(other.rotatedBoard().rotatedBoard()) ||
                this.Equals(other.rotatedBoard().rotatedBoard().rotatedBoard()) ||
                this.Equals(other.MirroredBoard()) ||
                this.Equals(other.MirroredBoard().rotatedBoard()) ||
                this.Equals(other.MirroredBoard().rotatedBoard().rotatedBoard()) ||
                this.Equals(other.MirroredBoard().rotatedBoard().rotatedBoard().rotatedBoard())
               ) return true; 

            return false;
        }

        /// <summary>
        /// Makes a copy of the board.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Board board = new Board(this.Size);

            foreach (var item in this.Queens)
            {
                board.AddQueen(item.X, item.Y);

            }
            return board;
        }
    }
}
