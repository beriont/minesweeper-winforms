using System;
using MineSweeperGame.Persistence;

namespace MineSweeperGame.Model
{
    public class GameWonEventArgs : EventArgs
    {
        public Player Player { get; private set; }

        public GameWonEventArgs(Player player) { Player = player; }
    }
}
