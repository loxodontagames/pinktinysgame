using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speede;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;
    private bool colliding;

    public LayerMask layer;

    // colisor desativado
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circlecollider2D;
    // fim do colisor desativado

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        rig.velocity = new Vector2(speede, rig.velocity.y); 
        // movimentando o inimigo
         colliding = Physics2D.Linecast(rightCol.position, leftCol.position, 
         layer);
         //colisor invisivel em formato de linha
         if(colliding){
             transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
             speede *= -1f;
            // movimentação de direita p/ esquerda
         }
    }

    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            float height = col.contacts[0].point.y - headPoint.position.y;
            if(height > 0 && !playerDestroyed){
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speede = 0;
                anim.SetTrigger("morrendo");
                boxCollider2D.enabled = false;
                circlecollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;

                Destroy(gameObject, 0.33f);
            }
            else{
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        }
    }

}
