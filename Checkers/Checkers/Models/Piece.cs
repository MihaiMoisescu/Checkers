using Checkers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Checkers.Models
{
    class Piece :BaseNotification
    {
        private PieceColor _color;
        private PieceType _type;
        private string _background;

        public Piece()
        {

        }
        public PieceColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged("Color");
            }

        }
        public PieceType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }
        public string Background
        {
            get
            {
                return _background;

            }
            set
            {
                _background = value;
                NotifyPropertyChanged("Background");
            }
        }
        public Piece(PieceColor color, PieceType type, string background)
        {
            Color = color;
            Type = type;
            Background = background;
        }
    }
}
