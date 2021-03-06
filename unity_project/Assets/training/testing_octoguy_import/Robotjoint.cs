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
        GetEndPoint();
    }

    void FixedUpdate()
    {
        // Debug.Log(this.transform.localPosition.x);
    }

    public Vector3 GetEndPoint()
    {
        Transform[] transform_in_children = GetComponentsInChildren<Transform>();
        Debug.Log(transform_in_children.Length);
        Debug.Log(transform_in_children[0].position.x);
        Debug.Log(this.transform.position.x);

        Vector3 EndPoint = new Vector3(0,0,0);
        return EndPoint;
    }

}
