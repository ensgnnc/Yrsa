using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Enemy list
    public List<GameObject> enemies;

    // Spawn Areas
    public List<BoxCollider2D> boxCollider2D;

    /* 
     * 1) Get random enemy prefab from enemies list.  
     * 2) Wait for wave start.
     * 3) Get random coordinates around player or a place.
     * 4) Instantiniate them with effect.
     * 5) Check for wave end. Maybe when enemy died for optimisation.
     */

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) spawnEnemy();
    }

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

    public void spawnEnemy() {
        var enemy = GetRandomEnemy();
        Instantiate(enemy, GetRandomLocation(), enemy.transform.rotation);
    }
}
