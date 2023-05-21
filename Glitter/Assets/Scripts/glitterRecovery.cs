using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitterRecovery : MonoBehaviour
{
    // Start is called before the first frame update
    ulong experience;
    GameObject Player;
    AudioSource _audio;
    Collider2D col,pCol;
    [SerializeField] GameObject recovery;
    void Awake()
    {
        col = gameObject.GetComponent<Collider2D>();
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        pCol = Player.GetComponent<Collider2D>();
        experience = Player.GetComponent<characterAttribute>().experience;
        _audio = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            recovery.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            recovery.SetActive(false);
        }
    }

    void Update()
    {
        if(col.IsTouching(pCol)&&Input.GetKeyDown(KeyCode.Space))
        {

            Player.GetComponent<characterAttribute>().experience += experience;
            AudioSource.PlayClipAtPoint(_audio.clip, transform.position);
            Destroy(gameObject);
        }
    }
}
