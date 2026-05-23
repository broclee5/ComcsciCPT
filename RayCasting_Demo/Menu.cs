using System;
using System.Drawing;

namespace RayCasting_Demo
{
    /// <summary>
    /// Provides the Menu display when the game starts,
    /// pauses or the game is over.  Players will be able to 
    /// view instructions, controls and quit the game.
    /// </summary>
    internal class Menu
    {
        //fields/data members


        //Properties
        /// <summary>
        /// Allows the menu to toggle on/off the display of the game 
        /// instructions
        /// </summary>
        public bool ShowInstructions { get; set; }


        //Methods
        /// <summary>
        /// Draws the Menu on the game screen
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            //set up Fonts for the Menu text
            Font titleFont = new Font("Segoe UI", 48,
                            FontStyle.Bold, GraphicsUnit.Pixel);
            Font promptFont = new Font("Segoe UI", 24,
                FontStyle.Regular, GraphicsUnit.Pixel);
            Font instrFont = new Font("Segoe UI", 14,
                    FontStyle.Regular, GraphicsUnit.Pixel);

            //set up Brush to draw with
            Brush textBrush = new SolidBrush(Color.White);


            //draw the titles
            g.DrawString("Parallax", titleFont,
                                        textBrush, 350, 200);
            g.DrawString("Press ENTER to Begin", promptFont,
                            textBrush, 450, 280);
            g.DrawString("Press A to Show Instructions", instrFont,
                            textBrush, 480, 330);

            //show instructions if selected
            if (ShowInstructions)
            {
                //show the instructions below the main menu
                string[] lines = {"Instructions:",
                            "- Use WASD to move",
                            "- Press LeftClick",
                            "- Destroy enemies and escapea",
                            "- Collect power-up to upgrade weapons"};

                //draw the instructions line by line on the surface
                int XPos = 480;
                int YPos = 330;
                for (int i = 0; i < lines.Length; i++)
                {
                    //change the YPos - add 20 pixels
                    YPos += 20;
                    g.DrawString(lines[i], instrFont, textBrush, XPos, YPos);
                }

            }


            //dispose of drawing objects
            titleFont.Dispose();
            promptFont.Dispose();
            instrFont.Dispose();
            textBrush.Dispose();
        }



    }
}
