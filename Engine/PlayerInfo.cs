using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
    /// <summary>
    ///  PlayerInfo - Информация об игроке
    ///   Информация об игроке
    /// Player details
    /// </summary>
    public class PlayerInfo
   {
      private readonly Player player;
      private String name;
      private bool isComputer;
      private int level = 7;
      private TimeSpan timeout = TimeSpan.FromMinutes(3);
      private IEngine engine;

        /// <summary>
        /// Construct the player details
        /// Построить детали игрока
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        public PlayerInfo(Player player) : this(player, false)
      {
      }

        /// <summary>
        /// Construct the player details
        /// Построить детали игрока
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        /// <param name="isComputer"><c>true</c> 
        /// если игрок является компьютером
        /// <c>false</c>
        ///  в противном случае
        /// </param>
        public PlayerInfo(Player player, bool isComputer)
      {
         this.player = player;
         this.isComputer = isComputer;
         this.engine = new SimpleEngine();
      }

        /// <summary>
        /// Player - Игрок
        /// Get the underlying player
        /// Получить основного игрока
        /// </summary>
        public Player Player
      {
         get { return player; }
      }

        /// <summary>
        /// Name - Имя
        /// Get or set the name for the player
        /// Получить или установить имя для игрока
        /// </summary>
        public string Name
      {
         get { return name; }
         set { name = value; }
      }

        /// <summary>
        /// IsComputer - является компьютером
        /// Get or set whether the player is computer controlled
        /// Получить или установить, управляется ли плеер компьютером
        /// </summary>
        public bool IsComputer
      {
         get { return isComputer; }
         set { isComputer = value; }
      }

        /// <summary>
        /// Level - уровень 
        /// Get or set the level of the player.  This is only relevant for computer controlled players
        /// Получить или установить уровень игрока. Это относится только к компьютерным игрокам
        /// </summary>
        public int Level
      {
         get { return level; }
         set { level = value; }
      }

        /// <summary>
        /// Timeout - Тайм-аут
        /// Get or set the move timeout of the player.  This is only relevant for computer controlled players
        ///  Получить или установить тайм-аут перемещения игрока. Это относится только к компьютерным игрокам
        /// </summary>
        public TimeSpan Timeout
      {
         get { return timeout; }
         set { timeout = value; }
      }

        /// <summary>
        /// Engine - движок игрока
        /// Get or set the move engine of the player.  This is only relevant for computer controlled players
        ///  Получить или установить движок игрока. Это относится только к компьютерным игрокам
        /// </summary>
        public IEngine Engine
      {
         get { return engine; }
         set { engine = value; }
      }

        /// <summary>
        /// GenerateMove - Создать ход
        /// Generate a move for the player given the specified board state
        /// Генерируем ход для игрока с учетом указанного состояния доски
        /// </summary>
        /// <param name="board">
        /// The board state
        /// Состояние доски
        /// </param>
        /// <returns>
        /// The generated move
        ///  Сгенерированный ход
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// If the player doesn't have an engine associated with it
        /// Если у игрока нет движка, связанного с ним
        /// </exception>
        public Move GenerateMove(IBoard board)
      {
         if (engine == null)
         {
            throw new InvalidOperationException("Engine not available for player");//Движок недоступен для игрока
            }

         return engine.GenerateMove(board, player, level, timeout);
      }
   }
}
