using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;

    bool isBlowing;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);// próprio da unity mesmo com a classe "Horizontal"
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        if(Input.GetAxis("Horizontal") == 0f){
            anim.SetBool("walk",false);
        }
    }
    void Jump(){
        if(Input.GetButtonDown("Jump") && !isBlowing){// próprio da unity mesmo com a classe "jump"
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump",true);
            } else{
                if(doubleJump){
                rig.AddForce(new Vector2(0f, JumpForce*0f), ForceMode2D.Impulse);
                doubleJump = false;
            }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            isJumping = false;
            anim.SetBool("jump",false);
        }
        if(collision.gameObject.tag == "spike"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Saw"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.layer == 10){
            isBlowing = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.layer == 10){
            isBlowing = false;
        }
    }
}
//if = é uma estrutura para checar se uma condição é atendida ou não.
//