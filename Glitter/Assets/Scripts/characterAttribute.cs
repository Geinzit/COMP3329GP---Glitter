using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class characterAttribute : MonoBehaviour, Idatapersistence
{
    [SerializeField] int maxHP;
    [SerializeField] HPBar hpBar;
    [SerializeField] AudioSource takedamage;

    [SerializeField] GameObject bloodEffect;
    [SerializeField] GameObject levelUpEffect;
    
    [SerializeField] Animator animBlackScreen;
    [SerializeField] GameObject GlitterRecovery;
    [SerializeField] Transform last_rested_lightFlame;
    [SerializeField] int armor;
    [SerializeField] AudioSource levelUp;

    [SerializeField] GameObject upgradeText;

    [SerializeField] GameObject blade;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject fire;

    Animator anim;
    int Level = 1;
    int hpLv = 1;
    int spdLv =1;
    public ulong experience = 0;
    [HideInInspector]
    public int curHP;
    [HideInInspector]
    public ulong TO_LEVEL_UP = 1000;


    public Transform getLastRest()
    {
        return last_rested_lightFlame;
    }

    public void updateLastRest(Transform transform)
    {
        last_rested_lightFlame = transform;
    }

    public void AddExperience(int amount)
    {
        experience = experience + (ulong)amount;
    }

    public void setFullHealth()
    {
        curHP = maxHP;
        hpBar.SetState(curHP, maxHP);
    }

    public void healthUpgrade()
    {
        hpLv++;
        string upgradeMessage;
        maxHP += 50;
        armor += 20;
        upgradeMessage = "max health increased by 20\narmor increased by 4";
        setFullHealth();
        upgradeText.GetComponent<upgradeTExt>().triggerDisplay(upgradeMessage);
    }

    public void speedUpgrade()
    {
        spdLv++;
        string upgradeMessage;
        gameObject.GetComponent<playerMovement>().speed += 0.2f;
        upgradeMessage = "movement speed increased by 20%";
        upgradeText.GetComponent<upgradeTExt>().triggerDisplay(upgradeMessage);
    }

    public void displayBladeUpgrade()
    {

        string upgradeMessage = "improved damage, range, cooldown of blade";
        if(blade.GetComponent<dragonblade>().Level == 11)
            upgradeMessage += "\nUnlocked whirlwind slash";
        else if(blade.GetComponent<dragonblade>().Level == 21)
            upgradeMessage += "\nUnlocked bleeding attack";
        else if(blade.GetComponent<dragonblade>().Level > 20)
        {
            upgradeMessage += "\nimproved bleeding duration";
        }
        upgradeText.GetComponent<upgradeTExt>().triggerDisplay(upgradeMessage); 
    }

    public void bladeUpgrade()
    {
        blade.GetComponent<dragonblade>().Upgrade();
        displayBladeUpgrade();
    }

    public void displayCrossbowMessage()
    {
        string upgradeMessage = "improved damage, size, speed, cooldown of crossbow";
        if(bow.GetComponent<crossbow>().Level % 6 == 1)
            upgradeMessage += "\nimproved arrow penetration";
        if(bow.GetComponent<crossbow>().Level == 21)
            upgradeMessage += "\nUnlocked slowing shot";
        else if(bow.GetComponent<crossbow>().Level > 20)
        {
            upgradeMessage += "\nimproved slow amount and duration";
        }
        upgradeText.GetComponent<upgradeTExt>().triggerDisplay(upgradeMessage); 
    }

    public void crossbowUpgrade()
    {
        bow.GetComponent<crossbow>().Upgrade();
        displayCrossbowMessage();
    }

    public void displayFireBallMessage()
    {
        string upgradeMessage = "improved damage, Area of Effect, speed of fireball";
        if(fire.GetComponent<fireBall>().Level % 5 == 1)
            upgradeMessage += "\nimproved fireball amount";
        upgradeText.GetComponent<upgradeTExt>().triggerDisplay(upgradeMessage); 
    }

    public void fireballUpgrade()
    {
        fire.GetComponent<fireBall>().Upgrade();
        displayFireBallMessage();
    }

    public void spawnerUpdate()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject s in spawners)
        {
            Debug.Log(s);
            s.GetComponent<Spawner>().Upgrade();
        }
    }

    /*
    Health +50
    Armor +2
    speed +0.2
    */
    public void checkLevelUp(int t)
    {
        if(experience >= (ulong)TO_LEVEL_UP)
        {
            experience -= TO_LEVEL_UP;
            Level += 1;
            levelUp.Play();
            Instantiate(levelUpEffect, transform.position, levelUpEffect.transform.localRotation);
            TO_LEVEL_UP += 1000ul * ((ulong)(Level - 1) / 10 + 1);
            spawnerUpdate();

            if(t == 0)
            {
                healthUpgrade();
            }
            else if(t == 1)
            {
                bladeUpgrade();
            }
            else if(t == 2)
            {
                crossbowUpgrade();
            }
            else if(t == 3)
            {
                fireballUpgrade();
            }
            else if(t == 4)
            {
                speedUpgrade();
            }
        }
    }

    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.position = last_rested_lightFlame.position;
    }

    public void disenableAllWeapons(bool v)
    {
        foreach(Transform child in transform)
        {
            // Debug.Log(child.gameObject.tag);
            if(child.gameObject.tag == "Weapon")
            {
                // Debug.Log("in??");
                child.gameObject.SetActive(v);
            }
        }
    }

    public void triggerFadeOut()
    {
        animBlackScreen.SetTrigger("fadeOut");
    }

    public void triggerFadeIn()
    {
        animBlackScreen.SetTrigger("fadeIn");
    }

    public void createGlitterRecovery()
    {
        GameObject[] Glitters = GameObject.FindGameObjectsWithTag("GlitterRecovery");
        foreach(GameObject g in Glitters)
        {
            Destroy(g);
        }
        Instantiate(GlitterRecovery, transform.position, GlitterRecovery.transform.localRotation);
        experience = 0;
    }

    public void clearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {

            enemy.GetComponent<enemyBehaviour>().Die();
        }
    }

    public void reviveCharacter()
    {
        clearEnemies();
        gameObject.transform.position = last_rested_lightFlame.position;
        anim.SetBool("isDead", false);
        setFullHealth();
        last_rested_lightFlame.gameObject.GetComponent<lightFlame>().enableSpawner(false);
        disenableAllWeapons(true);
    }

    public void youDied()
    {
        //Disable all firing weapons
        disenableAllWeapons(false);
        //Play death animation
        anim.SetBool("isDead", true);

        //fade to black
        
        //Create a Glitter Recovery Object, Destroy previous Glitter Recovery Object

        //lose all experience

        //Destroy all Enemies


        //move Player to last rested lightFlame

        //disenableAllWeapons(true);
        //fade to scene
    }

    public void takeDamage(int damage)
    {
        if(curHP <= 0)return;
        damage = damage * 300 / (300 + armor);
        if(damage < 1) damage = 1;
        curHP -= damage;
        if(!takedamage.isPlaying)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            takedamage.Play();
        }

        if (curHP <= 0)
        {
            youDied();
        }
        hpBar.SetState(curHP, maxHP);
    }

    public void LoadData(gameData data)
    {
        this.Level = data.level;
        this.hpLv = data.hpLv;
        this.spdLv = data.spdLv;
        this.transform.position = data.playerPos;
        this.experience = data.experience;
        for(int i = 2; i <= Level;i++)
        {
            TO_LEVEL_UP += 1000ul * ((ulong)(Level - 1) / 10 + 1);
            spawnerUpdate();
        }
        for(int i = 2; i <= hpLv; i++)
        {
            maxHP += 50;
            armor += 20;
        }
        for(int i = 2; i <= spdLv; i++)
        {
            gameObject.GetComponent<playerMovement>().speed += 0.2f;
        }
        this.curHP = data.curHP;
    }

    public void SaveData(ref gameData data)
    {
        data.level= this.Level;
        data.hpLv = this.hpLv;
        data.spdLv = this.spdLv;
        data.playerPos = this.transform.position;
        data.last_rested_lightFlame = this.last_rested_lightFlame;
        data.experience = this.experience;
        data.curHP = this.curHP;
    }


}
