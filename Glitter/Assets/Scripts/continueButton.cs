using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueButton : MonoBehaviour
{
    private void Start()
    {
        if(!dataPersistenceManager.instance.HasGameData()||gameObject.GetComponentInParent<EndGameDisplay>().isEnd)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartGameplay()
    {

        SceneManager.LoadSceneAsync("Progeni");
    }
}
