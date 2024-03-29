using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// IBoard - �����
    /// Interface for a checkers game board
    /// ��������� ��� ���������� ���� � �����
    /// </summary>
    public interface IBoard
   {
        /// <summary>
        /// Size - ������
        /// Get the size of the board.  This is the number of valid positions on the board.
        /// �������� ������ �����. ��� ���������� �������������� ������� �� �����
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Rows - ���
        /// Get the number of rows on the board
        ///  �������� ���������� ����� �� �����
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Cols - �������
        /// Get the number of columns on the board
        ///  �������� ���������� �������� �� �����
        /// </summary>
        int Cols { get; }

        /// <summary>
        /// this[int position]
        /// Get the piece at the specified board notation position
        /// �������� ����� � ��������� ������� ����������� �����
        /// </summary>
        /// <param name="position">
        /// The position.  This starts at one instead of zero.
        /// �������. ��� ���������� � ������ ������ ����.
        /// </param>
        /// <returns>
        /// The piece at the given board notation position
        ///  ����� � ��������� ������� ����������� �����
        /// </returns>
        Piece this[int position] { get; set; }

        /// <summary>
        /// this[int row, int col]
        /// Get the piece at the given location
        /// �������� ����� � ��������� �����
        /// </summary>
        /// <param name="row">
        /// The row
        /// ���
        /// </param>
        /// <param name="col">
        /// The column
        /// �������
        /// </param>
        /// <returns>
        /// The piece at the given location
        ///  ����� � ��������� �����
        /// </returns>
        Piece this[int row, int col] { get; set; }

        /// <summary>
        /// Clear - ��������
        /// Clear the board
        /// �������� �����
        /// </summary>
        void Clear();

        /// <summary>
        /// Copy - ����������
        /// Generate a copy of the board
        ///  ������� ����� �����
        /// </summary>
        /// <returns>
        /// A copy of this board
        /// ����� ���� �����
        /// </returns>
        IBoard Copy();

        /// <summary>
        /// Copy - ����������
        /// Copy the state of the given board
        /// ����������� ��������� ������ �����
        /// </summary>
        /// <param name="board">
        /// The board to copy
        /// ����� ��� ����������� 
        /// </param>
        void Copy(IBoard board);
   }
}
