using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    public Image energyBar;
    public GameObject energyBarUI;


    private void Update()
    {
        energyBar.fillAmount = GameData.energy / GameData.maxEnergy;
    }

    public void ToggleEnergyUI()
    {
        energyBarUI.SetActive(!energyBarUI.activeSelf);
    }
}
