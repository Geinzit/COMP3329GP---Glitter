using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class newGame : MonoBehaviour
{
    public void StartGameplay()
    {
        dataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Progeni");
    }
}
