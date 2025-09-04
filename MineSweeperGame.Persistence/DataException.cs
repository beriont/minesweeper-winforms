using System;

namespace MineSweeperGame.Persistence
{
    public class DataException : Exception
    {
        public DataException(String message) : base(message) { }
    }
}
