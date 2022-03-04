using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "collect all 4 items and win your freedom I guess...";
    public int maxItems = 4;
    public int _itemsCollected = 0;
    public Stack<string> lootStack = new Stack<string>();

    public string State
    {
        get { return _state; }
        set { _state = value; }
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
                labelText = "you've found all the items";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "items found, only " + (maxItems - _itemsCollected) + " more to go.";
            }
        }
       
    }

    private int _playerHP = 10;

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
                labelText = "lol rip";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "skill issue";
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "player health " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "items collected " + _itemsCollected);
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "you won."))
            {
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "you lose"))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
            }
        }
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
