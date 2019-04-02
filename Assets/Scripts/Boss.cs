using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    private Damagable _damagable;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private AudioClip sound;
    private SoundObject _soundObject;

    public static Boss Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _damagable = GetComponent<Damagable>();

        GameManager.Instance.BossStage(true);
    }
    private void Start()
    {
        _damagable.HealthChangeEvent += _damagable.OnDamage;
        _damagable.HealthChangeEvent += OnDamage;
        _damagable.DieEvent += OnDead;
        transform.DOMoveY(0.7f * World.Instance.TopBorder, 2f);
    }
    private void OnDead(Damagable damagable)
    {
        _damagable.HealthChangeEvent -= _damagable.OnDamage;
        _damagable.DieEvent -= OnDead;
        _damagable.HealthChangeEvent -= OnDamage;
        Instantiate(collectablePrefab, transform.position, transform.rotation);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        _soundObject = GameManager.Instance.CreateSoundObject();
        _soundObject.Play(sound);
        GameManager.Instance.NextStage();
    }

    private void OnDamage()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        Instantiate(damagePrefab, position, Quaternion.identity);
    }

}
