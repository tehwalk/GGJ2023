using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject buttonsLayout;
    [SerializeField] GameObject creditsLayout;
    [SerializeField] TextMeshProUGUI highScoreText;
    [Space]
    [SerializeField] MainGameControl mainGameControl;
    [SerializeField] SceneIndex nextScene;

    [SerializeField] GameObject player;
    [SerializeField] float speed;
    bool canMove;
    Vector3 buttonPos;

    [SerializeField] Animator playerAnim;
    public bool showCredits { get; private set; }

    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        playButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(ToggleCredits);
        backButton.onClick.AddListener(ToggleCredits);
        exitButton.onClick.AddListener(ExitGame);
        canMove = false;
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = "WON WITH FEWEST MOVES: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        
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
        playerAnim.SetTrigger("Jump");
        yield return new WaitUntil(() => VectorAppox(player.transform.position, buttonPos) == true);
        canMove = false;

        Instantiate(mainGameControl);
        SceneManager.LoadScene((int)nextScene);
    }
    void ToggleCredits()
    {
        showCredits = !showCredits;
        buttonsLayout.SetActive(!showCredits);
        creditsLayout.SetActive(showCredits);
    }

    void ExitGame() => Application.Quit();

    bool VectorAppox(Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
