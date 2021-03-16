using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter1
{
    abstract class Enemy : PhysicalObjects
    {


        public Enemy(Texture2D texture, float X, float Y, float speedX,float speedY) : base(texture, X, Y, speedX, speedY)
        {

        }
        public abstract void Update(GameWindow window);


    }
    class Mine : Enemy
    {
        public Mine(Texture2D image, float X, float Y) : base(image, X, Y, 6f, 1f)
        {
            
        }
        public override void Update(GameWindow window)
        {
            vector.X += speed.X;


            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
            {
                speed.X *= -1;

            }
            vector.Y += speed.Y;
            if (vector.Y > window.ClientBounds.Height)
            {
                IsAlive = false;
            }

        }
    }
    class Tripod : Enemy
    {
        public Tripod(Texture2D image, float X, float Y) : base(image, X, Y, 0f, 3f)
        {

        }
        public override void Update(GameWindow window)
        {
            vector += speed;

            
            if (vector.Y > window.ClientBounds.Height)
            {
                IsAlive = false;
            }

        }
    }

}
