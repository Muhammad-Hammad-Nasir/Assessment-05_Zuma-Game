using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    private Vector3 targetPos;
    private Vector3 lookPos;

    void Update()
    {
        LookDirection();
    }

    void LookDirection()
    {
        if (!gameManager.isGameover)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            lookPos = mouseRay.origin + mouseRay.direction;

            targetPos = new Vector3(lookPos.x, transform.position.y, lookPos.z);
            transform.LookAt(targetPos);
        }
    }
}
