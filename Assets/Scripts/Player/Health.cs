using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider playerSlider;
    private Slider _sliderUI;
    [SerializeField] int _health;
    Stats _stats;
    // Start is called before the first frame update
    void Start()
    {
        _stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        _sliderUI = GetComponent<Slider>();
        playerSlider.maxValue = _stats.maxHealth;
        _sliderUI.maxValue = _stats.maxHealth;
        _stats.health = _stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _sliderUI.value = _stats.health;
        playerSlider.value = _sliderUI.value;
    }
}
