using System;
using System.Drawing;
namespace RayCasting_Demo
{
    public class GameMap
    {

        public GameMap()
        {
            //set default map to alpha
            map = mapAlpha;
        }

        //configuration variables
        public int tileSize { get; } = 32;
        public bool ShiftError { get; private set; }
        //new learning lambda operator (=>) shorthand for creating a read only property 
        //without using a longer get method and i statement using get
        private int MapRows => map.GetLength(0);
        private int MapCollumns => map.GetLength(1);



        //Map Arrays
        public int[,] map;


        public int[,] mapAlpha =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,1,0,0,0,1,0,0,0,0,1},
            {1,0,1,0,1,0,1,0,1,1,0,1},
            {1,0,0,0,1,0,0,0,0,1,0,1},
            {1,0,1,0,1,1,1,1,0,1,0,1},
            {1,0,0,0,0,0,0,1,0,0,0,1},
            {1,0,1,1,1,1,0,1,1,1,0,1},
            {1,0,0,0,0,1,0,0,0,1,0,1},
            {1,1,1,1,0,1,1,1,0,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public int[,] mapBeta =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,1,0,0,0,1,0,0,1,0,1},
            {1,0,1,0,1,0,1,0,1,1,0,1},
            {1,0,0,0,1,0,0,0,1,0,0,1},
            {1,0,1,0,1,1,1,1,1,1,0,1},
            {1,0,1,0,0,0,1,1,0,0,0,1},
            {1,0,1,1,1,0,1,1,1,1,0,1},
            {1,0,0,0,1,0,0,0,0,1,0,1},
            {1,1,1,0,1,1,1,1,0,1,0,1},
            {1,0,0,0,0,0,1,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1},
        };

        //map switching

        public void SwitchMap(float playerX, float playerY)
        {
            int[,] nextMap;

            if (map == mapAlpha)
            {
                nextMap = mapBeta;
            }
            else
            {
                nextMap = mapAlpha;
            }

            if (Collision (playerX, playerY, nextMap) == true)
            {
                ShiftError = true;
                return;

            }

            map = nextMap;

            ShiftError = false;
            

        }

        



        //player collision
        public bool Collision(float x, float y, int[,] MapToCheck )
        {

            //convert player coordinates to map coordinates
            int mapX = (int)(x / tileSize);
            int mapY = (int)(y / tileSize);

            //treat out of boudns cooridnates as walls
            if (mapX < 0 || mapY < 0 || mapY >= MapToCheck.GetLength(0) || mapX >= MapToCheck.GetLength(1))
            {
                return true;
            }


            //check if player is colliding with a wall tile
            if (MapToCheck[mapY, mapX] == 1)
            {
                return true;
            }

            return false;
        }

        //Map Drawing For debugging
        public void DrawMap(Graphics g)
        {
            //loop through all the column elements
            //collumns come first in a 2d array
            for (int y = 0; y < map.GetLength(0); y++)
            {

                //loop through all the row elements
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        //fill the tile with red if its a wall
                        g.FillRectangle(Brushes.Red, (x * tileSize), (y * tileSize), tileSize, tileSize);
                    }
                    else
                    {
                        //fill the tile with black if its empty
                        g.FillRectangle(Brushes.Black, (x * tileSize), (y * tileSize), tileSize, tileSize);
                    }
                    //create a border around the tile for easier debugging
                    g.DrawRectangle(Pens.Gray, (x * tileSize), (y * tileSize), tileSize, tileSize);
                }
            }

        }
    }
}