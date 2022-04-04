using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public string labelText = "collect all 4 items and win your freedom I guess...";
    public int maxItems = 4;
    public int _itemsCollected = 0;
    public Stack<string> lootStack = new Stack<string>();
    public GameObject startLevelUI;
    public GameObject failedLevelUI;
    public GameObject completeLevelUI;

    public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }
    public int Items
    {
        get 
        {
            return _itemsCollected;
        }
        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);
            if (_itemsCollected >= maxItems)
            {
                CompleteLevel();
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "items found, only " + (maxItems - _itemsCollected) + " more to go.";
            }
        }
    }

    public int _playerHP = 10;

    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                FailedLevel();
                Time.timeScale = 0;
            }
            else
            {
                labelText = "skill issue";
            }
        }
    }

    public void StartLevel()
    {
        startLevelUI.SetActive(true);
        if (Input.GetMouseButtonDown(1))
        {
            startLevelUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void FailedLevel()
    {
        failedLevelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartLevel()
    {
        Utilities.RestartLevel(0);
        Time.timeScale = 1f;
    }

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "manager initialized";
        _state.FancyDebug();
        Debug.Log(_state);
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Braces");
        debug(_state);
        LogWithDelegate(debug);
        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }
    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}. You've got a good chance of finding a {1} next.", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you.", lootStack.Count);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
