using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foreground;
    [SerializeField] private Image midleground;
    [SerializeField] private Image background;
    [SerializeField] private bool boss = false;

    private Damagable damagable;

    private void OnEnable()
    {
        if (boss && Boss.Instance)
        {
            damagable = Boss.Instance.GetComponent<Damagable>();
            OnDamage(damagable);

        }
        else
        {
            damagable = GetComponentInParent<Damagable>();
        }
        if (damagable != null)
        {
            damagable.DamageEvent += OnDamage;
        }
    }

    private void OnDamage(Damagable obj)
    {
        midleground.DOFillAmount(obj.Health / obj.HealthMax, 0.5f);
        foreground.fillAmount = obj.Health / obj.HealthMax;
    }

}
