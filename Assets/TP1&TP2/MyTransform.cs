using System;
using System.Collections.Generic;
using TP1_TP2.Utilities;
using UnityEngine;

namespace TP1_TP2
{
    public class MyTransform : MonoBehaviour
    {
        public Vec3 position;
        public Vec3 localPosition;
        public MyQuaternion Rotation;
        public MyQuaternion LocalRotation;
        public Vec3 eulerAngles;
        public Vec3 localEulerAngles;
        public Vec3 right;
        public Vec3 up;
        public Vec3 forward;

        public MyMatrix4x4 WorldToLocalMatrix;
        public MyMatrix4x4 LocalToWorldMatrix;


        public Vec3 lossyScale;
        public Vec3 localScale;
        public bool hasChanged;
        public MyTransform parent;
        public List<MyTransform> childrens;
        public int childCount;


        public MyTransform()
        {
            Rotation = MyQuaternion.Identity;
            LocalRotation = MyQuaternion.Identity;
            WorldToLocalMatrix = MyMatrix4x4.Identity;

            childrens = new List<MyTransform>();
        }

        public void DetachChildrens()
        {
            foreach (MyTransform child in childrens)
            {
                child.SetParent(null);
            }

            childCount = 0;
            childrens.Clear();

            hasChanged = true;
        }

        public void DetachChildren(MyTransform children)
        {
            childrens.Remove(children);
            childCount--;
        }

        public int GetChildCount()
        {
            return childCount;
        }

        public void SetParent(MyTransform newParent)
        {
            if (this.parent != null)
            {
                this.parent.DetachChildren(this);
                this.parent = null;
            }

            Vec3 worldPosition = this.position;
            MyQuaternion worldRotation = this.Rotation;
            Vec3 worldScale = this.lossyScale;

            this.parent = newParent;

            if (newParent != null)
            {
                newParent.AddChildren(this);
                
                localPosition = new Vec3(MyQuaternion.Inverse(newParent.Rotation) * (worldPosition - newParent.position));
            }

            hasChanged = true;
            UpdateMatrix();
        }

        public void SetParent(MyTransform parent, bool worldPositionStays)
        {
            if (worldPositionStays)
            {
                Vec3 worldPosition = LocalToWorldMatrix.GetPosition();
                MyQuaternion worldRotation = Rotation;

                SetParent(parent);

                if (parent != null)
                {
                    position = parent.InverseTransformPoint(worldPosition);
                }
                else
                {
                    position = worldPosition;
                }

                Rotation = worldRotation;
            }
            else
            {
                SetParent(parent);
            }
        }

        public void AddChildren(MyTransform children)
        {
            childrens.Add(children);
            children.parent = this;
            childCount++;
        }

        public void LookAt(Vec3 targetPosition)
        {
            Vec3 direction = (targetPosition - position).normalized;

            MyQuaternion newRotation = MyQuaternion.LookRotation(direction, Vec3.Up);

            Rotation = newRotation;

            if (parent != null)
            {
                LocalRotation = MyQuaternion.Inverse(parent.Rotation) * Rotation;
            }
            else
            {
                LocalRotation = Rotation;
            }

            hasChanged = true;

            UpdateMatrix();
        }

        public void LookAt(Vec3 targetPosition, Vec3 worldUp)
        {
            Vec3 direction = (targetPosition - position).normalized;

            Vec3 up = worldUp.normalized;

            MyQuaternion newRotation = MyQuaternion.LookRotation(direction, up);

            Rotation = newRotation;

            if (parent != null)
            {
                LocalRotation = MyQuaternion.Inverse(parent.Rotation) * Rotation;
            }
            else
            {
                LocalRotation = Rotation;
            }

            hasChanged = true;

            UpdateMatrix();
        }

        public void LookAt(MyTransform target)
        {
            LookAt(target.position);
        }

        public void LookAt(Transform target)
        {
            LookAt(new Vec3(target.position));
        }

        public void LookAt(MyTransform target, Vec3 worldUp)
        {
            LookAt(target.position, worldUp);
        }

        public void LookAt(Transform target, Vec3 worldUp)
        {
            LookAt(new Vec3(target.position), worldUp);
        }

        public void Rotate(Vec3 eulers, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 eulers)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 axis, float angle, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void RotateAround(Vec3 point, Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void RotateAround(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void SetLocalPositionAndRotation(Vec3 localPosition, MyQuaternion rotation)
        {
            throw new NotImplementedException();
        }

        public void SetPositionAndRotation(Vec3 position, MyQuaternion rotation)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation, MyTransform relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z, MyTransform relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Scale(Vec3 scale)
        {
            throw new NotImplementedException();
        }

        public void SetLocalScale(Vec3 newScale)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformDirection(Vec3 direction)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformDirection(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformPoint(Vec3 position)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformPoint(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformVector(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformVector(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformDirection(Vec3 direction)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformDirection(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformPoint(Vec3 position)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformPoint(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformVector(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformVector(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        private void UpdateLocalToWorldMatrix()
        {
            throw new NotImplementedException();
        }

        private void UpdateWorldToLocalMatrix()
        {
            throw new NotImplementedException();
        }

        private void UpdateMatrix()
        {
            throw new NotImplementedException();
        }

        private void UpdateChildrens()
        {
            throw new NotImplementedException();
        }
    }
}