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
        
        public Camera (Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Player playerOne)
        {
            centre = new Vector2(playerOne.position.X - 80 ,  1);
            transform = Matrix.CreateScale(new Vector3(1 ,1 ,0)) *
                Matrix.CreateTranslation(new Vector3(centre.X /-2, -centre.Y,0));
        }

      
    }
}
