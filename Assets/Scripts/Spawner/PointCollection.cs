using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollection : MonoBehaviour
{
    protected Transform[] Points;

    private void Start()
    {
        Points = GetComponentsInChildren<Transform>();
    }

    public Transform GetRandomPosition()
    {
        if(Points.Length == 0)
        {
            return null;
        }

        int index = Random.Range(0, Points.Length);
        return Points[index];
    }
}
