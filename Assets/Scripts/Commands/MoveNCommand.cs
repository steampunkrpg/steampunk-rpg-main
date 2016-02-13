using System;
using UnityEngine;

public class MoveNCommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveN();
    }
}
