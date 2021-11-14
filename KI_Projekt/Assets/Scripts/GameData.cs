using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static float energy;
    public static float shipHealth;
    public static float progress;

    //Cargo Minigame
    public static List<Container> containers = new List<Container>();
    public static int containersInGame = 0;
    public static bool[] nodeHasContainer = new bool[35];

    static GameData()
    {
        for (int i = 0; i < nodeHasContainer.Length; i++)
        {
            nodeHasContainer[i] = false;
        }
    }

    public static void SaveContainer(int prefabNumber, Vector3 position, Quaternion rotation, bool hasContainer, string name)
    {
        Container newContainer = new Container(prefabNumber, position, rotation, hasContainer, name);
        containers.Add(newContainer);
        containersInGame++;
        //Debug.Log("Container added. Containers in game: " + containersInGame);
    }

    public static void DeleteContainer(GameObject _container)
    {
        foreach (Container container in containers)
        {
            if (container.name == _container.name) containers.Remove(container);
        }
    }

    public static void SaveNodeStatus(int position, bool value)
    {
        nodeHasContainer[position] = value;
    }

}
