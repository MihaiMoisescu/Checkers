using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Threading;
using Checkers.Enums;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> _board;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board)
        {
            this._board = board;
        }
        private bool VerifyPosition(Cell cell, int targetX, int targetY)
        {
            return cell.Piece == null &&
                   cell.Position.X == Helper.CurrentCell.Position.X + targetX &&
                   (cell.Position.Y == Helper.CurrentCell.Position.Y - 1 ||
                    cell.Position.Y == Helper.CurrentCell.Position.Y + 1);
        }

        private bool VerifyPositionForWhitePiece(Cell cell)
        {
            return VerifyPosition(cell, 1, -1) || VerifyPosition(cell, 1, 1);
        }
        bool VerifyAndErasePiece(Cell cell)
        {
            if(Helper.CurrentCell.Piece.Color==PieceColor.Red)
            {
                if ((VerifyPosition(cell, -1, -2) == true && _board[cell.Position.X - 1][cell.Position.Y-2].Piece.Color==PieceColor.White))
                {
                    _board[cell.Position.X - 1][cell.Position.Y - 1].Piece = null;
                    return true;
                }
                else if((VerifyPosition(cell, -1, 2) == true && _board[cell.Position.X - 1][cell.Position.Y +2].Piece.Color == PieceColor.White))
                {
                    _board[cell.Position.X - 1][cell.Position.Y + 1].Piece = null;
                    return true;
                }
            }
            else if(Helper.CurrentCell.Piece.Color == PieceColor.White)
            {
                if ((VerifyPosition(cell, 1, -2) == true && _board[cell.Position.X +1][cell.Position.Y - 2].Piece.Color == PieceColor.Red))
                {
                    _board[cell.Position.X - 1][cell.Position.Y - 1].Piece = null;
                    return true;
                }
                else if ((VerifyPosition(cell, 1, 2) == true && _board[cell.Position.X + 1][cell.Position.Y +2].Piece.Color == PieceColor.Red))
                {
                    _board[cell.Position.X - 1][cell.Position.Y + 1].Piece = null;
                    return true;
                }
            }
            return false;
        }
        private bool VerifyPositionForRedPiece(Cell cell)
        {
            return VerifyPosition(cell, -1, -1) || VerifyPosition(cell, -1, 1);
        }
        private bool VerifyAcceptablePosition(Cell cell)
        {
            if(Helper.CurrentCell!=null)
            {
                if(Helper.CurrentCell.Piece.Type == PieceType.Regular)
                {
                    if(Helper.CurrentCell.Piece.Color == PieceColor.White)
                    {
                        return VerifyPositionForWhitePiece(cell);
                    }
                    else if (Helper.CurrentCell.Piece.Color == PieceColor.Red)
                    {
                        return VerifyPositionForRedPiece(cell);
                    }
                }
                else
                {
                    return VerifyPositionForRedPiece(cell)&&VerifyPositionForWhitePiece(cell);
                }
            }
            return false;
        }
        private bool VerifyIfIsAcceptableMove(Cell cell)
        {
            if (cell == Helper.CurrentCell)
                return false;
            if (cell.CellType == CellType.White)
                return false;
            if (!VerifyAcceptablePosition(cell))
                return false;

            return true;
        }
        public void MoveCell(Cell cell)
        {
            if(Helper.CurrentCell ==null)
            {
                if (cell.Piece != null)
                    Helper.CurrentCell = cell;
            }
            else
            {
                if (VerifyIfIsAcceptableMove(cell))
                {
                    cell.Piece = Helper.CurrentCell.Piece;
                    _board[Helper.CurrentCell.Position.X][Helper.CurrentCell.Position.Y].Piece = null;
                    Helper.CurrentCell = null;
                }
                else
                {
                    MessageBox.Show("Mutare invalida");
                    Helper.CurrentCell = null;

                }

            }
        }
        public void NewGameAction()
        {
            Helper.ResetGame(_board);
        }
        public void OpenGame()
        {

        }
        public void SaveGame()
        {

        }
        public void Statistics()
        {

        }
        public void AllowMultipeJumps()
        {

        }
        public void AboutGame()
        {
            string text = "";
            try
            {
                StreamReader sr = new StreamReader("D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\about.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null && line != "")
                    {
                        text += line + "\n";
                    }
                }
                sr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(text, "About");
        }
    }
}
