using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Threading;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> _board;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> board)
        {
            this._board = board;
        }
        public void MoveCell(Cell cell)
        {
            if(Helper.CurrentCell ==null)
            {
                Helper.CurrentCell = cell;
            }
            else
            {
                cell.Piece=Helper.CurrentCell.Piece;
                var sourcePosition = Helper.CurrentCell.Position;
                var destinationPosition = cell.Position;
                // Mutăm piesa din celula sursă în celula destinație
                _board[sourcePosition.X][sourcePosition.Y].Piece = cell.Piece;
                // Golim celula sursă
                _board[sourcePosition.X][sourcePosition.Y].Piece = null;

                // Actualizăm celula destinație în colecția _board
                _board[destinationPosition.X][destinationPosition.Y].Piece = Helper.CurrentCell.Piece;
                // Golim celula sursă
                _board[destinationPosition.X][destinationPosition.Y].Piece = null;

            }
        }
        public void NewGameAction()
        {
            MessageBox.Show(" ");
            _board.Clear();
            _board=Helper.InitBoard();
            MessageBox.Show(" ");
        }
        public void OpenGame()
        {

        }
        public void SaveGame()
        {

        }
        public void Statistics()
        {

        }
        public void AllowMultipeJumps()
        {

        }
        public void AboutGame()
        {
            string text = "";
            try
            {
                StreamReader sr = new StreamReader("D:\\SCOALA\\AN 2\\SEM 2\\MAP\\Checkers\\Checkers\\Checkers\\Resources\\about.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null && line != "")
                    {
                        text += line + "\n";
                    }
                }
                sr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(text, "About");
        }
    }
}
