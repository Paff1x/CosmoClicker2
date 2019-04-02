using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text addScoreText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private CanvasGroup bossHealthBar;
    [SerializeField] private float scoreAnimationTime;
    [SerializeField] private Text timeUntilBossText;
    [SerializeField] private CanvasGroup timerUntilBoss;
    [SerializeField] private AudioClip sound;
    private SoundObject soundObject;

    internal float TimeUntilBoss;

    private bool bossStageIsActive;

    public static GameUIController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void OnRestartClick()
    {
        soundObject = GameManager.Instance.CreateSoundObject();
        soundObject.Play(sound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OnBackToMenuClick()
    {
        soundObject = GameManager.Instance.CreateSoundObject();
        soundObject.Play(sound);
        SceneManager.LoadScene("Menu");
    }

    private void FixedUpdate()
    {
        if (bossStageIsActive)
        {
            TimeUntilBoss -= Time.fixedDeltaTime;
            timeUntilBossText.text = string.Format("{0:00.00}", TimeUntilBoss);
        }
    }

    public void AddScore(int value, int curentScore)
    {
        scoreText.gameObject.SetActive(true);
        addScoreText.DOKill(true);
        scoreText.DOKill(true);
        addScoreText.gameObject.SetActive(true);

        SetTxt(value, addScoreText, "+");
        addScoreText.DOFade(1f, scoreAnimationTime / 4f).OnComplete(() =>
        {
            var animTime = scoreAnimationTime / 2f;
            float deltaTime = 0f;
            addScoreText.DOFade(1f, animTime).OnUpdate(() =>
            {
                deltaTime += Time.deltaTime;
                var newTime = (int)(value * (1 - deltaTime / animTime));

                SetTxt(newTime, addScoreText, "+");
                SetTxt(curentScore - value + (value - newTime), scoreText);
            }).OnComplete(() =>
            {
                SetTxt(0, addScoreText, "+");
                SetTxt(curentScore, scoreText);
                addScoreText.DOFade(0f, scoreAnimationTime / 4f);
            });
        });
    }
    private void SetTxt(int value, Text txt, string prefix = "")
    {
        txt.text = string.Format("{0}{1}", prefix, value);
    }

    public void ActivateTimerUntilBoss(bool flag)
    {

        if (flag)
        {
            timeUntilBossText.gameObject.SetActive(flag);
            timerUntilBoss.DOFade(1f, 1f);
        }
        else
        {
            timerUntilBoss.DOFade(0f, 1f).OnComplete(() => { timeUntilBossText.gameObject.SetActive(flag); });
        }
        bossStageIsActive = flag;
    }
    public void ShowStage(int gameStage)
    {
        stageText.text = "Stage: " + gameStage;
        stageText.DOFade(0.5f, 1f).OnComplete(() => { stageText.DOFade(0f, 1f); }).Play();
    }

    public void ActivateBossHealthBar(bool flag)
    {
        if (flag)
        {
            bossHealthBar.gameObject.SetActive(flag);
            bossHealthBar.DOFade(1f, 1f);
        }
        else
        {
            bossHealthBar.DOFade(0f, 1f).OnComplete(() => { bossHealthBar.gameObject.SetActive(flag); });
        }
    }

    public void ActivatePanel(CanvasGroup panel, bool flag)
    {
        if (flag)
        {
            panel.gameObject.SetActive(flag);
            panel.DOFade(1f, 1f);
        }
        else
        {
           panel.DOFade(0f, 1f).OnComplete(() => { panel.gameObject.SetActive(flag); });
        }
    }
    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Score: " + score;
    }


}
