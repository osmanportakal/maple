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
        Vector2 playerPos = new Vector2(200, 400);
        Texture2D spriteImage;
        public Level1 level1;
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
            level1 = new Level1();            
            level1.createTiles();
            camera = new Camera(GraphicsDevice.Viewport, level1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteImage = Content.Load<Texture2D>("spriteRight");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerOne = new Player(spriteImage, 0, 0, 140, 100, level1.levelWidth);
            playerOne.Position = playerPos;

        }

        protected override void UnloadContent()
        {

        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.transform);
            createWorld(spriteBatch);
            spriteBatch.Draw(playerOne.Texture, playerOne.Position, playerOne.SourceRect, Color.White, 0f, playerOne.Origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        protected override void Update(GameTime gameTime)
        {
            playerOne.collide(level1);
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

        public void createWorld(SpriteBatch spriteBatch)
        {
          
            foreach(Grass grass in level1.grassList)
            {
              spriteBatch.Draw(Content.Load<Texture2D>("grass"), new Rectangle((int)(grass.Position.X),(int)(grass.Position.Y), grass.width, grass.height), Color.White);
            }

            foreach (Dirt dirt in level1.dirtList)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("dirt"), new Rectangle((int)(dirt.Position.X), (int)(dirt.Position.Y), dirt.width, dirt.height), Color.White);
            }

            foreach (Corner corner in level1.cornerList)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("corner"), new Rectangle((int)(corner.Position.X), (int)(corner.Position.Y), corner.width, corner.height), Color.White);
            }
        }

    }
}
