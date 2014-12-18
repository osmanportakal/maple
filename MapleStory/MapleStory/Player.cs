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
using System.Diagnostics;

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

        Rectangle sourceRect;
        public Vector2 position;
        Vector2 origin;
        public Rectangle playerBounds;

        public Boolean onGround = false;
        Boolean hasJumped;
        Vector2 velocity;

        public int intersection;

 
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
            
            position += velocity;
            
           
	        previousKBState = currentKBState;
	        currentKBState = Keyboard.GetState();
	        sourceRect = new Rectangle(currentFrame * spriteWidth, frameHeight * spriteHeight, spriteWidth, spriteHeight);

            playerBounds = new Rectangle((int)(position.X - spriteWidth / 2), (int) (position.Y - spriteHeight / 2), 80, spriteHeight-10);

	        if (currentKBState.GetPressedKeys().Length == 0 || (onGround ==false && hasJumped ==false) )
	        {
                frameHeight = 0;                
                AnimateAlert(gameTime);

                if (onGround == false)
                {
                    isFalling(gameTime);
                }
		    }
	      
	        if (currentKBState.IsKeyDown(Keys.Right) == true && (onGround == true || hasJumped == true))
	        {
                frameHeight = 2;
		        Walk(gameTime);
		        if (position.X < levelWidth)
		        {
                    velocity.X = 5f;
                } else { velocity.X = 0; }
                
	        }
            else if (currentKBState.IsKeyDown(Keys.Left) == true && (onGround == true || hasJumped == true))
	        {
                frameHeight = 2;
		        Walk(gameTime);
		        if (position.X > 20)
		        {
                    velocity.X = -5f;
		        } else {velocity.X = 0;}
	        }

            else { velocity.X = 0f; }
           
            if ((currentKBState.IsKeyDown(Keys.Up) == true) && hasJumped ==false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                currentFrame = 0;
                frameHeight = 4;
                hasJumped = true;
            }

            if(hasJumped==true)
            {
                currentFrame = 0;
                frameHeight = 4;
               velocity.Y += 0.15f;
               position.Y -= 2f;
               
            }

            if (this.position.Y+30 >= intersection && onGround == true)
            {
                
                hasJumped = false;
                velocity.Y += 2f;
            }

            if(hasJumped == false)
            {
                velocity.Y = 0f;
            }

            if (currentKBState.IsKeyDown(Keys.LeftShift) == true)
            {
                frameHeight = 1;
                Attack(gameTime);  
            }

            if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                frameHeight = 5;
                velocity.X = 0;
                
                currentFrame = 0;
            }
	        origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
       
        public void collide(GameTime gametime, Level1 level1)
        {
            foreach (Grass grass in level1.grassList)
            {
                Rectangle tileBound = new Rectangle((int)grass.position.X, (int)grass.position.Y, 90, 90);
                if (this.playerBounds.Intersects(tileBound))
                {
                    intersection = (int)grass.Position.Y;
                    onGround = true;
                    break;
                }
                onGround = false;
            }

            foreach (Corner corner in level1.cornerList)
            {
                Rectangle tileBound = new Rectangle((int)corner.position.X-45, (int)corner.position.Y, 90, 90);
                if (this.playerBounds.Intersects(tileBound))
                {
                    intersection = (int)corner.Position.Y;
                    onGround = true;
                    break;
                }

                Rectangle cornerSide = new Rectangle((int)corner.position.X + 45, (int)corner.position.Y, 1, 90);
                if (this.playerBounds.Intersects(cornerSide) && velocity.X <= 0)
                {
                    velocity.X = 0;
                }
            }
        }

        
        //ANIMATE
        public void isFalling(GameTime gameTime)
        {
            frameHeight = 4;
            currentFrame = 0;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 2f)
            {
                position.Y += 5;
                position.X += 0;
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
