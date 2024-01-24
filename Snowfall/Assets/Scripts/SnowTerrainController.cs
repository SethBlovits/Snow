using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTerrainController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f,1f)]
    public float snowCover;
    //float value = 0;

    // Update is called once per frame
    void Update()
    {
       
        Shader.SetGlobalFloat("_SnowLevel",snowCover);
        //value += Time.deltaTime * snowCoverSpeed;
        
    }
}
