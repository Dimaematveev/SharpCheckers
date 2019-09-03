using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Location - ����� ����������
    /// A location on the checkers board
    /// ����� �� ����� �����
    /// </summary>
    public class Location
   {
      private int row;
      private int col;

        /// <summary>
        /// Constructs and initializes a location at the origin (0, 0) of the board
        ///  ������� � �������������� �������������� � ������ (0, 0) ����� 
        /// </summary>
        public Location()
         : this(0, 0)
      {
      }

        /// <summary>
        /// Constructor
        /// �����������
        /// </summary>
        /// <param name="row">
        /// the row on the board
        ///  ������ �� �����
        /// </param>
        /// <param name="col">
        /// the col on the board
        /// ������� �� �����
        /// </param>
        public Location(int row, int col)
      {
         this.row = row;
         this.col = col;
      }

        /// <summary>
        /// Row - ������
        /// Get or set the row
        /// �������� ��� ���������� ������
        /// </summary>
        public int Row
      {
         get { return row; }
         set { row = value; }
      }

        /// <summary>
        /// Col - �������
        /// Get or set the column
        /// �������� ��� ���������� �������
        /// </summary>
        public int Col
      {
         get { return col; }
         set { col = value; }
      }

        /// <summary>
        /// ToPosition - ��� �������
        /// Convert to board notation position
        /// ������������� � ��������� ����������� �����
        /// </summary>
        /// <returns>
        /// The board notation position corresponding to this location
        /// ������� ����������� �����, ��������������� ����� ������������
        /// </returns>
        public int ToPosition()
      {
         return ToPosition(this);
      }

        /// <summary>
        /// ToPosition - ��� �������
        /// Convert the given location to a board notation location
        /// ������������� ��������� �������������� � �������������� ����������� �����
        /// </summary>
        /// <param name="location">
        /// The location to get conversion for
        ///  �������������� ��� ��������� ��������� ��� 
        /// </param>
        /// <returns>
        /// The board notation position corresponding to the given location
        ///  ������� ����������� �����, ��������������� ������� ��������������
        /// </returns>
        public static int ToPosition(Location location)
      {
         return ToPosition(location.row, location.col);
      }

        /// <summary>
        /// ToPosition - ��� �������
        /// Convert location to the square position
        /// ������������� �������������� � ���������� �������
        /// </summary>
        /// <param name="row">
        /// the block row
        /// ������ �����
        /// </param>
        /// <param name="col">
        /// the block column
        ///  ������� �����
        /// </param>
        /// <returns>
        /// the position for the given location
        /// ������� ��� ������� ��������������
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
        /// FromPosition - � �������
        /// Convert the given board notation position to a location
        /// ������������� �������� ������� ����������� ����� � ��������������
        /// </summary>
        /// <param name="position">
        /// The board notation positon to convert to location
        /// ������� ����������� ����� ��� �������������� � ��������������
        /// </param>
        /// <returns>
        /// The location for the givne board notation position
        /// �������������� ��� ������� ����������� ���� �����
        /// </returns>
        public static Location FromPosition(int position)
      {
         int row = GetRowFromNotation(position);
         int col = GetColFromNotation(position);
         return new Location(row, col);
      }

        /// <summary>
        /// ConvertLocationToIndexBased - ������������� �������������� � ������
        /// Convert location to index based location (0 - 63)
        ///  ������������� �������������� � �������������� �� ������ ������� (0 - 63)
        /// </summary>
        /// <param name="row">
        /// the block row
        /// ������ ����� 
        /// </param>
        /// <param name="col">
        /// the block column
        ///  ������� ����� 
        /// </param>
        /// <returns>
        /// the indexed based location for the given location
        ///  ��������������� �������������� ��� ������� ��������������
        /// </returns>
        private static int ConvertLocationToIndexBased(int row, int col)
      {
         return row * BoardConstants.Rows + col;
      }

        /// <summary>
        /// ConvertNotationToIndexBased - ������������� ������� � ������
        /// Convert location in notation format to indexed based location
        ///  ������������� �������������� � ������� ������� � ��������������� ��������������
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        ///  ������������ � ������� �������
        /// </param>
        /// <returns>
        /// the index based format for notation formatted location
        ///  ���������� �� ������� ������ ��� ������������ � ������� ������� 
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
        /// GetRowFromNotation - �������� ������ �� ������
        /// Get row from notation location
        /// �������� ������ �� �������������� ������
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        /// ������������ � ������� �����������
        /// </param>
        /// <returns>
        /// the row from the notation location
        /// ������ �� �������������� �����������
        /// </returns>
        private static int GetRowFromNotation(int notation)
      {
         int location = ConvertNotationToIndexBased(notation);

         return (location / BoardConstants.Rows);
      }

        /// <summary>
        /// GetColFromNotation - �������� ������� �� ������
        /// Get col from notation location
        /// �������� ������� �� �������������� �����������
        /// </summary>
        /// <param name="notation">
        /// the notation formatted location
        ///  ������������ � ������� �����������
        /// </param>
        /// <returns>
        /// the col from the notationt location
        /// ������� �� �������������� �������
        /// </returns>
        private static int GetColFromNotation(int notation)
      {
         int location = ConvertNotationToIndexBased(notation);

         return (location % BoardConstants.Cols);
      }

        /// <summary>
        /// Equals - �����
        /// Is this location equal to the specified location
        /// �������� �� ��� �������������� ������ ���������� ��������������
        /// </summary>
        /// <param name="obj">
        /// the location to compare to
        /// ����� ��� ���������
        /// </param>
        /// <returns><c>true</c>
        /// if the location are equal
        /// ���� �������������� �����
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
        /// GetHashCode - �������� ���-���
        /// Serves as a hash function for a particular type.
        /// ������ ���-�������� ��� ������������� ����.
        /// </summary>
        /// <returns>
        /// A hash code for the current location
        ///  ���-��� ��� �������� ��������������
        /// </returns>
        public override int GetHashCode()
      {
         return ConvertLocationToIndexBased(row, col);
      }

        /// <summary>
        /// ToString - � ������
        /// Returns a System.String that represents the current location
        /// ���������� System.String, ������� ������������ ������� ��������������
        /// </summary>
        /// <returns>
        /// A System.String that represents the current location
        /// System.String, ������� ������������ ������� ��������������
        /// </returns>
        public override string ToString()
      {
         return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", row.ToString(CultureInfo.InvariantCulture), col.ToString(CultureInfo.InvariantCulture));
      }
   }
}
