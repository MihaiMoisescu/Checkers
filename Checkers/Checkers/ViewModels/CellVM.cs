using Checkers.Commands;
using Checkers.Enums;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class CellVM :BaseNotification
    {
        private GameBusinessLogic _businessLogic;
        private Cell _gameCell;
        public CellVM(Cell cell,GameBusinessLogic businessLogic)
        {
            GameCell = cell;
            this._businessLogic = businessLogic;
        }
        public Cell GameCell { 
            get
            {
                return _gameCell;
            }
            set
            {
                _gameCell = value;
                NotifyPropertyChanged("GameCell");
            }
        }

        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get
            {
                if(_moveCommand == null)
                {
                    _moveCommand = new RelayCommand<Cell>(_businessLogic.MoveCell);
                }
                return _moveCommand;
            }
        }
    }
}
