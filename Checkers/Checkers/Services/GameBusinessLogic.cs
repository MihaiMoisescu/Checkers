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
        public GamePlayer _playerTurn;
        private PieceService _pieceService;
        private bool _extraMove;
        private bool _extraJump;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board, GamePlayer playerTurn,PieceService service)
        {
            this._board = board;
            this._playerTurn = playerTurn;
            _pieceService= service;
        }
        public bool ExtraMove
        {
            get
            {
                return _extraMove;
            }
            set
            {
                _extraMove = value;
            }
        }
        private void NewGameInitialization()
        {
            _pieceService.WhitePieces = 12;
            _pieceService.RedPieces = 12;
            _playerTurn.Player = PieceColor.Red;
            _playerTurn.Image = Helper.redTurn;
        }
        private void CheckIfCanBeKingAndModify(Cell cell)
        {
            if(cell.Position.X==_board.Count-1||cell.Position.X==0)
            {
                Helper.MakeKing(cell);
            }
            
        }
        private void SwitchTurns(Cell cell)
        {
                if (cell.Piece.Color == PieceColor.Red)
                {
                    Helper.GPlayer.Player = PieceColor.White;
                    Helper.GPlayer.Image = Helper.whiteTurn;
                    _playerTurn.Player = PieceColor.White;
                    _playerTurn.Image = Helper.whiteTurn;
                }
                else
                {
                    Helper.GPlayer.Player = PieceColor.Red;
                    Helper.GPlayer.Image = Helper.redTurn;
                    _playerTurn.Player = PieceColor.Red;
                    _playerTurn.Image = Helper.redTurn;
                }
                Helper.ClearNeighboarWithPeace();
        }
        private List<Cell> FindWhiteMoves(Cell cell)
        {
            List<Cell> neighboards = new List<Cell>();

            if (cell.Position.X == _board.Count - 1)
                return neighboards; 

            if (cell.Position.Y < _board[cell.Position.X].Count - 1)  
            {
                Cell neighbor = _board[cell.Position.X + 1][cell.Position.Y + 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            if (cell.Position.Y > 0) 
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
            if (cell.Position.X == 0)
                return neighboards;  


            if (cell.Position.Y > 0) 
            {
                Cell neighbor = _board[cell.Position.X - 1][cell.Position.Y - 1];
                if (neighbor.Piece != null)
                    Helper.NeighboardsWithPiece.Add(neighbor);
                neighboards.Add(neighbor);
            }

            if (cell.Position.Y < _board[cell.Position.X].Count - 1)  
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
                    if (neighboarCell.Piece != null&& neighboarCell.Piece.Color != currentCell.Piece.Color)
                    {
                        Position position = Helper.Next(currentCell, neighboarCell);
                        if(destinationCell.Position.X == position.X&&destinationCell.Position.Y==position.Y){
                            if (position.X <= _board.Count - 1 && position.Y <= _board.Count - 1 && _board[position.X][position.Y].Piece == null)
                            {
                                Helper.CapturedCellPosition = neighboarCell.Position;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;

        }
        private bool CanExtraJump(Cell cell)
        {
            if(ExtraMove==true)
            {
                Helper.ClearNeighboarWithPeace();
                var moves=GetAllMoves(cell);
                if(Helper.NeighboardsWithPiece!= null)
                {
                    foreach(Cell neighboar in  Helper.NeighboardsWithPiece)
                    {
                        Position position =Helper.Next(cell, neighboar);
                        if(position.X>=0&&position.Y>=0&&position.X <= _board.Count -1&& position.Y <= _board.Count - 1 && _board[position.X][position.Y].Piece==null)
                        {
                             Helper.CapturedCellPosition=neighboar.Position;
                            Helper.ExtraMovePosition=position;
                            return true;
                        }
                    }
                }
                else
                    return false;

            }
            return false;
        }
        private void DoExtraJump(Cell cell)
        {
            if(CanExtraJump(cell))
            {
                Capture();
                MovePiece(cell, _board[Helper.ExtraMovePosition.X][Helper.ExtraMovePosition.Y]);
            }
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
            {
                _pieceService.RedPieces--;
                Helper.RED_PIECES--;
            }
            else
            {
                _pieceService.WhitePieces--;
                Helper.WHITES_PIECES--;
            }
        }
        private void Move(Cell currentCell,Cell destinationCell)
        {
            if (_playerTurn.Player == currentCell.Piece.Color&&currentCell!=destinationCell)
            {
                var moves=GetAllMoves(currentCell);
                if (!FindCapturePosition(currentCell,destinationCell))
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
                    if (CanExtraJump(destinationCell))
                    {
                        DoExtraJump(destinationCell);
                        SwitchTurns(_board[Helper.ExtraMovePosition.X][Helper.ExtraMovePosition.Y]);

                    }
                    else
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
            if(Helper.RED_PIECES==0||Helper.WHITES_PIECES==0)
            {
                GameOver();
            }
        }


        public void GameOver()
        {
            if (Helper.RED_PIECES == 0)
            {
                MessageBox.Show("White wins!");
            }
            else if (Helper.WHITES_PIECES == 0)
                MessageBox.Show("Red wins");
            Helper.WriteScore();
            NewGameInitialization();
            Helper.ResetGame(_board);
        }
        public void NewGameAction()
        {
            NewGameInitialization();
            Helper.ResetGame(_board);
        }
        public void OpenGame()
        {
            Helper.OpenGame(_board,_playerTurn,_extraMove);
        }
        public void SaveGame()
        {
            Helper.SaveGame(_board,_extraMove);
        }
        public void Statistics()
        {
            Helper.ShowStatistics();
        }
        public void AllowMultipeJumps()
        {
            if(ExtraMove==false)
                ExtraMove = true;
            else
                ExtraMove = false;
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
