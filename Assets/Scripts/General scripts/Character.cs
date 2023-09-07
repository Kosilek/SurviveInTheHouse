using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public static void Run(Rigidbody rb, float x, float z, float speed)
    {
        rb.velocity = new Vector3(x * speed, 0, z * speed);
    }

}
