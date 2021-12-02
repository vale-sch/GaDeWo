using UnityEngine;

public class Container {
    public ContainerSize size;
    public Department department;
    public bool isRevealed = false;
    public Transform transform;
    public Vector3 position;
    public Quaternion rotation;

    public Container(ContainerSize _size, Department _department, bool _isRevealed, Vector3 _position, Quaternion _rotation) {
        this.size = _size;
        this.department = _department;
        this.isRevealed = _isRevealed;
        this.position = _position;
        this.rotation = _rotation;
    }
}
