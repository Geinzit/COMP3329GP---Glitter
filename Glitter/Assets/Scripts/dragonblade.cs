using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonblade : MonoBehaviour, Idatapersistence
{
    
    float timer;
    [SerializeField] float coolDownTime = 2f;
    [SerializeField] GameObject leftAttack;
    [SerializeField] GameObject rightAttack;
    [SerializeField] int attackPower= 5;
    private AudioSource _audio;
    Collider2D[] colliders;
    public int Level = 1;

    float bleedDuration = 1.0f;

    Transform playert;
    private void Awake()
    {
        playert = GetComponentInParent<Transform>();
        _audio = GetComponent<AudioSource>();
        timer = coolDownTime;
        colliders = new Collider2D[1005];
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
        }
    }
    
    public void Upgrade()
    {
        if(Level > 20)bleedDuration += 0.1f;
        Level++;
        attackPower += 4;
        coolDownTime -= 0.02f;
        leftAttack.transform.localScale = new Vector3(leftAttack.transform.localScale.x + 0.06f, leftAttack.transform.localScale.y + 0.06f, leftAttack.transform.localScale.z); 
        rightAttack.transform.localScale = new Vector3(rightAttack.transform.localScale.x - 0.06f, rightAttack.transform.localScale.y + 0.06f, rightAttack.transform.localScale.z); 
    }
    private void lattack()
    {
        leftAttack.SetActive(true);
        Collider2D col = leftAttack.GetComponent<Collider2D>();
        int num = col.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        ApplyDamage(colliders, num, gameObject);
    }
    private void rattack()
    {
        rightAttack.SetActive(true);
        Collider2D col = rightAttack.GetComponent<Collider2D>();
        int num = col.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        ApplyDamage(colliders, num, gameObject);
    }
    private void Attack()
    {
        timer = coolDownTime;

        _audio.Play();
        if(Level <= 10)
        {
            rattack();
        }
        else 
        {
            lattack();
            rattack();        
        }
    }
    private void ApplyDamage(Collider2D[] colliders,int num, GameObject attacker)
    {
        for(int i = 0; i < num; i++)
        {
            enemyBehaviour e = colliders[i].GetComponent<enemyBehaviour>();
            if(e == null) continue;
            e.takeDamage(attackPower, attacker);
            if(Level > 20)
            {
                e.setBleeding(bleedDuration);
            }
        }
    }

    public void LoadData(gameData data)
    {
        for(int i = 2; i <= data.swordLv; i++)
        {
            Upgrade();
        }
    }

    public void SaveData(ref gameData data)
    {
        data.swordLv = Level;
    }
}
