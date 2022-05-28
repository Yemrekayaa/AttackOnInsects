using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float health;
    private float maxhealth = 100;
    private float healthRegen = 0.1f;
    private float mSpeed = 3f;
    private int exp;
    private int maxExp;
    private int level;
    private float energy;
    private float maxEnergy = 100;
    private float energyRegen = 5f;
    private float attackCost = 10;
    private float attackSpeed = 50;
    private float attackRange = 2;
    private int damageMax = 30;
    private int damageMin = 20;
    private int luck = 1;
    private int extraAttack = 1;
    private float lifeSteal = 0f;
    private float energySteal = 0f;
    private float damageReduction = 0f;
    public GameObject levelUp;
    public GameObject endGame;
    private GameObject mainGUI;
    public int totalKill;
    public AudioClip[] audioList;
    private bool isEnergyRegen;
    private bool isHealthRegen;

    private int score;
    private void Start()
    {
        mainGUI = GameObject.Find("MainGUI");
        energy = maxEnergy;
        health = maxhealth;
        exp = 0;
        level = 1;
        maxExp = 100 * level;
        luck = 1;
        GameObject.Find("AttackObject").GetComponent<Animator>().speed = AttackSpeed / 60;
        mainGUI.GetComponent<GUI>().UpdateHealthBar();
        mainGUI.GetComponent<GUI>().UpdateExpBar();
    }

    private void Update()
    {
        if (totalKill == GetComponent<Spawn>().maxEnemies)
        {
            EndGame();
        }
    }


    public int Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;
            if (Exp >= MaxExp)
            {
                StartCoroutine(LevelUp());
            }
            mainGUI.GetComponent<GUI>().UpdateExpBar();

        }
    }

    public int MaxExp
    {
        get
        {
            return maxExp;
        }
        set
        {
            maxExp = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            if (level < value)
            {
                Exp = Exp - maxExp;
                MaxExp = 100 * level;
                mainGUI.GetComponent<GUI>().UpdateExpBar();
            }

            level = value;
        }
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (health > value)
            {
                StartCoroutine(effectDamage());
                GetComponent<AudioSource>().clip = audioList[1];
                GetComponent<AudioSource>().Play();
            }
            health = value;



            if (health > maxhealth)
                health = maxhealth;


            if (health < MaxHealth && isHealthRegen == false)
            {
                StartCoroutine(healthSpeedR());
            }
            if (health <= 0)
            {
                EndGame();

            }
            mainGUI.GetComponent<GUI>().UpdateHealthBar();
        }

    }

    public void EndGame()
    {
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemies);
        }


        endGame.SetActive(true);
        GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        endGame.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        StopCoroutine(GetComponent<Spawn>().SpawnEnemy());
        Time.timeScale = 0;
    }

    public float MaxHealth
    {
        get
        {
            return maxhealth;
        }
        set
        {
            Health += value - maxhealth;
            maxhealth = value;
            mainGUI.GetComponent<GUI>().UpdateHealthBar();

        }
    }

    public float HealthRegen
    {
        get
        {
            return healthRegen;
        }
        set
        {
            healthRegen = value;
        }
    }
    public float Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            mainGUI.GetComponent<GUI>().UpdateEnergyBar();
            if (energy > MaxEnergy)
                energy = MaxEnergy;
            if (energy < MaxEnergy && isEnergyRegen == false)
            {
                StartCoroutine(energySpeedR());
            }


        }
    }

    public float MaxEnergy
    {
        get
        {
            return maxEnergy;
        }
        set
        {
            Energy += value - maxEnergy;
            maxEnergy = value;

        }
    }
    public float EnergyRegen
    {
        get
        {
            return energyRegen;
        }
        set
        {
            energyRegen = value;
        }
    }

    public float AttackCost
    {
        get
        {
            return attackCost;
        }
        set
        {
            attackCost = value;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
        set
        {
            attackSpeed = value;
            GameObject.Find("AttackObject").GetComponent<Animator>().speed = value / 60;
        }
    }

    public float AttackRange
    {
        get
        {
            return attackRange;
        }
        set
        {
            attackRange = value;
        }
    }

    public int DamageMin
    {
        get
        {
            return damageMin;
        }
        set
        {
            damageMin = value;
            if (DamageMin >= DamageMax)
                DamageMin = DamageMax - 2;
        }
    }

    public int DamageMax
    {
        get
        {
            return damageMax;
        }
        set
        {
            damageMax = value;
        }
    }

    public float MSpeed
    {
        get
        {
            return mSpeed;
        }
        set
        {

            mSpeed = value;
        }
    }

    public int Luck
    {
        get
        {
            return luck;
        }
        set
        {

            luck = value >= 20 ? 20 : value;
        }

    }

    public int ExtraAttack
    {
        get
        {
            return extraAttack;
        }
        set
        {
            extraAttack = value >= 5 ? 5 : value;
        }

    }

    public float LifeSteal
    {
        get
        {
            return lifeSteal;
        }
        set
        {
            lifeSteal = value >= 0.25f ? 0.25f : value;
        }

    }

    public float EnergySteal
    {
        get
        {
            return energySteal;
        }
        set
        {
            energySteal = value >= 0.25f ? 0.25f : value;
        }
    }
    public float DamageReduction
    {
        get
        {
            return damageReduction;
        }
        set
        {
            damageReduction = value >= 0.25f ? 0.25f : value;
        }

    }

    public int Score { get => score; set => score = value; }

    IEnumerator LevelUp()
    {
        Level += 1;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        levelUp.SetActive(true);
        levelUp.transform.GetChild(1).GetComponentInChildren<Text>().text = "Artık " + level + " seviyesiniz!";
    }
    IEnumerator effectDamage()
    {

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<AudioSource>().clip = audioList[0];
    }

    IEnumerator upEffect(Text gui)
    {
        gui.GetComponentInParent<Image>().color = Color.green;
        yield return new WaitForSeconds(0.6f);
        gui.GetComponentInParent<Image>().color = Color.white;
    }

    IEnumerator energySpeedR()
    {
        isEnergyRegen = true;
        while (isEnergyRegen)
        {

            energy += Time.deltaTime * energyRegen;
            yield return new WaitForSeconds(Time.deltaTime);
            mainGUI.GetComponent<GUI>().UpdateEnergyBar();

            if (Energy >= MaxEnergy)
                break;

        }
        isEnergyRegen = false;
    }

    IEnumerator healthSpeedR()
    {
        isHealthRegen = true;
        while (isHealthRegen)
        {
            health += Time.deltaTime * healthRegen;
            yield return new WaitForSeconds(Time.deltaTime);
            mainGUI.GetComponent<GUI>().UpdateHealthBar();

            if (health >= MaxHealth)
                break;

        }
        isHealthRegen = false;
    }
}
