using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{

    [SerializeField] private RectTransform[] transformButtons;
    [SerializeField] private Transform UFOTransform;
    [SerializeField] private float animationTime;
    [SerializeField] private SoundObject SoundObjectPrefab;
    [SerializeField] private AudioClip sound;
    private SoundObject soundObject;

    private List<float> startPositions = new List<float>();

    private void Start()
    {
        foreach (var transformButton in transformButtons)
        {
            startPositions.Add(transformButton.anchoredPosition.y);
            transformButton.DOAnchorPosY(50f, animationTime).OnComplete(() => { transformButton.DOAnchorPosY(0, animationTime / 2); });
        }

        UFOTransform.DOMoveX(0, animationTime).OnComplete(() => { UFOTransform.DORotate(new Vector3(80, 0, 0), animationTime / 2); });
    }

    public void OnPlayClick()
    {
        soundObject = Instantiate(SoundObjectPrefab);
        soundObject.Play(sound);
        StartCoroutine(WaitUntilStop());
    }


    public void OnExitClick()
    {
        soundObject = Instantiate(SoundObjectPrefab);
        soundObject.Play(sound);
        Application.Quit();
    }

 

    private IEnumerator WaitUntilStop()
    {
        yield return new WaitUntil(() =>!DOTween.IsTweening(UFOTransform));
        
        UFOTransform.DOMoveX(2f * World.Instance.RightBorder, animationTime);
        UFOTransform.DORotate(new Vector3(70, -70, 0), animationTime / 2);
        for(int i = 0;i<transformButtons.Length;i++)
        {
            transformButtons[i].DOAnchorPosY(startPositions[i], animationTime/2);
        }
        yield return new WaitUntil(() =>!DOTween.IsTweening(UFOTransform) && !DOTween.IsTweening(transformButtons[transformButtons.Length-1]));
        SceneManager.LoadScene("EndlessGame");
    }

}
