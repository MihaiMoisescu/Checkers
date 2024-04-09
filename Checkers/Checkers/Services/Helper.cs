using Checkers.Enums;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public static Cell CurrentCell { get; set; }
        public static Cell PreviousCell { get; set; }

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
    }

}
