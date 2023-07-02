using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public float speed = 1f;

    private float automaticDeactivationTime = 2f;
    private float currentDeactivationTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.localPosition += transform.up * speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentDeactivationTimer = 0f;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            currentDeactivationTimer += Time.deltaTime;
            if (currentDeactivationTimer >= automaticDeactivationTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
