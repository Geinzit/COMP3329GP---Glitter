using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class dataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;


    private gameData gamedata;

    private List<Idatapersistence> datapersistenceObjects;
    private fileDataHandler dataHandler;

    

    public static dataPersistenceManager instance{get; private set;}

    private void Awake()
    {
        Debug.Log("Awoken?" + instance);
        if (instance != null)
        {
            Debug.LogError("More than one Data Persistence Manager found in the scene. Destroying Newest One");
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        dataHandler = new fileDataHandler(Application.persistentDataPath, fileName);
    }

    public void NewGame()
    {
        gamedata = new gameData();
    }

    public void LoadGame()
    {
        Debug.Log("attempting to load game?");
        this.gamedata = dataHandler.Load();

        if(this.gamedata == null)
        {
            Debug.Log("No data was found. Please start a new game first!");
            return;
        }

        foreach(Idatapersistence datapersistenceObject in datapersistenceObjects)
        {
            Debug.Log(datapersistenceObject);
            datapersistenceObject.LoadData(gamedata);
        }
    }

    public void SaveGame()
    {
        if(gamedata == null)
        {
            Debug.Log("Don't have any game data! Please start a new game first!");
            return;
        }
        Debug.Log("attempted to save game!");
        foreach(Idatapersistence datapersistenceObject in datapersistenceObjects)
        {
            datapersistenceObject.SaveData(ref gamedata);
        }
        dataHandler.Save(gamedata);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<Idatapersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<Idatapersistence> datapersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<Idatapersistence>();

        return new List<Idatapersistence>(datapersistenceObjects);
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called");
        datapersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnLoaded Called");
        SaveGame();
    }

    public bool HasGameData()
    {
        return gamedata != null;
    }
}
