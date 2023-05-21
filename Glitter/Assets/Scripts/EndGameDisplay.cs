using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDisplay : MonoBehaviour, Idatapersistence
{
    public bool isEnd = false;
    // Start is called before the first frame update

    public void SaveData(ref gameData gamedata)
    {
        gamedata.isEnd = isEnd;
    } 

    public void LoadData(gameData gamedata)
    {
        isEnd = gamedata.isEnd;
    }
}
