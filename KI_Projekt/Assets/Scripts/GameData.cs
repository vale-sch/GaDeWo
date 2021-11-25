using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<Container> containers = new List<Container>();
    public static int containersInGame = 0;
    public static bool[] nodeHasContainer = new bool[35];

    static GameData() {
        for (int i = 0; i < nodeHasContainer.Length; i++) {
            nodeHasContainer[i] = false;
        }

        energy = maxEnergy;
        weaponEnergy = 0;
        navigationEnergy = 0;
        shieldEnergy = 0;
        sensorEnergy = 0;
    }

    public static void SaveContainer(int prefabNumber, Vector3 position, Quaternion rotation, bool hasContainer, string name) {
        Container newContainer = new Container(prefabNumber, position, rotation, hasContainer, name);
        containers.Add(newContainer);
        containersInGame++;
        //Debug.Log("Container added. Containers in game: " + containersInGame);
    }

    public static void DeleteContainer(GameObject _container) {
        foreach (Container container in containers) {
            if (container.name == _container.name) containers.Remove(container);
        }
    }

    public static void SaveNodeStatus(int position, bool value) {
        nodeHasContainer[position] = value;
    }

}
