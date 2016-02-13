using System;
using UnityEngine;

public class MoveSWCommand : ICommand
{
    void ICommand.Execute(Unit unit)
    {
        unit.MoveSW();
    }
}
