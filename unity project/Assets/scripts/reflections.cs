using System.Runtime;
using System.Net;
using System.Globalization;
using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflections : MonoBehaviour
{

    Camera m_ReflectionCamera;
    Camera m_MainCamera;

    public GameObject m_ReflectionPlane;

    [Range(0.0f , 1.0f)]
    public float m_ReflectionFactor = 0.5f;

    public Material m_FloorMaterial;
    public Shader Shader_crop;
    private RenderTexture m_RenderTarget;

    // Start is called before the first frame update
    void Start()
    {
        GameObject reflectionCameraGo = new GameObject("ReflectionCamera");
        m_ReflectionCamera = reflectionCameraGo.AddComponent<Camera>();
        
        //Setting clipping mask script
        reflectionCameraGo.AddComponent<clipping_mesh>();
        reflectionCameraGo.GetComponent<clipping_mesh>().EffectShader = Shader_crop;


        Behaviour berrave = (Behaviour)m_ReflectionCamera;
        berrave.enabled = false;

        m_MainCamera = Camera.main;

        m_RenderTarget = new RenderTexture(Screen.width, Screen.height, 24 );

    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_reflectionFactor", m_ReflectionFactor);
    }

    private void OnPostRender()
    {
        RenderReflection();
    }
    
    void RenderReflection()
    {
        m_ReflectionCamera.CopyFrom(m_MainCamera);

        // Position of main camera acquiring
        Vector3 cameraDirectionWorldSpace = m_MainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = m_MainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = m_MainCamera.transform.position;

        // Transforming the coordinates floor relative
        Vector3 cameraDirectionPlaneSpace = m_ReflectionPlane.transform.InverseTransformDirection(cameraDirectionWorldSpace);
        Vector3 cameraUpPlaneSpace = m_ReflectionPlane.transform.InverseTransformDirection(cameraUpWorldSpace);
        Vector3 cameraPositionPlaneSpace = m_ReflectionPlane.transform.InverseTransformPoint(cameraPositionWorldSpace);

        // Mirroring the vectors
        cameraDirectionPlaneSpace.y *= -1.0f;
        cameraUpPlaneSpace.y *= -1.0f;
        cameraPositionPlaneSpace.y*= -1.0f;

        // Transform the vectors back to world space
        cameraDirectionWorldSpace = m_ReflectionPlane.transform.TransformDirection(cameraDirectionPlaneSpace);
        cameraUpWorldSpace = m_ReflectionPlane.transform.TransformDirection(cameraUpPlaneSpace);
        cameraPositionWorldSpace = m_ReflectionPlane.transform.TransformPoint(cameraPositionPlaneSpace);

        // Set camera position and rotation
        m_ReflectionCamera.transform.position = cameraPositionWorldSpace;
        m_ReflectionCamera.transform.LookAt(cameraPositionWorldSpace + cameraDirectionWorldSpace, cameraUpWorldSpace);

        // Set render target for the reflection camera
        m_ReflectionCamera.targetTexture = m_RenderTarget;

        // Render the reflection camera
        m_ReflectionCamera.Render();

        DrawQuad();

    }

    void DrawQuad()
    {
        GL.PushMatrix();

        //Use material to draw the quad
        m_FloorMaterial.SetPass(0);
        m_FloorMaterial.SetTexture("_ReflectionTex", m_RenderTarget);

        GL.LoadOrtho();
        GL.Begin(GL.QUADS);

        GL.TexCoord2(1.0f, 0.0f);
        GL.Vertex3(0.0f , 0.0f, 0.0f);
        GL.TexCoord2(1.0f, 1.0f);
        GL.Vertex3(0.0f , 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 1.0f);
        GL.Vertex3(1.0f , 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(1.0f , 0.0f, 0.0f);
        GL.End();

        GL.PopMatrix();
    }

}
