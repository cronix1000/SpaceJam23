using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WorldSetup mapScript;
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        mapScript = GetComponent<WorldSetup>();
        InitGame();
    }
    void InitGame()
    {
        mapScript.SetupScene();
    }
}
