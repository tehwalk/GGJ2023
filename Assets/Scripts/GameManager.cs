using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] private SceneIndex nextScene;
    public int rootsNecessery;
    private int rootsCut = 0;
    public int hitableLayerValue;
    [SerializeField] private TextMeshProUGUI scoreText;
    MainGameControl gameControl;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
            _instance = null;
        _instance = this;

        gameControl = MainGameControl.Instance;
    }
    void Start()
    {
        Cursor.visible = false;
        UpdateScoreText();
        //rootsNecessery = FindGameObjectsInLayer(hitableLayerValue);

    }

    int FindGameObjectsInLayer(int layervalue)
    {
        List<GameObject> requiredObjects = new List<GameObject>();
        var gameObjectList = FindObjectsOfType<GameObject>();
        foreach (GameObject g in gameObjectList)
        {
            if (g.layer == layervalue)
            {
                requiredObjects.Add(g);
            }
        }
        return requiredObjects.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CutRoot()
    {
        rootsCut++;
        UpdateScoreText();
        if (AudioManager.instance != null) AudioManager.instance.PlayCutClip();

        if (rootsCut >= rootsNecessery)
        {
            Debug.Log("You won!!");
            NextLevel();
        }
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene((int)nextScene);
        if (nextScene == SceneIndex.ClosingCutscene) SceneManager.LoadScene((int)nextScene);
        else gameControl.GoToNextLevel(nextScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void TogglePause(bool isPauseMenuOpen)
    {
        if (isPauseMenuOpen == true)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = rootsCut.ToString() + " / " + rootsNecessery.ToString();
    }
}
