using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    ///  BoardTrace - Трассировка доски
    /// Utility class to trace the board state
    ///  Класс утилиты для отслеживания состояния платы
    /// </summary>
    public static class BoardTrace
   {
        /// <summary>
        /// DrawBoard - Доска для рисования
        /// Write the board state to the console
        /// Записать состояние платы в консоль
        /// </summary>
        /// <param name="board">
        /// The board
        /// Доска 
        /// </param>
        public static void DrawBoard(IBoard board)
      {
         DrawBoard(board, Console.Out);
      }

        /// <summary>
        ///  DrawBoard - Доска для рисования
        /// Write the board state to the console
        /// Записать состояние платы в консоль
        /// </summary>
        /// <param name="board">
        /// The board
        /// Доска
        /// </param>
        /// <param name="writer">
        /// The writer
        ///  писатель
        /// </param>
        public static void DrawBoard(IBoard board, TextWriter writer)
      {
         StringBuilder buf1 = new StringBuilder();
         StringBuilder buf2 = new StringBuilder();
         bool darkSquare = false;
         writer.WriteLine("+-----------------------+   +-----------------------+");

         buf1.Append("|  ");
         buf2.Append("|  ");
         int col = 1;
         for (int pos = board.Size; pos >= 1; pos--)
         {
            String ch = string.Empty;

            if ((col != 0) && (col % 8 == 0))
            {
               col = 0;
               pos++;
               darkSquare = !darkSquare;
               buf1.Append("|");
               buf2.Append("|");

               writer.WriteLine("{0}   {1}", buf1.ToString(), buf2.ToString());
               buf1.Length = 0;
               buf2.Length = 0;
               writer.WriteLine("+-----------------------+   +-----------------------+");
            }
            else
            {
               col++;
               darkSquare = !darkSquare;

               if (!darkSquare)
               {
                  ch = "..";
                  pos++;
               }
               else
               {
                  Piece piece = board[pos];
                  switch (piece)
                  {
                     case Piece.None: ch = " "; break;
                     case Piece.BlackMan: ch = "b"; break;
                     case Piece.WhiteMan: ch = "w"; break;
                     case Piece.BlackKing: ch = "B"; break;
                     case Piece.WhiteKing: ch = "W"; break;
                  }
               }

               buf1.AppendFormat("|{0,2}", ch);
               buf2.AppendFormat("|{0,2}", (darkSquare) ? pos.ToString(CultureInfo.CurrentCulture) : " ");
            }

         }

         buf1.Append("|");
         buf2.Append("|");
         writer.WriteLine("{0}  |   {1}  |", buf1.ToString(), buf2.ToString());
         writer.WriteLine("+-----------------------+   +-----------------------+");
      }
   }
}
