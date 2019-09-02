using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
    /// <summary>
    /// IEngine - двигатель
    /// The interface to a checker move engine
    /// Интерфейс для проверки движка движка
    /// </summary>
    public interface IEngine
   {
        /// <summary>
        /// GenerateMove - Создать ход
        /// Generate a move for the given board state
        /// Генерируем ход для данного состояния доски
        /// </summary>
        /// <param name="board">
        /// The board state
        ///  Состояние доски
        /// </param>
        /// <param name="player">
        /// The player to generate the move for
        /// Игрок, который генерирует ход для
        /// </param>
        /// <param name="ply">
        /// The maximum ply
        /// Максимальный слой
        /// </param>
        /// <param name="timeout">
        /// The maximum time allowed to generate teh move
        /// Максимальное время, необходимое для генерации перемещения 
        /// </param>
        /// <returns>
        /// The generated move
        /// Сгенерированный ход 
        /// </returns>
        Move GenerateMove(IBoard board, Player player, int ply, TimeSpan timeout);

        /// <summary>
        /// CancelProcessing - Отмена обработки
        /// Notify the engine to halt move generation
        /// Уведомить двигатель, чтобы остановить генерацию движения
        /// </summary>
        void CancelProcessing();

        /// <summary>
        /// ForceMove -Принудительный фильм
        /// Force the engine to move immediately
        /// Заставить двигатель двигаться немедленно
        /// </summary>
        void ForceMove();
   }
}
