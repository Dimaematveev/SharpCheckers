using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// MoveStatus -  статус движения
    /// Possible status of checker moves
    ///  Возможный статус ходов проверки
    /// </summary>
    public enum MoveStatus
   {
        /// <summary>
        /// Legal - разрешено
        /// The move is legal
        /// Движение законно
        /// </summary>
        Legal,

        /// <summary>
        /// Illegal -  Невозможно
        /// The move is illegal
        /// движение недопустимо
        /// </summary>
        Illegal,

        /// <summary>
        /// Incomplete - незавершенный
        /// The move is legal but not completed because it hasn't been determined if there is a multiple jump
        /// Перемещение разрешено, но не завершено, поскольку не было определено, есть ли многократный переход 
        /// </summary>
        Incomplete
    }
}
