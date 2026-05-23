using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RayCasting_Demo
{

    public partial class Form1 : Form
    {
        private KeyboardInputs keyboard;
        private GameMap map;
        private Player player;
        private Render render;
        private StateMachine state;
        private Menu menu;
        private Enemy enemy;

        private List<Enemy> enemies = new List<Enemy>();
        public Form1()
        {
            InitializeComponent();

            menu = new Menu();
            keyboard = new KeyboardInputs();
            state = new StateMachine();
            map = new GameMap();
            player = new Player(keyboard, 48, 48);
            render = new Render(player, map);

            enemies.Add(new Enemy(160, 160));

            this.ClientSize = new Size(1000, 600);
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            timer1.Interval = 16;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (state.CurrentState == StateMachine.GameState.Menu)
            {
                menu.Draw(e.Graphics);

                if (keyboard.IsDown(Keys.A))
                {
                    menu.ShowInstructions = true;
                }

                if (keyboard.IsDown(Keys.Enter))
                {
                    state.CurrentState = StateMachine.GameState.Playing;
                }
            }

            else if (state.CurrentState == StateMachine.GameState.Playing)
            {
                map.DrawMap(e.Graphics);
                render.DrawPlayer(e.Graphics);
                render.DrawRays(e.Graphics);
                render.Draw3DView(e.Graphics);
                render.DrawEnemies(e.Graphics, enemies);
            }
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            if (state.CurrentState == StateMachine.GameState.Playing)
            {
                player.UpdatePlayer(map);
                lblShift.Visible = map.ShiftError;
            }
                this.Invalidate();          
        }


        //handle key presses to update player movement
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyboard.SetKey(e.KeyCode, true);
        }

        //handle key releases to update player movement
        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyboard.SetKey(e.KeyCode, false);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

