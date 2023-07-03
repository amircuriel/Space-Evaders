using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private float recoilStrengthPerBullet = 25f;
    [SerializeField]
    private float intervalBetweenShots = 0.12f;
    [SerializeField]
    private float rotationSpeed = 2000f;

    [System.Serializable]
    public struct Gun
    {
        public Transform Body;
        public Transform BarrelEnd;
    }
    
    [SerializeField]
    private List<Gun> Guns;

    private Rigidbody2D myRigidbody;

    private float fireRateTimer = 0f;
    private int currentGun = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SessionManager.Instance.isGameRunning)
        {
            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
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
            ApplyRecoil(direction);

            fireRateTimer = intervalBetweenShots;

            currentGun++;
            if (currentGun == Guns.Count)
            {
                currentGun = 0;
            }
        }
    }



    public void ApplyRecoil(Vector2 direction)
    {
        myRigidbody.AddForce(-direction * recoilStrengthPerBullet * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
