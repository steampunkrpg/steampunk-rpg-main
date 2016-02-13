using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardController : IController {

    Dictionary<KeyCode, ICommand> commands;

    public KeyboardController()
    {
        commands = new Dictionary<KeyCode, ICommand>();
        ResetController();
    }

    public void AddCommand(KeyCode keyCode, ICommand command)
    {
        commands[keyCode] = command;
    }

    // execute the keys pressed.
    public void Update(GameObject activePlayer)
    {
        KeyCode keyCode = FetchKey();

        if (commands.ContainsKey(keyCode))
        {
            commands[keyCode].Execute(activePlayer.GetComponent<Unit>() as Unit);
        }
    }

    // add input controls here
    private void ResetController()
    {
        AddCommand(KeyCode.E, new MoveNECommand());
        AddCommand(KeyCode.W, new MoveNCommand());
        AddCommand(KeyCode.Q, new MoveNWCommand());
        AddCommand(KeyCode.A, new MoveSWCommand());
        AddCommand(KeyCode.S, new MoveSCommand());
        AddCommand(KeyCode.D, new MoveSECommand());
    }

    // return the key code of the button pressed
    private KeyCode FetchKey()
    {
        // can change to array if needed for multiple inputs
        var keyCodes = System.Enum.GetNames(typeof(KeyCode)).Length;
        for (int i = 0; i < keyCodes; i++)
        {
            if (Input.GetKey((KeyCode)i))
            {
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }
}
