using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BotChallenge.BLL.Logic;
using BotChallenge.BLL.Models;

namespace BotChallenge.BLL.UnitTests.Logic
{
    [TestClass]
    public class GamePoolUnitTest
    {
        [TestMethod]
        public void Test_Adding_1_Player_To_Pool()
        {
            // arrange
            Player p1 = new Player() { Name = "Lorem" };
            GamePool.FreeGames.Clear();

            // act
            GamePool.RegisterPlayer(p1);

            // assert
            Assert.IsTrue(GamePool.FreeGames.Count > 0);

            Game g = GamePool.FreeGames.Dequeue();
            Assert.IsNotNull(g);
            Assert.IsTrue(g.Players.Contains(p1));

        }

        [TestMethod]
        public void Test_Adding_2_Players_To_Pool()
        {
            // arrange
            Player p1 = new Player() { Name = "lorem" };
            Player p2 = new Player() { Name = "ipsum" };

            GamePool.FreeGames.Clear();
            GamePool.BusyGames.Clear();

            // act
            GamePool.RegisterPlayer(p1);
            GamePool.RegisterPlayer(p2);

            // assert
            Assert.IsTrue(GamePool.FreeGames.Count == 0);
            Assert.IsTrue(GamePool.BusyGames.Count == 1);

            Game g = GamePool.BusyGames[0];

            Assert.IsNotNull(g);
            Assert.IsTrue(g.Players.Contains(p1));
            Assert.IsTrue(g.Players.Contains(p2));
        }
    }
}
