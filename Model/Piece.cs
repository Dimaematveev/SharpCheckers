using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    //TODO:� Piece - �������(������� �� ����� - ������) ��� ������!!!!

    /// <summary>
    /// Piece - �������
    /// The various pieces
    /// ��������� �������
    /// </summary>
    public enum Piece
   {
        /// <summary>
        /// Illegal - ���������
        /// This indicates an invalid piece.  i.e. Invalid square
        /// ��� ��������� �� �������� �������. �� ���� �������� �������
        /// </summary>
        Illegal,

        /// <summary>
        /// None - �����
        /// This indicates that the square is empty and has no piece
        /// ��� ��������� �� ��, ��� ������� ���� � �� ����� ������
        /// </summary>
        None,

        /// <summary>
        /// BlackMan - ������ �����
        /// Black man piece
        /// �� �������� ������ �����
        /// </summary>
        BlackMan,

        /// <summary>
        /// WhiteMan -  ����� �����
        /// White man piece
        /// �� �������� ����� �����
        /// </summary>
        WhiteMan,

        /// <summary>
        /// BlackKing -  ������ ������
        /// Black king piece
        /// �� ��������  ������ ������
        /// </summary>
        BlackKing,

        /// <summary>
        /// WhiteKing - ����� ������
        /// White king piece
        /// �� �������� ����� ������
        /// </summary>
        WhiteKing
    }
}
