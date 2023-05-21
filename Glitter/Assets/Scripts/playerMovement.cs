using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource walkOnGrass;
    [SerializeField] AudioSource walkOnRock;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        AudioSource footstep;
        if(gameObject.transform.position.x < 170 && gameObject.transform.position.y > -180 && gameObject.transform.position.y < 110)
            footstep = walkOnGrass;
        else 
            footstep = walkOnRock;
        
        if(anim.GetBool("isDead"))
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        if(moveHorizontal != 0 || moveVertical != 0)
        {
            anim.SetBool("isMoving", true);
            if(!footstep.isPlaying)
                footstep.Play();
        }
        else 
        {
            anim.SetBool("isMoving", false);
            footstep.Stop();
        }
        if(moveHorizontal < 0)
        {
            Vector3 scale = gameObject.transform.localScale;
            if(scale.x > 0)scale.x = -scale.x;
            gameObject.transform.localScale = scale;
        }
        else if(moveHorizontal > 0)
        {
            Vector3 scale = gameObject.transform.localScale;
            if(scale.x < 0)scale.x = -scale.x;
            gameObject.transform.localScale = scale;
        }
        rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * speed;
    }
}
