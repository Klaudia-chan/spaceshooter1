using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace spaceshooter1
{
    class GameElements
    {
        static Texture2D menySprite;
        static Vector2 menyPos;
        static Player player;
        static List<Enemy> enemies;
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;
        static PrintText printText;
        public enum State { Meny, Run, Highscore, Quit};
        public static State currentState;

        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();

        }
        public static void Loadcontent(ContentManager content, GameWindow window)
        {
            //meny
            menySprite = content.Load<Texture2D>("Sprites/menu");
            menyPos.X = window.ClientBounds.Width / 2 - menySprite.Width / 2;
            menyPos.Y = window.ClientBounds.Height / 2 - menySprite.Height / 2;
            //player
            player = new Player(content.Load<Texture2D>("Sprites/ship"), 380, 400, 4.5f, 4.5f, content.Load<Texture2D>("Sprites/bullet"));
            goldCoinSprite = content.Load<Texture2D>("Sprites/coin");


            //att skapa fiender
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("Sprites/mine");
            for (int i = 0; i < 15; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);

                Enemy temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("Sprites/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);//lägg till i listan

                Enemy temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
            //För utskrift
            printText = new PrintText(content.Load<SpriteFont>("MinFont"));

        }
        public static State MenyUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S)) //Starta Spelet
                return State.Run;
            if (keyboardState.IsKeyDown(Keys.H)) //Visa highscore
                return State.Highscore;
            if (keyboardState.IsKeyDown(Keys.A))//Avsluta spelet
                return State.Quit;
            return State.Meny;
        }
        public static void MenyDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menySprite, menyPos, Color.White);
        }
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {

            player.Update(window, gameTime);

            foreach (Enemy e in enemies)
            {
                foreach (Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b))
                    {
                        e.IsAlive = false;
                        player.Points++;
                    }
                }

                if (e.IsAlive)
                {
                    if (e.CheckCollision(player))
                        player.IsAlive = false;

                    e.Update(window);
                }
                else
                    enemies.Remove(e);

            }
            Random random = new Random();
            int newCoin = random.Next(1, 80);
            if (newCoin == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }



            foreach (GoldCoin gc in goldCoins)
            {
                if (gc.IsAlive)
                {
                    gc.Update(gameTime);

                    if (gc.CheckCollision(player))
                    {

                        goldCoins.Remove(gc);
                        player.Points++;

                    }
                }
                else
                {
                    goldCoins.Remove(gc);
                }

            }
            if (!player.IsAlive)
                return State.Meny;
            return State.Run;
        }
        public static void RunDraw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            printText.Print("Points: " + player.Points, spriteBatch, 0, 0);
        }
        public static State HighScoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //Återgå till menyn om man trycker ESC:
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Meny;
            return State.Highscore;
        }
        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {

        }
    }
}
