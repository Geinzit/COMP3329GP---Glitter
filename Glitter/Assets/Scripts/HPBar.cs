using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(int current, int mx)
    {
        float state = (float)current;
        state /= mx;
        if(state < 0f) state = 0f;
        bar.transform.localScale = new Vector3(state, bar.transform.localScale.y, bar.transform.localScale.z);
    }
}
