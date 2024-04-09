using Checkers.Enums;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    class CellVM
    {
        GameBusinessLogic _businessLogic;
        public CellVM(Position position,string background,CellType cellType,Piece piece,GameBusinessLogic businessLogic)
        {
            GameCell = new Cell(position, background, cellType, piece);
            this._businessLogic = businessLogic;
        }
        public Cell GameCell { get; set; }
    }
}
