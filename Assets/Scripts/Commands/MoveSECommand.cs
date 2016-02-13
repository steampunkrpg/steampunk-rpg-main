using System;
using UnityEngine;

public class MoveSECommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveSE();
    }
}
