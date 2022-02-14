using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public string labelText = "collect all 4 items and win your freedom I guess...";
    public int maxItems = 4;
    private int _itemsCollected = 0;

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
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "player health" + _playerHP);
        GUI.Box(new Rect(20, 20, 150, 25), "items collected" + _itemsCollected);
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
