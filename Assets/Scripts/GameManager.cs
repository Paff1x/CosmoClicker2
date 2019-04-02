using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private ObjectGenerator bossGenerator;
    [SerializeField] private ObjectGenerator []normalGenerators;
    [SerializeField] private SoundObject soundObjectPrefab;

    [SerializeField] private CanvasGroup losePanel;
    [SerializeField] private CanvasGroup gamePanel;


    private int gameStage = 0;
    private int currentScore;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameUIController.Instance.ActivatePanel(gamePanel, true);
        CreatePlayer();
        NextStage();
    }   

    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(0, 1.65f * World.Instance.BotBorder, 0);
        GameObject player=Instantiate(playerPrefab, pos, Quaternion.identity);
        player.transform.DOMoveY(0.65f * World.Instance.BotBorder, 1f);
        var damagable = Player.Instance.GetComponent<Damagable>();
        damagable.DieEvent += OnPlayerDead;
    }

    private void ActivateGenerators(bool flag)
    {
        bossGenerator.gameObject.SetActive(flag);
        foreach (ObjectGenerator normalGenerator in normalGenerators)
            normalGenerator.gameObject.SetActive(flag);
    }

    public void BossStage(bool flag)
    {
        ActivateGenerators(!flag);
        GameUIController.Instance.ActivateBossHealthBar(flag);
        GameUIController.Instance.ActivateTimerUntilBoss(!flag);
    }
    public void AddScore(int value)
    {
        currentScore += value;
        GameUIController.Instance.AddScore(value, currentScore);
    }

    private void OnPlayerDead(Damagable damagable)
    {
        GameUIController.Instance.ActivatePanel(gamePanel, false);
        GameUIController.Instance.ActivatePanel(losePanel, true);
        GameUIController.Instance.ShowFinalScore(currentScore);
    }

    public SoundObject CreateSoundObject()
    {
        return Instantiate(soundObjectPrefab);
    }

    public void NextStage()
    {
          StartCoroutine(NextStageDelay());
    }

    IEnumerator NextStageDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            gameStage++;
            GameUIController.Instance.ShowStage(gameStage);
            BossStage(false);
            StopAllCoroutines();
        }
    }
}
