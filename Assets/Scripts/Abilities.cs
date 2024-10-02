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

    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 posUp;
    public float maxAbility2Distance;


    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public KeyCode ability3;
    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 1;
        abilityImage2.fillAmount = 1;
        abilityImage3.fillAmount = 1;
        
        skillShot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ability 1 Inputs
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        //Ability 2 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            posUp = new Vector3(hit.point.x, 10f, hit.point.z);
            position = hit.point;
        }

        //Ability 1 Canvas
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
        ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);

        //Ability 2 Canvas
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility2Distance);

        var newHitPos = transform.position + hitPosDir * distance;  
        ability2Canvas.transform.position = newHitPos;

    }

    private void Ability1()
    {
        if(Input.GetKeyDown(ability1) && isCooldown1 == false) {
            
            skillShot.GetComponent<Image>().enabled = true;
            // disable other UI
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
            
            
        }

        if(skillShot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0)) {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }

        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1/ cooldown1 * Time.deltaTime;
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
            indicatorRangeCircle.GetComponent <Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled= true;
            //disable
            skillShot.GetComponent <Image>().enabled = false;

          
        }

        if(targetCircle.GetComponent<Image>().enabled==true && Input.GetMouseButton(0))
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;    

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }
    }

    private void Ability3()
    {
        if (Input.GetKeyDown(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 1;
                isCooldown3 = false;
            }
        }
    }

}
