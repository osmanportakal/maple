using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapleStory.Levels.Level1
{
    public class Grass : Tiles
    {

        public Grass() 
        {
            imgSrc = "grass";
            id = 2;
            collision = true;
               
        }

    }
}
