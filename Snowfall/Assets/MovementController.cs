using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpheight;
    Rigidbody m_rigidbody;
    public float gravity;
    bool jumpBuffered = false;
    bool canJump = true; 
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    void buttonWatcher(){
        if(Input.GetKey("a")){
            transform.position += -transform.right*Time.deltaTime*speed;
        }
        if(Input.GetKey("d")){
            transform.position += transform.right*Time.deltaTime*speed;
        }
        if(Input.GetKey("w")){
            transform.position += transform.forward*Time.deltaTime*speed;
        }
        if(Input.GetKey("s")){
            transform.position += -transform.forward*Time.deltaTime*speed;
        }
        if(Input.GetKeyDown("space") && canJump){
            jumpBuffered = true;
        }
    }
    // Update is called once per frame

    void Update() {
        
        transform.eulerAngles = new Vector3(0,transform.rotation.eulerAngles.y,0);
        buttonWatcher(); 
        
    }
    void FixedUpdate(){
        if(jumpBuffered && canJump){
            m_rigidbody.AddForce(transform.up*jumpheight);
            jumpBuffered=false;
        }
        m_rigidbody.AddForce(Vector3.down * gravity * m_rigidbody.mass);
        
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "Floor"){
            canJump = true;
        }
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.name == "Floor"){
            canJump = false;
        }
    }
}
