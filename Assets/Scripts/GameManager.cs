using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] public SceneIndex nextScene;
    public int rootsNecessery;
    private int rootsCut = 0;
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

    public void CutRoot()
    {
        rootsCut++;
        if(rootsCut>=rootsNecessery)
        {
            Debug.Log("You won!!");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((int)nextScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
