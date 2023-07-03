using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Gun;
    public Transform GunEnd;

    public int MaxHealth = 7, Damage = 1;
    public float MoveSpeed, GunRotationSpeed = 60, IntervalBetweenShots = 2;

    private int currentHealth;
    private float fireRateTimer = 0f;

    /// <summary>
    /// band aid fix
    /// </summary>
    private bool firstTimeStart = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SessionManager.Instance.isGameRunning && gameObject.activeSelf)
        {
            UpdateMovement();

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, PlayerController.Instance.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, GunRotationSpeed * Time.deltaTime);

            if (fireRateTimer <= 0)
            {
                FireAtPlayer();
                fireRateTimer = IntervalBetweenShots;
            }

            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
        }
    }

    public void FireAtPlayer()
    {
        if (fireRateTimer <= 0)
        {
            GameObject bullet = EnemyLaserBulletPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = GunEnd.position;
                bullet.transform.rotation = GunEnd.rotation;
                bullet.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        if (!firstTimeStart)
        {
            currentHealth = MaxHealth;
            InitializePosition();
        }
    }

    private void OnDisable()
    {
        firstTimeStart = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth == 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        SessionManager.Instance.PointUp();
        //create effect and sound
        gameObject.SetActive(false);
    }


    #region Follow The Path

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    public Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Use this for initialization
    private void InitializePosition()
    {
        waypointIndex = 0;
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void UpdateMovement()
    {

        // Move Enemy
        Move();
    }

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
}

