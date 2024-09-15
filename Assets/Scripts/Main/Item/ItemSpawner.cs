using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefab;
    [SerializeField] private float spawnInterval = 10.0f;
    [SerializeField] private Transform parentObject;

    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    void Start()
    {
        StartCoroutine(ItemSpowner());
    }

    private void SetSpawnRandomPosition()
    {
        // ランダムな位置に生成
        UnityEngine.Vector3 spawnPosition = new UnityEngine.Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
        // 生成位置の高さを10に設定
        spawnPosition.y = 7.0f;
        SetSpawnPosition(spawnPosition);
    }

    private void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

    private Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    private GameObject GetRandomItemPrefab()
    {
        int index;
        index = Random.Range(0, itemPrefab.Length);

        return itemPrefab[index];
    }

    private IEnumerator ItemSpowner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SetSpawnRandomPosition();
            GameObject Item = Instantiate(GetRandomItemPrefab(), GetSpawnPosition(), Quaternion.identity, parentObject);

            Destroy(Item, 10.0f);
        }
    }
}
