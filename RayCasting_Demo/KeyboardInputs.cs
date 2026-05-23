using System;

public class KeyboardInputs
{
    private HashSet<Keys> keysPressed = new HashSet<Keys>();

    public void SetKey(Keys key, bool pressed)
    {
        if (pressed)
        {
            keysPressed.Add(key);
        }
        else
        {
            keysPressed.Remove(key);
        }
    }

    public bool IsDown(Keys key)
    {
        return keysPressed.Contains(key);
    }

}
 