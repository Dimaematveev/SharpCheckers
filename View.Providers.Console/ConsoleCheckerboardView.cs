using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Interfaces;
using Okorodudu.Checkers.Presenter;
using System.Threading;
using System.Globalization;

namespace Okorodudu.Checkers.View.Providers.Console
{
    /// <summary>
    /// ConsoleCheckerboardView - вид шахматной доски консоли
    /// Checkers application in console mode
    /// Приложение для проверки в режиме консоли
    /// </summary>
    public class ConsoleCheckerboardView : IBoardView, IDisposable
   {
        //TODO:
      private readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
      private readonly BoardPresenter presenter;
      private readonly IBoard board = new Checkerboard();
      private readonly TextWriter writer;
      private readonly TextReader reader;
      private int? startPosition = null;

        /// <summary>
        /// Construct checkers game in console I/O mode
        /// Построить игру в шашки в режиме консольного ввода-вывода
        /// </summary>
        public ConsoleCheckerboardView() : this(System.Console.Out, System.Console.In)
      {
      }

        /// <summary>
        /// Construct checkers game to use the given reader and writer for I/O
        /// Построить игру в шашки для использования данного читателя и писателя для ввода / вывода
        /// </summary>
        /// <param name="writer">
        /// Used to write game play information to user
        /// Используется для записи информации об игровом процессе пользователю
        /// </param>
        /// <param name="reader">
        /// Used to get input from user
        /// Используется для получения ввода от пользователя
        /// </param>
        public ConsoleCheckerboardView(TextWriter writer, TextReader reader)
      {
         this.writer = writer;
         this.reader = reader;
         this.presenter = new BoardPresenter(this);
      }

        /// <summary>
        /// run - бежать 
        /// Run the app
        /// Запустить приложение
        /// </summary>
        private void run()
      {
         this.presenter.StartGame();

            // Keep application running even if there's only a background thread working
            // Поддерживаем работу приложения, даже если работает только фоновый поток
            autoResetEvent.WaitOne();
      }

        /// <summary>
        /// Dispose - Утилизировать 
        /// Dispose this object
        /// Утилизировать этот объект
        /// </summary>
        public void Dispose()
      {
         this.Dispose(true);
         GC.SuppressFinalize(this);
      }

        /// <summary>
        /// Dispose - Утилизировать 
        /// Dispose this object
        /// Утилизировать этот объект
        /// </summary>
        /// <param name="disposing"><c>true</c> 
        /// to release both managed and unmanaged resources; 
        /// освободить как управляемые, так и неуправляемые ресурсы;
        /// <c>false</c> 
        /// to release only unmanaged resources
        /// освободить только неуправляемые ресурсы
        /// </param>
        protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            IDisposable disposableAutoResetEvent = this.autoResetEvent as IDisposable;
            if (disposableAutoResetEvent != null)
            {
               disposableAutoResetEvent.Dispose();
            }
         }
      }

        /// <summary>
        /// ShowGameStart -  Показать начало игры
        /// Show the start message for the game
        /// Показать стартовое сообщение к игре
        /// </summary>
        public void ShowGameStart()
      {
         writer.WriteLine("Game started");
      }

        /// <summary>
        /// SetBoardState - Установить состояние правления
        /// Set the board state to the state of the specified board
        /// Установить состояние платы в состояние указанной доски
        /// </summary>
        /// <param name="board">
        /// The board to copy the state of
        /// Доска для копирования состояния
        /// </param>
        public void SetBoardState(IBoard board)
      {
         this.board.Copy(board);
      }

        /// <summary>
        /// ShowPlayerChange -Показать изменения игрока 
        /// Indicate to the user the player with the current turn
        /// Укажите пользователю игрока с текущим ходом
        /// </summary>
        /// <param name="turn">TODO:</param>
        public void ShowPlayerChange(Player turn)
      {
         writer.WriteLine("{0}'s turn", turn.ToString());
         BoardTrace.DrawBoard(board, this.writer);
      }

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
        public void SetMoveStartPosition(int? position)
      {
         this.startPosition = position;
      }

        /// <summary>
        /// LockPlayer - Блокировка игрока
        /// Allow or disallow the given player from moving
        ///  Разрешить или запретить данному игроку двигаться
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        /// <param name="locked"> If <c>true</c>,
        /// the player is prevented from moving.  If otherwise, the player is allowed to move
        ///  игрок не может двигаться. Если иначе, игрок может двигаться
        /// </param>
        public void LockPlayer(Player player, bool locked)
      {
      }

        /// <summary>
        /// PromptMove - Быстрое движение
        /// Prompt the given player to make move
        /// Предложите данному игроку сделать ход
        /// </summary>
        /// <param name="player">
        /// The player to prompt move for
        /// Плеер, который подскажет ход
        /// </param>
        public void PromptMove(Player player)
      {
         writer.WriteLine("Select move ({0}):", player.ToString());
         OnMoveInput(MoveReader.ReadMove(reader, writer, true));
      }

        /// <summary>
        /// PromptMove -  Быстрое движение 
        /// Prompt the player to make a move at the specified position.  This indicates that a move must be completed.
        /// Предложите игроку сделать ход в указанной позиции. Это указывает на то, что движение должно быть завершено.
        /// </summary>
        /// <param name="player">
        /// The player to prompt
        /// Плеер подскажет
        /// </param>
        /// <param name="position">
        /// The position at which the player should start the move from
        ///  Позиция, с которой игрок должен начать движение с
        /// </param>
        public void PromptMove(Player player, int position)
      {
         writer.WriteLine("Finish move ({0}) for {1}:", player.ToString(), position.ToString(CultureInfo.CurrentCulture));
         OnMoveInput(MoveReader.ReadMove(reader, writer, true));
      }

        /// <summary>
        /// RenderMove - рендер двигаться
        /// Render the given move
        /// Отрисовать данный ход
        /// </summary>
        /// <param name="move">
        /// The move to render
        /// Ход рендеринга
        /// </param>
        /// <param name="after">
        /// The state the board should be in after the move is rendered
        /// Состояние, в котором доска должна быть после рендеринга хода
        /// </param>
        public void RenderMove(Move move, IBoard after)
      {
         board.Copy(after);
         writer.WriteLine("Move made: {0}", move.ToString());
         presenter.MakeMove(move);
      }

        /// <summary>
        /// ShowInvalidMove - Показать неверное движение 
        /// Indicate to the user that an invalid move was 
        /// Укажите пользователю, что был неверный ход
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
        /// Сообщение, указывающее на проблему с переездом
        /// </param>
        public void ShowInvalidMove(Move move, Player player, string message)
      {
         writer.WriteLine("Invalid move: " + message);
      }

        /// <summary>
        /// ShowGameOver - Показать игру окончена
        /// Indicate that the game is over
        ///  Укажите, что игра окончена
        /// </summary>
        /// <param name="winner">
        /// The winner of the match
        /// Победитель матча
        /// </param>
        /// <param name="loser">
        /// The loser of the match
        /// Проигравший
        /// </param>
        public void ShowGameOver(Player winner, Player loser)
      {
         writer.WriteLine("GAME OVER");
         writer.WriteLine("{0} Wins", winner.ToString());
      }

        /// <summary>
        /// OnMoveInput -  При перемещении ввода
        /// Invoked when a move is made
        /// Вызывается, когда сделан ход
        /// </summary>
        /// <param name="move">TODO:</param>
        protected virtual void OnMoveInput(Move move)
      {
         presenter.MakeMove(move, startPosition);
      }

        /// <summary>
        /// Main - Главный
        /// The main entry point into the application
        /// Основная точка входа в приложение
        /// </summary>
        public static void Main()
      {
         ConsoleCheckerboardView app = new ConsoleCheckerboardView();
         app.run();
      }
   }
}
