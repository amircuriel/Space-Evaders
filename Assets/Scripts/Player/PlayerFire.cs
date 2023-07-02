using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private float intervalBetweenShots = 0.05f;
    [SerializeField]
    private float rotationSpeed = 45f;

    [System.Serializable]
    public struct Gun
    {
        public Transform Body;
        public Transform BarrelEnd;
    }
    
    [SerializeField]
    private List<Gun> Guns;

    private PlayerMovement playerMovement;

    private float fireRateTimer = 0f;
    private int currentGun = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRateTimer > 0)
        {
            fireRateTimer -= Time.deltaTime;
        }
    }

    public void Fire(Vector2 direction)
    {
        foreach (Gun gun in Guns)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            gun.Body.rotation = Quaternion.RotateTowards(gun.Body.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (fireRateTimer <= 0)
        {
            Gun nextGun = Guns[currentGun];

            
            GameObject bullet = PlayerLaserBulletPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = nextGun.BarrelEnd.position;
                bullet.transform.rotation = nextGun.BarrelEnd.rotation;
                bullet.SetActive(true);
            }
            playerMovement.ApplyRecoil(direction);

            fireRateTimer = intervalBetweenShots;

            currentGun++;
            if (currentGun == Guns.Count)
            {
                currentGun = 0;
            }
        }
    }
}
