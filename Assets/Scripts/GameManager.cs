using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] public SceneIndex nextScene;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this) _instance = null;
        _instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel()
    {
        
    }
}
