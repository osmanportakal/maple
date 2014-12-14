using System;
using System.Collections.Generic;
using System.Linq;
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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player playerOne;
        Vector2 playerPos = new Vector2(200, 350);
        Texture2D spriteImage;

        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
           
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteImage = Content.Load<Texture2D>("spriteRight");
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            playerOne = new Player(spriteImage, 0, 0, 140, 100);
            playerOne.Position = playerPos;

        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            playerOne.HandleSpriteMovement(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Left) == true)
            {
                playerOne.spriteTexture = Content.Load<Texture2D>("spriteLeft");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
            {
                playerOne.spriteTexture = Content.Load<Texture2D>("spriteRight");
            }

            camera.Update(gameTime, playerOne);
      
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            createTiles(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(playerOne.Texture, playerOne.Position, playerOne.SourceRect, Color.White, 0f, playerOne.Origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void createTiles(SpriteBatch spriteBatch)
        {
            Level1 level1 = new Level1();
            level1.createTiles();

            Grass grass = new Grass();
            Dirt dirt = new Dirt();
            Corner corner = new Corner();
            Texture2D currentImg = Content.Load<Texture2D>(dirt.imgSrc);  

            for (int i = 0; i < level1.array.GetLength(0); i++)
            {
                for (int j = 0; j < level1.array.GetLength(1); j++)
                {
                    if (level1.array[i, j] == 0) continue;

                    switch (level1.array[i, j])
                    {
                        case 1:
                            currentImg = Content.Load<Texture2D>(dirt.imgSrc);
                            break;
                        case 2:
                            currentImg = Content.Load<Texture2D>(grass.imgSrc);
                            break;
                        case 3:
                            currentImg = Content.Load<Texture2D>(corner.imgSrc);
                            break;
                    }

                    spriteBatch.Begin(
                        SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.transform);                   
                    spriteBatch.Draw(currentImg, new Rectangle(j * grass.width, i * grass.height, grass.width, grass.height), Color.White);
                    spriteBatch.End();
                }
            }
        }
    }
}
