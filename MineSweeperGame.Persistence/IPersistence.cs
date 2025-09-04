using System;
using System.Threading.Tasks;

namespace MineSweeperGame.Persistence
{
    public interface IPersistence
    {
        Task<(Int32[,], Boolean[,], Player)> LoadAsync(String path);

        Task SaveAsync(String path, Int32[,] fields, Boolean[,] coveredFields, Player player);
    }
}
