using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class levelupDisplay : MonoBehaviour
{
    [SerializeField] GameObject Player;
    TMP_Text TextComponent;
    // Start is called before the first frame update
    void Start()
    {
        TextComponent = gameObject.GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        TextComponent.text= "[1]: level up blade\n[2]: level up crossbow\n[3]: level up fireball\n[4]: level up Health & Armor\n[5]: level up speed\n(req " + Player.GetComponent<characterAttribute>().TO_LEVEL_UP.ToString() + " glitters)";
    }
}
/*
[space]: Level Up Base Stats
[1]: level up blade
[2]: level up crossbow
[3]: level up fireball
(req xxx glitters)
*/
