using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;

    

    private Vector3 playerPos;
    private Vector3 offset;
    private Vector3 awayFromPlayer;
    private float xLimit = 25;
    private float zLimit = 20;
    private float speed = 15;
    private bool isShot = false;

    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isShot)
        {
            isShot = true;
            awayFromPlayer = transform.position - player.transform.position;
        }
    }

    void OutOfBounds()
    {
        if (transform.position.x > xLimit || transform.position.x < -xLimit)
        {
            gameManager.isBullet = false;
            Destroy(gameObject);
        }
        else if (transform.position.z > zLimit || transform.position.z < -zLimit)
        {
            gameManager.isBullet = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (tag == collision.gameObject.tag)
        {
            gameManager.UpdateScore();
            gameManager.playerAudio.PlayOneShot(gameManager.hitSound, 1);
            gameManager.isBullet = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            gameManager.playerAudio.PlayOneShot(gameManager.hitSound, 1);
            gameManager.isBullet = false;
            Destroy(gameObject);
        }
    }
}
