using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class upgradeTExt : MonoBehaviour
{
    bool isDisplaying = false;
    TMP_Text T;
    [SerializeField] float displayTime;
    float timer ;
    void Start()
    {
        T = gameObject.GetComponent<TMP_Text>();
        T.text = "";
    }

    void Update()
    {
        if(isDisplaying == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                T.text = "";
                isDisplaying = false;
            }
        }
    }

    public void triggerDisplay(string t)
    {
        isDisplaying = true;
        timer = displayTime;
        T.text = t;
    }
}
