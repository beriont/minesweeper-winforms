using MineSweeperGame.Persistence;
using MineSweeperGame.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;

namespace MineSweeperGame.Test
{
    [TestClass]
    public class MineSweeperModelTest
    {
        private Mock<IPersistence> _mock = null!;
        private MineSweeperModel _model = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mock = new Mock<IPersistence>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>())).Returns(Task.Run(() =>
            {
                Int32[,] t = new Int32[6, 6];
                Int32[] Xes = { 2, 3, 0, 3 };
                int[] Ys = { 2, 3, 4, 0 };
                for (int i = 0; i < 4; i++)
                {
                    t[Xes[i], Ys[i]] = -1;
                    for (int j = Xes[i] - 1; j <= Xes[i] + 1; j++)
                    {
                        if (j >= 0 && j < 6)
                        {
                            for (int k = Ys[i] - 1; k <= Ys[i] + 1; k++)
                            {
                                if (k >= 0 && k < 6 && t[j, k] != -1)
                                {
                                    t[j, k]++;
                                }
                            }
                        }
                    }
                }
                Boolean[,] c = new Boolean[6, 6];
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        c[i, j] = true;
                    }
                }
                Player p = Player.PlayerOne;
                return (t, c, p);
            }));
            _model = new MineSweeperModel(_mock.Object);
        }

        [TestMethod]
        public void MineSweeperConstructorTest()
        {
            _model.NewGame();
            Assert.AreEqual(6, _model.TableSize);
            Int32 bombCount = 0;
            for (Int32 i = 0; i < 6; i++)
                for (Int32 j = 0; j < 6; j++)
                {
                    (Boolean c, Int32 v) = _model[i, j];
                    Assert.IsTrue(c);
                    if (v == -1)
                    {
                        bombCount++;
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            if (k >= 0 && k < 6)
                            {
                                for (int l = j - 1; l <= j + 1; l++)
                                {
                                    if (l >= 0 && l < 6)
                                    {
                                        Assert.IsTrue(_model[k, l].Item2 != 0);
                                    }
                                }
                            }
                        }
                    }
                }
            Assert.AreEqual(6, bombCount);
        }

        [TestMethod]
        public async Task MineSweeperStepGameTest()
        {
            await _model.LoadGameAsync(String.Empty);
            _model.StepGame(1, 1);
            (Boolean c, Int32 v) = _model[1, 1];
            Assert.IsFalse(c);
            Assert.AreEqual(1, v);
            for (Int32 i = 0; i <= 2; i++)
                for (Int32 j = 0; j <= 2; j++)
                {
                    if (i != 1 || j != 1)
                        Assert.IsTrue(_model[i, j].Item1);
                }

            _model.StepGame(0, 1);
            Int32[] vals = { 0, 0, 0, 1, 0, 1, 1, 2, 1, 2 };
            Int32 counter = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    (c, v) = _model[i, j];
                    if (i <= 1 && j <= 3 || i == 2 && j <= 1)
                    {
                        Assert.IsFalse(c);
                        Assert.AreEqual(vals[counter++], v);
                    }
                    else
                    {
                        Assert.IsTrue(c);
                    }
                }
            }

            try
            {
                _model.StepGame(0, 1);
                Assert.Fail();
            }
            catch (InvalidOperationException) { }

            Assert.AreEqual((false, 0), _model[0, 1]);
        }

        [TestMethod]
        public async Task MineSweeperUncoveredFieldNumberTest()
        {
            await _model.LoadGameAsync(String.Empty);
            Assert.AreEqual(0, _model.UncoveredFieldCount);

            _model.StepGame(0, 0);
            _model.StepGame(5, 5);
            _model.StepGame(2, 3);
            Assert.AreEqual(29, _model.UncoveredFieldCount);
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 1, 1)]
        [DataRow(2, 0, 1)]
        [DataRow(2, 2, -1)]
        public async Task MineSweeperIndexerValidTest(int x, int y, int value)
        {
            await _model.LoadGameAsync(String.Empty);
            Assert.IsTrue(_model[x, y].Item1);
            Assert.AreEqual(value, _model[x, y].Item2);
            _model.StepGame(x, y);
            Assert.IsFalse(_model[x, y].Item1);
        }

        [TestMethod]
        [DataRow(-1, 0)]
        [DataRow(6, 1)]
        [DataRow(1, -1)]
        [DataRow(2, 6)]
        [ExpectedException(typeof(ArgumentException), "Invalid position.")]
        public void MineSweeperIndexerInvalidTest(int x, int y)
        {
            _model.NewGame();
            (Boolean c, Int32 v) = _model[x, y];
        }

        [TestMethod]
        public async Task MineSweeperGameWonTest()
        {
            bool eventRaised = false;
            _model.GameWon += delegate (object? sender, GameWonEventArgs e)
            {
                eventRaised = true;
                Assert.IsTrue(e.Player == Player.PlayerOne);
            };

            await _model.LoadGameAsync(String.Empty);
            _model.StepGame(0, 0);
            _model.StepGame(5, 5);
            _model.StepGame(3, 2);
            _model.StepGame(2, 2);

            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public async Task MineSweeperGameLoadAsyncTest()
        {
            _model.NewGame();
            _model.StepGame(0, 0);

            await _model.LoadGameAsync(String.Empty);

            Int32 uncoveredFieldCount = 0;
            for (Int32 i = 0; i < 6; i++)
                for (Int32 j = 0; j < 6; j++)
                {
                    (Boolean c, Int32 v) = _model[i, j];
                    Assert.IsTrue(c);
                    uncoveredFieldCount += c ? 0 : 1;
                }

            Assert.AreEqual(uncoveredFieldCount, _model.UncoveredFieldCount);
            Assert.AreEqual(Player.PlayerOne, _model.CurrentPlayer);

            _mock.Verify(dataAccess => dataAccess.LoadAsync(String.Empty), Times.Once());
        }

        [TestMethod]
        public async Task MineSweeperGameSaveTest()
        {
            Player currentPlayer = _model.CurrentPlayer;
            Int32 uncoveredFieldCount = _model.UncoveredFieldCount;
            Int32 bombCount = _model.BombCount;
            Boolean[,] coveredFields = new Boolean[6, 6];
            Int32[,] values = new Int32[6, 6];

            for (Int32 i = 0; i < 6; i++)
                for (Int32 j = 0; j < 6; j++)
                {
                    (Boolean c, Int32 v) = _model[i, j];
                    coveredFields[i, j] = c;
                    values[i, j] = v;
                }

            await _model.SaveGameAsync(String.Empty);

            Assert.AreEqual(currentPlayer, _model.CurrentPlayer);
            Assert.AreEqual(uncoveredFieldCount, _model.UncoveredFieldCount);
            Assert.AreEqual(bombCount, _model.BombCount);

            for (Int32 i = 0; i < 6; i++)
                for (Int32 j = 0; j < 6; j++)
                {
                    Assert.AreEqual((coveredFields[i, j], values[i, j]), _model[i, j]);
                }

            _mock.Verify(mock => mock.SaveAsync(String.Empty, It.IsAny<Int32[,]>(), It.IsAny<Boolean[,]>(), It.IsAny<Player>()), Times.Once());
        }
    }
}