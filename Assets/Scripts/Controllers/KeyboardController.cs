using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardController : IController {

    Dictionary<KeyCode, ICommand> commands;
    GameObject playerGO;

    public KeyboardController(GameObject playerGO)
    {
        this.playerGO = playerGO;
        commands = new Dictionary<KeyCode, ICommand>();
        ResetController();
    }

    public void AddCommand(KeyCode keyCode, ICommand command)
    {
        commands[keyCode] = command;
    }

    // execute the keys pressed.
    public void Update()
    {
       KeyCode keyCode = FetchKey();

       if (commands.ContainsKey(keyCode))
       {
            commands[keyCode].Execute();
       }
    }

    // add input controls here
    private void ResetController()
    {
        AddCommand(KeyCode.W, new MoveUpCommand(playerGO));
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
