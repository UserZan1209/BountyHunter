using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string savePath = "";
    private SaveData saveData;

    private string lastScene;
    private string currantScene;

    [SerializeField] protected GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        init();
        loadJson();

        lastScene = SceneManager.GetActiveScene().name; 
    }

    // Update is called once per frame
    void Update()
    {
        //debug save and load triggers
        if (Input.GetKeyUp(KeyCode.N))
        {
            saveJson();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            loadJson();
        }

        currantScene = SceneManager.GetActiveScene().name;
        if(lastScene != currantScene)
        {
            loadJson();
            lastScene = currantScene;
        }
    }

    private void init()
    {
        savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "saveData.json";
        player = GameObject.FindGameObjectWithTag("Player");
        //listeners
        gameEvents.loadSave += loadLevel;
        gameEvents.autoSave += saveJson;
    }

    public void saveJson()
    {
        //get values to save
        string activeSceneName = "S_HubArea";
        player = GameObject.FindGameObjectWithTag("Player");
        int curLevel = player.gameObject.GetComponent<playerProgressionn>().level;
        float curEXP = player.gameObject.GetComponent<playerProgressionn>().XPthisLevel;
        print("curlevl: " + curLevel);
        //create a new saveData
        saveData = new SaveData(activeSceneName, curLevel, curEXP);

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
        if(data != null)
        {
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.Log("no save file detected, loading the the debug level");
            SceneManager.LoadScene("DebugScene");
        }

    }

   
}

public class SaveData
{
    //values to save
    public string sceneName;
    public int level;
    public float xpThisLevel;

    public SaveData(string sceneName, int currantLevel, float currantXP)
    {
        this.sceneName = sceneName;
        this.level = currantLevel;
        this.xpThisLevel = currantXP;
    }
}

