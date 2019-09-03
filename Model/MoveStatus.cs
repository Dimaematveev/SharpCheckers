using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// MoveStatus -  ������ ��������
    /// Possible status of checker moves
    ///  ��������� ������ ����� ��������
    /// </summary>
    public enum MoveStatus
   {
        /// <summary>
        /// Legal - ���������
        /// The move is legal
        /// �������� �������
        /// </summary>
        Legal,

        /// <summary>
        /// Illegal -  ����������
        /// The move is illegal
        /// �������� �����������
        /// </summary>
        Illegal,

        /// <summary>
        /// Incomplete - �������������
        /// The move is legal but not completed because it hasn't been determined if there is a multiple jump
        /// ����������� ���������, �� �� ���������, ��������� �� ���� ����������, ���� �� ������������ ������� 
        /// </summary>
        Incomplete
    }
}
