using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    //TODO:ы Piece - ”часток(”часток на доске - клетка) или фигура!!!!

    /// <summary>
    /// Piece - ”часток
    /// The various pieces
    /// –азличные участки
    /// </summary>
    public enum Piece
   {
        /// <summary>
        /// Illegal - Ќезаконно
        /// This indicates an invalid piece.  i.e. Invalid square
        /// Ёто указывает на неверный участок. то есть неверный квадрат
        /// </summary>
        Illegal,

        /// <summary>
        /// None - Ќикто
        /// This indicates that the square is empty and has no piece
        /// Ёто указывает на то, что квадрат пуст и не имеет фигуры
        /// </summary>
        None,

        /// <summary>
        /// BlackMan - „ерна€ пешка
        /// Black man piece
        /// на квадрате черна€ пешка
        /// </summary>
        BlackMan,

        /// <summary>
        /// WhiteMan -  бела€ пешка
        /// White man piece
        /// на квадрате бела€ пешка
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
