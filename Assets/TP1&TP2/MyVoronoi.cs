using System.Collections.Generic;
using System.Linq;
using TP1_TP2.Utilities;
using UnityEngine;

namespace TP1_TP2
{
    public class MyVoronoi : MonoBehaviour
    {
        [SerializeField] private List<GameObject> staticObjects = new List<GameObject>();
        [SerializeField] private GameObject planePrefab;

        private List<MyPlane> _planes = new List<MyPlane>();
        private List<VoronoiObject> _voronoiObjects;

        private void Start()
        {
            InitializeVoronoiObjects();
            GeneratePlanes();
            CleanAllPlanes();
        }

        private void InitializeVoronoiObjects()
        {
            _voronoiObjects = staticObjects.Select(obj =>
            {
                var voronoiObject = new VoronoiObject();
                voronoiObject.ObjectMesh = obj.GetComponent<MeshRenderer>();
                return voronoiObject;
            }).ToList();
        }

        private void GeneratePlanes()
        {
            for (int i = 0; i < staticObjects.Count; i++)
            {
                var currentObject = staticObjects[i];
                var voronoiObject = _voronoiObjects[i];

                foreach (var targetObject in staticObjects)
                {
                    if (currentObject == targetObject) continue;
                    
                    Vec3 direction = new Vec3((currentObject.transform.position - targetObject.transform.position)
                        .normalized);
                    Vec3 position = Vec3.Lerp(
                        new Vec3(currentObject.transform.position),
                        new Vec3(targetObject.transform.position),
                        0.5f
                    );
                    
                    var newPlane = new MyPlane(direction, position);
                    _planes.Add(newPlane);
                    
                    var newPlaneObject = Instantiate(planePrefab, position, Quaternion.identity);
                    newPlaneObject.name = $"Plane{_planes.Count}";
                    newPlaneObject.transform.up = newPlane.Normal;
                    
                    voronoiObject.PlanePositions.Add(position);
                    voronoiObject.PlaneGameObject.Add(newPlaneObject);
                    voronoiObject.Planes.Add(newPlane);
                }
            }
        }

        private void CleanAllPlanes()
        {
            foreach (var voronoiObject in _voronoiObjects)
            {
                CleanPlanes(voronoiObject);
            }
        }

        private void CleanPlanes(VoronoiObject voronoiObject)
        {
            var planesToRemove = new HashSet<int>();

            for (int i = 0; i < voronoiObject.PlanePositions.Count; i++)
            {
                for (int j = 0; j < voronoiObject.Planes.Count; j++)
                {
                    if (i != j && !voronoiObject.Planes[j].GetSide(voronoiObject.PlanePositions[i]))
                    {
                        planesToRemove.Add(i);
                        break;
                    }
                }
            }
            
            foreach (var index in planesToRemove.OrderByDescending(i => i))
            {
                Destroy(voronoiObject.PlaneGameObject[index]);
                voronoiObject.Planes.RemoveAt(index);
                voronoiObject.PlaneGameObject.RemoveAt(index);
                voronoiObject.PlanePositions.RemoveAt(index);
            }
        }

        public VoronoiObject GetClosestPoint(Vec3 point)
        {
            foreach (var voronoiObject in _voronoiObjects)
            {
                if (voronoiObject.Planes.All(plane => plane.GetSide(point)))
                {
                    return voronoiObject;
                }
            }

            return null;
        }
    }
}