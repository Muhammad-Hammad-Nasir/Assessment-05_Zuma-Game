using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollow : MonoBehaviour
{
    private PathCreator pathcreator;
    private float speed = 1.5f;

    private float distanceTravelled;

    void Start()
    {
        pathcreator = GameObject.Find("PathCreator").GetComponent<PathCreator>();
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathcreator.path.GetPointAtDistance(distanceTravelled);
    }
}
