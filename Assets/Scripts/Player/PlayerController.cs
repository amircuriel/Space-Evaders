using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick FireJoystick;
    public PlayerMovement PlayerMovement;
    public PlayerFire PlayerFire;
    public PlayerLife PlayerLife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SessionManager.isGameRunning)
        {
            if (FireJoystick.Direction != Vector2.zero)
            {
                PlayerFire.Fire(FireJoystick.Direction);
            }
        }
    }
}
