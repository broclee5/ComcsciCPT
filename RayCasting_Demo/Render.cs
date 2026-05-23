using RayCasting_Demo;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

public class Render
{
    private Player player;
    private GameMap map;



    public Render(Player player, GameMap map)
    {
        this.player = player;
        this.map = map;
    }


    public void Draw3DView(Graphics g)
    {
        int screenWidth = 1000;
        int screenHeight = 600;

        // Draw ceiling for half the screen
        g.FillRectangle(Brushes.DarkGray, 0, 0, screenWidth, screenHeight / 2);

        //Draw floor for the other half of the screen
        g.FillRectangle(Brushes.Gray, 0, screenHeight / 2, screenWidth, screenHeight / 2);

        float fieldOfView = 60f;

        //draw a vertical line for each column of the screen based on the distance returned by the ray casting method
        for (int screenX = 0; screenX < screenWidth; screenX++)
        {

            float percentAcrossScreen = (float)screenX / screenWidth;
            
            float rayAngle = player.Angle - fieldOfView / 2 + percentAcrossScreen * fieldOfView;

            float distance = CastRay(rayAngle);

            if (distance < 1)
            {
                distance = 1;
            }

            //24000 is a wall size scaling factor
            float wallHeight = 20000 / distance;

            //vertically center the wall slice on the screen
            float wallTop = (screenHeight / 2 - wallHeight / 2);
            float wallBottom = (screenHeight / 2 + wallHeight / 2   );

            //distance color shading
            //and map color

            Pen mapPen;
            Pen AlphaPen;
            Pen BetaPen;

            if (distance < 80)
            {
                AlphaPen = Pens.LimeGreen;
            }
            else if (distance < 160)
            {
                AlphaPen = Pens.Green;
            }
            else
            {
                AlphaPen = Pens.DarkGreen;
            }


            if (distance < 80)
            {
                BetaPen = Pens.LightBlue;
            }
            else if (distance < 160)
            {
                BetaPen = Pens.MediumBlue;
            }
            else
            {
                BetaPen = Pens.DarkBlue;
            }

            if (map.map == map.mapAlpha)
            {
                mapPen = AlphaPen;
            }
            else
            {
                mapPen = BetaPen;
            }


            g.DrawLine(
                mapPen,
                screenX,
                wallTop,
                screenX,
                wallBottom
            );
        }
    }

    //shoots an array and returns distance to the first wall it hits,
    //or a max distance if it doesn't hit anything
    public float CastRay(float rayAngle)
    {
        float rayX = player.X;
        float rayY = player.Y;

        float stepSize = 2;
        float distance = 0;

        for (int i = 0; i < 1000; i++)
        {
            rayX += player.GetXMove(rayAngle, stepSize);
            rayY += player.GetYMove(rayAngle, stepSize);

            distance += stepSize;

            if (map.Collision(rayX, rayY, map.map))
            {
                break;
            }
        }

        return distance;
    }

    //raycasting method to draw a line with collision detection
    //precursor to full ray casting method
    public void DrawRay(Graphics g, float rayAngle)
    {
        float distance = CastRay(rayAngle);

        float endX = player.X + player.GetXMove(rayAngle, distance);
        float endY = player.Y + player.GetYMove(rayAngle, distance);


        //draw a ray as a line from the player position to the ray end position
        g.DrawLine(Pens.Lime, player.X, player.Y, endX, endY);

    }

    //use the ray casting method to draw a series of offset rays to form
    //a player field of view
    public void DrawRays(Graphics g)
    {
        float FOV = 60;
        float spacing = 0.1f;

        float startAngle = player.Angle - (FOV / 2);
        float endAngle = player.Angle + (FOV / 2);

        for (float rayAngle = startAngle; rayAngle <= endAngle; rayAngle += spacing)
        {
            DrawRay(g, rayAngle);
        }
    }


    public void DrawPlayer(Graphics g)
    {
        //draw player
        float size = 8;

        //create a circle centered on the player position
        g.FillEllipse(Brushes.Blue, player.X - size / 2, player.Y - size / 2, size, size);



        //draw player direction line
        float lineLength = 25;

        float lineEndX = player.X + player.GetXMove(player.Angle, lineLength);
        float lineEndY = player.Y + player.GetYMove(player.Angle, lineLength);

        g.DrawLine(
            Pens.Yellow,
            player.X,
            player.Y,
            lineEndX,
            lineEndY
            );

    }

    public void DrawEnemies(Graphics g, List<Enemy> enemies)
    {
        //define screen size

    }



}
