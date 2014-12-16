using MapleStory.Levels.Level1;
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

namespace MapleStory
{
    public class Level1 : Level
    {
        public int levelWidth = 1440;

        public Dirt dirt;
        public Grass grass;
        public Corner corner;
        public List<Grass> grassList;
        public List<Corner> cornerList;
        public List<Dirt> dirtList;


        public void createTiles()
        {
            // a 2d array wich contains the places for the tiles
            array = new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},

            };

            grassList = new List<Grass>();
            dirtList = new List<Dirt>();
            cornerList = new List<Corner>();

            //fill the lists with objects
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 0) continue;

                    switch (array[i, j])
                    {
                        case 1:
                            dirt = new Dirt();
                            dirt.position = new Vector2(j * 90, i * 90);
                            dirtList.Add(dirt);                            
                            break;
                        case 2:
                            grass = new Grass();
                            grass.position = new Vector2(j * 90, i * 90);
                            grassList.Add(grass);
                            break;
                        case 3:
                            corner = new Corner();
                            corner.position = new Vector2(j * 90, i * 90);                            
                            cornerList.Add(corner);
                            break;
                    }
               }
            }
        }

    }
}
