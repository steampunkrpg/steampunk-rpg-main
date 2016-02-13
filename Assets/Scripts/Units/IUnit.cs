using UnityEngine;
using System.Collections;

public interface IUnit {


    void Update();

    void MoveNE();
    void MoveN();
    void MoveNW();
    void MoveSW();
    void MoveS();
    void MoveSE();
}
