using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainGameControl : MonoBehaviour
{

    public static MainGameControl Instance { get; private set; }
    [Header("Transition Properties")]
    [SerializeField] GameObject circle;
    [SerializeField] float loadTime;
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
        

    }

    public void GoToNextLevel(SceneIndex nextLevel)
    {
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

}
