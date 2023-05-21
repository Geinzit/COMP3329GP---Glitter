using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    GameObject Player;
    [SerializeField] AudioSource forest;
    [SerializeField] AudioSource wasteland;
    [SerializeField] AudioSource alta;
    [SerializeField] AudioSource iceland;
    [SerializeField] AudioSource twinTree;
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    public void musicFadeOut()
    {

    }
    void Update()
    {
        Vector3 p = Player.GetComponent<Transform>().position;
        if(p.x > 170)
        {
            forest.Stop();
            alta.Stop();
            iceland.Stop();
            twinTree.Stop();
            if(!wasteland.isPlaying)
                wasteland.Play();
        }
        else if(p.x < -180 && p.y > 20)
        {
            wasteland.Stop();
            alta.Stop();
            iceland.Stop();
            if(!twinTree.isPlaying)
                twinTree.Play();
            if(!forest.isPlaying)
                forest.Play();
        }
        else if(p.y < -200)
        {
            forest.Stop();
            wasteland.Stop();
            iceland.Stop();
            twinTree.Stop();
            if(!alta.isPlaying)
                alta.Play();
        } 
        else if(p.y > 170)
        {
            forest.Stop();
            alta.Stop();
            wasteland.Stop();
            twinTree.Stop();
            if(!iceland.isPlaying)
                iceland.Play();
        }
        else 
        {
            wasteland.Stop();
            alta.Stop();
            iceland.Stop();
            twinTree.Stop();
            if(!forest.isPlaying)
                forest.Play();
        }
    }
}
