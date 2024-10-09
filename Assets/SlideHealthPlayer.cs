using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideHealthPlayer : MonoBehaviour
{
    [SerializeField] StatsMobile _stats;
    [SerializeField] Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = _stats.maxHealth;
        _stats.health = _stats.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _stats.health;
    }
}
