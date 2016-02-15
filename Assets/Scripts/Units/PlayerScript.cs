using UnityEngine;
using System.Collections;
using System;

public class PlayerScript :MonoBehaviour {

    public void MoveUp()
    {
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

}
