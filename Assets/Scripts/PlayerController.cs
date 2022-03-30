using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 lookPos;

    void Start()
    {
        
    }

    void Update()
    {
        LookDirection();
    }

    void LookDirection()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        lookPos = mouseRay.origin + mouseRay.direction;

        targetPos = new Vector3(lookPos.x, transform.position.y, lookPos.z);
        transform.LookAt(targetPos);
    }
}
