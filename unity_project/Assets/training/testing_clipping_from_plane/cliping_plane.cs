using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliping_plane : MonoBehaviour
{
    // Material we pass the values to
    public Material mat;
    public Material mat2;
    // Plane to create the effect
    public GameObject plane_mesh;

    // Update is called once per frame
    void Update()
    {
        // Create plane representation from game object
        var filter = plane_mesh.GetComponent<MeshFilter>();

        // Check if the plane exists
        if(filter && filter.mesh.normals.Length > 0){
            Vector3 normal_vector;

            // Takes the normal vector from plane
            normal_vector = filter.transform.TransformDirection(filter.mesh.normals[0]);

            // Define plane with location and rotatino from plane
            Plane plane = new Plane(normal_vector, plane_mesh.transform.position);

            //transfer values from plane to vector4
            Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
            //pass vector to shader
            mat.SetVector("_Plane", planeRepresentation);
            mat2.SetVector("_Plane", planeRepresentation);

        }
    }
}
