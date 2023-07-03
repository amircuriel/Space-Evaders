using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Transform rotatingArt;
    public float Speed = 2f;
    public float RotationSpeed = 50f;
    public int AsteroidMaxHealth = 2;

    private float automaticDeactivationTime = 7f;
    private float currentDeactivationTimer = 0f;
    private float asteroidCurrentHealth;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf && SessionManager.Instance.isGameRunning)
        {
            rb2d.velocity = (transform.up * Speed);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    private void OnEnable()
    {
        currentDeactivationTimer = 0f;
        asteroidCurrentHealth = AsteroidMaxHealth;
    }

    private void Update()
    {
        if (gameObject.activeSelf && SessionManager.Instance.isGameRunning)
        {
            rotatingArt.eulerAngles += new Vector3(0, 0, RotationSpeed * Time.deltaTime);

            currentDeactivationTimer += Time.deltaTime;
            if (currentDeactivationTimer >= automaticDeactivationTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void DamageAsteroid()
    {
        asteroidCurrentHealth--;
        if (asteroidCurrentHealth == 0)
        {
            DestroyAsteroid();
        }
    }

    public void DestroyAsteroid()
    {
        //create particle effects
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyAsteroid();
    }
}
