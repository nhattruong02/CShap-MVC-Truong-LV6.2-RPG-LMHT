using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider playerSlider;
    private Slider _sliderUI;
    [SerializeField] int _health;
    // Start is called before the first frame update
    void Start()
    {
        _sliderUI = GetComponent<Slider>();
        playerSlider.maxValue = _health;
        _sliderUI.maxValue = _health;
    }

    // Update is called once per frame
    void Update()
    {
        _sliderUI.value = _health;
        playerSlider.value = _sliderUI.value;
    }
}
