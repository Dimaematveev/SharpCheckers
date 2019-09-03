using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    ///  BoardUtilities - Совет Утилиты
    /// Various board utility methods
    /// Различные утилиты для плат
    /// </summary>
    public static class BoardUtilities
   {
        /// <summary>
        /// IsMan - является человеком
        /// Is the given piece a man
        ///  Является ли данный кусок человеком
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        /// Кусок для проверки 
        /// </param>
        /// <returns><c>true</c>
        /// if the given piece is a man and 
        /// если данный кусок - мужчина, и 
        /// <c>false</c>  
        /// if otherwise
        /// , если в противном случае
        /// </returns>
        public static bool IsMan(Piece piece)
      {
         return ((piece == Piece.BlackMan) || (piece == Piece.WhiteMan));
      }

        /// <summary>
        /// IsKing - является королем
        /// Is the given piece a king
        /// Является ли данный кусок королем
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        /// Кусок для проверки
        /// </param>
        /// <returns><c>true</c> 
        /// if the given piece is a king and 
        /// если данный фрагмент является королем, и 
        /// <c>false</c> 
        /// if otherwise
        /// если в противном случае
        /// </returns>
        public static bool IsKing(Piece piece)
      {
         return ((piece == Piece.BlackKing) || (piece == Piece.WhiteKing));
      }

        /// <summary>
        /// IsBlack - является черным
        /// Is the given piece black
        /// Является ли данный кусок черным
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        /// Кусок для проверки
        /// </param>
        /// <returns><c>true</c> 
        /// if the given piece is a black and 
        /// если данный фрагмент черного цвета, и
        /// <c>false</c> 
        /// if otherwise
        /// если в противном случае 
        /// </returns>
        public static bool IsBlack(Piece piece)
      {
         return ((piece == Piece.BlackMan) || (piece == Piece.BlackKing));
      }

        /// <summary>
        /// IsWhite - является белым
        /// Is the given piece white
        ///  Является ли данный кусок белым
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        /// Кусок для проверки 
        /// </param>
        /// <returns><c>true</c> 
        /// if the given piece is a white and 
        /// если данный фрагмент белого цвета, и
        /// <c>false</c> 
        /// if otherwise
        ///  если в противном случае
        /// </returns>
        public static bool IsWhite(Piece piece)
      {
         return ((piece == Piece.WhiteMan) || (piece == Piece.WhiteKing));
      }

        /// <summary>
        /// IsPiece - является куском
        /// Tests if the given piece is an actual piece as opposed to an an empty piece (no piece).
        /// Проверяет, является ли данный кусок фактическим, а не пустым (без куска).
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        ///  Кусок для проверки 
        /// </param>
        /// <returns><c>true</c> 
        /// if the given piece is an actual piece and 
        ///  если данный фрагмент является фактическим, и 
        /// <c>false</c> 
        /// if otherwise
        /// если в противном случае 
        /// </returns>
        public static bool IsPiece(Piece piece)
      {
         return (piece != Piece.None);
      }

        /// <summary>
        /// IsEmpty - является пустым
        /// Tests if the given piece is empty (no piece).
        /// Проверяет, является ли данный фрагмент пустым (нет фрагмента).
        /// </summary>
        /// <param name="piece">
        /// The piece to test
        /// Кусок для проверки
        /// </param>
        /// <returns><c>true</c> 
        /// if the given piece is empty and 
        /// , если данный фрагмент пуст, и 
        /// <c>false</c> 
        /// if otherwise
        /// , если нет
        /// </returns>
        public static bool IsEmpty(Piece piece)
      {
         return (piece == Piece.None);
      }

        /// <summary>
        /// IsOwner - является Владелец
        /// Is the given player the owner of the specified piece
        /// Является ли данный игрок владельцем указанной фигуры
        /// </summary>
        /// <param name="player">
        /// The player to check ownership
        /// Игрок, который проверяет владение 
        /// </param>
        /// <param name="piece">
        /// The piece to check ownership
        /// Часть для проверки владения
        /// </param>
        /// <returns><c>true</c> 
        /// if the player owns the given piece and 
        /// если игроку принадлежит данный кусок, и 
        /// <c>false</c> 
        /// if otherwise
        ///  если в противном случае
        /// </returns>
        public static bool IsOwner(Player player, Piece piece)
      {
         if (piece == Piece.None)
         {
            return false;
         }

         Player pieceOwner = GetPlayer(piece);
         return (player == pieceOwner);
      }

        /// <summary>
        /// IsOpponentPiece - является Противником
        /// Is the given player the opponent of the specified piece
        /// Является ли данный игрок противником указанной фигуры
        /// </summary>
        /// <param name="player">
        /// The player
        /// Игрок
        /// </param>
        /// <param name="piece">
        /// The piece to check if opponent's piece
        /// Часть, чтобы проверить, является ли часть противника
        /// </param>
        /// <returns><c>true</c> 
        /// if the player is the oppponent of the given piece and 
        /// , если игрок является оппонентом данного куска, и
        /// <c>false</c> 
        /// if otherwise
        /// , если в противном случае 
        /// </returns>
        public static bool IsOpponentPiece(Player player, Piece piece)
      {
         if (piece == Piece.None)
         {
            return false;
         }

         return !IsOwner(player, piece);
      }

        /// <summary>
        /// AreOpponents - Противники
        /// Are the specified pieces opponents
        /// Являются ли указанные фигуры противниками
        /// </summary>
        /// <param name="piece1">
        /// The first piece to compare
        /// Первый кусок для сравнения
        /// </param>
        /// <param name="piece2">
        /// The second piece to compare
        /// Второй кусок для сравнения
        /// </param>
        /// <returns><c>true</c> 
        /// if the pieces are opponents of each other and false if otherwise.
        /// , если фигуры являются противниками друг друга, и false, если иное.
        /// </returns>
        public static bool AreOpponents(Piece piece1, Piece piece2)
      {
         if ((piece1 == Piece.None) || (piece2 == Piece.None))
         {
            return false;
         }

         return (GetPlayer(piece1) != GetPlayer(piece2));
      }

        /// <summary>
        /// GetOpponent - Получить оппонента
        /// Get the player that is the opponent of the specified piece
        ///  Получить игрока, который является противником указанной фигуры
        /// </summary>
        /// <param name="piece">
        /// The piece to get opponent for
        /// Часть, чтобы получить противника для 
        /// </param>
        /// <returns>
        /// The opponent of the given piece
        /// Оппонент данной фигуры
        /// </returns>
        public static Player GetOpponent(Piece piece)
      {
         Player player = GetPlayer(piece);
         return GetOpponent(player);
      }

        /// <summary>
        /// GetOpponent - Получить оппонента
        /// Get the opponent of the given player
        /// Получить противника данного игрока
        /// </summary>
        /// <param name="player">
        /// The player to get the opponent for
        ///  Игрок, чтобы получить противника за
        /// </param>
        /// <returns>
        /// The opponent of the given player
        /// Оппонент данного игрока 
        /// </returns>
        public static Player GetOpponent(Player player)
      {
         if (player == Player.None)
         {
            return player;
         }

         return (player == Player.Black) ? Player.White : Player.Black;
      }

        /// <summary>
        /// GetPlayer - Получить игрока
        /// Get the player that owns the given piece
        ///  Получить игрока, которому принадлежит данный кусок
        /// </summary>
        /// <param name="piece">
        /// The piece to get the owner for
        /// Часть, чтобы получить владельца для
        /// </param>
        /// <returns>
        /// The player that owns the given piece.  If the piece is empty/invalid, then neither player is returned.
        /// Игрок, которому принадлежит данный кусок. Если фигура пуста / недействительна, то ни один игрок не возвращается. 
        /// </returns>
        public static Player GetPlayer(Piece piece)
      {
         if (IsBlack(piece))
         {
            return Player.Black;
         }
         else if (IsWhite(piece))
         {
            return Player.White;
         }
         else
         {
            return Player.None;
         }
      }
   }
}
