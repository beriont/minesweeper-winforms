using System;
using System.Collections.Generic;
using System.Drawing;
using MineSweeperGame.Persistence;
using System.Threading.Tasks;

namespace MineSweeperGame.Model
{
    public class MineSweeperModel
    {
        #region Private fields

        private Player _currentPlayer;
        private Int32[,] _gameTable;
        private Boolean[,] _coveredFields;
        private Int32 _uncoveredFieldCount;
        private Int32 _bombCount;
        private IPersistence _persistence;

        #endregion

        #region Public properties

        public Player CurrentPlayer { get { return _currentPlayer; } }

        public Int32 UncoveredFieldCount { get { return _uncoveredFieldCount; } }

        public Int32 BombCount { get { return _bombCount; } }

        public Int32 TableSize
        {
            get { return _gameTable.GetLength(0); }
        }

        public (Boolean, Int32) this[Int32 x, Int32 y]
        {
            get
            {
                if (x < 0 || x >= _gameTable.GetLength(0))
                    throw new ArgumentException("Bad column index.", nameof(x));
                if (y < 0 || y >= _gameTable.GetLength(1))
                    throw new ArgumentException("Bad row index.", nameof(y));

                return (_coveredFields[x, y], _gameTable[x, y]);
            }
        }

        #endregion

        #region Events

        public event EventHandler<GameWonEventArgs>? GameWon;
        public event EventHandler? GameOver;
        public event EventHandler<FieldChangedEventArgs>? FieldChanged;
        public event EventHandler? LoadSizeChanged;

        #endregion

        #region Constructors

        public MineSweeperModel(IPersistence persistence, Int32 size = 6)
        {
            _gameTable = new Int32[size, size];
            _coveredFields = new Boolean[size, size];
            _persistence = persistence;
            NewGame();
        }

        #endregion

        #region Public methods

        public void NewGame()
        {
            ClearTable();
            PutBombsInTable();
            _uncoveredFieldCount = 0;
            _currentPlayer = Player.PlayerOne;
        }
        public void StepGame(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _gameTable.GetLength(0))
                throw new ArgumentException("Bad column index.", nameof(x));
            if (y < 0 || y >= _gameTable.GetLength(1))
                throw new ArgumentException("Bad row index.", nameof(y));
            if (_uncoveredFieldCount >= _gameTable.Length - _bombCount)
                throw new InvalidOperationException("Game is over!");
            if (_coveredFields[x, y] != true)
                throw new InvalidOperationException("Field is already uncovered!");

            Boolean result = FieldClicked(x, y);
            OnFieldChanged(x, y, _currentPlayer);

            _currentPlayer = _currentPlayer == Player.PlayerOne ? Player.PlayerTwo : Player.PlayerOne;
            CheckGame(result);
        }
        public async Task LoadGameAsync(String path)
        {
            if (_persistence == null)
                return;

            (Int32[,] fields, Boolean[,] coveredFields, Player player) = await _persistence.LoadAsync(path);

            int size = fields.GetLength(0);
            _gameTable = new Int32[size, size];

            if (_gameTable.Length == 0)
                throw new DataException("Error occurred during game loading.");

            _gameTable = fields;
            _coveredFields = coveredFields;
            _currentPlayer = player;
            _uncoveredFieldCount = 0;
            _bombCount = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!_coveredFields[i, j]) _uncoveredFieldCount++;
                    if (_gameTable[i, j] == -1) _bombCount++;
                }
            }
            OnLoadSizeChanged();
            OnFieldChanged(0, 0, player == Player.PlayerOne ? Player.PlayerTwo : Player.PlayerOne);
        }

        public async Task SaveGameAsync(String path)
        {
            if (_persistence == null)
                return;

            await _persistence.SaveAsync(path, _gameTable, _coveredFields, _currentPlayer);
        }
        
        public void ChangeSize(Int32 size)
        {
            _gameTable = new Int32[size, size];
            _coveredFields = new Boolean[size, size];
            NewGame();
        }

        #endregion

        #region Private methods

        private void ClearTable()
        {
            for (Int32 i = 0; i < _gameTable.GetLength(0); i++)
                for (Int32 j = 0; j < _gameTable.GetLength(1); j++)
                {
                    _gameTable[i, j] = 0;
                    _coveredFields[i, j] = true;
                }
        }

        private void PutBombsInTable()
        {
            Random rand = new Random();
            _bombCount = Convert.ToInt32(Math.Ceiling(_gameTable.Length * 0.16));
            for (Int32 i = 0; i < _bombCount; i++)
            {
                Int32 x = rand.Next(0, _gameTable.GetLength(0));
                Int32 y = rand.Next(0, _gameTable.GetLength(1));
                while (_gameTable[x, y] == -1 || x == 0 && y == 0)
                {
                    x = rand.Next(0, _gameTable.GetLength(0));
                    y = rand.Next(0, _gameTable.GetLength(1));
                }
                _gameTable[x, y] = -1;
                FixVicinity(x, y);
            }
        }

        private void FixVicinity(Int32 x, Int32 y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i >= 0 && i < _gameTable.GetLength(0))
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (j >= 0 && j < _gameTable.GetLength(1) && _gameTable[i, j] != -1)
                        {
                            _gameTable[i, j]++;
                        }
                    }
                }
            }
        }

        private Boolean FieldClicked(Int32 x, Int32 y)
        {
            if (_gameTable[x, y] != -1)
            {
                Stack<Point> fields = new Stack<Point>();
                fields.Push(new Point(x, y));
                while (fields.Count > 0)
                {
                    Point field = fields.Pop();
                    int cx = field.X;
                    int cy = field.Y;
                    if (cx >= 0 && cx < _gameTable.GetLength(0) &&
                        cy >= 0 && cy < _gameTable.GetLength(1) &&
                        _coveredFields[cx, cy])
                    {
                        _coveredFields[cx, cy] = false;
                        _uncoveredFieldCount++;
                        if (_gameTable[cx, cy] == 0)
                        {
                            fields.Push(new Point(cx - 1, cy));
                            fields.Push(new Point(cx + 1, cy));
                            fields.Push(new Point(cx, cy - 1));
                            fields.Push(new Point(cx, cy + 1));
                            fields.Push(new Point(cx - 1, cy - 1));
                            fields.Push(new Point(cx - 1, cy + 1));
                            fields.Push(new Point(cx + 1, cy - 1));
                            fields.Push(new Point(cx + 1, cy + 1));
                        }
                    }
                }
                return false;
            }
            else
            {
                for (int i = 0; i < _gameTable.GetLength(0); i++)
                    for (int j = 0; j < _gameTable.GetLength(1); j++)
                    {
                        if (_gameTable[i, j] == -1) _coveredFields[i, j] = false;
                    }
                return true;
            }
        }

        private void CheckGame(Boolean bombClicked)
        {
            if (bombClicked)
            {
                OnGameWon(_currentPlayer);
            }
            else if (_uncoveredFieldCount == _gameTable.Length - _bombCount)
            {
                OnGameOver();
            }
        }

        #endregion

        #region Event triggers

        private void OnGameWon(Player player)
        {
            GameWon?.Invoke(this, new GameWonEventArgs(player));
        }

        private void OnGameOver()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        private void OnFieldChanged(Int32 x, Int32 y, Player player)
        {
            FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y, player));
        }

        private void OnLoadSizeChanged()
        {
            LoadSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
