using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioClip sound;

    private SoundObject _soundObject;
    
    private void Start()
    {
        StartCoroutine(ShootDelay());
    }
    IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootTime);
            Shoot();
        }
    }
    private void Shoot()
    {
        if (transform.position.y < World.Instance.TopBorder && transform.position.x < World.Instance.RightBorder && transform.position.y > World.Instance.LeftBorder)
        {
            GameObject _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody _rigidbody = _bullet.GetComponent<Rigidbody>();
            _rigidbody.AddRelativeForce(new Vector3(0, 0, bulletSpeed));
            _rigidbody.AddRelativeTorque(new Vector3(0, 1, 0));
            _soundObject = GameManager.Instance.CreateSoundObject();
            _soundObject.Play(sound);
        }
    }
    private void Update()
    {
        if(Player.Instance)
            transform.LookAt(Player.Instance.transform);
    }
}

