using TP1_TP2.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace TP1_TP2
{
    public class MyTransformTester : MonoBehaviour
    {
        [Header("Unity´s Transform Hierarchy")] [SerializeField]
        private Transform unityHierarchyCube;

        [Header("My Transforms")] [SerializeField]
        private Transform testCube;

        [SerializeField] private Transform testCube2;
        [SerializeField] private Transform testCube3;

        [SerializeField] private Transform point;

        [SerializeField] private float rotationX;
        [SerializeField] private float rotationY;
        [SerializeField] private float rotationZ;
        [SerializeField] private float scale;

        [SerializeField] private bool lookAtPoint;
        [SerializeField] private bool rotateAround;


        private MyTransform _myCube = new MyTransform();
        private MyTransform _myCube2 = new MyTransform();
        private MyTransform _myCube3 = new MyTransform();

        void Start()
        {
            _myCube.SetLocalPositionAndRotation(new Vec3(testCube.localPosition),
                new MyQuaternion(testCube.localRotation));
            _myCube.SetPositionAndRotation(new Vec3(testCube.position), new MyQuaternion(testCube.rotation));
            _myCube.SetLocalScale(new Vec3(scale, scale, scale));

            _myCube2.SetLocalPositionAndRotation(new Vec3(testCube2.localPosition),
                new MyQuaternion(testCube2.localRotation));
            _myCube2.SetPositionAndRotation(new Vec3(testCube2.position), new MyQuaternion(testCube2.rotation));
            _myCube2.SetLocalScale(new Vec3(scale, scale, scale));

            _myCube3.SetLocalPositionAndRotation(new Vec3(testCube3.localPosition),
                new MyQuaternion(testCube3.localRotation));
            _myCube3.SetPositionAndRotation(new Vec3(testCube3.position), new MyQuaternion(testCube3.rotation));
            _myCube3.SetLocalScale(new Vec3(scale, scale, scale));

            _myCube2.SetParent(_myCube);
            _myCube3.SetParent(_myCube2);
        }

        void Update()
        {
            if (rotateAround)
            {
                _myCube.RotateAround(new Vec3(point.position), Vec3.Up, rotationX);
                unityHierarchyCube.RotateAround(point.position, Vec3.Up, rotationX);
            }
            else
            {
                _myCube.Rotate(rotationX, rotationY, rotationZ);
                unityHierarchyCube.Rotate(rotationX, rotationY, rotationZ);
            }

            _myCube.SetLocalScale(new Vec3(scale, scale, scale));
            unityHierarchyCube.localScale = new Vector3(scale, scale, scale);

            if (lookAtPoint)
            {
                _myCube.LookAt(point, Vec3.Up);
                unityHierarchyCube.LookAt(point, Vec3.Up);
            }


            testCube.SetLocalPositionAndRotation(_myCube.localPosition, _myCube.LocalRotation.ToQuaternion());
            testCube.SetPositionAndRotation(_myCube.position, _myCube.Rotation.ToQuaternion());
            testCube.localScale = _myCube.lossyScale;

            testCube2.SetPositionAndRotation(_myCube2.position, _myCube2.Rotation.ToQuaternion());
            testCube2.localScale = _myCube2.lossyScale;

            testCube3.SetPositionAndRotation(_myCube3.position, _myCube3.Rotation.ToQuaternion());
            testCube3.localScale = _myCube3.lossyScale;
        }
    }
}