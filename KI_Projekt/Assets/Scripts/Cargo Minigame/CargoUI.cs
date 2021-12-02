using UnityEngine;
using UnityEngine.UI;

public class CargoUI : MonoBehaviour {
    public Text currentSupplies;
    private string supplies;

    private void Update() {
        supplies = "<size=38>SUPPLIES</size>\n" + Department.BRIDGE + ": " + GameData.bridgeSupplies + "\n" +
        Department.CREW + ": " + GameData.crewSupplies + "\n" +
        Department.ENGINEERING + ": " + GameData.engineeringSupplies + "\n" +
        Department.FIGHTING + ": " + GameData.fightingSupplies + "\n" +
        Department.LOGISTICS + ": " + GameData.logisticsSupplies;

        currentSupplies.text = supplies;
    }
}
