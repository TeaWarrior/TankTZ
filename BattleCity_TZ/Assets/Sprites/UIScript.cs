using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScript : MonoBehaviour
{


    [SerializeField] PlayerStats player;
    [SerializeField] GameObject losePanel;
    [SerializeField] PlayerBase playerBase;
    [SerializeField] GameObject winPanel;

    [SerializeField] GameObject gameInfoPanel;

    [SerializeField] GameObject controllInfo;
    public int score;
    [SerializeField] TextMeshProUGUI enemiesAmountTXT;
    [SerializeField] TextMeshProUGUI liveAmountTXT;
    [SerializeField] TextMeshProUGUI loseScoreAmountTXT;
    [SerializeField] TextMeshProUGUI winScoreAmountTXT;




    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        playerBase = MazeGenerator.instance.playerBase.GetComponent<PlayerBase>();
        player.OnPlayerDie += Player_OnPlayerDie;
        player.OnPlayerLoseLive += Player_OnPlayerLoseLive;
        playerBase.OnBaseDestroyed += PlayerBase_OnBaseDestroyed;
        EnemySpawner.instance.OnAllEnemiesDie += Instance_OnAllEnemiesDie;
        EnemySpawner.instance.OnEnemyKilled += Instance_OnEnemyKilled;
        
        score = 0;
    }

    private void Player_OnPlayerLoseLive(object sender, System.EventArgs e)
    {
        liveAmountTXT.text = player.liveAmount.ToString();
    }

    private void Instance_OnEnemyKilled(object sender, System.EventArgs e)
    {
        score += 75;
        enemiesAmountTXT.text = EnemySpawner.instance.enemyLive.ToString();

    }

    private void Instance_OnAllEnemiesDie(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;
        winScoreAmountTXT.text = score.ToString();
        gameInfoPanel.SetActive(false);
        winPanel.SetActive(true);
    }

    private void PlayerBase_OnBaseDestroyed(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;
        loseScoreAmountTXT.text = score.ToString();
        gameInfoPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    private void Player_OnPlayerDie(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;
        loseScoreAmountTXT.text = score.ToString();
        gameInfoPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
       // Time.timeScale = 1f;
    }

    public void RestartLevel()
    {

    }


    public void StartLevel()
    {
        controllInfo.SetActive(false);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
