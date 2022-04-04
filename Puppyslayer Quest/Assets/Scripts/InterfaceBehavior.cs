using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceBehavior : MonoBehaviour
{
    public Text displayStatus;
    private GameBehavior _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        displayStatus.text = _gameManager.labelText;
    }
}
