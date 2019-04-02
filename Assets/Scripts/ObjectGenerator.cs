using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField]private float spawnTime;

    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private bool bossGenerator = false;

    private Vector3 spawnPosition;


    private void OnEnable()
    {
        if (bossGenerator)
            GameUIController.Instance.TimeUntilBoss = spawnTime;
        StartCoroutine(SpawnDelay());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }
    }
    private void Spawn()
    {
        if(bossGenerator)
            spawnPosition = new Vector3(0, 1.25f * World.Instance.TopBorder, 0);
        else
            spawnPosition = new Vector3(Random.Range(0.8f*World.Instance.LeftBorder, 0.8f*World.Instance.RightBorder), 1.25f*World.Instance.TopBorder, 0);

        Instantiate(prefabs[Random.Range(0,prefabs.Length)], spawnPosition, transform.rotation);
    }
}
