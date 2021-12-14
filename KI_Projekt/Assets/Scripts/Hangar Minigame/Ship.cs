using UnityEngine;

public class Ship {
    public ShipType shipType;
    public Vector3 position;
    public Quaternion rotation;
    public bool isInSpace;
    public float timeStamp;

    public Ship(ShipType _shipType, Vector3 _position, Quaternion _rotation, bool _isInSpace, float _timeStamp) {
        this.shipType = _shipType;
        this.position = _position;
        this.rotation = _rotation;
        this.isInSpace = _isInSpace;
        this.timeStamp = _timeStamp;
    }
}
