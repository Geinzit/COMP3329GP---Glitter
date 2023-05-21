using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    public float effectRadius;
    public float speed;
    public int attackPower;
    [SerializeField] float timeToDestroy;
    float timer = 0f;
    Vector3 direction;
    public GameObject target;

    Collider2D col;
    Collider2D[] colliders;

    public void SetDirection()
    {
        if(target == null)
        {
            direction = new Vector3(1, 0, 0); 
        }
        else 
        {
            direction = (target.transform.position - transform.position).normalized;
        }
        transform.Rotate(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    void Awake()
    {
        col = gameObject.GetComponent<Collider2D>();
        colliders = new Collider2D[100];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        timer += Time.deltaTime;
        bool hit = false;
        int num = col.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        for(int i = 0; i < num; i++)
        {
            Collider2D c = colliders[i];
            if(c.gameObject.tag == "Enemy")
            {
                hit = true;
                break;
            }
        }
        if(hit)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
            foreach(Collider2D c in colliders)
            {
                enemyBehaviour e = c.GetComponent<enemyBehaviour>();
                if(e != null)
                {
                    e.takeDamage(attackPower, col.gameObject);
                }             
            }
            Destroy(gameObject);
        }
        if(timer > timeToDestroy)Destroy(gameObject);
    }
}
