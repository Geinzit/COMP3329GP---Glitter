using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy_grunt_1;
    [SerializeField] GameObject enemy_grunt_2;
    [SerializeField] GameObject enemy_elite;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject player;

    [SerializeField] float elite_percentage;
    [SerializeField] float grunt1_percentage;

    [SerializeField] float border_lx;
    [SerializeField] float border_rx;
    [SerializeField] float border_ly;
    [SerializeField] float border_ry;
    float timer;
    int gruntSpawnLimit;
    int eliteSpawnLimit;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void Upgrade()
    {
        if(spawnTimer > 0.02f)
            spawnTimer -= 0.002f;
        timer = spawnTimer;
        if(elite_percentage < 0.5f) elite_percentage += 0.001f;
        if(grunt1_percentage > 0.5f) grunt1_percentage -= 0.001f;
    }

    private void Update()
    {
        if(player.transform.position.x < border_rx && player.transform.position.x > border_lx &&
        player.transform.position.y > border_ly && player.transform.position.y < border_ry)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                spawnEnemy();
                timer = spawnTimer;
            }
        }
    }

    private Vector3 generateRandomPosition()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f: 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else 
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }
        return position;
    }

    private void spawnEnemy()
    {
        GameObject newEnemy;
        float random = UnityEngine.Random.value;
        // Debug.Log(random);
        if(random < elite_percentage)
        {
            newEnemy = Instantiate(enemy_elite);
        }
        else 
        {
            if(random < grunt1_percentage)
            {
                newEnemy = Instantiate(enemy_grunt_1);
            }
            else 
            {
                newEnemy = Instantiate(enemy_grunt_2);
            }
        }
        Vector3 position = generateRandomPosition();
        position += player.transform.position;

        newEnemy.transform.position = position;
        newEnemy.GetComponent<enemyBehaviour>().setTarget(player);
    }
}
