using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Department { NONE, BRIDGE, FIGHTING, ENGINEERING, CREW, LOGISTICS }
public static class GameData {
    public static float energy;
    public static float maxEnergy = 200;
    public static float weaponEnergy;
    public static float navigationEnergy;
    public static float shieldEnergy;
    public static float sensorEnergy;
    public static float shipHealth;
    public static float progress;

    //Cargo Minigame
    public static Container[][] containerGrid;
    public static int containersInTransfer;
    public static bool newContainerInTransfer;
    public static bool cargoIsInitialized;
    public static int bridgeSupplies;
    public static int fightingSupplies;
    public static int engineeringSupplies;
    public static int crewSupplies;
    public static int logisticsSupplies;

    //Hangar Minigame
    public static Ship[] shipsInHangar;
    public static Ship[] shipsInSpace;
    public static bool hangarIsInitialized;

    static GameData() {
        energy = maxEnergy;
        weaponEnergy = 0;
        navigationEnergy = 0;
        shieldEnergy = 0;
        sensorEnergy = 0;

        cargoIsInitialized = false;

        containersInTransfer = 0;
        newContainerInTransfer = false;

        bridgeSupplies = 0;
        fightingSupplies = 0;
        engineeringSupplies = 0;
        crewSupplies = 0;
        logisticsSupplies = 0;

        hangarIsInitialized = false;
    }
}
