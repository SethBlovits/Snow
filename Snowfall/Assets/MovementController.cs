using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed;
    public float runSpeed;
    float speed = 2;
    public float jumpheight;
    Rigidbody m_rigidbody;
    public float gravity;
    bool jumpBuffered = false;
    bool canJump = true;
    public Animator m_animator; 
    float currentMoveSpeedX=0;
    float currentMoveSpeedY=0;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    void buttonWatcher(){
        if(Input.GetKey("a")){
            transform.position += -transform.right*Time.deltaTime*speed;
            currentMoveSpeedX=-speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
        }
        if(Input.GetKey("d")){
            transform.position += transform.right*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedX=speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
        }
        if(Input.GetKey("w")){
            transform.position += transform.forward*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedY=speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
        }
        if(Input.GetKey("s")){
            transform.position += -transform.forward*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedY=-speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
        }
        if(Input.GetKeyDown("space") && canJump){
            jumpBuffered = true;
        }
    }
    // Update is called once per frame
    void checkShift(){
        if(Input.GetKey("left shift")){
            speed = runSpeed;
        }
        else{
            speed = walkSpeed;
        }
    }
    void Update() {
        
        transform.eulerAngles = new Vector3(0,transform.rotation.eulerAngles.y,0);
        currentMoveSpeedX = 0;
        currentMoveSpeedY = 0;
        checkShift();
        buttonWatcher();
        m_animator.SetFloat("X_Speed",currentMoveSpeedX); 
        m_animator.SetFloat("Y_Speed",currentMoveSpeedY); 
        
    }
    void FixedUpdate(){
        if(jumpBuffered && canJump){
            m_rigidbody.AddForce(transform.up*jumpheight);
            jumpBuffered = false;
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
