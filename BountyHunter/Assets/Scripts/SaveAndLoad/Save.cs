using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string savePath = "";
    private SaveData saveData;

    [SerializeField] protected GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        init();
        loadJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            saveJson();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            loadJson();
        }
    }

    private void init()
    {
        savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "saveData.json";
        player = GameObject.FindGameObjectWithTag("Player");
        //listeners
        gameEvents.loadSave += loadLevel;
    }

    public void saveJson()
    {
        //get values to save
        string activeSceneName = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player");
        int curLevel = player.gameObject.GetComponent<playerProgressionn>().level;
        print("curlevl: " + curLevel);
        //create a new saveData
        saveData = new SaveData(activeSceneName, curLevel);

        //create json string
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);

        //save to path
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        //complete save
        //Debug.Log("Save Complete");
    }

    public void loadJson()
    {
        using StreamReader reader = new StreamReader(savePath);

        string json = reader.ReadToEnd();

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        gameEvents.current.recentSaveData = data;

        //Debug.Log("Load Complete");
    }

    protected void loadLevel()
    {
        using StreamReader reader = new StreamReader(savePath);
        string json = reader.ReadToEnd();
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        SceneManager.LoadScene(data.sceneName);

    }

   
}

public class SaveData
{
    //values to save
    public string sceneName;
    public int level;

    public SaveData(string sceneName, int currantLevel)
    {
        this.sceneName = sceneName;
        this.level = currantLevel;
    }
}

