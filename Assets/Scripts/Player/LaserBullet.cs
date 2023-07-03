using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public float speed = 1f;
    public AudioSource PewPewSound;

    private float automaticDeactivationTime = 2f;
    private float currentDeactivationTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf && SessionManager.Instance.isGameRunning)
        {
            transform.localPosition += transform.up * speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Asteroid":
                collision.gameObject.GetComponentInParent<AsteroidController>().DamageAsteroid();
                break;
            case "Enemy":
                collision.gameObject.GetComponentInParent<EnemyController>().TakeDamage();
                break;
            case "Player":
                PlayerController.Instance.LoseHealth();
                break;
            default:
                break;
        }

        //create explosion effect
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentDeactivationTimer = 0f;
        PewPewSound.Play();
    }

    private void Update()
    {
        if (gameObject.activeSelf && SessionManager.Instance.isGameRunning)
        {
            currentDeactivationTimer += Time.deltaTime;
            if (currentDeactivationTimer >= automaticDeactivationTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
