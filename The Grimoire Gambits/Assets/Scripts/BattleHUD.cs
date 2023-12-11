using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        
        // Set the initial slider value to the maximum HP
        hpSlider.value = unit.maxHP;
    }

    // Update the HP slider based on the current HP

    public void SetHP(int currentHP, int maxHP)
{
    // Set the initial slider value to the current HP
    hpSlider.value = currentHP;

    // Ensure that the max value of the slider is updated
    hpSlider.maxValue = maxHP;
}

}
