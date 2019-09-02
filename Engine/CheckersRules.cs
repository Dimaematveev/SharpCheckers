using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
    /// <summary>
    /// CheckersRules - Правила шашек
    /// A checkers rules engine implementation
    /// Реализация механизма проверки правил
    /// </summary>
    public class CheckersRules : IBoardRules
   {
        /// <summary>
        /// ResetBoard - Сбросить плату
        /// Reset the board
        /// Сбросить плату
        /// </summary>
        /// <param name="board">
        /// The board to reset
        /// Доска для сброса
        /// </param>
        public void ResetBoard(IBoard board)
      {
         const int BLACK_BEGIN_POSITON = 1;
         const int BLACK_END_POSITION = 12;
         const int WHITE_BEGIN_POSITON = 21;
         const int WHITE_END_POSITION = 32;

         board.Clear();

         for (int position = BLACK_BEGIN_POSITON; position <= BLACK_END_POSITION; position++)
         {
            board[position] = Piece.BlackMan;
         }

         for (int position = WHITE_BEGIN_POSITON; position <= WHITE_END_POSITION; position++)
         {
            board[position] = Piece.WhiteMan;
         }
      }

        /// <summary>
        /// IsValidMove - Действительный ход
        /// Check if the given move is valid for the given state
        /// Проверить, является ли данный ход действительным для данного состояния
        /// </summary>
        /// <param name="board">
        /// The board state
        ///  Состояние доски
        /// </param>
        /// <param name="move">
        /// The move
        ///   Движение
        /// </param>
        /// <param name="player">
        /// The player that made the move
        ///  Игрок, который сделал ход
        /// </param>
        /// <returns>
        /// The status of the move
        ///  Статус перемещения 
        /// </returns>
        public MoveStatus IsValidMove(IBoard board, Move move, Player player)
      {
         return CheckerMoveRules.IsMoveLegal(board, move, player);
        }

        /// <summary>
        /// IsGameOver - Игра окончена
        /// Is the game over
        ///  Игра окончена
        /// </summary>
        /// <param name="board">
        /// The board state
        /// Состояние доски
        /// </param>
        /// <param name="turn">
        /// The player with the current turn
        /// Игрок с текущим ходом
        /// </param>
        /// <returns><c>true</c> 
        /// if the game is over and 
        /// если игра окончена, и
        /// <c>false</c> 
        /// false if otherwise
        /// ложь, если нет
        /// </returns>
        public bool IsGameOver(IBoard board, Player turn)
      {
         return !CheckerMoveRules.HasMovesAvailable(board, turn);
      }

        /// <summary>
        /// GetWinner - Получить победителя
        /// Get the winner of the game
        /// Получить победителя игры
        /// </summary>
        /// <param name="board">
        /// The board state
        /// Состояние доски
        /// </param>
        /// <param name="turn">
        /// The player with the turn
        ///  Игрок с терном
        /// </param>
        /// <returns>
        /// The player that won the game if any
        /// Игрок, который выиграл игру, если таковой имеется 
        /// </returns>
        public Player GetWinner(IBoard board, Player turn)
      {
         throw new NotImplementedException();
      }

        /// <summary>
        /// ApplyMove - Применить движение
        /// Apply the given move to the board
        /// Применить данный ход к доске
        /// </summary>
        /// <param name="board">
        /// The board state
        /// Состояние доски
        /// </param>
        /// <param name="move">
        /// The move to apply to the board
        /// Ход, применяемый к доске 
        /// </param>
        /// <returns><c>true</c> 
        /// if the move was applied successfully
        /// если перемещение было успешно применено
        /// </returns>
        public bool ApplyMove(IBoard board, Move move)
      {
         CheckerMoveRules.UpdateBoard(board, move);
         return true;
      }

        /// <summary>
        /// ResolveAmbiguousMove - Решить неоднозначное движение
        /// Attempt to resolve ambiguous jump move.  The longest move matching the first 
        /// location and last location is selected.
        /// Попытка разрешить неоднозначное прыжковое движение. Самый длинный ход, соответствующий первому и 
        /// последнему местоположению, выбран.
        /// </summary>
        /// <param name="board">
        /// The board
        /// Доска
        /// </param>
        /// <param name="move">
        /// The possibly ambiguos move
        /// Возможно двусмысленное движение
        /// </param>
        /// <returns><c>true</c> 
        /// if move could be resolved
        ///  если перемещение может быть разрешено
        /// </returns>
        public Move ResolveAmbiguousMove(IBoard board, Move move)
      {
         return CheckerMoveRules.ResolveAmbiguousMove(board, move);
      }
   }
}
