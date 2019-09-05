using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
    /// <summary>
    /// IEngine - ���������
    /// The interface to a checker move engine
    /// ��������� ��� �������� ������ ������
    /// </summary>
    public interface IEngine
   {
        /// <summary>
        /// GenerateMove - ������� ���
        /// Generate a move for the given board state
        /// ���������� ��� ��� ������� ��������� �����
        /// </summary>
        /// <param name="board">
        /// The board state
        ///  ��������� �����
        /// </param>
        /// <param name="player">
        /// The player to generate the move for
        /// �����, ������� ���������� ��� ���
        /// </param>
        /// <param name="ply">
        /// The maximum ply
        /// ������������ ����
        /// </param>
        /// <param name="timeout">
        /// The maximum time allowed to generate teh move
        /// ������������ �����, ����������� ��� ��������� ����������� 
        /// </param>
        /// <returns>
        /// The generated move
        /// ��������������� ��� 
        /// </returns>
        Move GenerateMove(IBoard board, Player player, int ply, TimeSpan timeout);

        /// <summary>
        /// CancelProcessing - ������ ���������
        /// Notify the engine to halt move generation
        /// ��������� ���������, ����� ���������� ��������� ��������
        /// </summary>
        void CancelProcessing();

        /// <summary>
        /// ForceMove -�������������� �����
        /// Force the engine to move immediately
        /// ��������� ��������� ��������� ����������
        /// </summary>
        void ForceMove();
   }
}
