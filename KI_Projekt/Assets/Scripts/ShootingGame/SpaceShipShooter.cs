using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooter : MonoBehaviour {
    public float minX;
    public float maxX;
    public float movingSpeed = 5;
    [Range(0f, 6f)]
    public float extendSpeed = 5;
    public Transform extendGunPos;
    public Transform disabledGunPos;
    public GameObject flak;
    public GameObject railGun;
    public LaserMachine tractorMachine;
    public EnergyBar energyBar;
    public GameObject loadEnergyText;

    private bool isFlak;
    private bool isRail;
    private bool isTractor;
    private float totalEnergy;
    private bool setTractor;
    void Awake() {
        flak.transform.GetChild(flak.transform.childCount - 1).GetComponent<RotateGunCamera>().energyBar = energyBar;
        railGun.transform.GetChild(railGun.transform.childCount - 1).GetComponent<RotateGunCamera>().energyBar = energyBar;
        tractorMachine.gameObject.GetComponent<RotateGunCamera>().energyBar = energyBar;
        tractorMachine.gameObject.GetComponent<RotateGunCamera>().spaceShipShooter = this;
        totalEnergy = GameData.weaponEnergy;
        energyBar.SetFillAmount("Weapon");
    }
    void Update() {
        if (totalEnergy == 0) {
            loadEnergyText.SetActive(true);
            return;
        } else {
            loadEnergyText.SetActive(false);
        }
        if (GameData.weaponEnergy < totalEnergy) {
            if (GameData.weaponEnergy <= 0f)
                if (isTractor)
                    StartCoroutine(ChangeTractorState());
            GameData.weaponEnergy += 0.06f;
            energyBar.SetFillAmount("Weapon");

        }

        CheckInput();
    }
    public void AddToTotalEnergy() {
        totalEnergy += 10;
    }
    public void SetTotalEnergyOnLeave() {
        GameData.weaponEnergy = totalEnergy;
    }
    void CheckInput() {
        if (Input.GetKey(KeyCode.D))
            if (transform.root.position.x < maxX) transform.root.transform.Translate(Vector3.right * movingSpeed * Time.fixedDeltaTime, Space.World);

        if (Input.GetKey(KeyCode.A))
            if (transform.root.position.x > minX) transform.root.transform.Translate(Vector3.left * movingSpeed * Time.fixedDeltaTime, Space.World);

        if (Input.GetKey(KeyCode.Q) && !isFlak) {
            if (isTractor)
                StartCoroutine(ChangeTractorState());
            if (isRail) {
                StartCoroutine(ChangeGun(railGun, flak));
                isFlak = true;
                isRail = false;
                return;
            } else {
                StartCoroutine(ActivateGun(flak));
                isFlak = true;
            }
        }

        if (Input.GetKey(KeyCode.W) && !isRail) {
            if (isTractor)
                StartCoroutine(ChangeTractorState());
            if (isFlak) {
                StartCoroutine(ChangeGun(flak, railGun));
                isFlak = false;
                isRail = true;
                return;
            } else {
                StartCoroutine(ActivateGun(railGun));
                isRail = true;
            }
        }
        if (Input.GetKey(KeyCode.E) && !setTractor) {
            if (isFlak) {
                isFlak = false;
                StartCoroutine(ChangeGun(flak, null));
            }
            if (isRail) {
                isRail = false;
                StartCoroutine(ChangeGun(railGun, null));
            }
            StartCoroutine(ChangeTractorState());
        }
    }
    IEnumerator ActivateGun(GameObject gun) {
        if (isTractor)
            StartCoroutine(ChangeTractorState());
        float dis = Vector3.Distance(gun.transform.position, extendGunPos.position);
        while (dis > 0.1f) {
            gun.transform.position = Vector3.Lerp(gun.transform.position, extendGunPos.position, Time.fixedDeltaTime * extendSpeed);
            dis = Vector3.Distance(gun.transform.position, extendGunPos.position);
            yield return null;
        }
        gun.transform.GetChild(gun.transform.childCount - 1).GetComponent<RotateGunCamera>().enabled = true;
    }
    IEnumerator ChangeGun(GameObject gun, GameObject gunToActivate) {
        gun.transform.GetChild(gun.transform.childCount - 1).GetComponent<RotateGunCamera>().enabled = false;
        float dis = Vector3.Distance(gun.transform.position, disabledGunPos.transform.position);
        while (dis > 0.1f) {
            gun.transform.position = Vector3.Lerp(gun.transform.position, disabledGunPos.transform.position, Time.fixedDeltaTime * extendSpeed);
            dis = Vector3.Distance(gun.transform.position, disabledGunPos.transform.position);
            yield return null;
        }
        if (gunToActivate)
            StartCoroutine(ActivateGun(gunToActivate));
    }
    public IEnumerator ChangeTractorState() {
        setTractor = true;
        if (!isTractor) {
            isTractor = true;
            tractorMachine.gameObject.GetComponent<RotateGunCamera>().enabled = true;
            tractorMachine.m_inspectorProperties.m_maxRadialDistance = 12;
        } else {
            isTractor = false;
            tractorMachine.gameObject.GetComponent<RotateGunCamera>().enabled = false;
            tractorMachine.m_inspectorProperties.m_maxRadialDistance = 0;
        }
        yield return new WaitForSeconds(1);
        setTractor = false;

    }
}
