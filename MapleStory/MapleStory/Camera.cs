using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapleStory
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;
        Level1 level;

        
        
        public Camera (Viewport newView, Level1 level1)
        {
            view = newView;
            level = level1;
        }

        public void Update(GameTime gameTime, Player playerOne)
        {
            int cameraCorrection = level.levelWidth - 880;

            centre = new Vector2(playerOne.position.X-200, 0);

            if(playerOne.position.X >= 200 && playerOne.position.X < cameraCorrection)
            {
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
               Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
           
        }
    }
}
