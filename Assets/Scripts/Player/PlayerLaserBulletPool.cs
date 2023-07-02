using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserBulletPool : ObjectPool
{
    public static PlayerLaserBulletPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
