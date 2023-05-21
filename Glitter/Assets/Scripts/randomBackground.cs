using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBackground : MonoBehaviour
{
    [SerializeField] Sprite image0;
    [SerializeField] Sprite image1;
    [SerializeField] Sprite image2;
    [SerializeField] Sprite image3;
    [SerializeField] Sprite image4;
    [SerializeField] Sprite image5;
    [SerializeField] Sprite image6;

    void Start()
    {
        if(gameObject.GetComponentInParent<EndGameDisplay>().isEnd)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image0;
        }
        else 
        {
            int num = (int)(UnityEngine.Random.value * 7);
            if(num == 1)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image1;
            }
            else if(num == 2)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image2;
            }
            else if(num == 3)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image3;
            }
            else if(num == 5)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image5;          
            }
            else if(num == 4){
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image4;
            }
            else 
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = image6;
            }
        }
    }
}
