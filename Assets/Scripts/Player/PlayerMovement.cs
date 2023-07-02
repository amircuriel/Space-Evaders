using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float recoilStrengthPerBullet = 1f;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float deacceleration;

    private Vector2 currentMoveDirection;
    private bool currentlyMoving;

    private Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyRecoil(Vector2 direction)
    {
        myRigidbody.AddForce(-direction * recoilStrengthPerBullet * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
