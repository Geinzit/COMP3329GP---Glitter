using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbow : MonoBehaviour, Idatapersistence
{
    [SerializeField] AudioSource _audio;
    [SerializeField] float speed = 8;
    [SerializeField] int attackPower = 7;
    [SerializeField] float coolDownTime = 1;
    [SerializeField] int penetration = 1;
    [SerializeField] float slowDuration = 1f;
    [SerializeField] float slowAmount = 0.5f;
    private float timer;
    [SerializeField] GameObject boltPrefab;
    float lastHorizontal, lastVertical;
    public int Level = 1;

    public void Upgrade()
    {
        if(Level > 20)
        {
            slowAmount += 0.01f;
            slowDuration += 0.1f;
        }
        if(Level % 6 == 0) penetration++;
        Level++;
        speed += 0.2f;
        attackPower += 4;
        if(coolDownTime > 0.1f)coolDownTime -= 0.02f;
        timer = 0;
    }

    private void SpawnBolt()
    {
        GameObject bolt = Instantiate(boltPrefab);

        crossbow_bolt c = bolt.GetComponent<crossbow_bolt>();

        bolt.transform.localScale = new Vector3(bolt.transform.localScale.x + 0.04f * (Level - 1), bolt.transform.localScale.y + 0.04f * (Level - 1), bolt.transform.localScale.z);
        c.attackPower = attackPower;
        c.speed = speed;
        c.penetration = penetration;
        c.slowDuration = slowDuration;
        c.level = Level;
        c.slowAmount = slowAmount;

        bolt.transform.position = transform.position;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        c.SetDirection(lastHorizontal, lastVertical);
        _audio.Play();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if(moveHorizontal != 0 || moveVertical != 0)
        {
            lastHorizontal = moveHorizontal;
            lastVertical = moveVertical;
        }
        if (timer <= coolDownTime)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        SpawnBolt();
    }

    public void LoadData(gameData data)
    {
        for(int i = 2; i <= data.crossbowLv; i++)
        {
            Upgrade();
        }
    }

    public void SaveData(ref gameData data)
    {
        data.crossbowLv = Level;
    }
}
