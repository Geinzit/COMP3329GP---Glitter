using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDisplay : MonoBehaviour
{
    GameObject Player;
    TMP_Text TextComponent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        TextComponent = gameObject.GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        TextComponent.text= "Glitters: " + Player.GetComponent<characterAttribute>().experience.ToString();
    }
}
