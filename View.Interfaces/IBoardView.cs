using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.View.Interfaces
{
    /// <summary>
    /// IBoardView - вид доски
    /// The interface for a game board
    /// Интерфейс для игрового поля
    /// </summary>
    public interface IBoardView
   {
        /// <summary>
        /// ShowGameStart - Показать начало игры
        /// Show the start message for the game
        /// Показать стартовое сообщение к игре
        /// </summary>
        void ShowGameStart();

        /// <summary>
        /// SetBoardState - Установить состояние правления 
        /// Set the board state to the state of the specified board
        /// Установить состояние платы в состояние указанной доски
        /// </summary>
        /// <param name="board">
        /// The board to copy the state of
        /// Доска для копирования состояния
        /// </param>
        void SetBoardState(IBoard board);

        /// <summary>
        /// ShowPlayerChange - Показать изменения игрока 
        /// Indicate to the user the player with the current turn
        /// Укажите пользователю игрока с текущим ходом
        /// </summary>
        /// <param name="turn">TODO:</param>
        void ShowPlayerChange(Player turn);

        /// <summary>
        /// SetMoveStartPosition - Установить начальную позицию перемещения
        /// Set the position that the player must begin the move from.  This usually
        /// indicates that the player must finish a move by completing a jump
        /// Установите позицию, с которой игрок должен начать движение. 
        /// Обычно это означает, что игрок должен закончить ход, выполнив прыжок
        /// </summary>
        /// <param name="position">
        /// The position at which the current player must start the move
        /// Позиция, с которой текущий игрок должен начать ход
        /// </param>
        void SetMoveStartPosition(int? position);

        /// <summary>
        /// LockPlayer - Блокировка игрока
        /// Allow or disallow the given player from moving
        /// Разрешить или запретить данному игроку двигаться
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        /// <param name="locked">If <c>true</c>,
        /// the player is prevented from moving.  If otherwise, the player is allowed to move
        ///  игрок не может двигаться. Если иначе, игрок может двигаться
        /// </param>
        void LockPlayer(Player player, bool locked);

        /// <summary>
        /// PromptMove - Быстрое движение
        /// Prompt the given player to make move
        /// Предложите данному игроку сделать ход
        /// </summary>
        /// <param name="player">
        /// The player to prompt move for
        /// Плеер, который подскажет ход
        /// </param>
        void PromptMove(Player player);

        /// <summary>
        /// PromptMove - Быстрое движение
        /// Prompt the player to make a move at the specified position.  This indicates that a move must be completed.
        /// Предложите игроку сделать ход в указанной позиции. Это указывает на то, что движение должно быть завершено.
        /// </summary>
        /// <param name="player">
        /// The player to prompt
        /// Плеер подскажет
        /// </param>
        /// <param name="position">
        /// The position at which the player should start the move from
        /// Позиция, с которой игрок должен начать движение с
        /// </param>
        void PromptMove(Player player, int position);

        /// <summary>
        /// RenderMove - сделать ход
        /// Render the given move
        /// Отрисовать данный ход
        /// </summary>
        /// <param name="move">
        /// The move to render
        /// Ход рендеринга
        /// </param>
        /// <param name="after">
        /// The state the board should be in after the move is rendered
        ///  Состояние, в котором доска должна быть после рендеринга хода
        /// </param>
        void RenderMove(Move move, IBoard after);

        /// <summary>
        /// ShowInvalidMove - Показать неверное движение
        /// Indicate to the user that an invalid move was made
        /// Укажите пользователю, что был сделан неверный ход
        /// </summary>
        /// <param name="move">
        /// The invalid move that was played
        /// Неверный ход, который был сыгран
        /// </param>
        /// <param name="player">
        /// The player that made the move
        /// Игрок, который сделал ход
        /// </param>
        /// <param name="message">
        /// An message indicating the problem with the move
        ///  Сообщение, указывающее на проблему с переездом
        /// </param>
        void ShowInvalidMove(Move move, Player player, string message);

        /// <summary>
        /// ShowGameOver - Показать игру окончена
        /// Indicate that the game is over
        /// Укажите, что игра окончена
        /// </summary>
        /// <param name="winner">
        /// The winner of the match
        /// Победитель матча
        /// </param>
        /// <param name="loser">
        /// The loser of the match
        /// Проигравший
        /// </param>
        void ShowGameOver(Player winner, Player loser);
   }
}
