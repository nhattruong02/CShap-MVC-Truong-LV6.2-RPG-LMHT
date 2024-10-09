using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode ability1;

    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillShot;
    public Transform player;

    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode ability2;

    public Image indicatorRangeCircle;


    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public KeyCode ability3;

    private PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 1;
        abilityImage2.fillAmount = 1;
        abilityImage3.fillAmount = 1;

        playerCombat = gameObject.GetComponent<PlayerCombat>();
        skillShot.GetComponent<Image>().enabled = false;
        /*        targetCircle.GetComponent<Image>().enabled = false;
        */
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCombat.isPlayerAlive)
        {
            Ability1();
            Ability2();
            Ability3();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ability 1 Inputs
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Ability 1 Canvas
            Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
            transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
            ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);

        }
    }

    private void Ability1()
    {
        if (Input.GetKeyUp(ability1))
        {
            indicatorRangeCircle.enabled = false;
        }
        if (Input.GetKeyDown(ability1) && isCooldown1 == false)
        {

            indicatorRangeCircle.enabled = true;
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.Ranged;
            if (playerCombat.targetedEnemy != null &&
            Vector3.Distance(playerCombat.transform.position, playerCombat.targetedEnemy.transform.position) <= playerCombat.attackRange)
            {
                indicatorRangeCircle.enabled = false;
                isCooldown1 = true;
                abilityImage1.fillAmount = 1;
                StartCoroutine(playerCombat.RangedAttackInterval());
            }
        }
        if (isCooldown1)
        {
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.NormalAttack;
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            skillShot.GetComponent<Image>().enabled = false;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 1;
                isCooldown1 = false;
            }
        }
    }

    private void Ability2()
    {
        if (Input.GetKeyDown(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.Heath;
            StartCoroutine(playerCombat.HealthInterval());

        }

        if (isCooldown2)
        {
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.NormalAttack;
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }
    }

    private void Ability3()
    {
        if (Input.GetKeyUp(ability3))
        {
            indicatorRangeCircle.enabled = false;
        }
        if (Input.GetKeyDown(ability3) && isCooldown3 == false)
        {
            indicatorRangeCircle.enabled = true;
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.Mage;
            if (playerCombat.targetedEnemy != null &&
            Vector3.Distance(playerCombat.transform.position, playerCombat.targetedEnemy.transform.position) <= playerCombat.attackRange)
            {
                abilityImage3.fillAmount = 1;
                isCooldown3 = true;
                indicatorRangeCircle.enabled = false;
                StartCoroutine(playerCombat.MageAttackInterval());
            }

        }
        if (isCooldown3)
        {
            playerCombat.heroAttackType = PlayerCombat.PlayerAttackType.NormalAttack;
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 1;
                isCooldown3 = false;
            }
        }
    }

}
