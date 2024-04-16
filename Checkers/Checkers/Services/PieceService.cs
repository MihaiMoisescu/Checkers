using Checkers.Models;
using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Services
{
    class PieceService : BaseNotification
    {
        GameVM game;
        private int _whitePieces;
        private int _redPieces;

        public PieceService(GameVM game)
        {
            this.game = game;
            _whitePieces = 12;
            _redPieces = 12;
        }
        public int WhitePieces
        {
            get
            {
                return _whitePieces;
            }
            set
            {
                _whitePieces = value;
                NotifyPropertyChanged(nameof(WhitePieces));
            }
        }
        public int RedPieces
        {
            get
            {
                return _redPieces;
            }
            set
            {
                _redPieces = value;
                NotifyPropertyChanged(nameof(RedPieces));
            }
        }
    }
}
