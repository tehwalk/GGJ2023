using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MainGameControl : MonoBehaviour
{

    public static MainGameControl Instance { get; private set; }
    [Header("Transition Properties")]
    [SerializeField] GameObject circle;
    [SerializeField] float loadTime;
    [Header("High Score Properties")]
    [SerializeField] TextMeshProUGUI numberOfClicksText;
    int numOfClicks = 0;
    GameObject circleInstance;
    Vector3 circleOGSCale;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { DestroyImmediate(gameObject); }
        // circle.SetActive(false);
        circleInstance = Instantiate(circle);
        circleInstance.SetActive(false);
        circleInstance.transform.SetParent(this.transform);
        circleOGSCale = circleInstance.transform.localScale;
        HideClickText();
    }

    public void GoToNextLevel(SceneIndex nextLevel)
    {
        //numberOfClicksText.gameObject.SetActive(true)
        circleInstance.SetActive(true);
        circleInstance.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene((int)nextLevel);
        // Time.timeScale = 0;
        circleInstance.transform.DOScale(new Vector3(0, 0, 0), loadTime).OnComplete(
             () =>
             {
                 // Time.timeScale = 1;
                 circleInstance.SetActive(false);
                 circleInstance.transform.localScale = circleOGSCale;
             }
         );
    }

    #region Number Of Clicks Text Methods
    public void ShowClickText()
    {
        numberOfClicksText.gameObject.SetActive(true);
        numberOfClicksText.text = "Moves: " + numOfClicks.ToString();
    }

    public void HideClickText()
    {
        numberOfClicksText.gameObject.SetActive(false);
    }

    public void AddClick()
    {
        numOfClicks++;
        numberOfClicksText.text = "Moves: " + numOfClicks.ToString();
    }

    public void ResetNumberOfClicks()
    {
        numOfClicks = 0;
    }

    public void PassHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") > numOfClicks)
            {
                PlayerPrefs.SetInt("HighScore", numOfClicks);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", numOfClicks);
        }
    }
    #endregion

}
