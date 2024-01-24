using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerapositioning : MonoBehaviour
{
    public GameObject player;
    public float vert_sens;
    public float horiz_sens;
    public float x_camera_adjust;
    public float y_camera_adjust;
    Vector3 currentAngle  = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float horiz  = horiz_sens * Input.GetAxis("Mouse X");
        float vert = -vert_sens * Input.GetAxis("Mouse Y");
        
        
        currentAngle += new Vector3(vert,horiz,0);
        transform.rotation = Quaternion.Euler(currentAngle);
        //player.transform.Rotate(0,horiz,0);
        float cameraY =  transform.rotation.y;
        player.transform.rotation = Quaternion.Euler(0,currentAngle.y,0);
        transform.position = player.transform.position + new Vector3(x_camera_adjust,y_camera_adjust,0);
        
    }
}
