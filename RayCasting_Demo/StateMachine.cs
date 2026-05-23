using System;

public class StateMachine
{

    //possible game states
    public enum GameState
    {
        Menu,
        Playing,    
        GameOver,
    }

    public GameState CurrentState { get; set; }

    public StateMachine()
    {
        CurrentState = GameState.Menu;
    }


}
