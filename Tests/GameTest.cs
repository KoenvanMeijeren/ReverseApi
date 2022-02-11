using System;
using NUnit.Framework;
using ReversiApi.Model;

namespace Tests
{
    [TestFixture]
    public class GameTest
    {
        // No color = 0
        // White = 1
        // Black = 2

        [Test]
        public void IntialiseerGame()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.White;
            // Assert
            Assert.AreEqual(Color.White, game.CurrentPlayer);
            Assert.AreEqual(0, game.Id);
            Assert.IsNull(game.Description);
            Assert.IsNull(game.TokenPlayerOne);
            Assert.IsNull(game.TokenPlayerTwo);
        }
        
        [Test]
        public void IntialiseerGame_Token_IsUnqiue()
        {
            // Arrange
            Game game = new Game();
            Game gameTwo = new Game();
            
            // Assert
            Assert.AreNotEqual(game.Token, gameTwo.Token);
        }
        
        [Test]
        public void AreMovesPossible_ColorNone_SkipTurn_ThrowsException()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.None;
            // Assert
            Exception ex = Assert.Throws<Exception>(delegate { game.SkipTurn(); });
            Assert.That(ex.Message, Is.EqualTo("Kleur mag niet gelijk aan Geen zijn!"));
        }
        
        [Test]
        public void AreMovesPossible_ColorNone_ThrowsException()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.None;
            // Assert
            Assert.IsFalse(game.IsMovePossible(3, 5));
            Assert.IsFalse(game.IsMovePossible(3, 3));
            Assert.IsFalse(game.IsMovePossible(3, 4));
        }
        
        [Test]
        public void ZetMogelijk__BuitenBord_Exception()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.White;
            //var actual = spel.ZetMogelijk(8, 8);
            Exception ex = Assert.Throws<Exception>(delegate { game.IsMovePossible(8, 8); });
            Assert.That(ex.Message, Is.EqualTo("Zet (8,8) ligt buiten het bord!"));

            // Assert
        }

        [Test]
        public void ZetMogelijk_StartSituatieZet23Zwart_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(2, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_StartSituatieZet23Wit_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(2, 3);
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ZetMogelijk_ZetAanDeRandBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
        }






        [Test]
        public void ZetMogelijk_ZetAanDeRandRechts_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 2 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechts_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }


        //     0 1 2 3 4 5 6 7
        //                     
        // 0   0 0 0 0 0 0 0 0  
        // 1   0 0 0 0 0 0 0 0
        // 2   0 0 0 0 0 0 0 0
        // 3   0 0 0 1 2 0 0 0
        // 4   0 0 0 2 1 0 0 0
        // 5   0 0 0 0 0 0 0 0
        // 6   0 0 0 0 0 0 0 0
        // 7   0 0 0 0 0 0 0 0



        [Test]
        public void ZetMogelijk_StartSituatieZet22Wit_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void ZetMogelijk_StartSituatieZet22Zwart_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(0, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(0, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(7, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  <
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(7, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(0, 0);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0          
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(0, 0);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsMovePossible(7, 0);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  <
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            var actual = game.IsMovePossible(7, 0);
            // Assert
            Assert.IsFalse(actual);
        }

        //---------------------------------------------------------------------------
        [Test]
        //[ExpectedException(typeof(Exception))]
        public void DoeZet_BuitenBord_Exception()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.White;
            //spel.DoeZet(8, 8);
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(8, 8); });
            Assert.That(ex.Message, Is.EqualTo("Zet (8,8) ligt buiten het bord!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.CurrentPlayer);
        }

        [Test]
        public void DoeZet_StartSituatieZet23Zwart_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(2, 3);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.CurrentPlayer);
        }

        [Test]
        public void DoeZet_StartSituatieZet23Wit_Exception()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(2, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.None, game.Board[2, 3]);

            Assert.AreEqual(Color.White, game.CurrentPlayer);
        }


        [Test]
        public void DoeZet_ZetAanDeRandBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(0, 3);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[0, 3]);
            Assert.AreEqual(Color.Black, game.Board[1, 3]);
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.CurrentPlayer);
        }

        [Test]
        public void DoeZet_ZetAanDeRandBoven_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(0, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.Board[1, 3]);
            Assert.AreEqual(Color.White, game.Board[2, 3]);

            Assert.AreEqual(Color.None, game.Board[0, 3]);

        }

        [Test]
        public void DoeZet_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(0, 3);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[0, 3]);
            Assert.AreEqual(Color.Black, game.Board[1, 3]);
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[5, 3]);
            Assert.AreEqual(Color.Black, game.Board[6, 3]);
            Assert.AreEqual(Color.Black, game.Board[7, 3]);

        }

        [Test]
        public void DoeZet_ZetAanDeRandBovenEnTotBenedenReedsGevuld_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(0, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.Board[1, 3]);
            Assert.AreEqual(Color.White, game.Board[2, 3]);
            Assert.AreEqual(Color.None, game.Board[0, 3]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechts_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(4, 7);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 5]);
            Assert.AreEqual(Color.Black, game.Board[4, 6]);
            Assert.AreEqual(Color.Black, game.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechts_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            //spel.DoeZet(4, 7);
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(4, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (4,7) is niet mogelijk!"));


            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.Board[4, 5]);
            Assert.AreEqual(Color.White, game.Board[4, 6]);
            Assert.AreEqual(Color.None, game.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(4, 7);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[4, 0]);
            Assert.AreEqual(Color.Black, game.Board[4, 1]);
            Assert.AreEqual(Color.Black, game.Board[4, 2]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 5]);
            Assert.AreEqual(Color.Black, game.Board[4, 6]);
            Assert.AreEqual(Color.Black, game.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsEnTotLinksReedsGevuld_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;

            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(4, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (4,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);

            Assert.AreEqual(Color.Black, game.Board[4, 0]);
            Assert.AreEqual(Color.White, game.Board[4, 1]);
            Assert.AreEqual(Color.White, game.Board[4, 2]);

            Assert.AreEqual(Color.White, game.Board[4, 5]);
            Assert.AreEqual(Color.White, game.Board[4, 6]);
            Assert.AreEqual(Color.None, game.Board[4, 7]);
        }


        //     0 1 2 3 4 5 6 7
        //                     
        // 0   0 0 0 0 0 0 0 0  
        // 1   0 0 0 0 0 0 0 0
        // 2   0 0 0 0 0 0 0 0
        // 3   0 0 0 1 2 0 0 0
        // 4   0 0 0 2 1 0 0 0
        // 5   0 0 0 0 0 0 0 0
        // 6   0 0 0 0 0 0 0 0
        // 7   0 0 0 0 0 0 0 0



        [Test]
        public void DoeZet_StartSituatieZet22Wit_Exception()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.White;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(2, 2); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,2) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.None, game.Board[2, 2]);
        }

        [Test]
        public void DoeZet_StartSituatieZet22Zwart_Exception()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            game.CurrentPlayer = Color.Black;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(2, 2); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,2) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.None, game.Board[2, 2]);
        }


        [Test]
        public void DoeZet_ZetAanDeRandRechtsBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.White;
            game.DoMove(0, 7);
            // Assert
            Assert.AreEqual(Color.White, game.Board[5, 2]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[2, 5]);
            Assert.AreEqual(Color.White, game.Board[1, 6]);
            Assert.AreEqual(Color.White, game.Board[0, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsBoven_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            game.CurrentPlayer = Color.Black;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(0, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.Black, game.Board[1, 6]);
            Assert.AreEqual(Color.Black, game.Board[2, 5]);

            Assert.AreEqual(Color.White, game.Board[5, 2]);

            Assert.AreEqual(Color.None, game.Board[0, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsOnder_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <
            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(7, 7);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[5, 5]);
            Assert.AreEqual(Color.Black, game.Board[6, 6]);
            Assert.AreEqual(Color.Black, game.Board[7, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsOnder_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1 <
            // Act
            game.CurrentPlayer = Color.White;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(7, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (7,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.White, game.Board[5, 5]);
            Assert.AreEqual(Color.White, game.Board[6, 6]);

            Assert.AreEqual(Color.None, game.Board[7, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 
            // Act
            game.CurrentPlayer = Color.Black;
            game.DoMove(0, 0);
            // Assert
            Assert.AreEqual(Color.Black, game.Board[0, 0]);
            Assert.AreEqual(Color.Black, game.Board[1, 1]);
            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[5, 5]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksBoven_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0          
            // Act
            game.CurrentPlayer = Color.White;
            //spel.DoeZet(0, 0);
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(0, 0); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,0) is niet mogelijk!"));


            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.Board[1, 1]);
            Assert.AreEqual(Color.White, game.Board[2, 2]);

            Assert.AreEqual(Color.Black, game.Board[5, 5]);

            Assert.AreEqual(Color.None, game.Board[0, 0]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksOnder_ZetCorrectUitgevoerd()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <
            // Act
            game.CurrentPlayer = Color.White;
            game.DoMove(7, 0);
            // Assert
            Assert.AreEqual(Color.White, game.Board[7, 0]);
            Assert.AreEqual(Color.White, game.Board[6, 1]);
            Assert.AreEqual(Color.White, game.Board[5, 2]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[2, 5]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksOnder_Exception()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0 <
            // Act
            game.CurrentPlayer = Color.Black;
            Exception ex = Assert.Throws<Exception>(delegate { game.DoMove(7, 0); });
            Assert.That(ex.Message, Is.EqualTo("Zet (7,0) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);

            Assert.AreEqual(Color.White, game.Board[2, 5]);
            Assert.AreEqual(Color.Black, game.Board[5, 2]);
            Assert.AreEqual(Color.Black, game.Board[6, 1]);

            Assert.AreEqual(Color.None, game.Board[7, 7]);

            Assert.AreEqual(Color.None, game.Board[7, 0]);
        }

        [Test]
        public void Pas_ZwartAanZetGeenZetMogelijk_ReturnTrueEnWisselBeurt()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            game.CurrentPlayer = Color.Black;
            game.SkipTurn();
            // Assert
            Assert.AreEqual(Color.White, game.CurrentPlayer);
        }

        [Test]
        public void Pas_WitAanZetGeenZetMogelijk_ReturnTrueEnWisselBeurt()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            game.CurrentPlayer = Color.White;
            game.SkipTurn();
            // Assert
            Assert.AreEqual(Color.Black, game.CurrentPlayer);
        }
        
        [Test]
        public void Pas_WitAanZetEnZetMogelijk_ThrowsException()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.White;
            // Assert
            Exception ex = Assert.Throws<Exception>(delegate { game.SkipTurn(); });
            Assert.That(ex.Message, Is.EqualTo("Passen mag niet, er is nog een zet mogelijk"));
        }
        
        [Test]
        public void Pas_ZwartAanZetEnZetMogelijk_ThrowsException()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            game.CurrentPlayer = Color.Black;
            // Assert
            Exception ex = Assert.Throws<Exception>(delegate { game.SkipTurn(); });
            Assert.That(ex.Message, Is.EqualTo("Passen mag niet, er is nog een zet mogelijk"));
        }

        [Test]
        public void Afgelopen_GeenZetMogelijk_ReturnTrue()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsFinished();
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Afgelopen_GeenZetMogelijkAllesBezet_ReturnTrue()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.White;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.Black;
            game.Board[4, 7] = Color.Black;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.Black;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.Black;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 2
            // 4   1 1 1 1 1 1 2 2
            // 5   1 1 1 1 1 1 2 2
            // 6   1 1 1 1 1 1 1 2
            // 7   1 1 1 1 1 1 1 1
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsFinished();
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Afgelopen_WelZetMogelijk_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            game.CurrentPlayer = Color.White;
            var actual = game.IsFinished();
            // Assert
            Assert.IsFalse(actual);
        }



        [Test]
        public void OverwegendeKleur_Gelijk_ReturnKleurGeen()
        {
            // Arrange
            Game game = new Game();
            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = game.PredominantColor();
            // Assert
            Assert.AreEqual(Color.None, actual);
        }

        [Test]
        public void OverwegendeKleur_Zwart_ReturnKleurZwart()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 3] = Color.Black;
            game.Board[3, 3] = Color.Black;
            game.Board[4, 3] = Color.Black;
            game.Board[3, 4] = Color.Black;
            game.Board[4, 4] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0
            // 3   0 0 0 2 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = game.PredominantColor();
            // Assert
            Assert.AreEqual(Color.Black, actual);
        }

        [Test]
        public void OverwegendeKleur_Wit_ReturnKleurWit()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[4, 4] = Color.Black;


            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 1 0 0 0
            // 4   0 0 0 1 2 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = game.PredominantColor();
            // Assert
            Assert.AreEqual(Color.White, actual);
        }
    }
}