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
using MapleStory.Levels.Level1;

namespace MapleStory
{
    public class Player
    {

        KeyboardState currentKBState;
        KeyboardState previousKBState;
        public Texture2D spriteTexture;
        float timer = 0f;
        float interval = 200f;
        int currentFrame = 0;
        int frameHeight = 0;
        int spriteWidth = 140;
        int spriteHeight = 100;
        public int spriteSpeed = 4;
        Rectangle sourceRect;
        public Vector2 position;
        Vector2 origin;
        public Rectangle playerBounds;

        public Boolean onGround = false;

 
        public int levelWidth;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Texture2D Texture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Player(Texture2D texture, int currentFrame, int frameHeight, int spriteWidth, int spriteHeight, int level)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.levelWidth = level;
        }
 

        public void HandleSpriteMovement(GameTime gameTime)
        {
	        previousKBState = currentKBState;
	        currentKBState = Keyboard.GetState();
	        sourceRect = new Rectangle(currentFrame * spriteWidth, frameHeight * spriteHeight, spriteWidth, spriteHeight);

            playerBounds = new Rectangle((int)(position.X - spriteWidth / 2), (int) (position.Y - spriteHeight / 2), spriteWidth, spriteHeight);

	        if (currentKBState.GetPressedKeys().Length == 0)
	        {
                frameHeight = 0;                
                AnimateAlert(gameTime);

                if (onGround == false)
                {
                    isFalling(gameTime);
                }
		    }
	      
	        if (currentKBState.IsKeyDown(Keys.Right) == true)
	        {
                frameHeight = 2;
		        Walk(gameTime);
		        if (position.X < levelWidth)
		        {
			        position.X += spriteSpeed;
		        }
	        }

	        if (currentKBState.IsKeyDown(Keys.Left) == true && currentKBState.IsKeyDown(Keys.Up) == false)
	        {
                frameHeight = 2;
		        Walk(gameTime);
		        if (position.X > 20)
		        {
			        position.X -= spriteSpeed;
		        }
	        }

            if (currentKBState.IsKeyDown(Keys.LeftShift) == true)
            {
                frameHeight = 1;
                Attack(gameTime);  
            }

            if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                frameHeight = 5;
                currentFrame = 0;
            }

            if (currentKBState.IsKeyDown(Keys.Up) == true && currentKBState.IsKeyDown(Keys.Left) == true)
            {
                frameHeight = 4;
                currentFrame = 0;
                if (position.X > 10)
                {
                    position.X -= spriteSpeed;
                    jump(gameTime);
                }
            }

	        origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

        }

        public void collide(List<Grass> grassList, List<Dirt> dirtList, List <Corner>cornerList)
        {
            for (int i = 0; i < grassList.Count(); i++)
            {
                if(this.playerBounds.Intersects(grassList[i].tileBounds))
                {
                    onGround = true;
                }
                else
                {
                    onGround = false;
                }
            }
        }

        
        //ANIMATE
        public void isFalling(GameTime gameTime)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 50f)
            {
                position.Y += 3;
                position.X += 1;

            
                timer = 0f;
            }
        }

        public void AnimateAlert(GameTime gameTime)
        {
          
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {

                currentFrame++;
                if (currentFrame > 2) {  currentFrame = 0; }
                timer = 0f;
            }
        }

        public void Walk(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > 2) { currentFrame = 0; }
                timer = 0f;
            }
        }

        public void jump(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                    position.Y -= 80; 
                    position.X -=20;
                    position.Y += 80;
                    
                   
                    timer = 0f;
                }
        }

        public void Attack(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > 9) { currentFrame = 0; }
                timer = 0f;
            }
        }



    }

        
}
