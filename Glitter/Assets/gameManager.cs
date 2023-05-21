using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Panel.activeInHierarchy)Panel.SetActive(false);
            else Panel.SetActive(true);
        }
        
    }
}
