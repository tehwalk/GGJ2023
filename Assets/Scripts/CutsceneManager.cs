using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Image[] cutsceneImages;
    [SerializeField] Button skipButton;
    //public Image blackImage;
    [SerializeField] float fadeTime, waitTime;
    [SerializeField] SceneIndex nextScene;
    MainGameControl gameControl;
    // Start is called before the first frame update

    private void Awake()
    {
        gameControl = MainGameControl.Instance;
        if (skipButton != null) skipButton.onClick.AddListener(GoToNextScene);
    }
    void Start()
    {
        foreach (Image i in cutsceneImages)
        {
            //i.gameObject.SetActive(false);
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
        StartCoroutine(ShowImage());
    }

    IEnumerator ShowImage()
    {
        for (int i = 0; i < cutsceneImages.Length; i++)
        {
            cutsceneImages[i].DOFade(1, fadeTime);
            // cutsceneImages[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            //cutsceneImages[i].gameObject.SetActive(false);
            cutsceneImages[i].DOFade(0, fadeTime);
            yield return new WaitForSeconds(fadeTime);
        }
        GoToNextScene();
    }

    void GoToNextScene()
    {
        if(nextScene == SceneIndex.MainMenu) 
        {
            Destroy(gameControl);
            SceneManager.LoadScene((int)nextScene);
        }
        else
        {
           gameControl.ShowClickText();
           gameControl.GoToNextLevel(nextScene);
        }
    }
}
