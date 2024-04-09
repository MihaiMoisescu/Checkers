using Checkers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Cell :BaseNotification
    {
        private Position _position;
        private string _background;
        private Piece _piece;
        private CellType _cellType;
        public Cell() { }

        public string Background
        {
            get { return _background; }
            set
            {
                _background = value;
                NotifyPropertyChanged("Background");
            }
        }
        public Piece Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                NotifyPropertyChanged("Piece");
            }
        }
        public CellType CellType
        {
            get
            {
                return _cellType;
            }
            set
            {
                _cellType = value;
                NotifyPropertyChanged("CellType");
            }
        }
        public Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                NotifyPropertyChanged("Position");
            }
        }
        public Cell(Position position, string background, CellType cellType,Piece piece)
        {
            Background = background;
            Piece = piece;
            CellType = cellType;
            Position = position;
        }
    }
}
