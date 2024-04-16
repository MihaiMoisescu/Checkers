using Checkers.Enums;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Checkers.Models
{
    internal class GamePlayer:BaseNotification
    {
        PieceColor player;
        private string image;
        public GamePlayer(PieceColor color) {
            player = color;
            loadImages();
        }
        public void loadImages()
        {
            if (player == PieceColor.Red)
            {
                image = Helper.redTurn;
                return;
            }
            image = Helper.whiteTurn;
        }
        public PieceColor Player
        {
            get 
            { 
                return player; 
            }
            set 
            { 
                player = value;
                NotifyPropertyChanged("Player");
            }
        }
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }
    }
}
