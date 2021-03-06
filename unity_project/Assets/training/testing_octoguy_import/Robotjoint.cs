using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robotjoint : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Start()
    {
        StartOffset = transform.localPosition;
        Axis = new Vector3(1,1,1);
    }

    void FixedUpdate()
    {
        Debug.Log(this.transform.localPosition.x);
    }

    public Vector3 GetEndPoint()
    {
        var tx :Transform[] = GetComponentsInChildren(Transform);
        Vector3 EndPoint = new Vector3(0,0,0);
        return EndPoint;
    }

}
