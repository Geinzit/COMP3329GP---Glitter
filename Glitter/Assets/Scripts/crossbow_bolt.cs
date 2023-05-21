using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbow_bolt : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    public float speed;
    public int attackPower;
    public int penetration = 1;
    [SerializeField] float timeToDestroy;
    public float slowDuration;
    public float slowAmount;
    public int level;
    private float timer;
    Collider2D col;
    Collider2D[] colliders;
    enemyBehaviour[] got;
    int ind = 0;
    int hit = 0;

    void Awake()
    {
        col = gameObject.GetComponent<Collider2D>();
        got = new enemyBehaviour[15];
        colliders = new Collider2D[100];
    }

    public void SetDirection(float dir_x, float dir_y)
    {
        if(dir_x == 0f && dir_y == 0f)
            direction = new Vector3(1, 0 ,0);
        else direction = new Vector3 (dir_x, dir_y, 0).normalized;
        transform.Rotate(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        timer += Time.deltaTime;
        int num = col.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        for(int i = 0; i < num; i++)
        {
            Collider2D c = colliders[i];
            enemyBehaviour e = c.GetComponent<enemyBehaviour>();
            if(e != null)
            {
                bool check = true;
                for(int j = 0; j < ind; j++)
                    if(e == got[j]){check=false;break;}
                if(!check)continue;
                e.takeDamage(attackPower, gameObject);
                if(level > 20)
                {
                    e.setSlowed(slowAmount, slowDuration);
                }
                hit++;
                got[ind++] = e;
                Debug.Log(e);
                if(hit == penetration)break;
            }
        }
        if(hit == penetration)Destroy(gameObject);
        if(timer > timeToDestroy)Destroy(gameObject);
    }
}
