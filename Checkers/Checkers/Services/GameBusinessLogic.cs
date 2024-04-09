using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> _board;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board)
        {
            this._board = board;
        }
    }
}
