using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int waveEnemyCount;
    int activeEnemyCount;

    public bool isWaveRunning = false;


    public IdoenInteract idoenInteract;

    // Enemy list
    public List<GameObject> enemies;

    // Spawn Areas
    public List<BoxCollider2D> boxCollider2D;

    public QuestManager questManager;
    public TalkManager talkManager;

    List<string> complate = new List<string>()
    {
        "Well done!",
        "That was cool!",
        "Next time it will be more difficult!"
    };

    /* 
     * 1) Get random enemy prefab from enemies list.  
     * 2) Wait for wave start.
     * 3) Get random coordinates around player or a place.
     * 4) Instantiniate them with effect.
     * 5) Check for wave end. Maybe when enemy died for optimisation.
     */

    public Vector3 GetRandomLocation()
    {
        int randomColliderInt = Random.Range(0, boxCollider2D.Count);
        var randomCollider = boxCollider2D[randomColliderInt].bounds;

        return new Vector3(
            Random.Range(randomCollider.max.x, randomCollider.min.x),
            Random.Range(randomCollider.max.y, randomCollider.min.y),
            0);
    }

    public GameObject GetRandomEnemy()
    {
        int randomEnemyInt = Random.Range(0, enemies.Count);
        return enemies[randomEnemyInt];
    }

    public void spawnEnemy(int amount)
    {
        isWaveRunning = true;
        int i = 1;
        activeEnemyCount = amount;
        waveEnemyCount = amount;
        while (i <= amount)
        {
            var enemy = GetRandomEnemy();
            Instantiate(enemy, GetRandomLocation(), enemy.transform.rotation);
            i++;
        }

    }

    public void onEnemyDie()
    {
        --activeEnemyCount;
        if (activeEnemyCount == 0)
        {
            endWave();
        }
    }

    public void endWave()
    {
        if (questManager.questNumber == 1) questManager.nextQuest();
        talkManager.onInteractWithNPC(complate[UnityEngine.Random.Range(0, complate.Count)]);
        isWaveRunning = false;
    }

    public void startWave(int level)
    {
        spawnEnemy(4 * level);
    }
}
