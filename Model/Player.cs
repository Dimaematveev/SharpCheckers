using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Player - игрок
    /// The game player.  A checkers match is between two opponents.  One player
    /// controls the black pieces and the other controls the white pieces.
    ///  Игрок. Матч по шашкам между двумя противниками. Один игрок 
    ///  контролирует черные фигуры, а другой контролирует белые фигуры.
    /// </summary>
    public enum Player
   {
        /// <summary>
        /// None - никто
        /// Neither player
        /// Ни один игрок
        /// </summary>
        None,

        /// <summary>
        /// Black - Черный
        /// The black piece player
        /// Черная фигура игрока
        /// </summary>
        Black,

        /// <summary>
        /// White - Белый
        /// The white piece player
        /// Белая фигура игрока
        /// </summary>
        White
    }
}
