using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
    /// <summary>
    /// Правила перемещения шашек
    /// </summary>
    static class CheckerMoveRules
   {
        /// <summary>
        /// GetWalks - Получить прогулки
        /// Get availables walks
        /// Получить доступные прогулки
        /// </summary>
        /// <param name="moves">
        /// Walk moves will be added to the collection
        /// Ходовые ходы будут добавлены в коллекцию
        /// </param>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="player">
        /// the player
        /// игрок
        /// </param>
        private static void GetWalks(ICollection<Move> moves, IBoard board, Player player)
      {
         for (int pos = 1; pos <= BoardConstants.LightSquareCount; pos++)
         {
            Piece piece = board[pos];
            Location location = Location.FromPosition(pos);
            int row = location.Row;
            int col = location.Col;

            if (BoardUtilities.IsOwner(player, piece))
            {
               int forwardDirection = (BoardUtilities.IsBlack(piece)) ? 1 : -1;
               int backwardDirection = (!BoardUtilities.IsKing(piece)) ? 0 : (BoardUtilities.IsBlack(piece)) ? -1 : 1;

               GetWalks(moves, board, row, col, forwardDirection, -1);
               GetWalks(moves, board, row, col, forwardDirection, 1);

               if (backwardDirection != 0)
               {
                  GetWalks(moves, board, row, col, backwardDirection, -1);
                  GetWalks(moves, board, row, col, backwardDirection, 1);
               }
            }
         }
      }

        /// <summary>
        /// GetWalks - Получить прогулки
        /// Get available walks
        /// Получить доступные прогулки
        /// </summary>
        /// <param name="moves">
        /// Moves added to this collection
        /// Ходы добавлены в эту коллекцию
        /// </param>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="row">
        /// the row the piece is on
        /// строка, на которой стоит произведение
        /// </param>
        /// <param name="col">
        /// the column the piece is on
        /// колонка, на которой находится произведение
        /// </param>
        /// <param name="verticalDirection">
        /// the vertical direction to move in
        /// вертикальное направление движения
        /// </param>
        /// <param name="horizontalDirection">
        /// the horizontal direction to move in
        /// горизонтальное направление движения
        /// </param>
        private static bool GetWalks(ICollection<Move> moves, IBoard board, int row, int col, int verticalDirection, int horizontalDirection)
      {
         int newRow = row + verticalDirection;
         int newCol = col + horizontalDirection;

         if ((InBounds(newRow, newCol, board)) && (BoardUtilities.IsEmpty(board[newRow, newCol])))
         {
            //space is empty
            moves.Add(new Move(Location.ToPosition(row, col), Location.ToPosition(newRow, newCol)));
         }

         return false;
      }


        /// <summary>
        /// CanWalk - Может ходить
        /// Check if piece at the positoin can move
        /// Проверьте, может ли кусок в позиции может двигаться
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="row">
        /// the row the piece is on
        /// строка, на которой стоит произведение
        /// </param>
        /// <param name="col">
        /// the column the piece is on
        /// колонка, на которой находится произведение
        /// </param>
        /// <returns><code>true</code>
        /// if the piece can move
        /// если кусок может двигаться
        /// </returns>
        private static bool CanWalk(IBoard board, int row, int col)
      {
         Piece piece = board[row, col];

         if (!BoardUtilities.IsPiece(piece))
         {
            return false;
         }

         int forwardDirection = (BoardUtilities.IsBlack(piece)) ? 1 : -1;
         int backwardDirection = (!BoardUtilities.IsKing(piece)) ? 0 : (BoardUtilities.IsBlack(piece)) ? -1 : 1;

         return CanWalk(board, row, col, forwardDirection, -1)
               || CanWalk(board, row, col, forwardDirection, 1)
               || (backwardDirection != 0
                     && (
                        CanWalk(board, row, col, backwardDirection, -1)
                        || CanWalk(board, row, col, backwardDirection, 1)
                     )
               );
      }

        /// <summary> 
        /// CanWalk - Может ходить
        /// Check if piece can move
        /// Проверьте, может ли кусок двигаться
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="row">
        /// the row the piece is on
        /// строка, на которой стоит произведение
        /// </param>
        /// <param name="col">
        /// the column the piece is on
        /// колонка, на которой находится произведение
        /// </param>
        /// <param name="verticalDirection">
        /// the vertical direction to move in
        /// вертикальное направление движения
        /// </param>
        /// <param name="horizontalDirection">
        /// the horizontal direction to move in
        /// горизонтальное направление движения
        /// </param>
        /// <returns><code>true</code> 
        /// if the piece can move
        /// если кусок может двигаться
        /// </returns>
        private static bool CanWalk(IBoard board, int row, int col, int verticalDirection, int horizontalDirection)
      {
         int newRow = row + verticalDirection;
         int newCol = col + horizontalDirection;

         if (!InBounds(newRow, newCol, board))
         {//not within board bounds
            return false;
         }

         if (BoardUtilities.IsEmpty(board[newRow, newCol]))
         {//space is empty
            return true;
         }

         return false;
      }

        /// <summary>
        /// CanJump - Может прыгать
        /// Check the player jump any pieces on the board
        /// Проверьте игрока, прыгайте любые фигуры на доске
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="player">
        /// the player with the turn
        /// игрок с терном
        /// </param>
        /// <returns><code>true</code> 
        /// if the player can make a jump
        /// если игрок может сделать прыжок
        /// </returns>
        static bool CanJump(IBoard board, Player player)
      {
         for (int row = 0; row < board.Rows; row++)
         {
            for (int col = 0; col < board.Cols; col++)
            {
               if (BoardUtilities.IsPiece(board[row, col]) &&
                     (BoardUtilities.GetPlayer(board[row, col]) == player) &&
                     CanJump(board, row, col)
               )
               {
                  return true;
               }
            }
         }

         return false;
      }

        /// <summary>
        /// CanJump - Может прыгать
        /// Check if piece at position can jump
        /// Проверьте, может ли кусок в положении прыгать
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="row">
        /// the row the piece is on
        /// строка, на которой стоит произведение
        /// </param>
        /// <param name="col">
        /// the column the piece is on
        /// колонка, на которой находится произведение
        /// </param>
        /// <returns><code>true</code> 
        /// if the piece can jump
        /// если кусок может прыгать
        /// </returns>
        static bool CanJump(IBoard board, int row, int col)
      {
         Piece piece = board[row, col];

         if (!BoardUtilities.IsPiece(piece))
         {
            return false;
         }

         int forwardDirection = (BoardUtilities.IsBlack(piece)) ? 1 : -1;
         int backwardDirection = (!BoardUtilities.IsKing(piece)) ? 0 : (BoardUtilities.IsBlack(piece)) ? -1 : 1;

         return CanJump(board, row, col, forwardDirection, -1)
               || CanJump(board, row, col, forwardDirection, 1)
               || (backwardDirection != 0
                     && (
                        CanJump(board, row, col, backwardDirection, -1)
                        || CanJump(board, row, col, backwardDirection, 1)
                     )
               );
      }

        /// <summary>
        /// CanJump - Может прыгать
        /// Check the piece at the postion can jump any pieces on the board
        /// Проверьте фигуру в позиции, можете прыгать любые фигуры на доске
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="row">
        /// the row the piece is on
        /// строка, на которой стоит произведение
        /// </param>
        /// <param name="col">
        /// the column the piece is on
        /// колонка, на которой находится произведение
        /// </param>
        /// <param name="verticalDirection">
        /// the vertical direction to move in
        /// вертикальное направление движения
        /// </param>
        /// <param name="horizontalDirection">
        /// the horizontal direction to move in
        /// горизонтальное направление движения
        /// </param>
        /// <returns><code>true</code> 
        /// if the piece can make a jump
        /// если кусок может совершить прыжок
        /// </returns>
        private static bool CanJump(IBoard board, int row, int col, int verticalDirection, int horizontalDirection)
      {
         int newRow = row + verticalDirection;
         int newCol = col + horizontalDirection;
         Piece piece = board[row, col];

         if (!InBounds(newRow, newCol, board))
         {//not within board bounds
            return false;
         }

         if (BoardUtilities.AreOpponents(piece, board[newRow, newCol]))
         {// check if you can jump enemy
            int endRow = newRow + verticalDirection;
            int endCol = newCol + horizontalDirection;
            return (InBounds(endRow, endCol, board) && BoardUtilities.IsEmpty(board[endRow, endCol]));
         }

         return false;
      }

        /// <summary>
        /// HasMovesAvailable - Есть фильмы в наличии
        /// Check if the player has any more moves
        /// Проверьте, есть ли у игрока больше ходов
        /// </summary>
        /// <param name="board">
        /// the state of the board
        /// состояние правления
        /// </param>
        /// <param name="player">
        /// the player with the turn
        /// игрок с терном
        /// </param>
        /// <returns><code>true</code> 
        /// if the player can make a move
        /// если игрок может сделать ход
        /// </returns>
        public static bool HasMovesAvailable(IBoard board, Player player)
      {
         for (int row = 0; row < board.Rows; row++)
         {
            for (int col = 0; col < board.Cols; col++)
            {
               if (BoardUtilities.IsPiece(board[row, col]) && (player == BoardUtilities.GetPlayer(board[row, col])))
               {
                  if (CanWalk(board, row, col))
                  {
                     return true;
                  }
                  else if (CanJump(board, row, col))
                  {
                     return true;
                  }
               }
            }
         }

         return false;
      }

        /// <summary>
        /// InBounds - В границах
        /// Check if the location is within the board bounds
        /// Проверьте, находится ли локация в границах доски
        /// </summary>
        /// <param name="row">
        /// the block row
        /// ряд блока
        /// </param>
        /// <param name="col">
        /// the block column
        /// колонка блока
        /// </param>
        /// <param name="board">
        /// The board to check bounds with
        /// Доска для проверки границ
        /// </param>
        /// <returns><code>true</code> 
        /// if the location is within the board bounds
        /// если локация находится в пределах досок
        /// </returns>
        private static bool InBounds(int row, int col, IBoard board)
      {
         return ((row >= 0) && (row < board.Rows) && (col >= 0) && (col < board.Cols));
      }


        /// <summary>
        /// IsMoveLegal - Это законно
        /// Is the move legal. This move doesn't check multiple jumps.
        /// Это законный ход. Этот ход не проверяет несколько прыжков.
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="move">
        /// the move to test
        /// движение к тесту
        /// </param>
        /// <param name="player">
        /// the player with the turn
        /// игрок с поворотом
        /// </param>
        /// <returns>
        /// LEGAL if the move is legal.  Illegal if the move is not legal.  INCOMPLETE if the move results in a jump.
        /// ЗАКОННЫЙ, если движение законно. Незаконный, если движение не законно. НЕПОЛНЫЙ, если движение приводит к скачку.
        /// </returns>
        public static MoveStatus IsMoveLegal(IBoard board, Move move, Player player)
      {
         MoveStatus status = MoveStatus.Illegal;

         board = board.Copy();
         for (int i = 0; i < move.Count - 1; i++)
         {
            status = IsMoveLegal(
               board,
               move.GetLocation(i).Row, move.GetLocation(i).Col,
               move.GetLocation(i + 1).Row, move.GetLocation(i + 1).Col,
               player
            );

            if (status == MoveStatus.Illegal)
            {
               return MoveStatus.Illegal;
            }
            else if ((status == MoveStatus.Legal) && (i != move.Count - 2))
            {
               return MoveStatus.Illegal;
            }

            UpdateBoard(board, new Move(move.GetLocation(i), move.GetLocation(i + 1)));
         }

         if ((status == MoveStatus.Incomplete) && (!CanJump(board, move.DestinationLocation.Row, move.DestinationLocation.Col)))
         {
            status = MoveStatus.Legal;
         }

         return status;
      }

        /// <summary>
        /// IsMoveLegal - Это законно
        /// Is the move legal. This move doesn't check multiple jumps.
        /// Это законный ход. Этот ход не проверяет несколько прыжков.
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="startRow">
        /// the start row
        /// начальный ряд
        /// </param>
        /// <param name="startCol">
        /// the start col
        /// начальный столб
        /// </param>
        /// <param name="endRow">
        /// the end row
        /// конец строки
        /// </param>
        /// <param name="endCol">
        /// the end col
        /// конец колонки
        /// </param>
        /// <param name="player">
        /// the player with the turn
        /// игрок с поворотом
        /// </param>
        /// <returns>
        /// LEGAL if the move is legal.  Illegal if the move is not legal.  INCOMPLETE if the move results in a jump.
        /// ЗАКОННЫЙ, если движение законно. Незаконный, если движение не законно. НЕПОЛНЫЙ, если движение приводит к скачку.
        /// </returns> 
        private static MoveStatus IsMoveLegal(IBoard board, int startRow, int startCol, int endRow, int endCol, Player player)
      {
         if (!InBounds(startRow, startCol, board) || !InBounds(endRow, endCol, board))
         {
                // out of board bounds
                //из границ правления
                return MoveStatus.Illegal;
         }

         Piece startPosition = board[startRow, startCol];
         Piece endPosition = board[endRow, endCol];

         if ((player == Player.Black && !BoardUtilities.IsBlack(startPosition)) || (player == Player.White && !BoardUtilities.IsWhite(startPosition)))
         {
                // wrong player attempting to make a move
                //неправильный игрок, пытающийся сделать движение
                return MoveStatus.Illegal;
         }
         else if (!BoardUtilities.IsEmpty(endPosition))
         {// destination is not empty
          //место назначения не пусто
                return MoveStatus.Illegal;
         }

         int forwardDirection = (BoardUtilities.IsBlack(startPosition)) ? 1 : -1;
         int backwardDirection = (!BoardUtilities.IsKing(startPosition)) ? 0 : (BoardUtilities.IsBlack(startPosition)) ? -1 : 1;

            // check for single step along vertical axis
            //проверьте на единственный шаг вдоль вертикальной оси
            if (Math.Abs(endRow - startRow) == 1)
         {//possible walk made
          //возможная ходьба сделана
          // check if we took a walk when a jump was available
          //проверьте, прогулялись ли мы, когда скачок был доступен
                if (CanJump(board, player))
            {
               return MoveStatus.Illegal;
            }

                // one step along the horizontal axis and proper vertical direction movement
                // один шаг по горизонтальной оси и правильное вертикальное движение
                // men can't go backwards but kings can
                // люди не могут идти назад, но короли могут
                if ((Math.Abs(endCol - startCol) == 1) && (startRow + forwardDirection == endRow || startRow + backwardDirection == endRow))
            {
               return MoveStatus.Legal;
            }
         }
         else if (Math.Abs(endRow - startRow) == 2)
         {// possible jump made
          // возможный прыжок сделан
                int jumpedRow = (endRow + startRow) / 2;
            int jumpedCol = (endCol + startCol) / 2;

            if (BoardUtilities.IsOpponentPiece(player, board[jumpedRow, jumpedCol]))
            {
                    // one step along the horizontal axis and proper vertical direction movement
                    // один шаг по горизонтальной оси и правильное вертикальное движение
                    // men can't go backwards but kings can
                    // люди не могут идти назад, но короли могут
                    if ((Math.Abs(endCol - startCol) == 2) && (startRow + forwardDirection * 2 == endRow || startRow + backwardDirection * 2 == endRow))
               {
                  return MoveStatus.Incomplete;
               }
            }
         }

         return MoveStatus.Illegal;
      }

        /// <summary>
        /// UpdateBoard - Обновление доски
        /// update the board with the given move
        /// обновить доску заданным ходом
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="move">
        /// the move (which can be encoded moves containing multiple jumps)
        /// ход (который может быть закодирован ходами, содержащими несколько прыжков)
        /// </param>
        public static void UpdateBoard(IBoard board, Move move)
      {
         if (move == null)
         {
            throw new ArgumentException("Can't apply NULL move");
         }

         for (int i = 0; i < move.Count - 1; i++)
         {
            PerformMove(
               board,
               move.GetLocation(i).Row, move.GetLocation(i).Col,
               move.GetLocation(i + 1).Row, move.GetLocation(i + 1).Col
            );
         }
      }

        /// <summary>
        /// PerformMove - Выполнить движение
        /// Perform a single move
        /// Выполнить один ход
        /// </summary>
        /// <param name="board">
        /// the board state
        /// состояние правления
        /// </param>
        /// <param name="startRow">
        /// the start row
        /// начальный ряд
        /// </param>
        /// <param name="startCol">
        /// the start column
        /// начальный столбец
        /// </param>
        /// <param name="endRow">
        /// the end row
        /// конец строки
        /// </param>
        /// <param name="endCol">
        /// the end column
        /// конец столбца
        /// </param>
        private static MoveStatus PerformMove(IBoard board, int startRow, int startCol, int endRow, int endCol)
      {
         MoveStatus moveStatus = IsMoveLegal(board, startRow, startCol, endRow, endCol, BoardUtilities.GetPlayer(board[startRow, startCol]));

         if (moveStatus != MoveStatus.Illegal)
         {
            if (Math.Abs(endRow - startRow) == 1)
            {//walk
             // ходить
                    board[endRow, endCol] = board[startRow, startCol];
               board[startRow, startCol] = Piece.None;
            }
            else
            {// jump piece
             // прыжок
                    int jumpedRow = (startRow + endRow) / 2;
               int jumpedCol = (startCol + endCol) / 2;
               board[jumpedRow, jumpedCol] = Piece.None;
               board[endRow, endCol] = board[startRow, startCol];
               board[startRow, startCol] = Piece.None;
            }

            if ((moveStatus == MoveStatus.Incomplete) && (!CanJump(board, endRow, endCol)))
            {
               moveStatus = MoveStatus.Legal;
            }

                // check if the piece is now a king
                // проверяем, является ли фигура королем
            if ((board[endRow, endCol] == Piece.BlackMan) && (endRow == BoardConstants.Rows - 1))
            {
               board[endRow, endCol] = Piece.BlackKing;
            }
            else if ((board[endRow, endCol] == Piece.WhiteMan) && (endRow == 0))
            {
               board[endRow, endCol] = Piece.WhiteKing;
            }
         }

         return moveStatus;
      }

        /// <summary>
        /// GetCaptures - Получить захваты
        /// Generate captures list for the piece at the given location
        /// Создать список снимков для произведения в заданном месте
        /// </summary>
        /// <param name="moves">
        /// capture moves will be added to the collection
        /// захватить движения будут добавлены в коллекцию
        /// </param>
        /// <param name="board">
        /// the board state
        ///  состояние правления
        /// </param>
        /// <param name="row">
        /// the row of the piece
        /// строка произведения
        /// </param>
        /// <param name="col">
        /// the column of the piece
        /// столбец произведения
        /// </param>
        private static void GetCaptures(ICollection<Move> moves, IBoard board, int row, int col)
      {
         Piece piece = board[row, col];
         IList<Location> locations = new List<Location>();
         locations.Add(new Location(row, col));

         if (BoardUtilities.IsKing(piece) || BoardUtilities.IsBlack(piece))
         {// go up
          //подниматься
                GetCaptures(moves, new List<Location>(locations), board.Copy(), piece, row, col, -1, 1); // right
            GetCaptures(moves, new List<Location>(locations), board.Copy(), piece, row, col, 1, 1); // left
         }
         if (BoardUtilities.IsKing(piece) || BoardUtilities.IsWhite(piece))
         {// go down
          // опускаться
                GetCaptures(moves, new List<Location>(locations), board.Copy(), piece, row, col, -1, -1); // right
            GetCaptures(moves, new List<Location>(locations), board, piece, row, col, 1, -1); // left
         }
      }

        /// <summary>
        /// GetCaptures - Получить захваты
        /// Generate captures list for the piece at the given location
        /// Создать список снимков для произведения в заданном месте
        /// </summary>
        /// <param name="moves">
        /// stores the list of moves generated
        /// сохраняет список сгенерированных ходов
        /// </param>
        /// <param name="locations">
        /// list of parent locations
        /// список родительских локаций
        /// </param>
        /// <param name="board">
        /// the board state
        ///  состояние правления
        /// </param>
        /// <param name="piece">
        /// the piece
        /// кусок
        /// </param>
        /// <param name="row">
        /// the row of the piece
        ///  строка произведения
        /// </param>
        /// <param name="col">
        /// the column of the piece
        /// столбец произведения
        /// </param>
        /// <param name="dx">
        /// the horizontal direction
        ///  горизонтальное направление
        /// </param>
        /// <param name="dy">
        /// the vertical direction
        /// вертикальное направление
        /// </param>
        /// <returns><c>true</c> 
        /// if capture available
        /// если захват доступен
        /// </returns>
        private static bool GetCaptures(ICollection<Move> moves, IList<Location> locations, IBoard board, Piece piece, int row, int col, int dx, int dy)
      {
         int endRow = row + dy * 2;
         int endCol = col + dx * 2;
         int jumpRow = row + dy;
         int jumpCol = col + dx;

            // jump available
            // прыжок доступен
            if (InBounds(endRow, endCol, board) && BoardUtilities.AreOpponents(piece, board[jumpRow, jumpCol]) && BoardUtilities.IsEmpty(board[endRow, endCol]))
         {
            locations.Add(new Location(endRow, endCol));
            board[row, col] = Piece.None;
            board[jumpRow, jumpCol] = Piece.None;
            board[endRow, endCol] = piece;

            bool captureAvailable = false;
            int[] DIRECTIONS = { -1, 1 }; // {down/right, up/left} // {вниз / вправо, вверх / влево}
            int Y_START_INDEX = (BoardUtilities.IsKing(piece) || BoardUtilities.IsWhite(piece)) ? 0 : 1;
            int Y_END_INDEX = (BoardUtilities.IsKing(piece) || BoardUtilities.IsBlack(piece)) ? 1 : 0;

            for (int idxY = Y_START_INDEX; idxY <= Y_END_INDEX; idxY++)
            {
               for (int idxX = 0; idxX < DIRECTIONS.Length; idxX++)
               {
                  bool result = GetCaptures(
                     moves, new List<Location>(locations),
                     board.Copy(), piece,
                     endRow, endCol, DIRECTIONS[idxX], DIRECTIONS[idxY]
                  );
                  captureAvailable = captureAvailable || result;
               }
            }


            if ((!captureAvailable) && (locations.Count > 1))
            {
               Move move = new Move();
               foreach (Location location in locations)
               {
                  move.AddMoves(location);
               }

               moves.Add(move);
            }

            return true;
         }
         else
         {
            return false;
         }
      }









      public static void GetCaptures(ICollection<Move> moves, IBoard board, Player player)
      {
            // Get captures
            // Получить снимки
            for (int pos = 1; pos <= BoardConstants.LightSquareCount; pos++)
         {
            if (BoardUtilities.IsOwner(player, board[pos]))
            {
               Location location = Location.FromPosition(pos);
               GetCaptures(moves, board.Copy(), location.Row, location.Col);
            }
         }
      }
        //Получить доступные ходы
        public static ICollection<Move> GetAvailableMoves(IBoard board, Player player)
      {
         List<Move> moves = new List<Move>();
         GetCaptures(moves, board, player);

         if ((moves == null) || (moves.Count == 0))
         {
            GetWalks(moves, board, player);
         }

         return moves;
      }


        /// <summary>
        /// ResolveAmbiguousMove -   Решить неоднозначное движение
        /// Attempt to resolve ambiguous jump move.  The longest move matching the first 
        /// location and last location is selected.
        ///  Попытка разрешить неоднозначное прыжковое движение. Самый длинный ход, соответствующий первому и 
        ///  последнему местоположению, выбран.
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
        /// если движение может быть решено
        /// </returns>
        public static Move ResolveAmbiguousMove(IBoard board, Move move)
      {
         if (move.Count < 2)
         {
                // can't resolve moves with less than 2 locations in path
                // не можем разрешить ходы с менее чем 2 местоположениями в пути
                return move;
         }

         Location startLocation = move.OriginLocation;
         Location endLocation = move.DestinationLocation;
         ICollection<Move> possibleMoves = new List<Move>();
         GetCaptures(possibleMoves, board.Copy(), startLocation.Row, startLocation.Col);

         Move bestMove = null;
         foreach (Move possibleMove in possibleMoves)
         {
            Location possibleStart = possibleMove.OriginLocation;
            Location possibleEnd = possibleMove.DestinationLocation;

            if (startLocation.Equals(possibleStart) && endLocation.Equals(possibleEnd))
            {
               if ((bestMove == null) || (possibleMove.Count > bestMove.Count))
               {
                  bestMove = possibleMove;
               }
            }
         }

         return (bestMove != null) ? bestMove : move;
      }
   }
}
