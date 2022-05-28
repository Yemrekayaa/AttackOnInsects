using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Drops : MonoBehaviour
{
    public int addHeal = 0;
    public int addMaxHeal = 0;
    public float addHealthRegen = 0;
    public int addAS = 0;
    public float addAR = 0;
    public int addDamage = 0;
    public float addLS = 0;
    public float addES = 0;
    public int addEA = 0;
    public float addDR = 0;
    public int addLuck = 0;
    public float addMS = 0;
    public float addEnergy = 0;
    public float addMaxEnergy = 0;
    public float addEnergyRegen = 0;

    public string itemName;
    public string desc;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (addHeal != 0)
                other.GetComponent<Player>().Health += (Random.Range(addHeal - 10, addHeal));
            if (addMaxHeal != 0)
                other.GetComponent<Player>().MaxHealth += (Random.Range(addMaxHeal - 10, addMaxHeal));
            if (addHealthRegen != 0)
                other.GetComponent<Player>().HealthRegen += addHealthRegen;
            if (addAS != 0)
                other.GetComponent<Player>().AttackSpeed += (Random.Range(addAS - 10, addAS));
            if (addAR != 0)
                other.GetComponent<Player>().AttackRange += (Random.Range(0, addAR));
            if (addDamage != 0)
            {
                other.GetComponent<Player>().DamageMax += (Random.Range(addDamage - 5, addDamage));
                other.GetComponent<Player>().DamageMin += (Random.Range(addDamage - 5, addDamage));
            }
            if (addLuck != 0)
                other.GetComponent<Player>().Luck += (addLuck);
            if (addLS != 0)
                other.GetComponent<Player>().LifeSteal += (addLS);
            if (addES != 0)
                other.GetComponent<Player>().EnergySteal += (addES);
            if (addMS != 0)
                other.GetComponent<Player>().MSpeed += (addMS);
            if (addEA != 0)
                other.GetComponent<Player>().ExtraAttack += (addEA);
            if (addDR != 0)
                other.GetComponent<Player>().DamageReduction += (addDR);
            if (addEnergy != 0)
                other.GetComponent<Player>().Energy += addEnergy;
            if (addMaxEnergy != 0)
                other.GetComponent<Player>().MaxEnergy += addMaxEnergy;

            if (addEnergyRegen != 0)
                other.GetComponent<Player>().EnergyRegen += addEnergyRegen;


            other.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }


}

