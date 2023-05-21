using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class knockbackFeedback : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] 
    float strength = 3, delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    private Coroutine knockback;
    bool isplaying;
    public bool isPlaying()
    {
        return isplaying;
    }
    public void PlayFeedback(GameObject sender)
    {
        if(knockback != null)
        {
            StopCoroutine(knockback);
        }
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        isplaying = true;
        knockback = StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        isplaying = false;
        OnDone?.Invoke();
    }
}
