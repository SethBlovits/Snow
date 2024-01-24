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
    bool canJump = false;
    public Animator m_animator; 
    float currentMoveSpeedX=0;
    float currentMoveSpeedY=0;
    RaycastHit hit;
    RaycastHit[] hits;
    int layerMask;
    
    float maxSpeed;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        maxSpeed = runSpeed;
       // layerMask = 1 << 6;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
    }
    void collideAndSlide(){

        Physics.Raycast(transform.position + new Vector3(0,1,0),transform.forward,out hit,1f);
        Debug.DrawRay(transform.position+new Vector3(0,1,0),transform.forward,Color.green,0.0f);
        //for(int i = 0; i < hits.Length;i++){
        //    hit = hits[i];
        //    Debug.Log(hit.collider);
        //}
        /*if(hit.collider!=null){
            Debug.Log(hit.collider);
        }*/
        
    }
    void buttonWatcher(){
        
        if(Input.GetKey("a")){
            //transform.position += -transform.right*Time.deltaTime*speed;
            currentMoveSpeedX=-speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
            //m_rigidbody.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
        }
        if(Input.GetKey("d")){
            //transform.position += transform.right*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedX=speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
            //m_rigidbody.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
        }
        if(Input.GetKey("w")){
            //transform.position += transform.forward*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedY=speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
           // m_rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        if(Input.GetKey("s")){
            //transform.position += -transform.forward*Time.deltaTime*speed;
            //m_animator.SetTrigger("WalkTrigger");
            currentMoveSpeedY=-speed;
            //m_animator.SetFloat("Speed%",currentMoveSpeed);
            //m_rigidbody.MovePosition(transform.position - transform.forward * Time.deltaTime * speed);
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
    void checkSpeed(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //currentSpeed = m_rigidbody.velocity.magnitude;
        m_rigidbody.AddForce(transform.rotation * new Vector3(horizontal,0,vertical) * Time.deltaTime * speed,ForceMode.Impulse);
        float newSpeed = m_rigidbody.velocity.magnitude;
        if(newSpeed>=maxSpeed){
            m_rigidbody.AddForce(transform.rotation * -new Vector3(horizontal,0,vertical) * Time.deltaTime * speed,ForceMode.Impulse);
        }
    }
    void Update() {
        collideAndSlide();
        transform.eulerAngles = new Vector3(0,transform.rotation.eulerAngles.y,0);
        currentMoveSpeedX = 0;
        currentMoveSpeedY = 0;
       
        checkShift();
        buttonWatcher();
        
        m_animator.SetFloat("X_Speed",currentMoveSpeedX); 
        m_animator.SetFloat("Y_Speed",currentMoveSpeedY);
         
        
    }
    void FixedUpdate(){
        
        checkSpeed();
        
        if(jumpBuffered && canJump){
            m_rigidbody.AddForce(transform.up*jumpheight,ForceMode.Impulse);
            jumpBuffered = false;
        }
        //m_rigidbody.AddForce(Vector3.down * gravity * m_rigidbody.mass);
        
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "Mountains"){
            canJump = true;
        }
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.name == "Mountains"){
            canJump = false;
        }
    }
}
