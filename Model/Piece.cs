using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Piece - Кусок
    /// The various pieces
    /// Различные части
    /// </summary>
    public enum Piece
   {
        /// <summary>
        /// Illegal - Незаконно
        /// This indicates an invalid piece.  i.e. Invalid square
        /// Это указывает на неверный кусок. то есть неверный квадрат
        /// </summary>
        Illegal,

        /// <summary>
        /// None - Никто
        /// This indicates that the square is empty and has no piece
        /// Это указывает на то, что квадрат пуст и не имеет фигуры
        /// </summary>
        None,

        /// <summary>
        /// BlackMan - Черная пешка
        /// Black man piece
        /// на квадрате черная пешка
        /// </summary>
        BlackMan,

        /// <summary>
        /// WhiteMan -  белая пешка
        /// White man piece
        /// на квадрате белая пешка
        /// </summary>
        WhiteMan,

        /// <summary>
        /// BlackKing -  черный король
        /// Black king piece
        /// на квадрате  черный король
        /// </summary>
        BlackKing,

        /// <summary>
        /// WhiteKing - белый король
        /// White king piece
        /// на квадрате белый король
        /// </summary>
        WhiteKing
    }
}
