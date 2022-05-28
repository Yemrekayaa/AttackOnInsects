using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public int maxEnemies = 100;
    public GameObject[] enemies;
    //private Text time;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        //time = GameObject.Find("Time").GetComponent<Text>();

    }

    void Update()
    {

    }
    public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            Vector3 randomPos = new Vector3(gameObject.transform.position.x + (Random.Range(0, 2) * 2 - 1) * 10, gameObject.transform.position.y + (Random.Range(0, 2) * 2 - 1) * 10, 0);
            Instantiate(enemies[Random.Range(0, enemies.Length)], randomPos, Quaternion.identity);
            if(i % 10 == 0 && i != 0){
                getBoss();
            }
        } 
    }

    void getBoss()
    {
        Vector3 randomPos = new Vector3(gameObject.transform.position.x + (Random.Range(0, 2) * 2 - 1) * 10, gameObject.transform.position.y + (Random.Range(0, 2) * 2 - 1) * 10, 0);
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], randomPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().damage *= 2;
        enemy.GetComponent<Enemy>().AS *= 2;
        enemy.GetComponent<Enemy>().maxhealth *= 5;
        enemy.GetComponent<Enemy>().maxExp *= 6;
        enemy.GetComponent<Enemy>().minExp *= 6;
        enemy.transform.localScale *= 2f;
        enemy.GetComponent<Enemy>().score *=5;
        
    }
}
