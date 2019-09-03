using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
    /// <summary>
    /// Player - �����
    /// The game player.  A checkers match is between two opponents.  One player
    /// controls the black pieces and the other controls the white pieces.
    ///  �����. ���� �� ������ ����� ����� ������������. ���� ����� 
    ///  ������������ ������ ������, � ������ ������������ ����� ������.
    /// </summary>
    public enum Player
   {
        /// <summary>
        /// None - �����
        /// Neither player
        /// �� ���� �����
        /// </summary>
        None,

        /// <summary>
        /// Black - ������
        /// The black piece player
        /// ������ ������ ������
        /// </summary>
        Black,

        /// <summary>
        /// White - �����
        /// The white piece player
        /// ����� ������ ������
        /// </summary>
        White
    }
}
