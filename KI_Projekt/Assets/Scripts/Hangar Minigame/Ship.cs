using UnityEngine;

public class Ship {
    public ShipType shipType;
    public Vector3 position;
    public Quaternion rotation;

    public Ship(ShipType _shipType, Vector3 _position, Quaternion _rotation) {
        this.shipType = _shipType;
        this.position = _position;
        this.rotation = _rotation;
    }
}
