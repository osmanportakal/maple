using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapleStory.Levels.Level1
{
    class Dirt :Tiles
    {
         public Dirt() 
        {
            imgSrc = "dirt";
            id = 1;
            collision = true;
        }

    }
}
