using System;
using System.Collections.Generic;


namespace Ex02_Othello.Logics
{
    public class Move
    {
        private Point m_TileIndex;
        private List<Point> m_TilesToFlip;

        public Move(Point i_TileIndex)
        {
            m_TileIndex = i_TileIndex;
            m_TilesToFlip = null;
        }

        public List<Point> TilesToFlipProp
        {
            get
            {
                return m_TilesToFlip;
            }

            set
            {
                m_TilesToFlip = value;
            }
        }

        public Point MyTile
        {
            get
            {
                return m_TileIndex;
            }

            set
            {
                m_TileIndex = value;
            }
        }
        
        public static bool CheckIfQuit(string i_IsQuit)
        {
            return i_IsQuit == "Q" ? true : false;
        }
    }
}
