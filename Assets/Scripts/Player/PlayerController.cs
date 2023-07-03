using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton Implementation
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public Joystick FireJoystick;
    public TMPro.TMP_Text PlayerHealth;
    public AudioSource DamageSound;
    public AudioSource HealSound;
    public PlayerFire PlayerFire;

    public int MaxHealth = 5;
    //heals if didn't take damage during that time
    public float TimeToRegenHealth = 10f;
    public float invulnurabilityTimeAfterHit = 0.5f;

    private int currentHealth;

    private float healthRegenTimer = 0f;

    private float invulnurabilityTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        PlayerHealth.text = currentHealth.ToString();
        invulnurabilityTimer = invulnurabilityTimeAfterHit;
    }

    // Update is called once per frame
    void Update()
    {
        if (SessionManager.Instance.isGameRunning)
        {
            if (FireJoystick.Direction != Vector2.zero)
            {
                PlayerFire.Fire(FireJoystick.Direction);
            }

            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer >= TimeToRegenHealth)
            {
                RestoreHealth();
                healthRegenTimer = 0f;
            }

            invulnurabilityTimer += Time.deltaTime;
            PlayerHealth.text = currentHealth.ToString();
        }
    }

    public void LoseHealth()
    {
        if (invulnurabilityTimer > invulnurabilityTimeAfterHit && SessionManager.Instance.isGameRunning)
        {
            currentHealth--;
            DamageSound.Play();
            healthRegenTimer = 0f;

            if (currentHealth == 0)
            {
                SessionManager.Instance.GameOver();
            }

            invulnurabilityTimer = 0f;
        }
    }

    public void RestoreHealth()
    {
        if (currentHealth < MaxHealth)
        {
            currentHealth++;
            HealSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            LoseHealth();
        }
    }
}
