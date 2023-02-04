using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] public SceneIndex nextScene;
    public int rootsNecessery;
    private int rootsCut = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this) _instance = null;
        _instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CutRoot()
    {
        rootsCut++;
        UpdateScoreText();
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

    void UpdateScoreText()
    {
        scoreText.text = rootsCut.ToString() + " / " + rootsNecessery.ToString();
    }
}
