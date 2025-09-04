using System;
using System.IO;
using System.Threading.Tasks;

namespace MineSweeperGame.Persistence
{
    public class TextFilePersistence : IPersistence
    {
        public async Task<(Int32[,], Boolean[,], Player)> LoadAsync(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String[] line = (await reader.ReadLineAsync())!.Split();
                    Int32 n = Convert.ToInt32(line[0]);
                    Int32[,] fields = new Int32[n, n];
                    Boolean[,] coveredFields = new Boolean[n, n];
                    Player player = (Player)Convert.ToInt32(line[1]);
                    for (int i = 0; i < n; i++)
                    {
                        line = (await reader.ReadLineAsync())!.Split();
                        for (int j = 0; j < 2 * n; j += 2)
                        {
                            fields[i, j / 2] = Convert.ToInt32(line[j]);
                            coveredFields[i, j / 2] = line[j + 1] == "1";
                        }
                    }
                    return (fields, coveredFields, player);
                }
            }
            catch
            {
                throw new DataException("Error occurred during reading.");
            }
        }

        public async Task SaveAsync(String path, Int32[,] fields, Boolean[,] coveredFields, Player player)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            if (coveredFields == null)
                throw new ArgumentNullException(nameof(coveredFields));

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    int n = fields.GetLength(0);
                     await writer.WriteLineAsync(n.ToString() + " " + ((Int32)player).ToString());
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n - 1; j++)
                        {
                            await writer.WriteAsync(fields[i, j].ToString() + " " + (coveredFields[i, j] ? "1" : "0") + " ");
                        }
                        await writer.WriteAsync(fields[i, n - 1].ToString() + " " + (coveredFields[i, n - 1] ? "1" : "0") + "\n");
                    }
                }
            }
            catch
            {
                throw new DataException("Error occurred during writing.");
            }
        }
    }
}
