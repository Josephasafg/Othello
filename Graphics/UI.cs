using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;
using Ex02_Othello.Logics;

namespace Ex02_Othello.Graphics
{
    public class UIManager
    {   
        public static void RunGame()
        {
            bool quitGame = false;

            // $G$ CSS-999 (-3) use ! instead of == false
            while (quitGame == false)
            {
                Game othelloGame = new Game();
                quitGame = othelloGame.GameFlow();
            }
        }

        
        public static void DrawBoard(Board i_CurrentGameBoard)
        {
            Screen.Clear();
            Console.Write(" ");

            // $G$ DSN-999 (-5) bad code duplication
            for (byte i = 0; i < i_CurrentGameBoard.BoardSizeProperties; i++)
            {
                Console.Write("   " + (char)(i + 65));
            }

            Console.Write(Environment.NewLine + "  =");
            for (byte i = 0; i < i_CurrentGameBoard.BoardSizeProperties; i++)
            {
                Console.Write("====");
            }

            Console.Write(Environment.NewLine);
            for (byte i = 0; i < i_CurrentGameBoard.BoardSizeProperties; i++)
            {
                Console.Write((i + 1).ToString() + " |");
                for (byte j = 0; j < i_CurrentGameBoard.BoardSizeProperties; j++)
                {
                    Console.Write(" {0} |", i_CurrentGameBoard.BoardMatrixProperties[i, j]);
                }

                Console.Write(Environment.NewLine + "  =");
                for (byte k = 0; k < i_CurrentGameBoard.BoardSizeProperties; k++)
                {
                    Console.Write("====");
                }

                Console.WriteLine(string.Empty);
            }
        }

        public static short GetBoardSize()
        {
            bool inputIsValid = false;
            short sizeDecisionToBeReturned = 8;
            while (!inputIsValid)
            {
                Console.WriteLine(@"Please choose a board size: 
6 - 6X6
8 - 8X8");
                string sizeDecision = Console.ReadLine();
                if (short.TryParse(sizeDecision, out short parsedSize))
                {
                    inputIsValid = Board.CheckVaildBoardSize(parsedSize);
                    if (inputIsValid)
                    {
                        sizeDecisionToBeReturned = parsedSize;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }

            return sizeDecisionToBeReturned;
        }

        public static bool ChooseRival()
        {
            Console.WriteLine("Do you want to play against a second player? y/n");
            string userInput = Console.ReadLine().ToString();
            return (string.Equals(userInput, "y") || string.Equals(userInput, "Y")) ? true : false;
        }

        public static string AskPlayerForName()
        {
            Screen.Clear();
            Console.WriteLine("Hey! Please enter your name: ");
            string userNameInput = Console.ReadLine().ToString();
            Console.WriteLine(string.Format("Welcome to OthelLo {0}",userNameInput));

            return userNameInput;
        }

        public static Move ChooseYourMove(Game i_CurrentGame, Board i_CurrentGameBoard, Player i_CurrentPlayer)
        {
            bool isMove = false;
            bool isQuit = false;
            Move userSelectedMove = null;
            Console.WriteLine(string.Format(@"{0}'s turn. Shape: {1}
Please choose your next move.", i_CurrentPlayer.Name, (char)i_CurrentPlayer.Disc));

            string moveInput = Console.ReadLine().ToString();
            while (isMove == false)
            {
                isQuit = Move.CheckIfQuit(moveInput);
                if (isQuit == true)
                {
                    break;
                }
                isMove = Game.ChooseYourMove(i_CurrentGame, i_CurrentGameBoard, i_CurrentPlayer, moveInput, ref userSelectedMove);
                if (isMove == false)
                {
                    Console.WriteLine("Input is not valid or legal.Please try again");
                    moveInput = Console.ReadLine().ToString();
                }
            }
            return userSelectedMove;
        }
        

        public static void PrintScore(Board i_CurrentGameBoard, Player i_CurrentPlayerOne, Player i_CurrentPlayerTwo)
        {
            Game.CountPlayersPoints(i_CurrentGameBoard, i_CurrentPlayerOne, i_CurrentPlayerTwo);
            string scoreMessage = string.Format(@"Score:
{0}: {1}
{2}: {3}", i_CurrentPlayerOne.Name, i_CurrentPlayerOne.Score, i_CurrentPlayerTwo.Name, i_CurrentPlayerTwo.Score);
            Console.WriteLine(scoreMessage);
        }

        public static bool GameIsOverUI(string i_WinningPlayer)
        {
            Console.WriteLine(string.Format(@"GAME OVER ! 
{0} is the Winner", i_WinningPlayer));
            Console.WriteLine("Do you want to play a new round?   P");
            Console.WriteLine("Do you want to quit?               Q");
            string userInput = Console.ReadLine().ToString();
            return Game.CheckNextRound(userInput);
        }
    }
}
