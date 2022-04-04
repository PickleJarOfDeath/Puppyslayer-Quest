using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    public Text displayHealth;
    private GameBehavior _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        displayHealth.text = "player health " + _gameManager._playerHP.ToString();
    }
}
