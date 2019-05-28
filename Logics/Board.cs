using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othello.Logics
{
    public class Board
    {
        private short m_GameBoardSize;
        private char[,] m_GameBoardMatrix = null;

        public enum eColor
        {
            BlackDisc = 'X',
            WhiteDisc = 'O',
            Empty = ' '
        }

        public Board(short m_GameBoardSize)
        {
            this.m_GameBoardSize = m_GameBoardSize;
            m_GameBoardMatrix = new char[this.m_GameBoardSize, this.m_GameBoardSize];
            for (byte i = 0; i < this.m_GameBoardSize; i++)
            {
                for (byte j = 0; j < this.m_GameBoardSize; j++)
                {
                    m_GameBoardMatrix[i, j] = (char)eColor.Empty;
                }
            }

            m_GameBoardMatrix[(this.m_GameBoardSize / 2) - 1, (this.m_GameBoardSize / 2) - 1] = (char)eColor.WhiteDisc;
            m_GameBoardMatrix[(this.m_GameBoardSize / 2), (this.m_GameBoardSize / 2)] = (char)eColor.WhiteDisc;
            m_GameBoardMatrix[(this.m_GameBoardSize / 2) - 1, (this.m_GameBoardSize / 2)] = (char)eColor.BlackDisc;
            m_GameBoardMatrix[(this.m_GameBoardSize / 2), (this.m_GameBoardSize / 2) - 1] = (char)eColor.BlackDisc;
        }

        public short BoardSizeProperties
        {
            get
            {
                return m_GameBoardSize;
            }

            set
            {
                m_GameBoardSize = value;
            }
        }

        public char [,] BoardMatrixProperties
        {
            get
            {
                return m_GameBoardMatrix;
            }

            set
            {
                m_GameBoardMatrix = value;
            }
        }

        public void ChangeSquare(int io_RowIndexIndex, int i_colIndex, eColor i_CurrentPlayerColor)
        {
            m_GameBoardMatrix[(io_RowIndexIndex - 1), (i_colIndex - 1)] = (char)i_CurrentPlayerColor;
        }

        public static bool CheckVaildBoardSize(short i_SizeDecision)
        {
            return ((i_SizeDecision == 6) || (i_SizeDecision == 8)) ? true : false;
        }
    }
}
