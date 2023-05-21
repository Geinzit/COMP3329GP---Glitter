using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour,Idatapersistence
{
    GameObject Player;
    bool isProtected;
    bool isEnd;
    [SerializeField] GameObject endGamePrompt;
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject lightFlameEffect;

    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        _audio = gameObject.GetComponent<AudioSource>();
        isEnd = false;
    }
     void OnTriggerEnter2D(Collider2D collider)
    {
        characterAttribute c = collider.gameObject.GetComponent<characterAttribute>();
        if(c != null)
        {
            isProtected = true;          

            Instantiate(lightFlameEffect, transform.position, Quaternion.identity);
            _audio.Play();

            c.setFullHealth();

            c.updateLastRest(gameObject.transform);

            spawner.gameObject.GetComponent<Spawner>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        characterAttribute c = collider.gameObject.GetComponent<characterAttribute>();
        if(c != null)
        {
            isProtected = false;
            endGamePrompt.GetComponent<TMPro.TMP_Text>().text = "";
            spawner.gameObject.GetComponent<Spawner>().enabled = true;
        }
        else
        {
            enemyBehaviour e = collider.gameObject.GetComponent<enemyBehaviour>();
            if(e != null)
                e.Die();
        }
    }

    void revealTheTruth()
    {
        isEnd = true;
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(isProtected)
        {
            if(Player.GetComponent<characterAttribute>().experience > 100000000)
            {
                endGamePrompt.GetComponent<TMPro.TMP_Text>().text = "Ascend? [space] to confirm";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    revealTheTruth();
                }
            }
            else 
            {
                endGamePrompt.GetComponent<TMPro.TMP_Text>().text = "The Mother only receives the shiniest of individuals";
            }
        }
    }
    public void SaveData(ref gameData gamedata)
    {
        gamedata.isEnd = this.isEnd;
    }
    public void LoadData(gameData gamedata)
    {
        this.isEnd = gamedata.isEnd;
    }
}
