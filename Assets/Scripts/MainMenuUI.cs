using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    //[SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;

    public GameObject player;
    public float speed;
    bool canMove;
    Vector3 buttonPos;

    void Start()
    {
        Time.timeScale = 1;
        playButton.onClick.AddListener(StartGame);
        //creditsButton.onClick.AddListener();				
        exitButton.onClick.AddListener(ExitGame);
        canMove = false;
    }

    private void Update()
    {
        if (canMove == true)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, buttonPos, speed * Time.deltaTime);
        }
    }

    void StartGame()
    {
        canMove = true;
        StartCoroutine(PlayerPressesPlay());
    }

    IEnumerator PlayerPressesPlay()
    {
        buttonPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return new WaitUntil(() => VectorAppox(player.transform.position, buttonPos) == true);
        canMove = false;
        SceneManager.LoadScene((int)SceneIndex.Scene1);
    }
    //void DisplayCredits() { }

    void ExitGame() => Application.Quit();

    bool VectorAppox(Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
