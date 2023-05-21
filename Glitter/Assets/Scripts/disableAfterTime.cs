using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDisable;
    float timer;

    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
