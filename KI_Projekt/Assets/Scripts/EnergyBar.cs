using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {
    private string weapon = "Weapon";
    private string shield = "Shield";
    private string sensor = "Sensor";
    private string navigation = "Navigation";
    public Image weaponFill;
    public Image shieldFill;
    public Image sensorFill;
    public Image navigationFill;
    public void SetFillAmount(string energyType) {
        if (energyType == weapon) weaponFill.fillAmount = GameData.weaponEnergy / 100;

        if (energyType == shield) shieldFill.fillAmount = GameData.shieldEnergy / 100;

        if (energyType == sensor) sensorFill.fillAmount = GameData.sensorEnergy / 100;

        if (energyType == navigation) navigationFill.fillAmount = GameData.navigationEnergy / 100;
    }


    public void AddWeaponEnergy() {
        if (GameData.energy < 10) return;

        if (GameData.weaponEnergy <= 90) {
            GameData.weaponEnergy += 10;
            GameData.energy -= 10;
            SetFillAmount(weapon);
        }
    }
    public void AddShieldEnergy() {
        if (GameData.energy < 10) return;

        if (GameData.shieldEnergy <= 90) {
            GameData.shieldEnergy += 10;
            GameData.energy -= 10;
            SetFillAmount(shield);
        }
    }
    public void AddSensorEnergy() {
        if (GameData.energy < 10) return;

        if (GameData.sensorEnergy <= 90) {
            GameData.sensorEnergy += 10;
            GameData.energy -= 10;
            SetFillAmount(sensor);
        }
    }
    public void AddNavigationEnergy() {
        if (GameData.energy < 10) return;

        if (GameData.navigationEnergy <= 90) {
            GameData.navigationEnergy += 10;
            GameData.energy -= 10;
            SetFillAmount(navigation);
        }
    }

    public void ReduceWeaponEnergy() {
        if (GameData.weaponEnergy >= 10) {
            GameData.weaponEnergy -= 10;
            GameData.energy += 10;
            SetFillAmount(weapon);
        }
    }
    public void ReduceShieldEnergy() {
        if (GameData.shieldEnergy >= 10) {
            GameData.shieldEnergy -= 10;
            GameData.energy += 10;
            SetFillAmount(shield);
        }
    }
    public void ReduceSensorEnergy() {
        if (GameData.sensorEnergy >= 10) {
            GameData.sensorEnergy -= 10;
            GameData.energy += 10;
            SetFillAmount(sensor);
        }
    }
    public void ReduceNavigationEnergy() {
        if (GameData.navigationEnergy >= 10) {
            GameData.navigationEnergy -= 10;
            GameData.energy += 10;
            SetFillAmount(navigation);
        }
    }
}
