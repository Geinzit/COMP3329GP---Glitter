using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class areaText : MonoBehaviour
{
    GameObject Player;
    TMP_Text displayText;
    Animator anim;
    [SerializeField] AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        displayText = gameObject.GetComponent<TMP_Text>();
        anim = gameObject.GetComponent<Animator>();
    }

    public string getAreaInformation()
    {
        Vector3 p = Player.GetComponent<Transform>().position;
        if(p.x > 170)return "Scorched Wasteland";
        else if(p.x < -180 && p.y > 20) return "The Twin Trees";
        else if(p.y < -200) return "Alta Potens";
        else if(p.y > 170) return "Frozen Township";
        else 
        {
            return "Forest of Ithil";
        }
    }

    public void triggerAppear()
    {
        anim.SetTrigger("appear");
    }

    // Update is called once per frame
    void Update()
    {
        string area = getAreaInformation();       
        if(displayText.text != area)
        {
            _audio.Play();
            displayText.text = area;
            triggerAppear();
        }
    }
}
