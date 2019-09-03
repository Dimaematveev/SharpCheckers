using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Checkerboard - шахматная доска
    /// An 8x8 checker board
    /// Доска для проверки 8x8
    /// </summary>
    public class Checkerboard : IBoard
   {
      private readonly Piece[] pieces = new Piece[32];


        /// <summary>
        /// Size - размер
        /// Get the size of the board.  This is the number of valid positions on the board.
        /// Получить размер доски. Это количество действительных позиций на доске.
        /// </summary>
        public int Size
      {
         get { return this.pieces.Length; }
      }

        /// <summary>
        /// Rows - Ряды
        /// Get the number of rows on the board
        /// Получить количество рядов на доске
        /// </summary>
        public int Rows { get { return BoardConstants.Rows; } }

        /// <summary>
        /// Cols - колонки
        /// Get the number of columns on the board
        /// Получить количество столбцов на доске
        /// </summary>
        public int Cols { get { return BoardConstants.Cols; } }

        /// <summary>
        /// this[int position]
        /// Get the piece at the specified board notation position
        /// Получить кусок в указанной позиции обозначения доски
        /// </summary>
        /// <param name="position">
        /// The position.  This starts at one instead of zero.
        /// Позиция. Это начинается с одного вместо нуля. 
        /// </param>
        /// <returns>
        /// The piece at the given board notation position
        ///  Кусок в указанной позиции обозначения доски
        /// </returns>
        public Piece this[int position]
      {
         get
         {
            if ((position <= 0) || (position > BoardConstants.LightSquareCount))
            {
               throw new ArgumentOutOfRangeException("position", "Position must be between 1 and 32 inclusive");
            }

            int idx = position - 1;
            return this.pieces[idx];
         }

         set
         {
            if ((position <= 0) || (position > BoardConstants.LightSquareCount))
            {
               throw new ArgumentOutOfRangeException("position", "Position must be between 1 and 32 inclusive");
            }

            int idx = position - 1;
            this.pieces[idx] = value;
         }
      }

        /// <summary>
        /// this[int row, int col] 
        /// Get the piece at the given location
        /// Получить кусок в указанном месте
        /// </summary>
        /// <param name="row">
        /// The row
        /// Строка
        /// </param>
        /// <param name="col">
        /// The column
        /// Столбец
        /// </param>
        /// <returns>
        /// The piece at the given location
        ///  Кусок в указанном месте
        /// </returns>
        public Piece this[int row, int col]
      {
         get
         {
            if ((row < 0) || (row >= BoardConstants.Rows))
            {
               throw new ArgumentOutOfRangeException("row", string.Format(CultureInfo.InvariantCulture, "must be between 0 and {0}", BoardConstants.Rows.ToString(CultureInfo.InvariantCulture)));
            }
            else if ((col < 0) || (col >= BoardConstants.Cols))
            {
               throw new ArgumentOutOfRangeException("col", string.Format(CultureInfo.InvariantCulture, "must be between 0 and {0}", BoardConstants.Cols.ToString(CultureInfo.InvariantCulture)));
            }

            int position = Location.ToPosition(row, col);
            if ((position <= 0) || (position > BoardConstants.LightSquareCount))
            {
               return Piece.Illegal;
            }

            return this[position];
         }

         set
         {
            int position = Location.ToPosition(row, col);
            this[position] = value;
         }
      }

        /// <summary>
        /// Clear - Очистить
        /// Clear the board
        /// Очистить доску
        /// </summary>
        public void Clear()
      {
         for (int i = 0; i < pieces.Length; i++)
         {
            this.pieces[i] = Piece.None;
         }
      }

        /// <summary>
        /// Copy - копия 
        /// Generate a copy of the board
        /// Создать копию доски
        /// </summary>
        /// <returns>
        /// A copy of this board
        ///  Копия этой доски
        /// </returns>
        public IBoard Copy()
      {
         Checkerboard board = new Checkerboard();
         System.Array.Copy(this.pieces, board.pieces, this.pieces.Length);
         return board;
      }

        /// <summary>
        /// Copy - копия
        /// Copy the state of the given board
        /// Скопировать состояние данной доски
        /// </summary>
        /// <param name="board">
        /// The board to copy
        /// Доска для копирования 
        /// </param>
        public void Copy(IBoard board)
      {
         if (board.Size != this.Size)
         {
            throw new ArgumentException("Incompatable board sizes");//Несовместимые размеры платы
            }

         Checkerboard checkerboard = board as Checkerboard;
         if (checkerboard != null)
         {
                // Copy board in an optimized fashion
                // Копируем доску оптимизированным способом
                this.Copy(checkerboard);
         }
         else
         {
            for (int i = 1; i <= pieces.Length; i++)
            {
               this[i] = board[i];
            }
         }
      }

        /// <summary>
        /// Copy - копия
        /// Copy the state of the given checker board
        /// Копируем состояние данной доски проверки
        /// </summary>
        /// <param name="board">
        /// The checker board to copy
        ///  Контрольная доска для копирования
        /// </param>
        public void Copy(Checkerboard board)
      {
         if (board == null)
         {
            throw new ArgumentNullException("board");
         }

         System.Array.Copy(board.pieces, this.pieces, this.pieces.Length);
      }
   }
}
