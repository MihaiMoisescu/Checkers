using Checkers.Enums;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static int RED_PIECES= 12;
        public static int WHITES_PIECES= 12;
        public static Cell CurrentCell { get; set; }
        public static Position CapturedCellPosition { get; set; }
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
        
        public static ObservableCollection<ObservableCollection<Cell>> InitBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> board=new ObservableCollection<ObservableCollection<Cell>>();
            int boardSize = 8;
            for (int i = 0;i<boardSize;i++)
            {
                board.Add(new ObservableCollection<Cell>());
                for (int j = 0;j<boardSize;j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i].Add(new Cell(new Position(i, j), whiteCell, CellType.White, null)) ;
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
            board.Clear();
            board = InitBoard();
        }
        public static Position Next(Cell currentCell,Cell occupiedCell)
        {
            int nexTX = occupiedCell.Position.X + (occupiedCell.Position.X - currentCell.Position.X);
            int nextY= occupiedCell.Position.Y + (occupiedCell.Position.Y - currentCell.Position.Y);

            return new Position(nexTX, nextY);
        }
        public static void ClearNeighboarWithPeace()
        {
            if(NeighboardsWithPiece!=null)
                NeighboardsWithPiece.Clear();
        }
        public static void MakeKing()
        {
            if(CurrentCell.Piece.Type!=PieceType.King)
            {
                if(CurrentCell.Piece.Color==PieceColor.Red)
                {
                    CurrentCell.Piece.Type=PieceType.King;
                    CurrentCell.Piece.Background = redPieceKing;
                }
                else
                {
                    CurrentCell.Piece.Type = PieceType.King;
                    CurrentCell.Piece.Background = whitePieceKing;
                }
            }
        }
    }

}
