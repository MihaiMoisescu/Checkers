using Checkers.Enums;
using Checkers.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;

namespace Checkers.Services
{
    class Helper
    {
        public const string redCell = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\red1.png";
        public const string whiteCell = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\white1.png";
        public const string redPiece = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\redPiece.png";
        public const string whitePiece = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\whitePiece.png";
        public const string redPieceKing = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\redKingPiece.png";
        public const string whitePieceKing = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\whiteKingPiece.png";
        public const string redTurn = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\redturn.jpeg";
        public const string whiteTurn = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\whiteturn.jpeg";

        private static GamePlayer player = new GamePlayer(PieceColor.Red);
        public static int RED_PIECES = 12;
        public static int WHITES_PIECES = 12;
        public static Cell CurrentCell { get; set; }
        public static Position CapturedCellPosition { get; set; }
        public static Position ExtraMovePosition { get; set; }
        private static List<Cell> _neighboardsWithPiece = new List<Cell>();
        public static List<Cell> NeighboardsWithPiece
        {
            get
            {
                return _neighboardsWithPiece;
            }
            set
            {
                _neighboardsWithPiece = value;
            }
        }
        public static GamePlayer GPlayer
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
        public static ObservableCollection<ObservableCollection<Cell>> InitBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            int boardSize = 8;
            for (int i = 0; i < boardSize; i++)
            {
                board.Add(new ObservableCollection<Cell>());
                for (int j = 0; j < boardSize; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i].Add(new Cell(new Position(i, j), whiteCell, CellType.White, null));
                    }
                    else if (i < 3)
                    {
                        board[i].Add(new Cell(new Position(i, j), redCell, CellType.Black, new Piece(PieceColor.White, PieceType.Regular, whitePiece)));
                    }
                    else if (i > 4)
                    {
                        board[i].Add(new Cell(new Position(i, j), redCell, CellType.Black, new Piece(PieceColor.Red, PieceType.Regular, redPiece)));
                    }
                    else
                        board[i].Add(new Cell(new Position(i, j), redCell, CellType.Black, null));
                }
            }
            return board;
        }

        public static void ResetGame(ObservableCollection<ObservableCollection<Cell>> board)
        {
            WHITES_PIECES = 12;
            RED_PIECES = 12;
            CurrentCell = null;
            NeighboardsWithPiece.Clear();
            CapturedCellPosition = null;
            player.Player = PieceColor.Red;
            player.Image = redPiece;
            ResetBoard(board);
        }
        public static void ResetBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[i].Count; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i][j].Piece = null;
                    }
                    else if (i < 3)
                    {
                        board[i][j].Piece = new Piece(PieceColor.White, PieceType.Regular, whitePiece);
                    }
                    else if (i > 4)
                    {
                        board[i][j].Piece = new Piece(PieceColor.Red, PieceType.Regular, redPiece);
                    }
                    else
                        board[i][j].Piece = null;
                }
            }
        }

        public static Position Next(Cell currentCell, Cell occupiedCell)
        {
            int nexTX = occupiedCell.Position.X + (occupiedCell.Position.X - currentCell.Position.X);
            int nextY = occupiedCell.Position.Y + (occupiedCell.Position.Y - currentCell.Position.Y);

            return new Position(nexTX, nextY);
        }
        public static void ClearNeighboarWithPeace()
        {
            if (NeighboardsWithPiece != null)
                NeighboardsWithPiece.Clear();
        }
        public static void MakeKing(Cell cell)
        {
            if (cell.Piece.Type != PieceType.King)
            {
                if (cell.Piece.Color == PieceColor.Red)
                {
                    cell.Piece.Type = PieceType.King;
                    cell.Piece.Background = redPieceKing;
                }
                else
                {
                    cell.Piece.Type = PieceType.King;
                    cell.Piece.Background = whitePieceKing;
                }
            }
        }
        public static void OpenGame(ObservableCollection<ObservableCollection<Cell>> board, GamePlayer player, bool extra)
        {

            OpenFileDialog openDialog = new OpenFileDialog();
            bool? answer = openDialog.ShowDialog();

            if (answer == true)
            {
                string path = openDialog.FileName;
                using (var reader = new StreamReader(path))
                {
                    string text;
                    while ((text = reader.ReadLine()) != null) 
                    {
                        if (string.IsNullOrWhiteSpace(text)) 
                        {
                            break; 
                        }

                        if (text == "ON")
                        {
                            extra = true;
                        }
                        else if (text == "OFF")
                        {
                            extra = false;
                        }
                        else if (text == "R")
                        {
                            GPlayer.Player = PieceColor.Red;
                            GPlayer.Image = redTurn;
                            player.Image = redTurn;
                            player.Player = PieceColor.Red;
                        }
                        else if (text == "W")
                        {
                            GPlayer.Player = PieceColor.White;
                            GPlayer.Image = whiteTurn;
                            player.Image = whiteTurn;
                            player.Player = PieceColor.White;
                        }
                    }
                    for (int i = 0; i < board.Count; i++)
                    {
                        for (int j = 0; j < board[i].Count; j++)
                        {
                            text = reader.ReadLine();

                            if (text == "white")
                            {
                                board[i][j].Piece = null;
                                board[i][j].CellType = CellType.White;
                            }
                            else if (text == "black")
                            {
                                board[i][j].Piece = null;
                                board[i][j].CellType = CellType.Black;
                            }
                            else if (text == "blackWhiteKing")
                            {
                                board[i][j].Piece = new Piece(PieceColor.White, PieceType.King, whitePieceKing);
                            }
                            else if (text == "blackRedKing")
                            {
                                board[i][j].Piece = new Piece(PieceColor.Red, PieceType.King, redPieceKing);
                            }
                            else if (text == "blackWhite")
                            {
                                board[i][j].Piece = new Piece(PieceColor.White, PieceType.Regular, whitePiece);
                            }
                            else if (text == "blackRed")
                            {
                                board[i][j].Piece = new Piece(PieceColor.Red, PieceType.Regular, redPiece);
                            }
                        }
                    }
                }
            }
        }

        public static void SaveGame(ObservableCollection<ObservableCollection<Cell>> board,bool extraMove)
        {

            SaveFileDialog saveDialog = new SaveFileDialog();
            bool? answer = saveDialog.ShowDialog();
            if (answer == true)
            {
                var path = saveDialog.FileName;
                using(var writer =new StreamWriter(path))
                {
                    if (extraMove == true)
                        writer.WriteLine("ON");
                    if(extraMove==false)
                        writer.WriteLine("OFF");
                    if (GPlayer.Player == PieceColor.Red)
                        writer.WriteLine("R");
                    if (GPlayer.Player == PieceColor.White)
                        writer.WriteLine("W");
                    writer.WriteLine();
                    for(int i=0; i<board.Count; i++)
                    {
                        for(int j=0; j < board[i].Count; j++)
                        {
                            if (board[i][j].CellType==CellType.White)
                            {
                                writer.WriteLine("white");
                            }
                            else
                            {
                                if (board[i][j].Piece == null)
                                {
                                    writer.WriteLine("black");
                                }
                                else
                                {
                                    if (board[i][j].Piece.Type == PieceType.King && board[i][j].Piece.Color == PieceColor.White)
                                        writer.WriteLine("blackWhiteKing");
                                    if (board[i][j].Piece.Type == PieceType.King && board[i][j].Piece.Color == PieceColor.Red)
                                        writer.WriteLine("blackRedKing");
                                    if (board[i][j].Piece.Type == PieceType.Regular && board[i][j].Piece.Color == PieceColor.White)
                                        writer.WriteLine("blackWhite");
                                    if (board[i][j].Piece.Type == PieceType.Regular && board[i][j].Piece.Color == PieceColor.Red)
                                        writer.WriteLine("blackRed");
                                }
                            }
                        }
                    }

                }
            }
        }
        public static void WriteScore()
        {
            try
            {
                string path = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\winner.txt";
                string[] text=File.ReadAllLines(path);
                if(RED_PIECES==0)
                {
                    int.TryParse(text[0], out int redWin);
                    redWin++;
                    text[0]=redWin.ToString();
                }
                else
                {
                    int.TryParse(text[1], out int whiteWin);
                    whiteWin++;
                    text[0] = whiteWin.ToString();
                }
                int.TryParse(text[2], out int redPieces);
                if(redPieces<RED_PIECES)
                {
                    redPieces=RED_PIECES;
                    text[2]=redPieces.ToString();
                }
                int.TryParse(text[3], out int whitePieces);
                if(whitePieces<WHITES_PIECES)
                {
                    whitePieces=WHITES_PIECES;
                    text[3]=whitePieces.ToString();
                }
                File.WriteAllLines(path, text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static  void ShowStatistics()
        {
            string statisticsText = "";
            try
            {
                string path = "D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\winner.txt";
                string[] text = File.ReadAllLines(path);
                statisticsText += "Score:\n";
                statisticsText += "Red wins:" + text[0] + "\n";
                statisticsText += "White wins:" + text[1] + "\n";
                statisticsText+="Maximum number of red pieces:" + text[2]+"\n";
                statisticsText += "Maximum number of white pieces:" + text[3] + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(statisticsText, "Statistics");
        }
    }

}
