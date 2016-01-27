using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : Unit {

    public override void MoveUp()
    {
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

}
