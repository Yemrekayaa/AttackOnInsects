using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public int minExp;
    public int maxExp;
    public int score = 10;
    public static int count;

    public float damage = 10;
    public float AS = 0.5f;
    private GameObject PlayerObject;
    public GameObject[] items;
    void Start()
    {
        health = maxhealth;

        PlayerObject = GameObject.Find("CharacterPlayer");
        count++;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Death();        
        StartCoroutine(Damaged());
    }

    public void Death()
    {
        GetComponent<AudioSource>().Play();
        PlayerObject.GetComponent<Player>().Exp += (Random.Range(minExp, maxExp));
        Drop();
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().speed = 0;
        }
        StartCoroutine(animDeath());
        PlayerObject.GetComponent<Player>().totalKill++;
        PlayerObject.GetComponent<Player>().Score += score;
    }

    IEnumerator animDeath()
    {
        for (float t = 0f; t < 1.5f; t += Time.deltaTime)
        {
            GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }

    private void Drop()
    {
        int dropRate = Random.Range(0, 100) + PlayerObject.GetComponent<Player>().Luck;
        if (dropRate >= 40)
            Instantiate(items[Random.Range(0, items.Length)], gameObject.transform.position, Quaternion.identity);
    }

    IEnumerator Damaged()
    {

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    float zaman = 0;
    private void OnCollisionEnter2D(Collision2D other)
    {
        zaman = 0;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Health -= damage - (damage * PlayerObject.GetComponent<Player>().DamageReduction);

        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            zaman += Time.deltaTime;
            if (zaman >= AS)
            {
                PlayerObject.GetComponent<Player>().Health -= damage - (damage * PlayerObject.GetComponent<Player>().DamageReduction);
                zaman = 0;
            }
        }
    }


}
