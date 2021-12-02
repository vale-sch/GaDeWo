using UnityEngine;

public class TransferNode : MonoBehaviour {
    public Department department;

    public void TransferSupplies(int supplies) {
        switch (department) {
            case Department.BRIDGE:
                GameData.bridgeSupplies += supplies;
                break;
            case Department.FIGHTING:
                GameData.fightingSupplies += supplies;
                break;
            case Department.CREW:
                GameData.crewSupplies += supplies;
                break;
            case Department.ENGINEERING:
                GameData.engineeringSupplies += supplies;
                break;
            case Department.LOGISTICS:
                GameData.logisticsSupplies += supplies;
                break;
        }
    }
}
