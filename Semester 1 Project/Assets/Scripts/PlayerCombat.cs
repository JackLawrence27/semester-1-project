﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public PlayerScript pS; 
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        //Checking if the time since the last attack has been long enough to allow the player to attack again
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //If the player is on the ground and not crouching it can continue with the attack routine
        if (pS.isGrounded == true && pS.crouchPressed == false)
        {
            //Play Attack animation
            animator.SetTrigger("Attack");
            //Deal Damage
            StartCoroutine(Damage());
        }
    }
     IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManagerScript.PlaySound("attack_snd");
        yield return new WaitForSeconds(0.2f);
        

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        //Show the attack range in the unity editor
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}
