using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    internal class GamePlayerVM:BaseNotification
    {
        GameBusinessLogic _gameBusinessLogic;
        GamePlayer _gamePlayer;
        public GamePlayerVM(GameBusinessLogic gameBusinessLogic, GamePlayer gamePlayer)
        {
            _gameBusinessLogic = gameBusinessLogic;
            _gamePlayer = gamePlayer;
        }

        public GamePlayer GamePlayer
        {
            get
            {
                return _gamePlayer; 
            }
            set 
            { 
                _gamePlayer = value;
                NotifyPropertyChanged("GamePlayer");
            }

        }
    }
}
