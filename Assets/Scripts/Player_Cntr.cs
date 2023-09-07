using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cntr : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private JoystickHandler joystick;
    [Header("Cntr Values")]
    [SerializeField] private float speed;

    private void Start()
    {
        SetValues();
    }

    #region Start

    private void SetValues()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Character.Run(rb, joystick.inputVector.x, joystick.inputVector.y, speed);
    }
    #endregion
}
