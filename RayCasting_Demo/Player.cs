using RayCasting_Demo;
using System;
using System.Reflection.Metadata;

public class Player
{
    private KeyboardInputs keyboard;
    //map switching key held
    private bool tKeyHeld = false;


    //player porperties
    // private set allows the property to be read but from prevents outsdie code
    //from changing
    public float X { get; private set; }
	public float Y { get; private set; }
	public float Angle { get; private set; }

	//movement speeds
	public float MoveSpeed { get; private set; } = 3f;
	public float TurnSpeed { get; private set; } = 5f;


	//Spawn Constructor
	public Player (KeyboardInputs keyboard, float spawnX, float SpawnY)
	{
        this.keyboard = keyboard;
		X = spawnX;
		Y = SpawnY;
		Angle = 180f;
	}

    //calcualte the X and Y vectors for movements
    public float GetXMove(float angleDegrees, float speed)
    {

        //convert angle to radians for math functions
        double radians = angleDegrees * Math.PI / 180.0;

        //calculate the x movement using sine of the angle
        float xMove = (float)(Math.Sin(radians) * speed);

        return xMove;
    }

    public float GetYMove(float angleDegrees, float speed)
    {
        //convert angle to radians for math functions
        double radians = angleDegrees * Math.PI / 180.0;

        //calculate the y movement using cosine of the angle
        float yMove = (float)(-Math.Cos(radians) * speed);

        return yMove;

    }


    //update player positon
    // Change to public so Form1 can run it on every Timer tick
    public void UpdatePlayer(GameMap map)
    {


        if (keyboard.IsDown(Keys.T))
        { 
            if(!tKeyHeld)
            {
                map.SwitchMap(X, Y);
                tKeyHeld = true;
            }  
        }
        else
        {
            tKeyHeld = false;
        }


        if (keyboard.IsDown(Keys.A))
        {
            Angle -= TurnSpeed;
        }

        if (keyboard.IsDown(Keys.D))
        {
            Angle += TurnSpeed;
        }

        if (Angle < 0)
        {
            Angle += 360f;
        }

        if (Angle >= 360f)
        {
            Angle -= 360f;
        }

        float nextX = X;
        float nextY = Y;

        if (keyboard.IsDown(Keys.W))
        {
            nextX += GetXMove(Angle, MoveSpeed);
            nextY += GetYMove(Angle, MoveSpeed);
        }

        if (keyboard.IsDown(Keys.S))
        {
            nextX -= GetXMove(Angle, MoveSpeed);
            nextY -= GetYMove(Angle, MoveSpeed);
        }

        if (keyboard.IsDown(Keys.Q))
        {
            float leftAngle = Angle - 90f;

            nextX += GetXMove(leftAngle, MoveSpeed);
            nextY += GetYMove(leftAngle, MoveSpeed);
        }

        if (keyboard.IsDown(Keys.E))
        {
            float rightAngle = Angle + 90f;

            nextX += GetXMove(rightAngle, MoveSpeed);
            nextY += GetYMove(rightAngle, MoveSpeed);
        }

        if (!map.Collision(nextX, nextY, map.map))
        {
            X = nextX;
            Y = nextY;
        }
    }






}
