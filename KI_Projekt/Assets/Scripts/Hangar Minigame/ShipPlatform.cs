using UnityEngine;
using UnityEngine.UI;

public class ShipPlatform : MonoBehaviour {
    public PlatformType platformType;
    public ShipType shipType;
    public Transform dockPoint;
    public GameObject canvas;
    public Image energyBar;
    [HideInInspector] public GameObject ship;

    private void Update() {
        if (canvas == null) return;
        if (ship == null) {
            canvas.SetActive(false);
            return;
        }
        canvas.SetActive(true);
        energyBar.fillAmount = ship.GetComponent<ShipDrag>().energy / 100;
        if (platformType == PlatformType.PARK && ship != null && GameData.logisticsEnergy > 0 && ship.GetComponent<ShipDrag>().energy < 100) ChargeShip();
    }
    public void ParkShip(GameObject _ship) {
        if (_ship.GetComponent<ShipDrag>().shipPlatform != null) _ship.GetComponent<ShipDrag>().shipPlatform.GetComponent<ShipPlatform>().LeaveParkingSpot();
        _ship.GetComponent<ShipDrag>().shipPlatform = gameObject;
        this.ship = _ship;
    }

    public void LeaveParkingSpot() {
        ship = null;
    }

    private void ChargeShip() {
        if (ship.GetComponent<ShipDrag>().energy < 100) {
            float chargingOutput = GameData.logisticsEnergy / 100;
            float chargePerFrame = 0.025f;
            ship.GetComponent<ShipDrag>().energy += chargePerFrame * chargingOutput;
        }
    }
}
