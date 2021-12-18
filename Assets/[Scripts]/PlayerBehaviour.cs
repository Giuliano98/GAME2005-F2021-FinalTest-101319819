using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;

    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
        _Move();
    }

    private void _Move()
    {
        if (isGrounded)
        {
            // Todo: Coudn't make it move in air
            // move right
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            // move left
            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            // move forward
            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            // move Back
            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y

            
            // transform - Jump   
            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity = transform.up * speed * 0.1f * Time.deltaTime;
            }

            // Update Position
            transform.position += body.velocity;
        }

    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
