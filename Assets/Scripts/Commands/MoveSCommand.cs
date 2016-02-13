using System;
using UnityEngine;

public class MoveSCommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveS();
    }
}
