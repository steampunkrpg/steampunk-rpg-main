using System;
using UnityEngine;

public class MoveNECommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveNE();
    }
}
