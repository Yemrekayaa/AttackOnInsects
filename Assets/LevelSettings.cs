using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public GameObject playerObject;

    private GameObject[] enemies;
    void Start()
    {
        enemies = playerObject.GetComponent<Spawn>().enemies;
        switch (GameData.SelectedLevel)
        {
            case 0:

                break;
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            default:
                break;
        }
    }

    public void Level1()
    {
        playerObject.GetComponent<Spawn>().maxEnemies = 150;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().damage *= 1.5f;
            enemies[i].GetComponent<Enemy>().score += 5;
        }
    }

    public void Level2()
    {
        playerObject.GetComponent<Spawn>().maxEnemies = 200;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().damage *= 2f;
            enemies[i].GetComponent<Enemy>().score += 10;
            enemies[i].GetComponent<Enemy>().maxhealth *= 1.5f;
        }
    }

    public void Level3()
    {
        playerObject.GetComponent<Spawn>().maxEnemies = 300;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().damage *= 2f;
            enemies[i].GetComponent<Enemy>().score += 15;
            enemies[i].GetComponent<Enemy>().maxhealth *= 2f;
            enemies[i].GetComponent<Enemy>().AS += 0.25f;
        }
    }
    public void Level4()
    {
        playerObject.GetComponent<Spawn>().maxEnemies = 400;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().damage *= 2f;
            enemies[i].GetComponent<Enemy>().score += 20;
            enemies[i].GetComponent<Enemy>().maxhealth *= 2.5f;
            enemies[i].GetComponent<Enemy>().AS += 0.5f;
        }
    }

    public void Level5()
    {
        playerObject.GetComponent<Spawn>().maxEnemies = 500;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().damage *= 2f;
            enemies[i].GetComponent<Enemy>().score += 25;
            enemies[i].GetComponent<Enemy>().maxhealth *= 2.5f;
            enemies[i].GetComponent<Enemy>().AS += 1f;
            enemies[i].GetComponent<EnemyMove>().speed += 0.5f;
        }
    }
}
