using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Vector3 offset;
    private Vector3 awayFromPlayer;
    private float xLimit = 25;
    private float zLimit = 20;
    private float speed = 30;
    private bool isShot = false;
    private bool isCollided = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Movement();
        Fire();
        OutOfBounds();
    }

    void Movement()
    {
        if (!isShot)
        {
            transform.position = (player.transform.forward * 2) + (Vector3.up * 0.5f);
        }
        else if (isShot)
        {
            transform.Translate(awayFromPlayer * Time.deltaTime * speed, Space.World);
        }

        if (isCollided)
        {
            GetComponent<PathFollow>().enabled = true;
            isCollided = false;
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShot = true;
            awayFromPlayer = transform.position - player.transform.position;
        }
    }

    void OutOfBounds()
    {
        if (transform.position.x > xLimit || transform.position.x < -xLimit)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z > zLimit || transform.position.z < -zLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Red"))
        {
            Debug.Log("Collided");
            isCollided = true;
        }
    }
}
