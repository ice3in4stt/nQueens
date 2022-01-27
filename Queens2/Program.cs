using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;


//Program solves nQueen problem. Enhanced with methods which will specify the solution variatons and increase the performance of the program.

namespace Queens2
{
    class Program 
    {
        static void Main(string[] args)
        {
            SolvedQueens(8);
            
            
            
            var timer = new Stopwatch();
            timer.Start();


            for (int i = 4; i < 4; i++)
            {
                Console.WriteLine("Laudan koko: " + i);
                List<Board> list = AllUniqueSolutions(i);
                Console.WriteLine("Uniikkien ratkaisuiden määrä: " + list.Count);
                /*foreach (var item in list)
                {
                    item.Print();
                    Console.WriteLine();
                }*/

                Console.WriteLine();
                Console.WriteLine();
            

            Console.WriteLine();
            }


            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            string totalTime = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            Console.WriteLine(totalTime);


            /*

            int[] array = new int[] { 1, 2, 3, 4 };

            Board board;
            Board board2;
            Board board3;

            board3 = Board.randomBoard(5);
            do
            {
                board = Board.randomBoard(5);
            } while (!board.IsValid());

            Console.WriteLine();


            
            board.Queens.ForEach(q => Console.WriteLine(q));
            board.Print();

            

            Console.WriteLine();

            board.Queens.Reverse();
            board.Queens.ForEach(q => Console.WriteLine(q));

            board2 = board.rotatedBoard();


            Console.WriteLine(board.isRotationMirror(board));

            Console.WriteLine(board.isRotationMirror(board3));

            Console.WriteLine(board2.Queens.Last());



            /*
            lauta = lauta.randomBoard(lauta.Size);

            lauta.Print(lauta);
            Console.WriteLine(lauta.isValid());

            foreach (var item in lauta.Queens)
            {
                Console.WriteLine(item.ToString());
            }

            while (!lauta.isValid())
            {
                lauta = lauta.randomBoard(lauta.Size);
            }

            lauta.Print(lauta);
            Console.WriteLine(lauta.isValid());

            foreach (var item in lauta.Queens)
            {
                Console.WriteLine(item.ToString());
            }
            */
            /*
            Board board = new Board(4);
           
            board.addQueen(1, 1);
            board.addQueen(1, 1);

            Board board2 = new Board(4);
            board2.addQueen(1, 2);
            board2.addQueen(1, 1);

            Board board3 = new Board(4);
            board3.addQueen(1, 1);
            board3.addQueen(1, 3);

            Board board4 = new Board(4);
            board4.addQueen(1, 3);
            board4.addQueen(1, 1);


            Console.WriteLine(board.Equals(board2));

            Console.WriteLine(board3.Equals(board4));

            Console.WriteLine(board.isValid());*/


        }

        /// <summary>
        /// Returns List<Board> of boards with all unique n queen solutions for the given board size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>

        public static List<Board> AllUniqueSolutions(int size)
        {

            List<Board> list = new List<Board>();
            List<Board> result = new List<Board>();

            foreach (var item in Generator2(size))
            {
                foreach (var item2 in SolutionsForInitialBoard(item))
                {
                    list.Add(item2);
                }
            }

            List<int> indexes = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                if (indexes.Contains(i))
                {
                    continue;
                }
                
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[i].GetHashCode() != list[j].GetHashCode())
                    {
                        if (list[i].isRotationMirror(list[j]))
                        {
                            indexes.Add(j);
                        }
                    }
                }

                result.Add(list[i]);
            }


            return result;
                        
        }



        /// <summary>
        /// Returns a list of boards with each containing a queen on each horizontal positon on the first row.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// Threading explanation to be added here.

        public static List<Board> Generator(int size)
        {

            List<Board> boards = new List<Board>();

            Board board = new Board(size);
            board.AddQueen(1, 1);

            boards.Add((Board)board.Clone());

            while (board.Queens.First().tryMove(1,0))
            {
                boards.Add((Board)board.Clone());
            }

            return boards;
        }


        /// <summary>
        /// Returns list of boards with one queen placed in each board on the first row in different position
        /// from the first square(chess board) till the middle square (included).
        /// Tähän tulee selitys siitä että käytetään säikeistämiseen...-----
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>

        public static List<Board> Generator2(int size)
        {

            List<Board> boards = new List<Board>();

            Board board = new Board(size);
            board.AddQueen(1, 1);

            boards.Add((Board)board.Clone());
            int halfPoint = (size + 1) / 2;

            while (board.Queens.First().tryMove(1, 0) && board.Queens.First().X <= halfPoint )
            {
                boards.Add((Board)board.Clone());
            }

            return boards;
        }


        static void ThreadingTest()
        {
            new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.Write("1");
                    Thread.Sleep(1);
                    Console.Write("\n");
                }
                
            }).Start();

         

            

        }


        /// <summary>
        /// Solves and prints the board solution for n queen problem.
        /// </summary>
        /// <param name="size"></param>

        static void SolvedQueens(int size)
        {
            Board board = new Board(size);

            board.AddQueen(1, 1);
            board.AddQueen(1, 2);

            do
            {
                if (!board.IsValid())
                {
                        while(!board.Queens.Last().tryMove(1, 0))
                        {
                            board.Queens.RemoveAt(board.Queens.Count - 1);
                        }
                
                }
                else
                {
                    board.AddQueen(1, board.Queens.Last().Y + 1);
                }

            } while (!board.IsValid() || board.Queens.Count != size);


            board.Print();

        }


        /// <summary>
        /// Takes board as a parameter and returns solved board for the given initial board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        static List<Board> SolutionsForInitialBoard(Board board)
        {
            int queenCount = board.Queens.Count;
            
            List<Board> boards = new List<Board>();

                do
                {  
                    if (!board.IsValid())
                    {
                        while (!board.Queens.Last().tryMove(1, 0))
                        {
                            board.Queens.RemoveAt(board.Queens.Count - 1);

                            if (board.Queens.Count == queenCount)
                            {
                                return boards;
                            }
                        }
                    }
                    else board.AddQueen(1, board.Queens.Last().Y + 1);

                    if (board.IsValid() && board.Queens.Count == board.Size) boards.Add((Board)board.Clone());

                } while (true);

     

            return boards;
        }



    }
  
}
