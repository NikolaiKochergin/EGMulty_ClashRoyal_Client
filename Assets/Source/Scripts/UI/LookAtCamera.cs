using UnityEngine;

namespace Source.Scripts.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _camera;

        private void Awake() => 
            _camera = Camera.main.transform;

        private void Update() => 
            transform.rotation = _camera.rotation;
    }
}
