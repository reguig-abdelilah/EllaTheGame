using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsGenerator : MonoBehaviour {
    public float[] itemsSpawningTimer;
    public GameObject[] items;
    public GameObject[] itemsSpawnPositions;
    float minimumSpawnDistance = 10;
    float lastXPos;
	// Use this for initialization
	void Awake () {
        GameManager.GameStartEvent += GameStartEventExecuted;  
    }

    private void GameStartEventExecuted()
    {
        if (items != null)
        {
            Debug.Log("not null");
            for (int i = 0; i < items.Length; i++)
            {
                StartCoroutine(SpawnItems(i));
            }
        }
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update () {
    }
    IEnumerator SpawnItems(int index)
    {
        while (GameManager.Instance.gameState == GameManager.GameState.GameRunning)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(itemsSpawningTimer[index]/2, itemsSpawningTimer[index]));
            Debug.Log(transform.position.x - lastXPos);
            if(transform.position.x - lastXPos >= minimumSpawnDistance)
            {
                GameObject tmp = Instantiate(items[index], itemsSpawnPositions[index].transform.position, itemsSpawnPositions[index].transform.rotation);
                Destroy(tmp, 30);
                lastXPos = transform.position.x;
            }
        }
    }
    void OnDestroy()
    {
        GameManager.GameStartEvent -= GameStartEventExecuted;
    }
}
