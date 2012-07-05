using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static int N = 7;

        static void Main(string[] args)
        {
            Board MaxSolvedBoard = new Board(0,0);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var aBoard = SearchBoardPosition(i, j);
                    if (MaxSolvedBoard.NumOfQueen < aBoard.NumOfQueen)
                    {
                        MaxSolvedBoard = aBoard;
                    }
                    Console.Out.Write(".");
                }
                Console.Out.WriteLine("");
            }

            Console.Out.WriteLine("");
            Console.Out.WriteLine("solved num:{0}", MaxSolvedBoard.NumOfQueen);


            //配置の出力
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (MaxSolvedBoard.xy[i, j])
                        Console.Out.Write("Q");
                    else
                        Console.Out.Write(".");
                }
                Console.Out.WriteLine("");
            }
        }

        /// <summary>
        /// クイーンの位置を仮決めして探索スタート
        /// </summary>
        static Board SearchBoardPosition(int x, int y){
            Board aBoard = new Board(x, y);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    aBoard.UpdateBoard(i, j);
                } 
            }
            return aBoard;
        }
    }

    class Board
    {
        static int N = 7;
        public int NumOfQueen = 0;

        //初期配置
        public bool[,] xy = new bool[,]{
            {false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false},
            {false, false, true, false, false, false, false},
            {false, false, false, false, false, false, false},
            {false, false, false, true, false, false, false},
            {false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false}
        };

        public Board(int x, int y) {
            xy[x, y] = true;
        }

        public bool UpdateBoard(int x, int y){

            int[] direction = new int[]{ -1,0,1 };
            //全方向チェック
            for (int i = 0; i < 3/*方向*/; i++)
            {
                for (int j = 0; j < 3/*方向*/; j++)
                {
                    //自分自身のチェックはしない
                    if (direction[i] == 0 && direction[j] == 0)
                        continue;
                        
                    bool result = CheckUpdatable(x, y, direction[i], direction[j]);
                    if (!result)
                        return false;
                }
            }

            //進行可能な方向にクイーンがいなければそこにクイーンを置く
            xy[x, y] = true;
            NumOfQueen++;

            return true;
        }

        /// <summary>
        /// クイーンが動ける方向にクイーンがいれば置けないのでfalseを返す
        /// </summary>
        public bool CheckUpdatable(int x, int y, int dx, int dy)
        {
            int xx = x;
            int yy = y;
            for (int i = 0; i < N; i++)
            {
                xx = xx + dx;
                yy = yy + dy;
                if (xx < 0 || N - 1 < xx)
                {
                    continue;
                }
                if (yy < 0 || N - 1 < yy)
                {
                    continue;
                }
                if (xy[xx, yy])
                    return false;
            }

            return true;

        }
    }
}
