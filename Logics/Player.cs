using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02_Othello.Logics
{
    public class Player
    {
        private string m_PlayerName;
        private bool m_IsPlayerHuman;
        private byte m_ScoreCount;
        private Board.eColor m_UserDiscs;
        private Move m_CurrentMoveChosenByPlayer;
        private bool m_NoLegalMovesFlag;

        public Player(string i_CurrentPlayerName)
        {
            m_PlayerName = i_CurrentPlayerName;
            m_IsPlayerHuman = true;
            m_ScoreCount = 2;
            m_NoLegalMovesFlag = false;
        }

        public Player()
        {
            m_PlayerName = "Computer";
            m_IsPlayerHuman = false;
            m_ScoreCount = 2;
            m_NoLegalMovesFlag = false;
        }

        public byte Score
        {
            get
            {
                return m_ScoreCount;
            }

            set
            {
                m_ScoreCount = value;
            }
        }

        public Board.eColor Disc
        {
            get
            {
                return m_UserDiscs;
            }

            set
            {
                m_UserDiscs = value;
            }
        }

        public Move CurrentMove
        {
            get
            {
                return m_CurrentMoveChosenByPlayer;
            }

            set
            {
                m_CurrentMoveChosenByPlayer = value;
            }
        }

        public bool isHuman
        {
            get
            {
                return m_IsPlayerHuman;
            }

            set
            {
                m_IsPlayerHuman = value;
            }
        }

        public bool NoLegalMoves
        {
            get
            {
                return m_NoLegalMovesFlag;
            }

            set
            {
                m_NoLegalMovesFlag = value;
            }
        }

        public string Name
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public Move ChooseRandomMove(Game i_CurrentGame, Board i_GameBoard) 
        {
            List<Move> arrayOfLegalMoves = i_CurrentGame.ValidateMove(i_GameBoard, this);
            Random randomValueGenerator = new Random();
            if (arrayOfLegalMoves.Count <= 1)  
            {
                NoLegalMoves = true;
            }

            if (arrayOfLegalMoves.Count() > 0)
            {
                return arrayOfLegalMoves[randomValueGenerator.Next(arrayOfLegalMoves.Count())];
            }
            else
            {
                return null;
            }
        }

        public Board.eColor GetOpponentColor(Board.eColor i_CurrentPlayersColor)
        {
            Board.eColor colorChosen;
            if (i_CurrentPlayersColor == Board.eColor.BlackDisc)
            {
                colorChosen = Board.eColor.WhiteDisc;
            }
            else
            {
                colorChosen = Board.eColor.BlackDisc;
            }
            return colorChosen;
        }
    }
}
