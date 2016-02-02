using UnityEngine;
using System.Collections;

public interface IUnit {


    void Update();

    // Z is zero
    // N is negative
    // P is positive
    void MoveZN();
    void MovePZ();
    void MovePP();
    void MoveZP();
    void MoveNP();
    void MoveNZ();
}
