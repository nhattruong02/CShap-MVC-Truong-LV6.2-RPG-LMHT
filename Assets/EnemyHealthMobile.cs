using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthMobile : MonoBehaviour
{
    [SerializeField] Slider _enemySlider;
    [SerializeField] StatsMobile _statsScript;
    // Start is called before the first frame update
    void Start()
    {
        _statsScript = GetComponent<StatsMobile>();
        _enemySlider.maxValue = _statsScript.maxHealth;
        _statsScript.health = _statsScript.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _enemySlider.value = _statsScript.health;
    }
}
