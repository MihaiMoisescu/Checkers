using Checkers.Commands;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class ActionsVM : BaseNotification
    {
        private GameBusinessLogic logic;
        private ICommand newGame;
        private ICommand openGame;
        private ICommand saveGame;
        private ICommand aboutGame;
        private ICommand statistics;
        private ICommand allowMultipleJumps;

        public ActionsVM(GameBusinessLogic logic)
        {
            this.logic = logic;
        }
        public ICommand NewGame
        {
            get
            {
                if (newGame == null)
                    newGame = new ActionCommands(logic.NewGameAction);
                return newGame;
            }
        }
        public ICommand SaveGame
        {
            get
            {
                if (saveGame == null)
                    saveGame = new ActionCommands(logic.SaveGame);
                return saveGame;
            }
        }
        public ICommand OpenGame
        {
            get
            {
                if (openGame == null)
                    openGame = new ActionCommands(logic.OpenGame);
                return openGame;
            }
        }
        public ICommand AboutGame
        {
            get
            {
                if (aboutGame == null)
                    aboutGame = new ActionCommands(logic.AboutGame);
                return aboutGame;
            }
        }
        public ICommand Statistics
        {
            get
            {
                if (statistics == null)
                    statistics = new ActionCommands(logic.Statistics);
                return statistics;
            }
        }
        public ICommand AllowMultipleJumps
        {
            get
            {
                if (allowMultipleJumps == null)
                    allowMultipleJumps = new ActionCommands(logic.AllowMultipeJumps);
                return allowMultipleJumps;
            }
        }
    }
}
