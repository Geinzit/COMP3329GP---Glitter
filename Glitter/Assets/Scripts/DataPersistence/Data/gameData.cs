using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class gameData
{
    public int level, hpLv, spdLv, swordLv, crossbowLv, fireLv, curHP;
    public ulong experience;
    public Vector3 playerPos;
    public Transform last_rested_lightFlame;
    public bool isEnd;
    public gameData()
    {
        curHP = 200;
        level = hpLv = spdLv = swordLv = crossbowLv = fireLv = 1;
        experience = 0;
        playerPos = new Vector3(65.43f, -141.04f, 0);
        isEnd = false;
    }

}
