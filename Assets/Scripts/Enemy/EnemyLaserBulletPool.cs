using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBulletPool : ObjectPool
{
    public static EnemyLaserBulletPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
