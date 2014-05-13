using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace myEp3
{
    class GameSprite
    {
        public Texture2D texture;
        public Rectangle gameRect;
        public Vector2 gamePosition;
        public float gameSpeed;
        KeyboardState ks;
        public float scale;

        public GameSprite(Texture2D texture, Rectangle rectangle, Vector2 position, float speed, float scale)
        {
            this.texture = texture;
            this.gameRect = rectangle;
            this.gamePosition = position;
            this.gameSpeed = speed;
            this.scale = scale;
        }


        public void DrawSelected(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, gamePosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.9f);

        }


        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, gamePosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.5f);

        }

        public void DrawBack(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, gamePosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.1f);

        }

        public void Draw2Position(SpriteBatch theSpriteBatch, Vector2 position)
        {
            theSpriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.9f);

        }

        public void DrawMouse(SpriteBatch theSpriteBatch, Vector2 position)
        {
            theSpriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);

        }

        public void Update2Position()
        {

            gameRect = new Rectangle((int)gamePosition.X*2, (int)gamePosition.Y*2, (int)texture.Width*2, (int)texture.Height*2);

        }


        public void Update()
        {

            gameRect = new Rectangle((int)gamePosition.X, (int)gamePosition.Y, (int)texture.Width, (int)texture.Height);

        

            //ks = Keyboard.GetState();

            //if (ks.IsKeyDown(Keys.Right))
            //    gamePosition.X += gameSpeed;

            //if (ks.IsKeyDown(Keys.Left))
            //    gamePosition.X -= gameSpeed;

            //if (ks.IsKeyDown(Keys.Down))
            //    gamePosition.Y += gameSpeed;

            //if (ks.IsKeyDown(Keys.Up))
            //    gamePosition.Y -= gameSpeed;




        }

        public Rectangle getRectangle()
        {
            return gameRect;
        }

    }
}
