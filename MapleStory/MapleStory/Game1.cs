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
        Level1 level1;
        Camera camera;
        public Dirt dirt;
        public Grass grass;
        SpriteFont font;

        public Corner corner;
        public List<Grass> grassList;
        public List<Corner> cornerList;
        public List<Dirt> dirtList;

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
            camera = new Camera(GraphicsDevice.Viewport, level1);
            corner = new Corner();
            dirt = new Dirt();
            grass = new Grass();
            grassList = new List<Grass>();
            dirtList = new List<Dirt>();
            cornerList = new List<Corner>();

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
            createLevelOne(spriteBatch);
            spriteBatch.Draw(playerOne.Texture, playerOne.Position, playerOne.SourceRect, Color.White, 0f, playerOne.Origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void createLevelOne(SpriteBatch spriteBatch)
        {
            
            level1.createTiles();

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
                            dirt.position = new Vector2(i*90, j*90);
                            dirtList.Add(dirt);
                            break;
                        case 2:
                            currentImg = Content.Load<Texture2D>(grass.imgSrc);
                            grassList.Add(grass);
                            break;
                        case 3:
                            currentImg = Content.Load<Texture2D>(corner.imgSrc);
                            corner.position = new Vector2(i*90, j*90);
                            corner = new Corner();
                            cornerList.Add(corner);
                            break;
                    }
                    

                    spriteBatch.Draw(currentImg, new Rectangle(j * grass.width, i * grass.height, grass.width, grass.height), Color.White);
                    font = Content.Load<SpriteFont>("Courier New");
                    for (int q = 0; q < grassList.Count(); q++)
                    {
                        spriteBatch.DrawString(font, grassList.Count.ToString(), new Vector2(100, 100), Color.Black);
                    }
                   
                    
                }
            }
        }



        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            playerOne.HandleSpriteMovement(gameTime);

            playerOne.collide(grassList, dirtList, cornerList);


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

    }
}
