using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public struct PlayerData {
    [SerializeField] public int gold;
    [SerializeField] public int damagePerHit;
    [SerializeField] public float speed;
    [SerializeField] public float jumpSize;

    public PlayerData(int g, int d, float s, float j)
    {
        gold = g;
        damagePerHit = d;
        speed = s;
        jumpSize = j;
    }
};

[System.Serializable]
public struct LevelData {
    [SerializeField] public string scenePath;

    public LevelData(string s)
    {
        scenePath = s;
    }
};

[System.Serializable]
public struct PowerupData {
    [SerializeField] public string spritePath;
    [SerializeField] public int hpAddition;
    [SerializeField] public int damageAddition;
    [SerializeField] public float speedAddition;
    [SerializeField] public float jumpAddition;
    [SerializeField] public float powerupDuration;
    [SerializeField] public int price;

    public PowerupData(string i, GameObject powerup)
    {
        spritePath = i;
        hpAddition = powerup.GetComponent<Powerup>().getHpAddition();
        damageAddition = powerup.GetComponent<Powerup>().getDamageAddition();
        speedAddition = powerup.GetComponent<Powerup>().getSpeedAddition();
        jumpAddition = powerup.GetComponent<Powerup>().getJumpAddition();
        powerupDuration = powerup.GetComponent<Powerup>().getPowerupDuration();
        price = powerup.GetComponent<Powerup>().getPowerupPrice();
    }
}




[System.Serializable]
public class GameData:MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public LevelData levelData;
    [SerializeField] public List<PowerupData> shopItems; 
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

    private void computePlayerData()
    {
        GameObject player = GameObject.Find("Player");
        int gold = player.GetComponent<PlayerScript>().GetGold();
        int damagePerHit = player.GetComponent<PlayerScript>().getDamagePerHit();
        float speed = player.GetComponent<PlayerMovement>().getSpeed();
        float jumpSize = player.GetComponent<PlayerMovement>().getJumpSize();
        playerData = new PlayerData(gold, damagePerHit, speed, jumpSize);
    }

    private void computeShopItems()
    {
        List<GameObject> items = GameObject.Find("UI").GetComponent<ShopMenuScript>().getItems();
        shopItems = new List<PowerupData>();
        foreach(GameObject y in items) {
            string path = AssetDatabase.GetAssetPath(y.transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            shopItems.Add(new PowerupData(path, y));
        }
    }

    private void computeLevelData()
    {
        levelData = new LevelData(SceneManager.GetActiveScene().path);
    }

    private void makePlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerScript>().SetGold(playerData.gold);
        player.GetComponent<PlayerScript>().setDamagePerHit(playerData.damagePerHit);
        player.GetComponent<PlayerMovement>().setSpeed(playerData.speed);
        player.GetComponent<PlayerMovement>().setJumpSize(playerData.jumpSize);
    }

    private void makeLevel()
    {
        SceneManager.LoadScene(levelData.scenePath);
        SceneManager.sceneLoaded += SceneLoaded;


        if (SceneManager.GetActiveScene().path == levelData.scenePath) {
            makeShopItems();
            makePlayer();
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().path == levelData.scenePath) {
            makeShopItems();
            makePlayer();
        }
    }

    public static GameObject FindGameObjectInScene(string name)
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.name == name)
            {
                return go;
            }
        }

        return null;
    }

    private void makeShopItems()
    {
        // In order to load items according to save
        // we have to instantiate items at load time.
        GameObject ui = FindGameObjectInScene("UI");
        GameObject shopItemList = FindGameObjectInScene("ItemsList");

        List<GameObject> newItems = new List<GameObject>();
        // Load only saved items
        foreach (PowerupData powerupData in shopItems) {
            GameObject prefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/ShopItem.prefab");
            GameObject childprefab = Instantiate(prefab, new Vector3(0, 0 , 0), Quaternion.identity);
            childprefab.GetComponent<Powerup>().setHpAddition(powerupData.hpAddition);
            childprefab.GetComponent<Powerup>().setDamageAddition(powerupData.damageAddition);
            childprefab.GetComponent<Powerup>().setSpeedAddition(powerupData.speedAddition);
            childprefab.GetComponent<Powerup>().setJumpAddition(powerupData.jumpAddition);
            childprefab.GetComponent<Powerup>().setPowerupDuration(powerupData.powerupDuration);
            childprefab.GetComponent<Powerup>().setPowerupPrice(powerupData.price);
            
            childprefab.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
                (Sprite)AssetDatabase.LoadAssetAtPath(powerupData.spritePath, typeof(Sprite));
            
            childprefab.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            childprefab.transform.GetChild(0).gameObject.GetComponent<RectTransform>().position = new Vector2(400, 200);
            
            childprefab.transform.SetParent(shopItemList.transform);

            newItems.Add(childprefab);
        }

        ui.GetComponent<ShopMenuScript>().setItems(newItems);

    }

    public void saveState()
    {
        computeLevelData();
        computePlayerData();
        computeShopItems();
    }

    public void loadState()
    {
        makeLevel();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            DataManager.SaveJsonData();
            Debug.Log("Save game with key P");
        } 
    }

    IEnumerator waitForSceneLoad(string scenePath)
    {
        Debug.Log("wait for scene");
        Debug.Log(scenePath);
        Debug.Log(SceneManager.GetActiveScene().path);
        while (SceneManager.GetActiveScene().path != scenePath)
        {
            Debug.Log(SceneManager.GetActiveScene().path);
            yield return null;
        }

        Debug.Log("ce plm dc nu merge");

        if (SceneManager.GetActiveScene().path == scenePath) {
            makeShopItems();
            makePlayer();
        } else 
        {
            Debug.Log("ce plm dc nu merge");
        }
    }
}

