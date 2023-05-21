using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour, Idatapersistence
    
{
    [SerializeField] int attackPower = 5;
    [SerializeField] float effectRadius = 1f;
    [SerializeField] int amount = 1;
    [SerializeField] float coolDownTime;
    [SerializeField] GameObject Ball;
    [SerializeField] AudioSource _audio;
    [SerializeField] float speed = 4f;
    GameObject ball;
    float timer;
    public int Level = 1;

    GameObject[] closestEnemies;int ind = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = coolDownTime;
        closestEnemies = new GameObject[1005];
    }

    public void Upgrade()
    {
        if(Level % 5 == 0)amount++;
        Level++;
        attackPower += 5;
        speed += 0.1f;
        effectRadius += 0.04f;
        timer = coolDownTime;
    }

    public bool notAlreadyFound(GameObject enemy)
    {
        for(int i = 0 ; i < ind ; i++)
        {
            if(enemy == closestEnemies[i])
                return false;
        }return true;
    }

    public void FindClosestEnemy()
    {
        
        ind = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < amount; i++)
        {
            float mnDis = 100000000.0f;
            GameObject ClosestEnemy = null;
            foreach(GameObject enemy in enemies)
            {
                float Dis = Vector3.Distance(transform.position, enemy.transform.position);
                if (notAlreadyFound(enemy) && mnDis > Dis)
                {
                    mnDis = Dis;
                    ClosestEnemy = enemy;
                }
            }
            Debug.Log(i);
            Debug.Log(ClosestEnemy);
            if(ClosestEnemy == null && ind > 0) ClosestEnemy = closestEnemies[ind - 1];
            closestEnemies[ind++] = ClosestEnemy;
        }
    }

    public void spawnFireBall()
    {
        FindClosestEnemy();
        for(int i = 0; i < amount; i++)
        {
            ball = Instantiate(Ball, transform.position, Quaternion.identity);
            fireBallLaunch l = ball.GetComponent<fireBallLaunch>();
            l.target = closestEnemies[i];
            
            l.SetDirection();
            l.effectRadius = effectRadius;
            l.speed = speed;
            l.attackPower = attackPower;
            ball.transform.localScale = new Vector3(ball.transform.localScale.x + (Level - 1) * 0.012f, ball.transform.localScale.y + (Level - 1) * 0.012f, ball.transform.localScale.z);
        }
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            spawnFireBall();
            timer = coolDownTime;
        }
    }

    public void LoadData(gameData data)
    {
        for(int i = 2; i <= data.fireLv; i++)
        {
            Upgrade();
        }
    }

    public void SaveData(ref gameData data)
    {
        data.fireLv = Level;
    }
}
