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
        private GamePlayer _playerTurn;
        private bool canCapture;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board, GamePlayer playerTurn)
        {
            this._board = board;
            this._playerTurn = playerTurn;
        }
        private void CheckIfCanBeKingAndModify(Cell cell)
        {
            if(cell.Position.X==_board.Count-1||cell.Position.X==0)
            {
                Helper.MakeKing();
            }
            
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
                canCapture = false;
                Helper.ClearNeighboarWithPeace();
            }
        }
        private List<Cell> FindWhiteMoves(Cell cell)
        {
            List<Cell> neighboards = new List<Cell>();

            // Verificăm dacă celula se află pe ultima linie a bordului
            if (cell.Position.X == _board.Count - 1)
                return neighboards;  // Nu există mutări posibile către vecinii de dedesubt

            // Verificăm vecinul din dreapta-jos (X+1, Y+1)
            if (cell.Position.Y < _board[cell.Position.X].Count - 1)  // Verificăm limita de coloane
            {
                Cell neighbor = _board[cell.Position.X + 1][cell.Position.Y + 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            // Verificăm vecinul din stânga-jos (X+1, Y-1)
            if (cell.Position.Y > 0)  // Verificăm limita de coloane
            {
                Cell neighbor = _board[cell.Position.X + 1][cell.Position.Y - 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            return neighboards;
        }

        private List<Cell> FindRedMoves(Cell cell)
        {
            List<Cell> neighboards = new List<Cell>();

            // Verificăm dacă celula se află pe ultima linie a bordului
            if (cell.Position.X == 0)
                return neighboards;  // Nu există mutări posibile către vecinii de deasupra

            // Verificăm vecinul din stânga-sus (X-1, Y-1)
            if (cell.Position.Y > 0)  // Verificăm limita de coloane
            {
                Cell neighbor = _board[cell.Position.X - 1][cell.Position.Y - 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            // Verificăm vecinul din dreapta-sus (X-1, Y+1)
            if (cell.Position.Y < _board[cell.Position.X].Count - 1)  // Verificăm limita de coloane
            {
                Cell neighbor = _board[cell.Position.X - 1][cell.Position.Y + 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            return neighboards;
        }
        private bool FindCapturePosition(Cell currentCell,Cell destinationCell)
        {
            if (Helper.NeighboardsWithPiece != null)
            {
                foreach (Cell neighboarCell in Helper.NeighboardsWithPiece)
                {
                    if (neighboarCell.Piece.Color != currentCell.Piece.Color)
                    {
                        Position position = Helper.Next(currentCell, neighboarCell);
                        if(destinationCell.Position.X == position.X&&destinationCell.Position.Y==position.Y){
                            if (position.X <= _board.Count - 1 && position.Y <= _board.Count - 1 && _board[position.X][position.Y].Piece == null)
                            {
                                canCapture = true;
                                Helper.CapturedCellPosition = neighboarCell.Position;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;

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
            CheckIfCanBeKingAndModify(destinationCell);
            _board[currentCell.Position.X][currentCell.Position.Y].Piece = null;
        }
        private void Capture()
        {
            PieceColor color = _board[Helper.CapturedCellPosition.X][Helper.CapturedCellPosition.Y].Piece.Color;
            _board[Helper.CapturedCellPosition.X][Helper.CapturedCellPosition.Y].Piece = null;
            if (color == PieceColor.Red)
                Helper.RED_PIECES--;
            else
                Helper.WHITES_PIECES--;
        }
        private void Move(Cell currentCell,Cell destinationCell)
        {
            if (_playerTurn.Player == currentCell.Piece.Color&&currentCell!=destinationCell)
            {
                var moves=GetAllMoves(currentCell);
                if (canCapture == false&& !FindCapturePosition(currentCell,destinationCell))
                {
                    foreach (var move in moves)
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
                if (Helper.RED_PIECES == 0)
                {
                    MessageBox.Show("Alb a castigat");
                }
                else if(Helper.WHITES_PIECES==0)
                    MessageBox.Show("Rosu a castigat");
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
