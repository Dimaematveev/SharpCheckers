using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Location - Место нахождения
    /// A location on the checkers board
    /// Место на доске шашек
    /// </summary>
    public class Location
   {
      private int row;
      private int col;

        /// <summary>
        /// Constructs and initializes a location at the origin (0, 0) of the board
        ///  Создает и инициализирует местоположение в начале (0, 0) доски 
        /// </summary>
        public Location()
         : this(0, 0)
      {
      }

        /// <summary>
        /// Constructor
        /// Конструктор
        /// </summary>
        /// <param name="row">
        /// the row on the board
        ///  строка на доске
        /// </param>
        /// <param name="col">
        /// the col on the board
        /// столбец на доске
        /// </param>
        public Location(int row, int col)
      {
         this.row = row;
         this.col = col;
      }

        /// <summary>
        /// Row - строка
        /// Get or set the row
        /// Получить или установить строку
        /// </summary>
        public int Row
      {
         get { return row; }
         set { row = value; }
      }

        /// <summary>
        /// Col - столбец
        /// Get or set the column
        /// Получить или установить столбец
        /// </summary>
        public int Col
      {
         get { return col; }
         set { col = value; }
      }

        /// <summary>
        /// ToPosition -   К позиции
        /// Convert to board notation position
        /// Преобразовать в положение обозначения доски
        /// </summary>
        /// <returns>
        /// The board notation position corresponding to this location
        /// Позиция обозначения доски, соответствующая этому расположению
        /// </returns>
        public int ToPosition()
      {
         return ToPosition(this);
      }

        /// <summary>
        /// ToPosition -   К позиции
        /// Convert the given location to a board notation location
        /// Преобразовать указанное местоположение в местоположение обозначения доски
        /// </summary>
        /// <param name="location">
        /// The location to get conversion for
        ///  Местоположение для получения конверсии для 
        /// </param>
        /// <returns>
        /// The board notation position corresponding to the given location
        ///  Позиция обозначения доски, соответствующая данному местоположению
        /// </returns>
        public static int ToPosition(Location location)
      {
         return ToPosition(location.row, location.col);
      }

        /// <summary>
        /// ToPosition -   К позиции
        /// Convert location to the square position
        /// Преобразовать местоположение в квадратную позицию
        /// </summary>
        /// <param name="row">
        /// the block row
        /// строка блока
        /// </param>
        /// <param name="col">
        /// the block column
        ///  столбец блока
        /// </param>
        /// <returns>
        /// the position for the given location
        /// позиция для данного местоположения
        /// </returns>
        public static int ToPosition(int row, int col)
      {
         if (col % 2 == row % 2)
         {
            return -1;
         }
         else
         {
            return (int)((row * BoardConstants.Rows + col) / 2) + 1;
         }
      }

        /// <summary>
        /// FromPosition - С позиции
        /// Convert the given board notation position to a location
        /// Преобразовать заданную позицию обозначения доски в местоположение
        /// </summary>
        /// <param name="position">
        /// The board notation positon to convert to location
        /// Позиция обозначения доски для преобразования в местоположение
        /// </param>
        /// <returns>
        /// The location for the givne board notation position
        /// Местоположение для позиции обозначения дать доски
        /// </returns>
        public static Location FromPosition(int position)
      {
         int row = GetRowFromNotation(position);
         int col = GetColFromNotation(position);
         return new Location(row, col);
      }

        /// <summary>
        /// ConvertLocationToIndexBased - Преобразовать местоположение в индекс
        /// Convert location to index based location (0 - 63)
        ///  Преобразовать местоположение в местоположение на основе индекса (0 - 63)
        /// </summary>
        /// <param name="row">
        /// the block row
        /// строка блока 
        /// </param>
        /// <param name="col">
        /// the block column
        ///  столбец блока 
        /// </param>
        /// <returns>
        /// the indexed based location for the given location
        ///  индексированное местоположение для данного местоположения
        /// </returns>
        private static int ConvertLocationToIndexBased(int row, int col)
      {
         return row * BoardConstants.Rows + col;
      }

        /// <summary>
        /// ConvertNotationToIndexBased - Преобразовать нотацию в индекс
        /// Convert location in notation format to indexed based location
        ///  Преобразовать местоположение в формате нотации в индексированное местоположение
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        ///  расположение в формате нотации
        /// </param>
        /// <returns>
        /// the index based format for notation formatted location
        ///  основанный на индексе формат для расположения в формате нотации 
        /// </returns>
        private static int ConvertNotationToIndexBased(int notation)
      {
         if ((notation % BoardConstants.Rows > BoardConstants.Rows / 2) || (notation % BoardConstants.Rows == 0))
         {
            return (notation * 2) - 2;
         }
         else
         {
            return (notation * 2) - 1;
         }
      }

        /// <summary>
        /// GetRowFromNotation - Получить строку из записи
        /// Get row from notation location
        /// Получить строку из местоположения записи
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        /// расположение в формате обозначений
        /// </param>
        /// <returns>
        /// the row from the notation location
        /// строка из местоположения обозначений
        /// </returns>
        private static int GetRowFromNotation(int notation)
      {
         int location = ConvertNotationToIndexBased(notation);

         return (location / BoardConstants.Rows);
      }

        /// <summary>
        /// GetColFromNotation - Получить столбец из записи
        /// Get col from notation location
        /// Получить столбец из местоположения обозначений
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        ///  расположение в формате обозначений
        /// </param>
        /// <returns>
        /// the col from the notationt location
        /// столбец из местоположения нотации
        /// </returns>
        private static int GetColFromNotation(int notation)
      {
         int location = ConvertNotationToIndexBased(notation);

         return (location % BoardConstants.Cols);
      }

        /// <summary>
        /// Equals - равно
        /// Is this location equal to the specified location
        /// Является ли это местоположение равным указанному местоположению
        /// </summary>
        /// <param name="obj">
        /// the location to compare to
        /// место для сравнения
        /// </param>
        /// <returns><c>true</c>
        /// if the location are equal
        /// если местоположение равно
        /// </returns>
        public override bool Equals(object obj)
      {
         if (obj == null)
         {
            return false;
         }

         Location loc = obj as Location;
         if (loc != null)
         {
            return ((row == loc.row) && (col == loc.col));
         }

         return false;
      }

        /// <summary>
        /// GetHashCode - Получить хэш-код
        /// Serves as a hash function for a particular type.
        /// Служит хэш-функцией для определенного типа.
        /// </summary>
        /// <returns>
        /// A hash code for the current location
        ///  Хеш-код для текущего местоположения
        /// </returns>
        public override int GetHashCode()
      {
         return ConvertLocationToIndexBased(row, col);
      }

        /// <summary>
        /// ToString - В строку
        /// Returns a System.String that represents the current location
        /// Возвращает System.String, которая представляет текущее местоположение
        /// </summary>
        /// <returns>
        /// A System.String that represents the current location
        /// System.String, которая представляет текущее местоположение
        /// </returns>
        public override string ToString()
      {
         return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", row.ToString(CultureInfo.InvariantCulture), col.ToString(CultureInfo.InvariantCulture));
      }
   }
}
