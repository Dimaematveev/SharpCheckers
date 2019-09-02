using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Board constants
    /// Совет константы
    /// </summary>
    public static class BoardConstants
   {
        /// <summary>
        /// The number of rows on a checker board
        /// Количество рядов на шахматной доске
        /// </summary>
        public static readonly int Rows = 8;

        /// <summary>
        /// The number of columns on a checker board
        ///  Количество столбцов на шахматной доске
        /// </summary>
        public static readonly int Cols = 8;

        /// <summary>
        /// The number of legal squares.  These are the light colored squares on the board.
        /// Количество легальных квадратов. Это светлые квадраты на доске.
        /// </summary>
        public static readonly int LightSquareCount = 32;
   }
}
