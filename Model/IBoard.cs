using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// IBoard - Доска
    /// Interface for a checkers game board
    /// Интерфейс для настольной игры в шашки
    /// </summary>
    public interface IBoard
   {
        /// <summary>
        /// Size - размер
        /// Get the size of the board.  This is the number of valid positions on the board.
        /// Получить размер доски. Это количество действительных позиций на доске
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Rows - ряд
        /// Get the number of rows on the board
        ///  Получить количество рядов на доске
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Cols - столбец
        /// Get the number of columns on the board
        ///  Получить количество столбцов на доске
        /// </summary>
        int Cols { get; }

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
        Piece this[int position] { get; set; }

        /// <summary>
        /// this[int row, int col]
        /// Get the piece at the given location
        /// Получить кусок в указанном месте
        /// </summary>
        /// <param name="row">
        /// The row
        /// Ряд
        /// </param>
        /// <param name="col">
        /// The column
        /// Столбец
        /// </param>
        /// <returns>
        /// The piece at the given location
        ///  Кусок в указанном месте
        /// </returns>
        Piece this[int row, int col] { get; set; }

        /// <summary>
        /// Clear - Очистить
        /// Clear the board
        /// Очистить доску
        /// </summary>
        void Clear();

        /// <summary>
        /// Copy - копировать
        /// Generate a copy of the board
        ///  Создать копию доски
        /// </summary>
        /// <returns>
        /// A copy of this board
        /// Копия этой доски
        /// </returns>
        IBoard Copy();

        /// <summary>
        /// Copy - копировать
        /// Copy the state of the given board
        /// Скопировать состояние данной доски
        /// </summary>
        /// <param name="board">
        /// The board to copy
        /// Доска для копирования 
        /// </param>
        void Copy(IBoard board);
   }
}
