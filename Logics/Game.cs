using System.Collections.Generic;
using System.Linq;
using Ex02_Othello.Graphics;

namespace Ex02_Othello.Logics
{
    public class Game
    {
        private Board m_GameBoard;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private bool m_GameOverFlag = false;
        
        public static void CountPlayersPoints(Board i_CurrentGameBoard, Player io_CurrentPlayerOne, Player io_CurrentPlayerTwo)
        {
            io_CurrentPlayerOne.Score = 0;
            io_CurrentPlayerTwo.Score = 0;
            for (byte i = 0; i < i_CurrentGameBoard.BoardSizeProperties; i++)
            {
                for (byte j = 0; j < i_CurrentGameBoard.BoardSizeProperties; j++)
                {
                    if (i_CurrentGameBoard.BoardMatrixProperties[i, j] == (char)io_CurrentPlayerOne.Disc)
                    {
                        io_CurrentPlayerOne.Score++;
                    }
                    else if (i_CurrentGameBoard.BoardMatrixProperties[i, j] == (char)io_CurrentPlayerTwo.Disc)
                    {
                        io_CurrentPlayerTwo.Score++;
                    }
                }
            }
        }

        private void updateMoveOnBoard(Player i_CurrentPlayer)
        {
            for (byte i = 0; i < i_CurrentPlayer.CurrentMove.TilesToFlipProp.Count; i++)
            {
                int columnIndex = i_CurrentPlayer.CurrentMove.TilesToFlipProp[i].YValue + 1;
                int rowIndex = i_CurrentPlayer.CurrentMove.TilesToFlipProp[i].XValue + 1;
                m_GameBoard.ChangeSquare(rowIndex, columnIndex, i_CurrentPlayer.Disc);
            }
        }

        private List<Point> createListToFlip(Board.eColor i_CurrentPlayersColor, int io_RowDistance, int io_ColumnDistance, int io_RowIndex, int io_ColumnIndex, Board i_CurrentGameBoard, List<Point> o_CurrentMove)
        {
            if (i_CurrentGameBoard.BoardMatrixProperties[io_RowIndex, io_ColumnIndex] == (char)i_CurrentPlayersColor)
            {
                return o_CurrentMove;
            }

            if ((io_RowDistance + io_RowIndex < 0) || (io_RowDistance + io_RowIndex == i_CurrentGameBoard.BoardSizeProperties))
            {
                o_CurrentMove.Clear();
                return o_CurrentMove;
            }

            if ((io_ColumnDistance + io_ColumnIndex < 0) || (io_ColumnDistance + io_ColumnIndex == i_CurrentGameBoard.BoardSizeProperties))
            {
                o_CurrentMove.Clear();
                return o_CurrentMove;
            }

            o_CurrentMove.Add(new Point(io_RowIndex, io_ColumnIndex));
            return createListToFlip(i_CurrentPlayersColor, io_RowDistance, io_ColumnDistance, io_RowIndex + io_RowDistance, io_ColumnIndex + io_ColumnDistance, i_CurrentGameBoard, o_CurrentMove);
        }

      
        private List<Point> getValidDiscs(Player i_CurrentPlayer, int io_RowDistance, int io_ColumnDistance, int io_RowIndex, int io_ColumnIndex, Board i_CurrentGameBoard)
        {
            List<Point> pointListToBuild = new List<Point>();
            bool checkIfValid = true;
            if ((io_RowDistance + io_RowIndex < 0) || (io_RowDistance + io_RowIndex == i_CurrentGameBoard.BoardSizeProperties))
            {
                checkIfValid = false;
            }

            if (checkIfValid && (io_ColumnDistance + io_ColumnIndex < 0) || (io_ColumnDistance + io_ColumnIndex == i_CurrentGameBoard.BoardSizeProperties))
            {
                checkIfValid = false;
            }

            if (checkIfValid && i_CurrentGameBoard.BoardMatrixProperties[io_RowDistance + io_RowIndex, io_ColumnDistance + io_ColumnIndex] != (char)i_CurrentPlayer.GetOpponentColor(i_CurrentPlayer.Disc))
            {
                checkIfValid = false;
            }

            if (checkIfValid == true)
            {
                pointListToBuild = createListToFlip(i_CurrentPlayer.Disc, io_RowDistance, io_ColumnDistance, io_RowIndex, io_ColumnIndex, i_CurrentGameBoard, pointListToBuild);
            }
            return pointListToBuild;
        }

        private string parseMove(Point i_Tile)
        {
            char j = (char)(i_Tile.XValue + 49);
            char i = (char)(i_Tile.YValue + 65);

            return string.Concat(i, j);
        }

        public List<Move> ValidateMove(Board i_CurrentGameBoard, Player i_CurrentPlayer)
        {
            List<Move> arrayOfCurrentValidMoves = new List<Move>();
            List<Point> arrayOfDiscsToFlip = new List<Point>();
            Board.eColor opponentColorDisc = i_CurrentPlayer.GetOpponentColor(i_CurrentPlayer.Disc);
            Move currentLegalMove;
            for (byte i = 0; i < i_CurrentGameBoard.BoardSizeProperties; i++)
            {
                for (byte j = 0; j < i_CurrentGameBoard.BoardSizeProperties; j++)
                {
                    if (i_CurrentGameBoard.BoardMatrixProperties[i, j] == (char)Board.eColor.Empty)
                    {
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer, -1, -1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer, -1,  0, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer, -1,  1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer,  0, -1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer,  0,  1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer,  1, -1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer,  1,  1, i, j, i_CurrentGameBoard));
                        arrayOfDiscsToFlip.AddRange(getValidDiscs(i_CurrentPlayer,  1,  0, i, j, i_CurrentGameBoard));
                    }

                    if (arrayOfDiscsToFlip.Any())
                    {
                        currentLegalMove = new Move(new Point(i, j));
                        currentLegalMove.TilesToFlipProp = new List<Point>(arrayOfDiscsToFlip);
                        arrayOfDiscsToFlip.Clear();
                        arrayOfCurrentValidMoves.Add(currentLegalMove);
                    }
                }
            }

            return arrayOfCurrentValidMoves;
        }

        public Move CheckMoveLegality(List<Move> i_MoveList, string i_CurrentMove)
        {
            Move moveToBeReturned = null;
            foreach (Move currentMove in i_MoveList)
            {
                if (currentMove.MyTile.YValue == -1 && currentMove.MyTile.XValue == -1)
                {
                    continue;
                }

                if (i_CurrentMove.Equals(parseMove(currentMove.MyTile)))
                {
                    moveToBeReturned = currentMove;
                }
            }

            return moveToBeReturned;
        }

        public bool GameFlow()
        {
            m_FirstPlayer = new Player(UIManager.AskPlayerForName());
            m_FirstPlayer.Disc = Board.eColor.WhiteDisc;
            if (UIManager.ChooseRival())
            {
                m_SecondPlayer = new Player(UIManager.AskPlayerForName());
            }
            else
            {
                m_SecondPlayer = new Player();
            }

            m_SecondPlayer.Disc = Board.eColor.BlackDisc;
            m_GameBoard = new Board(UIManager.GetBoardSize());
            while (!m_GameOverFlag)
            {
                UIManager.DrawBoard(m_GameBoard);
                UIManager.PrintScore(m_GameBoard, m_FirstPlayer, m_SecondPlayer);
                m_FirstPlayer.CurrentMove = UIManager.ChooseYourMove(this, m_GameBoard, m_FirstPlayer);
                if (m_FirstPlayer.CurrentMove == null)
                {
                    m_GameOverFlag = true;
                    break;
                }

                if (m_FirstPlayer.CurrentMove != null)
                {
                    updateMoveOnBoard(m_FirstPlayer);
                    UIManager.DrawBoard(m_GameBoard);
                    UIManager.PrintScore(m_GameBoard, m_FirstPlayer, m_SecondPlayer);
                }

                if (m_SecondPlayer.isHuman)
                {
                    m_SecondPlayer.CurrentMove = UIManager.ChooseYourMove(this, m_GameBoard, m_SecondPlayer);
                    if (m_SecondPlayer.CurrentMove == null)
                    {
                        break;
                    }
                }
                else /// Player is a computer
                {
                    m_SecondPlayer.CurrentMove = m_SecondPlayer.ChooseRandomMove(this, m_GameBoard);
                }

                if (m_SecondPlayer.CurrentMove != null)
                {
                    updateMoveOnBoard(m_SecondPlayer);
                    UIManager.DrawBoard(m_GameBoard);
                    UIManager.PrintScore(m_GameBoard, m_FirstPlayer, m_SecondPlayer);
                }

                if ((m_FirstPlayer.NoLegalMoves == true) && (m_SecondPlayer.NoLegalMoves == true))
                {
                    m_GameOverFlag = true;
                }
            }

            string winningPlayerName = GameIsOver(m_GameBoard, m_FirstPlayer, m_SecondPlayer);
            bool quitGame = UIManager.GameIsOverUI(winningPlayerName);

            return quitGame;
        }

        private string GameIsOver(Board i_CurrentGameBoard, Player i_CurrentPlayerOne, Player i_CurrentPlayerTwo)
        {
            CountPlayersPoints(i_CurrentGameBoard, i_CurrentPlayerOne, i_CurrentPlayerTwo);
            string winningPlayerName;
            string userInput;
            if (i_CurrentPlayerOne.Score > i_CurrentPlayerTwo.Score)
            {
                winningPlayerName = i_CurrentPlayerOne.Name;
            }
            else if (i_CurrentPlayerTwo.Score > i_CurrentPlayerOne.Score)
            {
                winningPlayerName = i_CurrentPlayerTwo.Name;
            }
            else
            {
                winningPlayerName = "it's a DRAW. either player";
            }

            return winningPlayerName;
        }

        public static bool CheckNextRound(string i_UserInput)
        {
            return i_UserInput == "P" ? false : true;
        }

        public static bool CheckInRange(string i_InputToCheck, short i_CurrentGameBoardSize)
        {
            return ((i_InputToCheck != "") &&
                (i_InputToCheck.Length == 2) &&
                (i_InputToCheck[0] >= (char)(65)) &&
                (i_InputToCheck[0] <= (char)(65 + i_CurrentGameBoardSize)) &&
                (i_InputToCheck[1] > 48) &&
                (i_InputToCheck[1] <= (char)(48 + i_CurrentGameBoardSize))) ? true : false;
        }

        public static bool ChooseYourMove(Game i_CurrentGame, Board i_CurrentGameBoard, Player i_CurrentPlayer, string i_MoveInput, ref Move i_Move)
        {
            bool isMoveValid = true;

            List<Move> arrayOfCurrentLegalMoves = new List<Move>();

            if (CheckInRange(i_MoveInput, i_CurrentGameBoard.BoardSizeProperties))
            {
                arrayOfCurrentLegalMoves = i_CurrentGame.ValidateMove(i_CurrentGameBoard, i_CurrentPlayer);
                i_Move = i_CurrentGame.CheckMoveLegality(arrayOfCurrentLegalMoves, i_MoveInput);
                if (i_Move != null)
                {
                    if (arrayOfCurrentLegalMoves.Count <= 1)
                    {
                        i_CurrentPlayer.NoLegalMoves = true;
                    }
                    isMoveValid = true;
                }
                else
                {
                    isMoveValid = false;
                }
            }
            else
            {
                isMoveValid = false;
            }
            
            return isMoveValid;
        }

    }
}
