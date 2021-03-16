using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter1
{
     class Player : PhysicalObjects
    {
        int points;
        List<Bullet> bullets;
        Texture2D bulletGfx;
        double timeSinceLastBullet = 0;
        public int Points { get { return points; } set { points = value; } }
        public List<Bullet> Bullets { get { return bullets; } }

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletGfx = bulletTexture;
        }

        public void Update(GameWindow window, GameTime gameTime) 
        {

            KeyboardState keyboardState = Keyboard.GetState();


            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }


            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.Up))
                    vector.Y -= speed.Y;


                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
                    {
                        Bullet temp = new Bullet(bulletGfx, vector.X + texture.Width/2, vector.Y);
                        bullets.Add(temp);
                        timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }


            }
                if (vector.X < 0)
                    vector.X = 0;
                if (vector.X > window.ClientBounds.Width - texture.Width)
                    vector.X = window.ClientBounds.Width - texture.Width;
                if (vector.Y < 0)
                    vector.Y = 0;
                if (vector.Y > window.ClientBounds.Height - texture.Height)
                    vector.Y = window.ClientBounds.Height - texture.Height;


                foreach(Bullet b in bullets.ToList())
            {
                b.Update();
                if (!b.IsAlive)
                    bullets.Remove(b);
            }





        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }
        
        
        }
    }

    



    class Bullet : PhysicalObjects
    {
        
        public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 3f)
        {
        }

        public void Update()
        {
            vector.Y -= speed.Y;
           if (vector.Y < 0)
            {
                IsAlive = false;
            }
        }

    }






}
