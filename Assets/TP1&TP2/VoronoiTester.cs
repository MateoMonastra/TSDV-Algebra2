using TP1_TP2.Utilities;
using UnityEngine;

namespace TP1_TP2
{
    public class VoronoiTester : MonoBehaviour
    {
        [SerializeField] private GameObject pointGameObject;
        [SerializeField] private MyVoronoi myVoronoi;
        [SerializeField] private Material defaultMat;
        [SerializeField] private Material highLightMat;

        private VoronoiObject _lastPoint;

        void Update()
        {
            UpdateClosestPoint();
        }

        private void UpdateClosestPoint()
        {
            VoronoiObject newVoronoiPoint = myVoronoi.GetClosestPoint(new Vec3(pointGameObject.transform.position));
            
            if (_lastPoint == null)
                _lastPoint = newVoronoiPoint;

            else if (_lastPoint != newVoronoiPoint)
            {
                _lastPoint.ObjectMesh.material = defaultMat;
                _lastPoint = newVoronoiPoint;
            }

            _lastPoint.ObjectMesh.material = highLightMat;
        }
    }
}