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
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> _board;
        private GamePlayer _playerTurn;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board, GamePlayer playerTurn)
        {
            this._board = board;
            _playerTurn = playerTurn;
        }
        private void SwitchTurns(Cell cell)
        {
            if (cell.Piece != null)
            {
                if (cell.Piece.Color == PieceColor.Red)
                {
                    _playerTurn.Player = PieceColor.White;
                    _playerTurn.Image = Helper.whitePiece;
                }
                else
                {
                    _playerTurn.Player = PieceColor.Red;
                    _playerTurn.Image = Helper.redPiece;
                }
            }
        }

        private bool VerifyCanCapture(Cell destinationCell,int posX,int posY,int posX1,int posY1,PieceColor piece)
        {
            if (destinationCell == _board[posX][posY] && _board[posX][posY].Piece == null &&
                (_board[posX1][posY1].Piece != null &&
                _board[posX1][posY1].Piece.Color == piece))
            {
                return true;
            }
            return false;
        }
        private bool CanCapture(Cell currentCell, Cell destinationCell)
        {
            if (currentCell.Piece == null || _board == null)
            {
                return false;
            }

            int posX = currentCell.Position.X;
            int posY = currentCell.Position.Y;

            if (posX < 2 || posX >= _board.Count - 2 ||
                posY < 2 || posY >= _board.Count - 2)
            {
                return false;
            }

            if (currentCell.Piece.Color == PieceColor.White)
            {
                if (currentCell.Piece.Type == PieceType.King)
                {
                    if (VerifyCanCapture(destinationCell,posX-2,posY-2,posX-1,posY-1,PieceColor.Red))
                    {

                        Helper.CapturedCellPosition=new Position(posX-1, posY-1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX - 2, posY + 2, posX - 1, posY + 1, PieceColor.Red))
                    {
                        Helper.CapturedCellPosition = new Position(posX - 1, posY + 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX + 2, posY + 2, posX + 1, posY + 1, PieceColor.Red))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY + 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX + 2, posY - 2, posX + 1, posY - 1, PieceColor.Red))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY - 1);
                        return true;
                    }
                }
                else
                {
                    if (VerifyCanCapture(destinationCell, posX + 2, posY + 2, posX + 1, posY + 1, PieceColor.Red))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY + 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX + 2, posY - 2, posX + 1, posY - 1, PieceColor.Red))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY - 1);
                        return true;
                    }
                }
            }
            else
            {
                if (currentCell.Piece.Type == PieceType.King)
                {
                    if (VerifyCanCapture(destinationCell, posX - 2, posY - 2, posX - 1, posY - 1,PieceColor.White))
                    {

                        Helper.CapturedCellPosition = new Position(posX - 1, posY - 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX - 2, posY + 2, posX - 1, posY + 1, PieceColor.White))
                    {
                        Helper.CapturedCellPosition = new Position(posX - 1, posY + 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX + 2, posY + 2, posX + 1, posY + 1, PieceColor.White))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY + 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX + 2, posY - 2, posX + 1, posY - 1, PieceColor.White))
                    {
                        Helper.CapturedCellPosition = new Position(posX + 1, posY - 1);
                        return true;
                    }
                }
                else
                {
                    if (VerifyCanCapture(destinationCell, posX - 2, posY - 2, posX - 1, posY - 1, PieceColor.White))
                    {
                        Helper.CapturedCellPosition = new Position(posX - 1, posY - 1);
                        return true;
                    }
                    if (VerifyCanCapture(destinationCell, posX - 2, posY + 2, posX - 1, posY + 1, PieceColor.White))
                    {
                        Helper.CapturedCellPosition = new Position(posX - 1, posY + 1);
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Cell> FindWhiteMoves(Cell cell)
        {
            List<Cell> neighboards = new List<Cell>();
            if(cell.Position.X==_board.Count-1)
                return neighboards;
            if(cell.Position.Y<_board.Count-1)
                neighboards.Add(_board[cell.Position.X + 1][cell.Position.Y+1]);
            if (cell.Position.Y > 0)
                neighboards.Add(_board[cell.Position.X + 1][cell.Position.Y - 1]);

            return neighboards;
        }
        private List<Cell> FindRedMoves(Cell cell)
        {
            List<Cell> neighboards = new List<Cell>();
            if (cell.Position.X == _board.Count - 1)
                return neighboards;
            if (cell.Position.Y >0)
                neighboards.Add(_board[cell.Position.X - 1][cell.Position.Y - 1]);
            if (cell.Position.Y < _board.Count-1)
                neighboards.Add(_board[cell.Position.X - 1][cell.Position.Y + 1]);

            return neighboards;
        }
        private List<Cell> GetAllMoves(Cell cell)
        {
            if (cell.Piece.Type == PieceType.King)
                return FindRedMoves(cell).Concat(FindWhiteMoves(cell)).ToList();
            if (cell.Piece.Color == PieceColor.Red)
                return FindRedMoves(cell);
            else
                return FindWhiteMoves(cell);

        }
        private void MovePiece(Cell currentCell, Cell destinationCell)
        {
            destinationCell.Piece = currentCell.Piece;
            _board[currentCell.Position.X][currentCell.Position.Y].Piece = null;
        }
        private void Capture()
        {
            PieceColor color = _board[Helper.CapturedCellPosition.X][Helper.CapturedCellPosition.Y].Piece.Color;
            _board[Helper.CapturedCellPosition.X][Helper.CapturedCellPosition.Y].Piece = null;

        }
        private void Move(Cell currentCell,Cell destinationCell)
        {
            if (_playerTurn.Player == currentCell.Piece.Color&&currentCell!=destinationCell)
            {
                if (!CanCapture(currentCell, destinationCell))
                {
                    foreach (var move in GetAllMoves(currentCell))
                    {
                        if (move == destinationCell && move.Piece == null)
                        {
                            MovePiece(currentCell, destinationCell);
                            SwitchTurns(destinationCell);

                            break;
                        }
                    }
                }
                else
                {
                    MovePiece(currentCell, destinationCell);
                    Capture();
                    SwitchTurns(destinationCell);
                }
            }
        }

        public void MoveCell(Cell cell)
        {
            if(Helper.CurrentCell ==null)
            {
                if (cell.Piece != null)
                {
                    Helper.CurrentCell = cell;
                }
            }
            else
            {
                Move(Helper.CurrentCell, cell);
                Helper.CurrentCell = null;

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
