using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clipping_mesh : MonoBehaviour
{

    public Shader EffectShader;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Camera>().SetReplacementShader(EffectShader, "");
    }

}
