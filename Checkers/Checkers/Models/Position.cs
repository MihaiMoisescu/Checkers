using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Position:BaseNotification
    {
        private int _x;
        private int _y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position()
        {

        }
        public int X
        {
            get { return _x; }
            set 
            { 
                _x = value;
                NotifyPropertyChanged("X");
            }
        }
        public int Y
        {
            get
            {
               return _y;
            }
            set
            {
                _y = value;
                NotifyPropertyChanged("Y");
            }
        }
    }
}
