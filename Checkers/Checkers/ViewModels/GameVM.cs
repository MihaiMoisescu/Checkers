﻿using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    class GameVM
    {
        private GameBusinessLogic businessLogic;
        public ObservableCollection<ObservableCollection<CellVM>> GameBoard {  get; set; }
        public GameVM()
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitBoard();
            businessLogic = new GameBusinessLogic(board);
            GameBoard = CellBoardToCellVMBoard(board);
        }
        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(new Position(c.Position.X,c.Position.Y),c.Background,c.CellType,c.Piece,businessLogic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
    }
}
