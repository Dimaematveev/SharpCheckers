using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Move - Движение
    /// Encaspsulates a single checkers move.  This includes possible jumps via captures.
    /// Включает в себя один ход шашки. Это включает в себя возможные прыжки через захваты
    /// </summary>
    public class Move
   {
      private readonly List<int> positions = new List<int>();


        /// <summary>
        /// Construct an empty move
        /// Построить пустой ход
        /// </summary>
        public Move()
      {
      }

        /// <summary>
        /// Construct a move based on the board postion notation
        /// Построить ход на основе обозначения позиции на доске
        /// </summary>
        /// <param name="first">
        /// The origin of the move
        /// Происхождение переезда
        /// </param>
        /// <param name="rest">
        /// The remaining moves.  Greater than one if this move contains jumps
        ///  Остальные ходы. Больше единицы, если этот ход содержит прыжки
        /// </param>
        public Move(int first, params int[] rest)
      {
         AddMoves(first, rest);
      }

        /// <summary>
        /// Construct a move with the given board locations
        /// Построить ход с заданными местоположениями доски
        /// </summary>
        /// <param name="first">
        /// The origin of the move
        ///  Происхождение переезда
        /// </param>
        /// <param name="rest">
        /// The remaining moves.  Greater than one if this move contains jumps
        /// Остальные ходы. Больше единицы, если этот ход содержит прыжки
        /// </param>
        public Move(Location first, params Location[] rest)
      {
         AddMoves(first, rest);
      }

        /// <summary>
        /// this[int moveIndex]
        /// Get the board notation location at the given index.  This is zero-based.
        ///  Получить расположение обозначения доски по заданному индексу. Это с нуля.
        /// </summary>
        /// <param name="moveIndex">
        /// The index to get position for
        /// Индекс для получения позиции для
        /// </param>
        /// <returns>
        /// The position at the given index
        /// Позиция по данному индексу
        /// </returns>
        public int this[int moveIndex]
      {
         get
         {
            if ((moveIndex < 0) || (moveIndex >= positions.Count))
            {
               throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
            }

            return positions[moveIndex];
         }
      }

        /// <summary>
        /// GetLocation - Получить местоположение
        /// Get the location at the given index.  This is zero-based.
        /// Получить местоположение по заданному индексу. Это с нуля.
        /// </summary>
        /// <param name="moveIndex">
        /// TODO:
        /// </param>
        /// <returns>
        /// TODO:
        /// </returns>
        public Location GetLocation(int moveIndex)
      {
         if ((moveIndex < 0) || (moveIndex >= positions.Count))
         {
            throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
         }

         return Location.FromPosition(positions[moveIndex]);
      }

        /// <summary>
        /// AddMoves - Добавить ходы
        /// Add the given locations to this move
        /// Добавить указанные места к этому ходу
        /// </summary>
        /// <param name="first">
        /// The first location to add
        ///  Первое место для добавления
        /// </param>
        /// <param name="rest">
        /// The remaining positions to add
        /// Остальные позиции для добавления
        /// </param>
        public void AddMoves(Location first, params Location[] rest)
      {
         positions.Add(first.ToPosition());
         foreach (Location location in rest)
         {
            positions.Add(location.ToPosition());
         }
      }

        /// <summary>
        /// AddMoves - Добавить ходы
        /// Add the given locations to this move using board notation
        /// Добавьте указанные места к этому ходу, используя обозначение на доске
        /// </summary>
        /// <param name="first">
        /// The first location to add
        ///  Первое место для добавления
        /// </param>
        /// <param name="rest">
        /// The remaining positions to add
        /// Остальные позиции для добавления
        /// </param>
        public void AddMoves(int first, params int[] rest)
      {
         positions.Add(first);
         foreach (int position in rest)
         {
            positions.Add(position);
         }
      }

        /// <summary>
        /// Count - подсчитывать
        /// Get the number of positions in this move
        ///  Получить количество позиций в этом ходу
        /// </summary>
        public int Count
      {
         get { return positions.Count; }
      }

        /// <summary>
        /// Origin - Происхождение
        /// Get the origin of this move in board notation.  This is the position the move originated at.
        /// Получить происхождение этого хода в нотации доски. Это позиция, в которой возникло движение.
        /// </summary>
        public int? Origin
      {
         get
         {
            return (positions.Count > 0) ? new Nullable<int>(positions[0]) : null;
         }
      }

        /// <summary>
        /// Destination - Место назначения
        /// Get the final position of this move in board notation.
        /// Получить окончательную позицию этого хода в нотации доски.
        /// </summary>
        public int? Destination
      {
         get
         {
            return (positions.Count > 0) ? new Nullable<int>(positions[positions.Count - 1]) : null;
         }
      }

        /// <summary>
        /// OriginLocation - Место происхождения
        /// Get the origin of this move.  This is the position the move originated at.
        /// Получите источник этого движения. Это позиция, в которой возникло движение.
        /// </summary>
        public Location OriginLocation
      {
         get
         {
            return (positions.Count > 0) ? Location.FromPosition(positions[0]) : null;
         }
      }

        /// <summary>
        /// DestinationLocation - Место назначения
        /// Get the final position of this move.
        /// Получить окончательную позицию этого хода.
        /// </summary>
        public Location DestinationLocation
      {
         get
         {
            return (positions.Count > 0) ? Location.FromPosition(positions[positions.Count - 1]) : null;
         }
      }

        /// <summary>
        /// IsJump - это прыжок
        /// Is this a jumping move
        /// Это прыжок
        /// </summary>
        /// <returns><code>true</code>
        /// if this is a jumping move
        /// если это прыжок
        /// </returns>
        public bool IsJump()
      {
         if (positions.Count > 2)
         {
            return true;
         }
         else
         {
            const int INVALID_POSITION = -1;
            Location origin = Location.FromPosition(Origin ?? INVALID_POSITION);
            Location destination = Location.FromPosition(Destination ?? INVALID_POSITION);
            return (
               (Math.Abs(origin.Row - destination.Row) > 1) ||
               (Math.Abs(origin.Col - destination.Col) > 1)
            );
         }
      }

        /// <summary>
        /// ToShortNotationLocation - Для краткого обозначения местоположения
        /// Get the short notation formatted location
        ///  Получить краткую запись в формате
        /// </summary>
        /// <returns>
        /// the short notation formatted location
        ///  расположение в короткой записи
        /// </returns>
        public String ToShortNotationLocation()
      {
         if (positions.Count == 0)
         {
            const String BLANK_MOVE = "...";
            return BLANK_MOVE;
         }

         return Origin +
               ((!IsJump()) ? "-" : "x") +
               Destination;

      }

        /// <summary>
        /// ToString - в строку
        /// String representation of move
        /// Строковое представление хода
        /// </summary>
        public override String ToString()
      {
         return ToShortNotationLocation();
      }
   }
}