using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameManager gameManager;

    private bool isCollided;

    void Update()
    {
        if (isCollided)
        {
            gameManager.playerLife--;
            isCollided = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isCollided = true;
        Destroy(collision.gameObject);
    }
}
