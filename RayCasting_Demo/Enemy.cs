using RayCasting_Demo;
using System;
using System.Drawing;

public class Enemy
{
	public float X;
	public float Y;
	public Bitmap sprite;
	public float DistanceToPlayer;


	public Enemy(float x, float y)
	{
		X = x;
		Y = y;

		sprite = Assets.Enemy;

	}



}
