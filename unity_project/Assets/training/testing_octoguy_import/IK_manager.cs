using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_manager : MonoBehaviour
{
    // For the bones identification
    public GameObject Bone_L;
    public GameObject Bone2_L;
    public GameObject Bone3_L;
    public GameObject Bone4_L;
    private GameObject[] Leg_1_L;
    
    void awake()
    {
        Leg_1_L = new GameObject[4]{Bone_L, Bone2_L, Bone3_L, Bone4_L};
    }

    public float DistanceFromTarget(Vector3 target, float [] angles, GameObject[] Leg)
    {
        Vector3 point = FowardKinematics (angles, Leg);
        return Vector3.Distance(point, target);
    }

    public Vector3 FowardKinematics(float[] angles, GameObject[] Leg)
    {
        
        // Acquiring a list of scripts
        Robotjoint[] JointScript = new Robotjoint[Leg.Length];
        for (int i = 0; i < Leg.Length; i++)
        {
            JointScript [i] = Leg[i].GetComponent ("RobotJoint") as Robotjoint;
        }

        // Applying the rotation
        Quaternion rotation = Quaternion.identity;
        Vector3 prevPoint = Leg[0].transform.position;
        for (int i = 1; i < Leg.Length; i++)
        {
            //Rotate around a new axis
            rotation *= Quaternion.AngleAxis(angles[i-1], JointScript[i-1].Axis);
            Vector3 nextPoint = prevPoint + rotation * JointScript[i].StartOffset;

            prevPoint = nextPoint;
        }

        // Returns the endpoint
        return prevPoint;
    }

}
