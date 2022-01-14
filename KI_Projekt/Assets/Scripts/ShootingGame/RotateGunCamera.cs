using UnityEngine;
using UnityEngine.UI;

public class RotateGunCamera : MonoBehaviour {
    public GameObject shootParticlePrefab;
    public string weaponType;
    public float coolDown;
    [HideInInspector]
    public EnergyBar energyBar;
    [HideInInspector]
    public SpaceShipShooter spaceShipShooter;

    private float startCoolDown;
    private bool hasShoot;



    void Start() {
        startCoolDown = coolDown;
    }

    void Update() {
        if (GameData.weaponEnergy <= 0f) {
            if (weaponType == "rail")
                this.transform.GetChild(this.transform.childCount - 1).GetComponent<LaserMachine>().m_inspectorProperties.m_maxRadialDistance = 0f;
            return;

        }
        CheckInput();
        if (hasShoot) {
            if (coolDown >= 0f)
                coolDown -= Time.deltaTime;
            else {
                hasShoot = false;
                coolDown = startCoolDown;
            }
        }
    }

    void CheckInput() {
        this.transform.rotation = Quaternion.Euler(Input.mousePosition.x, 90, -90);
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (weaponType == "rail") {

                GameData.weaponEnergy -= 0.1f;
                energyBar.SetFillAmount("Weapon");
                this.transform.GetChild(this.transform.childCount - 1).GetComponent<LaserMachine>().m_inspectorProperties.m_maxRadialDistance = 150f;
            }

            if (!hasShoot) {
                GameData.weaponEnergy -= 10;
                energyBar.SetFillAmount("Weapon");
                hasShoot = true;
                if (weaponType == "flak") {
                    var bulletChache = Instantiate(shootParticlePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(Input.mousePosition.x - 90, 90, 0));
                    bulletChache.GetComponent<BulletScript>().flyDir = Quaternion.Euler(Input.mousePosition.x - 90, 90, 0) * Vector3.forward;
                }
            }
        }

        if (weaponType == "tractor") {
            GameData.weaponEnergy -= 0.1f;
            energyBar.SetFillAmount("Weapon");
        }
        if (weaponType == "rail") {

            GameData.weaponEnergy -= 0.1f;
            energyBar.SetFillAmount("Weapon");
        }

    }
}
