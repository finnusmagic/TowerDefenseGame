using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {

    [SerializeField] int totalWaves = 10;
    [SerializeField] Text totalMoneyLbl;
    [SerializeField] Text currentWaveLbl;
    [SerializeField] Text totalEscapedLbl;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject startImage;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] enemies;

    [SerializeField] int totalEnemies = 3;
    [SerializeField] int enemiesPerSpawn;

    [SerializeField] Text playButtonLbl;
    [SerializeField] Button playButton;

    GameStatus currentState = GameStatus.play;

    private AudioSource audioSource;

    int waveNumber = 0;
    int totalMoney = 10;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled = 0;
    int whichEnemiesToSpawn = 0;

    public List<Enemy> EnemyList = new List<Enemy>();
    const float spawnDelay = 1f;

    public int TotalEscaped
    {
        get
        {
            return totalEscaped;
        }
        set
        {
            totalEscaped = value;
        }
    }

    public int RoundEscaped
    {
        get
        {
            return roundEscaped;
        }
        set
        {
            roundEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return totalKilled;
        }
        set
        {
            totalKilled = value;
        }
    }

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

    public AudioSource AudioSource
    {
        get
        {
            return audioSource;
        }
    }

    void Update()
    {
        HandleEscape();
    }

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        playButton.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
        startImage.gameObject.SetActive(false);

        ShowMenu();
	}

    IEnumerator SpawnEnemies()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemies());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        IsWaveOver();
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }	

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    public void IsWaveOver()
    {
        totalEscapedLbl.text = "Escaped: " + TotalEscaped + " / 10";

        if ((RoundEscaped + TotalKilled) == totalEnemies)
        {
            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if(TotalEscaped >= 3)
        {
            currentState = GameStatus.gameover;
        }
        else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0)
        {
            currentState = GameStatus.play;
        }
        else if (waveNumber >= totalWaves)
        {
            currentState = GameStatus.win;
        }
        else
        {
            currentState = GameStatus.next;
        }
    }

    public void ShowMenu()
    {
        switch (currentState)
        {
            case GameStatus.gameover:
                audioSource.PlayOneShot(SoundManager.Instance.Gameover);
                playButtonLbl.text = "Play Again!";
                startImage.gameObject.SetActive(false);
                winImage.gameObject.SetActive(false);
                gameOverImage.gameObject.SetActive(true);
                break;
            case GameStatus.next:
                startImage.gameObject.SetActive(true);
                winImage.gameObject.SetActive(false);
                gameOverImage.gameObject.SetActive(false);
                playButtonLbl.text = "Next Wave";
                break;
            case GameStatus.play:
                startImage.gameObject.SetActive(true);
                winImage.gameObject.SetActive(false);
                gameOverImage.gameObject.SetActive(false);
                playButtonLbl.text = "Play";
                break;
            case GameStatus.win:
                playButtonLbl.text = "Play Again!";
                startImage.gameObject.SetActive(false);
                winImage.gameObject.SetActive(true);
                gameOverImage.gameObject.SetActive(false);
                break;
        }

        playButton.gameObject.SetActive(true);
    }

    public void PlayButtonPressed()
    {
        switch (currentState)
        {
            case GameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                TotalMoney = 10;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagsBuildSites();
                totalMoneyLbl.text = TotalMoney.ToString();
                totalEscapedLbl.text = "Escaped: " + TotalEscaped + " / 10";
                audioSource.PlayOneShot(SoundManager.Instance.NewGame);
                break;
        }
        DestroyAllEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWaveLbl.text = "Wave: " + (waveNumber + 1);
        StartCoroutine(SpawnEnemies());
        playButton.gameObject.SetActive(false);
        startImage.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
    }

    private void HandleEscape()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.towerButtonPressed = null;
        }
    }
}
