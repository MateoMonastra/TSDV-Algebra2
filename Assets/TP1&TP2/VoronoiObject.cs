using System.Collections.Generic;
using TP1_TP2.Utilities;
using UnityEngine;

namespace TP1_TP2
{
    public class VoronoiObject
    {
        public List<MyPlane> Planes = new List<MyPlane>();
        public List<Vec3> PlanePositions = new List<Vec3>();
        public List<GameObject> PlaneGameObject = new List<GameObject>();
        public MeshRenderer ObjectMesh;
    }
}