using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lightFlame : MonoBehaviour
{
    GameObject Player;
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject levelupPrompt;
    [SerializeField] GameObject lightFlameEffect;

    bool isProtected = false;

    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        _audio = gameObject.GetComponent<AudioSource>();
    }

    public void enableSpawner(bool t)
    {
        spawner.gameObject.GetComponent<Spawner>().enabled = t;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        characterAttribute c = collider.gameObject.GetComponent<characterAttribute>();
        if(c != null)
        {

            if(c.getLastRest() != gameObject.transform)
            {
                isProtected = true;
                Instantiate(lightFlameEffect, transform.position, Quaternion.identity);
                _audio.Play();

                c.setFullHealth();

                c.clearEnemies();

                c.updateLastRest(gameObject.transform);
                
                levelupPrompt.SetActive(true);
            }
            enableSpawner(false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        characterAttribute c = collider.gameObject.GetComponent<characterAttribute>();
        if(c != null)
        {
            
            isProtected = false;
            levelupPrompt.SetActive(false);
            enableSpawner(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isProtected)
        {
            if(Input.GetKeyDown(KeyCode.Keypad1)||Input.GetKeyDown(KeyCode.Alpha1))
                Player.GetComponent<characterAttribute>().checkLevelUp(1);
            else if(Input.GetKeyDown(KeyCode.Keypad2)||Input.GetKeyDown(KeyCode.Alpha2))
                Player.GetComponent<characterAttribute>().checkLevelUp(2);
            else if(Input.GetKeyDown(KeyCode.Keypad3)||Input.GetKeyDown(KeyCode.Alpha3))
                Player.GetComponent<characterAttribute>().checkLevelUp(3);        
            else if(Input.GetKeyDown(KeyCode.Keypad4)||Input.GetKeyDown(KeyCode.Alpha4))
                Player.GetComponent<characterAttribute>().checkLevelUp(0);
            else if(Input.GetKeyDown(KeyCode.Keypad5)||Input.GetKeyDown(KeyCode.Alpha5))
                Player.GetComponent<characterAttribute>().checkLevelUp(4);
            
        }
    }
}
