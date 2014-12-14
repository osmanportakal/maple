﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace MapleStory
{
    public abstract class Tiles 
    {
        public int width = 90;
        public int height = 90;
        public Boolean collision = false;
        public int id;
        public String imgSrc;

        public void collide() 
        {

        }


    }
}
