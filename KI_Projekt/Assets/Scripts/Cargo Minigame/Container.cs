using UnityEngine;

public class Container {
    public int prefabNumber;
    public Vector3 position;
    public Quaternion rotation;
    public bool hasContainer;
    public string name;

    public Container(int _prefabNumber, Vector3 _position, Quaternion _rotation, bool _hasContainer, string _name) {
        prefabNumber = _prefabNumber;
        position = _position;
        rotation = _rotation;
        hasContainer = _hasContainer;
        name = _name;
    }
}
