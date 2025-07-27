using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PF_GameManager : MonoBehaviour
{
    public static PF_GameManager Instance { get; private set; }

    // 게임 관련 변수들
    private GameObject Player;
    [SerializeField] float timeLeft = 30f; // 30초 제한
    private int bonus = 3;
    private int gold = 0;
    private bool isGameOver = false;
    //private bool isGamePlay = false;
    private static int currentStage;
    [SerializeField] Text timerText;
    [SerializeField] GameObject goldUIPrefab;

    [SerializeField] GameObject ItemPanel;
    [SerializeField] GameObject bonusUIPrefab;    
    [SerializeField] GameObject BonusPanel;
    [SerializeField] GameObject gameOverPopup;

    private List<GameObject> bonusUIList = new List<GameObject>();

    private GameObject spawnPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //UpdateTimerUI();
        Initialized();
    }

    void Update()
    {
        if (!isGameOver && timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;

            if(timerText != null)
            {
                timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            }

            if (timeLeft <= 0f)
            {
                timeLeft = 0;
                OnTimeOver();
            }
        }
    }

    void OnTimeOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("시간 종료!");
            GameOver();
        }
    }

    public void Initialized()
    {
        timeLeft = 30f;
        isGameOver = false;
        Time.timeScale = 1;
        bonus = 3;
        gold = 0;
        if(gameOverPopup == true) gameOverPopup.SetActive(false);
        foreach (Transform child in ItemPanel.transform)
        {
            Destroy(child.gameObject);
        }

        DrawBonus();
    }

    public void GameStart()
    {
        Initialized();
    }


    public void GameOver()
    {
        gameOverPopup.SetActive(true);
        Time.timeScale = 0;
    }
    public void DrawBonus()
    {
        // 기존 UI 모두 파괴 및 리스트 비움
        foreach (var go in bonusUIList)
        {
            Destroy(go);
        }
        bonusUIList.Clear();

        // bonus 값이 1 이상일 때만 생성
        if (bonusUIPrefab != null && BonusPanel != null && bonus > 0)
        {
            for (int i = 0; i < bonus; i++)
            {
                GameObject newBonus = Instantiate(bonusUIPrefab, BonusPanel.transform);
                newBonus.SetActive(true);
                bonusUIList.Add(newBonus);
            }
            Debug.Log("보너스 UI " + bonus + "개로 재생성됨 (리스트 관리)");
        }
        else
        {
            Debug.LogWarning("보너스 UI 생성 실패: bonus=" + bonus + ", bonusUIPrefab=" + bonusUIPrefab + ", BonusPanel=" + BonusPanel);
        }
    }

    public void SpawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            Debug.Log("플레이어 리스폰 완료! 위치: " + spawnPoint.transform.position);
        }
        else
        {
            Debug.LogWarning("리스폰 실패! player 또는 respawnPoint가 null입니다.");
        }
    }

    public void DecreaseBonus()
    {
        if (bonus > 0)
        {
            bonus--;
            var go = bonusUIList[bonusUIList.Count - 1];
            Destroy(go);
            bonusUIList.RemoveAt(bonusUIList.Count - 1);
            Debug.Log("보너스 감소! 현재 보너스: " + bonus);
        }
        else
        {
            GameOver();
        }
    }

    public void IncreaseGold()
    {
        gold++;
        if (goldUIPrefab != null && ItemPanel != null)
        {
            GameObject newGold = Instantiate(goldUIPrefab, ItemPanel.transform);
            newGold.SetActive(true);
        }
        Debug.Log("골드 획득! 현재 골드: " + gold);
    }

    public void ResetStege()
    {
        currentStage = 1; // 타이틀에서 스테이지 1로 시작
    }
    public void NextStage()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        int currentStage = SceneManager.GetActiveScene().buildIndex;
        int nextStage = currentStage + 1;
        if (nextStage < totalSceneCount)
        {
            SceneManager.LoadScene(nextStage);
            Debug.Log("현재 스테이지: " + nextStage);
        }
        else
        {
            TitleScreen();
            Debug.Log("마지막 스테이지 - 타이틀로 이동!");
        }
    }
    public void TitleScreen()
    {
        ResetStege();
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
