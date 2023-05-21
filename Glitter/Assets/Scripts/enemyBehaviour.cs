using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    Transform targetDestination;
    [SerializeField] float speed;
    float speed_backup;
    [SerializeField] int attackPower;

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;
    [SerializeField] GameObject BleedingEffect;
    [SerializeField] GameObject SlowedEffect;

    [SerializeField] int experience_point;
    [SerializeField] GameObject damageNumber;

    private AudioSource _audio;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine,bleedRoutine;
    bool dead = false;
    float is_slowed = 0f;
    Animator anim;

    GameObject targetGameobject;
    characterAttribute targetCharacter;
    Rigidbody2D rb;

    GameObject bleed, slow;

    [SerializeField] float attackCooldown;
    float timer;

    [SerializeField] int hp = 10;


    public void Start()
    {
        setTarget(GameObject.FindGameObjectsWithTag("Player")[0]);
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        _audio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        speed_backup = speed;
    }

    public void setTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        timer += Time.deltaTime;
        if(timer >= 40f) Destroy(gameObject);
        Vector3 v = gameObject.transform.localScale;
        if(direction.x < 0 && v.x > 0 || direction.x > 0 && v.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-v.x, v.y, v.z);
        }
        if(!gameObject.GetComponent<knockbackFeedback>().isPlaying())
            rb.velocity = direction * speed;

        if(bleedRoutine != null)
        {
            hp--;
            if(hp == 0)
            {
                targetGameobject.GetComponent<characterAttribute>().AddExperience(experience_point);
                Die();
            }           
        }

        if(is_slowed > 0)
        {
            is_slowed -= Time.deltaTime;
            if(is_slowed <= 0)
            {   
                speed = speed_backup;
                Destroy(slow);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject == targetGameobject && timer >= attackCooldown && !dead)
        {
            timer = 0;
            Attack();
        }
    }
    
    private void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameobject.GetComponent<characterAttribute>();
        }

        targetCharacter.takeDamage(attackPower);
    }

    public void Die()
    {
        anim.SetBool("isDead", true);
        dead = true;
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void takeDamage(int damage, GameObject attacker)
    {
        if(hp <= 0)return;
        _audio.Play();
        hp -= damage;

        Instantiate(damageNumber,transform.position, Quaternion.identity);
        damageNumber.GetComponentInChildren<TMPro.TMP_Text>().text = damage.ToString();
        
        gameObject.GetComponent<knockbackFeedback>().PlayFeedback(attacker);
        Flash();
        
        if(hp <= 0)
        {
            targetGameobject.GetComponent<characterAttribute>().AddExperience(experience_point);
            Die();
        }
    }


    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator BleedRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        bleedRoutine = null;
        Destroy(bleed);
    }

    public void setBleeding(float t)
    {
        if(bleedRoutine != null)
        {
            StopCoroutine(bleedRoutine);
            if(bleed != null) Destroy(bleed);
        }
        bleed = Instantiate(BleedingEffect, transform.position, BleedingEffect.transform.localRotation);
        bleed.transform.parent = transform;
        bleedRoutine = StartCoroutine(BleedRoutine(t));
    }

    public void setSlowed(float t, float duration)
    {
        if(is_slowed == 0)
        {
            speed -= t;
            if(speed < 0)speed = 0;
            slow = Instantiate(SlowedEffect, transform.position, SlowedEffect.transform.localRotation);
            slow.transform.parent = transform;
        }
        is_slowed = duration;
    }
}
