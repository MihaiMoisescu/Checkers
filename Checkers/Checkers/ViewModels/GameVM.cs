using Checkers.Enums;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    class GameVM :BaseNotification
    {
        private GameBusinessLogic businessLogic;

        public ActionsVM Actions { get; set; }
        public GamePlayerVM Player { get; set; }
        public PieceService Service { get; set; }
        public ObservableCollection<ObservableCollection<CellVM>> GameBoard {  get; set; }
        public GameVM()
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitBoard();
            GamePlayer player = new GamePlayer(PieceColor.Red);
            Service = new PieceService(this);
            businessLogic = new GameBusinessLogic(board, player,Service);

            Player = new GamePlayerVM(businessLogic, player);
            GameBoard = CellBoardToCellVMBoard(board);
            Actions = new ActionsVM(businessLogic);
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
                    CellVM cellVM = new CellVM(c,businessLogic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
    }
}
