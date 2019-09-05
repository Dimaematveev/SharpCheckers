using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Interfaces;
using Okorodudu.Checkers.Engine;


namespace Okorodudu.Checkers.Presenter
{
    /// <summary>
    /// BoardPresenter - Ведущий Совета
    /// The board presenter
    /// ведущий форума
    /// </summary>
    public class BoardPresenter
   {

        //TODO:
      private delegate void GenerateComputerMoveDelegate();

      private readonly PlayerInfo blackPlayerInfo = new PlayerInfo(Player.Black, false);
      private readonly PlayerInfo whitePlayerInfo = new PlayerInfo(Player.White, true);
      private readonly IBoardView view;
      private readonly IBoard board;
      private readonly IBoardRules boardRules;
      private PlayerInfo turn;

        /// <summary>
        /// Construct a board presenter that will manage the given board view
        /// Создайте докладчика, который будет управлять заданным видом доски
        /// </summary>
        /// <param name="view">
        /// The board view
        /// Вид доски
        /// </param>
        public BoardPresenter(IBoardView view)
      {
         this.view = view;
         this.board = new Checkerboard();
         this.boardRules = new CheckersRules();
         this.turn = blackPlayerInfo;

         boardRules.ResetBoard(board);
         view.SetBoardState(board.Copy());
      }

        /// <summary>
        /// SetComputer - Установить компьютер
        /// Set whether the given player is controlled by the computer
        /// Установить, управляется ли данный игрок компьютером
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        /// <param name="computer"><c>true</c> 
        /// if this player should be computer controlled and 
        ///  если этот плеер должен управляться компьютером и
        /// <c>false</c>
        /// if otherwise
        ///  если не так
        /// </param>
        public void SetComputer(Player player, bool computer)
      {
         PlayerInfo computerPlayer = GetPlayerInfo(player);
         computerPlayer.IsComputer = computer;
      }

        /// <summary>
        /// StartGame - Начать игру
        /// Start the game
        /// Начать игру
        /// </summary>
        public void StartGame()
      {
         this.StartGame(Player.Black);
      }


        /// <summary>
        /// StartGame - Начать игру
        /// Start the game and give the intial turn to the specified player
        /// Запустите игру и дайте начальный ход указанному игроку
        /// </summary>
        /// <param name="startingPlayer">
        /// The player with the starting move
        /// Игрок со стартовым ходом
        /// </param>
        public void StartGame(Player startingPlayer)
      {
         blackPlayerInfo.Engine.CancelProcessing();
         whitePlayerInfo.Engine.CancelProcessing();
         boardRules.ResetBoard(board);
         view.SetBoardState(board.Copy());
         view.ShowGameStart();
         this.turn = GetPlayerInfo(startingPlayer);
         this.GameStep();
      }

      private PlayerInfo GetPlayerInfo(Player player)
      {
         return (player == blackPlayerInfo.Player) ? blackPlayerInfo : whitePlayerInfo;
      }

      private void GameStep()
      {
         GameStep(null);
      }

      private void GameStep(int? startPosition)
      {
         if (!boardRules.IsGameOver(board, turn.Player))
         {
            view.LockPlayer(Player.Black, true);
            view.LockPlayer(Player.White, true);
            view.ShowPlayerChange(turn.Player);
            this.PromptMove(startPosition);
         }
         else
         {
            this.OnGameOver(BoardUtilities.GetOpponent(turn.Player), turn.Player);
         }
      }

        /// <summary>
        /// OnGameOver - Игра окончена
        /// Invoked when the game is over
        /// Вызывается, когда игра окончена
        /// </summary>
        /// <param name="winner">
        /// The winner
        /// Победитель
        /// </param>
        /// <param name="loser">
        /// The loser
        /// Неудачник
        /// </param>
        protected void OnGameOver(Player winner, Player loser)
      {
         if (winner != Player.None)
         {
            view.LockPlayer(Player.Black, true);
            view.LockPlayer(Player.White, true);
            view.ShowGameOver(winner, loser);
         }
      }



        //TODO:
      private void PromptMove(int? startPosition)
      {
         if (!turn.IsComputer)
         {
            if (!startPosition.HasValue)
            {
               view.LockPlayer(turn.Player, false);
               view.PromptMove(turn.Player);
            }
            else
            {
               view.LockPlayer(turn.Player, false);
               view.PromptMove(turn.Player, startPosition.Value);
            }
         }
         else
         {
            GenerateComputerMoveDelegate computerMoveDelegate = new GenerateComputerMoveDelegate(HandleComputerMove);
            computerMoveDelegate.BeginInvoke(null, null);
            //this.HandleComputerMove();
         }
      }


        //TODO:
      private void HandleComputerMove()
      {
         Move move = turn.GenerateMove(board);
         view.RenderMove(move, board.Copy());
      }


        //TODO:
      private void HandleInvalidMove(Move move, String message)
      {
         view.ShowInvalidMove(move, turn.Player, message);
         view.SetBoardState(board.Copy());
         PromptMove(null);
      }

        /// <summary>
        /// MakeMove - Сделать ход
        /// Attempt to make the specified move on the board
        /// Попытка сделать указанный ход на доске
        /// </summary>
        /// <param name="move">TODO:</param>
        public void MakeMove(Move move)
      {
         MakeMove(move, null);
      }

        /// <summary>
        ///  MakeMove - Сделать ход
        /// Attempt to make the specified move on the board starting at the given position if present
        /// Попытка сделать указанное движение на доске, начиная с заданной позиции, если присутствует
        /// </summary>
        /// <param name="move">
        /// The move
        /// Движение
        /// </param>
        /// <param name="startPosition">
        /// The starting position of the move if this move is a jump continuation
        /// Начальная позиция хода, если этот ход является продолжением прыжка
        /// </param>
        public void MakeMove(Move move, int? startPosition)
      {
         const int INVALID_POSITION = -1;
         int origin = move.Origin ?? INVALID_POSITION;

         if (origin == INVALID_POSITION)
         {
            HandleInvalidMove(move, "Move contains no steps");//ход не содержит шагов
            }
         else if ((startPosition.HasValue) && (origin != startPosition.Value))
         {
            HandleInvalidMove(move, "You must finish jump");//Вы должны закончить прыжок
            }
         else
         {
            Piece piece = board[origin];
            Player player = BoardUtilities.GetPlayer(piece);
            PlayerInfo playerInfo = GetPlayerInfo(player);
            MoveStatus moveStatus = MoveStatus.Illegal;
            move = boardRules.ResolveAmbiguousMove(board, move);

            if (piece == Piece.None)
            {
               HandleInvalidMove(move, "No piece selected");//Не выбрано ни одного предмета
                }
            else if (playerInfo != turn)
            {
               HandleInvalidMove(move, string.Format(CultureInfo.InvariantCulture, "Not {0}'s turn", playerInfo.Player.ToString()));//Не ход {0}
                }
            else if ((moveStatus = boardRules.IsValidMove(board, move, turn.Player)) == MoveStatus.Illegal)
            {
               HandleInvalidMove(move, string.Format(CultureInfo.InvariantCulture, "Invalid move: {0}", move));//  Неверный ход: {0}
                }
            else if (!boardRules.ApplyMove(board, move))
            {
               HandleInvalidMove(move, string.Format(CultureInfo.InvariantCulture, "Unable to apply move move: {0}", move));//     Невозможно применить ход перемещения: {0}
                }
            else
            {
               view.SetBoardState(board.Copy());

                    // swap turn if the player has a complete move (no jumps)
                    // меняем ход, если у игрока полный ход (без прыжков)
                    if (moveStatus == MoveStatus.Incomplete)
               {
                        // at this point the move was valid and hence must have a destination
                        // в этот момент перемещение было допустимым и, следовательно, должно иметь пункт назначения
                        view.SetMoveStartPosition(move.Destination.Value);
               }
               else
               {
                  view.SetMoveStartPosition(null);
                  view.LockPlayer(turn.Player, true);
                  this.SwapTurn();
               }
               this.GameStep();
            }
         }
      }

        //TODO:
      private void SwapTurn()
      {
         turn = GetOpponent(turn);
         view.SetMoveStartPosition(null);
      }

        //TODO:
      private PlayerInfo GetOpponent(PlayerInfo playerInfo)
      {
         return (playerInfo == blackPlayerInfo) ? whitePlayerInfo : blackPlayerInfo;
      }
   }
}
