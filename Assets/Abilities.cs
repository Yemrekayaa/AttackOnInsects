using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private int selectedClass = 0;
    void Start()
    {
        Debug.Log("selectedClass " + GameData.SelectedClass);
        selectedClass = GameData.SelectedClass;
        switch (selectedClass)
        {
            case 0:

                break;
            case 1:
                Knight();
                break;
            case 2:
                Rogue();
                break;
            case 3:
                Vampire();
                break;
            case 4:
                Berserker();
                break;
            case 5:
                Duelist();
                break;
            default:
                break;
        }
    }


    public void Knight()
    {
        GetComponent<Player>().DamageReduction += 0.15f;
        GetComponent<Player>().HealthRegen += 0.15f;
        GetComponent<Player>().AttackRange += 1;
        GetComponent<Player>().DamageMax += 10;
        GetComponent<Player>().DamageMin += 10;
        StartCoroutine(setHealth(25));

        GetComponent<Player>().MSpeed -= 0.5f;
        GetComponent<Player>().AttackCost += 2;
        GetComponent<Player>().AttackSpeed -= 20;
    }

    public void Rogue()
    {
        GetComponent<Player>().AttackSpeed += 20;
        GetComponent<Player>().ExtraAttack += 1;
        GetComponent<Player>().MSpeed += 0.5f;
        GetComponent<Player>().Luck += 5;

        GetComponent<Player>().AttackRange -= 0.75f;
        GetComponent<Player>().DamageMax -= 10;
        GetComponent<Player>().DamageMin -= 10;
    }

    public void Duelist()
    {
        GetComponent<Player>().AttackSpeed += 20;
        GetComponent<Player>().ExtraAttack += 1;
        GetComponent<Player>().AttackCost -= 1.5f;
        GetComponent<Player>().EnergyRegen += 2f;

        GetComponent<Player>().AttackRange -= 0.30f;
        StartCoroutine(setHealth(-25));
    }


    public void Vampire()
    {
        GetComponent<Player>().HealthRegen += 0.15f;
        GetComponent<Player>().LifeSteal += 0.15f;
        GetComponent<Player>().MSpeed += 0.3f;
        GetComponent<Player>().EnergySteal += 0.10f;

        GetComponent<Player>().DamageMax -= 15;
        GetComponent<Player>().DamageMin -= 15;
        GetComponent<Player>().AttackCost += 0.5f;
    }

    public void Berserker()
    {
        GetComponent<Player>().DamageMax += 50;
        GetComponent<Player>().DamageMin += 50;
        GetComponent<Player>().AttackCost -= 1;
        GetComponent<Player>().HealthRegen += 0.20f;
        StartCoroutine(setHealth(20));

        GetComponent<Player>().DamageReduction -= 0.2f;
    }


    IEnumerator setHealth(float value)
    {
        yield return new WaitForSeconds(0.01f);
        GetComponent<Player>().MaxHealth += value;
    }
}
