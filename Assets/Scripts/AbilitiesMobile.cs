using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesMobile : MonoBehaviour
{
    public Image indicatorRangeCircle;
    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;



    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;



    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;

    private PlayerCombatMobile playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 1;
        abilityImage2.fillAmount = 1;
        abilityImage3.fillAmount = 1;

        playerCombat = gameObject.GetComponent<PlayerCombatMobile>();
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

    }
    private void Update()
    {
        if (isCooldown1)
        {
            playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.NormalAttack;
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 1;
                isCooldown1 = false;
            }
        }
        if (isCooldown2)
        {
            playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.NormalAttack;
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }

        if (isCooldown3)
        {
            playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.NormalAttack;
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 1;
                isCooldown3 = false;
            }
        }
    }

    public void Ability1()
    {
        if (playerCombat.isPlayerAlive)
        {
            indicatorRangeCircle.enabled = false;
            if (isCooldown1 == false)
            {
                indicatorRangeCircle.enabled = true;
                playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.Ranged;
                if (playerCombat.targetedEnemy != null &&
                Vector3.Distance(playerCombat.transform.position, playerCombat.targetedEnemy.transform.position) <= playerCombat.attackRange)
                {
                    indicatorRangeCircle.enabled = false;
                    isCooldown1 = true;
                    abilityImage1.fillAmount = 1;
                    StartCoroutine(playerCombat.RangedAttackInterval());
                }
            }
        }
    }

    public void Ability2()
    {
        if (playerCombat.isPlayerAlive)
        {
            if (isCooldown2 == false)
            {
                isCooldown2 = true;
                abilityImage2.fillAmount = 1;
                playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.Heath;
                StartCoroutine(playerCombat.HealthInterval());
            }
        }
    }

    public void Ability3()
    {
        if (playerCombat.isPlayerAlive)
        {
            indicatorRangeCircle.enabled = false;
            if (isCooldown3 == false)
            {
                indicatorRangeCircle.enabled = true;
                playerCombat.heroAttackType = PlayerCombatMobile.PlayerAttackType.Mage;
                if (playerCombat.targetedEnemy != null &&
                Vector3.Distance(playerCombat.transform.position, playerCombat.targetedEnemy.transform.position) <= playerCombat.attackRange)
                {
                    abilityImage3.fillAmount = 1;
                    isCooldown3 = true;
                    indicatorRangeCircle.enabled = false;
                    StartCoroutine(playerCombat.MageAttackInterval());
                }

            }
        }
    }

}

