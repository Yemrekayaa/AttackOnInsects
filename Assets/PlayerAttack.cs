using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    Vector3 attackDirection;
    private GameObject attackObject;
    public bool attacking;
    //public int extraAttack = 1;
    private Player playerComponent;
    private void Start()
    {
        attacking = false;
        playerComponent = GetComponent<Player>();
        attackObject = GameObject.Find("AttackObject");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (attacking == false && playerComponent.Energy >= playerComponent.AttackCost)
            {
                StartCoroutine(AttackDelay(playerComponent.ExtraAttack));

            }
        }
    }

    void Attack()
    {
        attackObject.GetComponent<AudioSource>().Play();
        attacking = true;
        playerComponent.Energy -= playerComponent.AttackCost;
        Vector2 attackSize = Vector2.zero;
        Vector3 attackDirection = gameObject.GetComponent<PlayerMove>().attackDirection;
        if (attackDirection == Vector3.down)
        {
            attackSize = new Vector2(1, playerComponent.AttackRange);
            attackDirection -= new Vector3(0, -0.5f + (playerComponent.AttackRange / 2), 0);
        }
        else if (attackDirection == Vector3.up)
        {
            attackSize = new Vector2(1, playerComponent.AttackRange);
            attackDirection += new Vector3(0, (playerComponent.AttackRange / 2) - 1, 0);
        }
        else if (attackDirection == Vector3.left)
        {
            attackSize = new Vector2(playerComponent.AttackRange, 1);
            attackDirection -= new Vector3(-0.75f + playerComponent.AttackRange / 2, 0, 0);

        }
        else if (attackDirection == Vector3.right)
        {
            attackSize = new Vector2(playerComponent.AttackRange, 1);
            attackDirection += new Vector3(-0.75f + playerComponent.AttackRange / 2, 0, 0);
        }


        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position + attackDirection, attackSize, 0);
        attackObject.transform.position = attackPoint.position + attackDirection;
        attackObject.transform.localScale = attackSize / 2;
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                int dealDamage = Random.Range(playerComponent.DamageMin, playerComponent.DamageMax);
                enemy.GetComponent<Enemy>().DealDamage(dealDamage);
                playerComponent.Health += dealDamage * playerComponent.LifeSteal;
                playerComponent.Energy += dealDamage * playerComponent.EnergySteal;
            }
        }
    }



    IEnumerator AttackDelay(int attackNumber)
    {

        StartCoroutine(GameObject.FindGameObjectWithTag("MainGUI").GetComponent<GUI>().AttackCD());
        attackObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        for (int i = 0; i < attackNumber; i++)
        {
            if (playerComponent.Energy >= playerComponent.AttackCost)
            {
                if (i != 0)
                {
                    attackObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0.2f, 0.2f, 0);
                }
                attackObject.GetComponent<Animator>().SetTrigger("Attack");
                Attack();

                yield return new WaitForSeconds(((60 / playerComponent.AttackSpeed) / attackNumber));
            }
        }

        attacking = false;
    }




}
