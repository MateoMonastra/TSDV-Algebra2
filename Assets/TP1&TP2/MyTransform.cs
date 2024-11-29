using System;
using System.Collections.Generic;
using TP1_TP2.Utilities;
using UnityEngine;

namespace TP1_TP2
{
    public class MyTransform
    {
        public Vec3 position;
        public Vec3 localPosition;
        public MyQuaternion Rotation;
        public MyQuaternion LocalRotation;
        public Vec3 eulerAngles;
        public Vec3 localEulerAngles;

        public MyMatrix4x4 WorldToLocalMatrix;
        public MyMatrix4x4 LocalToWorldMatrix;


        public Vec3 lossyScale;
        public Vec3 localScale;
        public MyTransform parent;
        public List<MyTransform> childrens;
        
        private int _childCount;


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

            _childCount = 0;
            childrens.Clear();
        }

        public void DetachChildren(MyTransform children)
        {
            if (childrens.Contains(children))
            {
                childrens.Remove(children);
                _childCount--;
            }
            else
            {
                Debug.LogWarning("Children not found");
            }
        }

        public int GetChildCount()
        {
            return _childCount;
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

                //convierte a local la global en base al padre
                localPosition =
                    new Vec3(MyQuaternion.Inverse(newParent.Rotation) * (worldPosition - newParent.position));
            }
            
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
            _childCount++;
        }

        public void LookAt(Vec3 targetPosition)
        {
            LookAt(targetPosition, Vec3.Up);
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
            MyQuaternion eulerRot = MyQuaternion.Euler(eulers.x, eulers.y, eulers.z);

            if (relativeTo == Space.Self)
            {
                LocalRotation *= eulerRot;

                if (parent != null)
                {
                    Rotation = parent.Rotation * LocalRotation;
                }
                else
                {
                    Rotation = LocalRotation;
                }
            }
            else
            {
                Rotation = MyQuaternion.Inverse(Rotation) * eulerRot * Rotation;

                if (parent != null)
                {
                    LocalRotation = MyQuaternion.Inverse(parent.Rotation) * Rotation;
                }
                else
                {
                    LocalRotation = Rotation;
                }
            }

            UpdateMatrix();
        }

        public void Rotate(Vec3 eulers)
        {
            Rotate(eulers, Space.Self);
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            Rotate(new Vec3(xAngle, yAngle, zAngle));
        }

        public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
        {
            Rotate(new Vec3(xAngle, yAngle, zAngle), relativeTo);
        }

        public void Rotate(Vec3 axis, float angle, Space relativeTo)
        {
            MyQuaternion axisRotation = MyQuaternion.AngleAxis(angle, axis);

            if (relativeTo == Space.Self)
            {
                LocalRotation = LocalRotation * axisRotation;
            }
            else
            {
                Rotation = Rotation * (MyQuaternion.Inverse(Rotation) * axisRotation * Rotation);
            }
        }

        public void Rotate(Vec3 axis, float angle)
        {
            Rotate(axis, angle, Space.Self);
        }

        public void RotateAround(Vec3 point, Vec3 axis, float angle)
        {
            axis = axis.normalized;

            MyQuaternion newRotation = MyQuaternion.AngleAxis(angle, axis);

            Vec3 localPoint = position - point;

            Vec3 rotatedPoint = newRotation * localPoint;

            position = rotatedPoint + point;

            this.Rotation = newRotation * this.Rotation;

            UpdateMatrix();
        }

        public void RotateAround(Vec3 axis, float angle)
        {
            RotateAround(Vec3.Zero, axis, angle);
        }

        public void SetLocalPositionAndRotation(Vec3 localPosition, MyQuaternion rotation)
        {
            this.localPosition = localPosition;
            this.LocalRotation = rotation;

            if (parent != null)
            {
                position = parent.position + parent.Rotation * localPosition;
                this.Rotation = parent.Rotation * LocalRotation;
            }
            else
            {
                position = localPosition;
                this.Rotation = LocalRotation;
            }
        }

        public void SetPositionAndRotation(Vec3 position, MyQuaternion rotation)
        {
            this.position = position;
            this.Rotation = rotation;

            if (parent != null)
            {
                localPosition = MyQuaternion.Inverse(parent.Rotation) * (position - parent.position);
                LocalRotation = MyQuaternion.Inverse(parent.Rotation) * rotation;
            }
            else
            {
                localPosition = position;
                LocalRotation = rotation;
            }

        }

        public void Translate(Vec3 translation, Space relativeTo)
        {
            if (relativeTo == Space.World)
            {
                position += translation;
            }
            else
            {
                position += Rotation * translation;
            }
            
        }

        public void Translate(Vec3 translation)
        {
            Translate(translation, Space.Self);
        }

        public void Translate(float x, float y, float z, Space relativeTo)
        {
            Translate(new Vec3(x, y, z), relativeTo);
        }

        public void Translate(float x, float y, float z)
        {
            Translate(new Vec3(x, y, z));
        }

        public void Translate(Vec3 translation, MyTransform relativeTo)
        {
            if (relativeTo != null)
            {
                position += relativeTo.TransformDirection(translation);
            }
            else
            {
                position += translation;
            }
            
        }

        public void Translate(float x, float y, float z, MyTransform relativeTo)
        {
            Translate(new Vec3(x, y, z), relativeTo);
        }

        public void Scale(Vec3 scale)
        {
            localScale = Vec3.Scale(localScale, scale);

            UpdateMatrix();
        }

        public void SetLocalScale(Vec3 newScale)
        {
            localScale = newScale;

            UpdateMatrix();
        }

        public Vec3 TransformDirection(Vec3 direction)
        {
             return Rotation * direction;
        }

        public Vec3 TransformDirection(float x, float y, float z)
        {
            return TransformDirection(new Vec3(x, y, z));
        }

        public Vec3 TransformPoint(Vec3 position)
        {
            Vec3 transformedPoint = Rotation * Vec3.Scale(localPosition, localScale);

            transformedPoint += position;

            return transformedPoint;
        }

        public Vec3 TransformPoint(float x, float y, float z)
        {
            return TransformPoint(new(x, y, z));
        }

        public Vec3 TransformVector(Vec3 vector)
        {
            Vec3 transformedVector = Rotation * vector;

            transformedVector = Vec3.Scale(transformedVector, localScale);

            return transformedVector;
        }

        public Vec3 TransformVector(float x, float y, float z)
        {
            return TransformVector(new Vec3(x, y, z));
        }

        public Vec3 InverseTransformDirection(Vec3 direction)
        {
            return MyQuaternion.Inverse(Rotation) * direction;
        }

        public Vec3 InverseTransformDirection(float x, float y, float z)
        {
            return InverseTransformDirection(new Vec3(x, y, z));
        }

        public Vec3 InverseTransformPoint(Vec3 position)
        {
            Vec3 transformedPoint = position - localPosition;
            transformedPoint = MyQuaternion.Inverse(Rotation) * transformedPoint;
            transformedPoint = Vec3.Scale(transformedPoint, Vec3.Inverse(localScale));

            return transformedPoint;
        }

        public Vec3 InverseTransformPoint(float x, float y, float z)
        {
            return InverseTransformPoint(x, y, z);
        }

        public Vec3 InverseTransformVector(Vec3 vector)
        {
            Vec3 transformedVector = MyQuaternion.Inverse(Rotation) * vector;
            transformedVector = Vec3.Scale(transformedVector, Vec3.Inverse(localScale));

            return transformedVector;
        }

        public Vec3 InverseTransformVector(float x, float y, float z)
        {
            return InverseTransformVector(x, y, z);
        }

        private void UpdateLocalToWorldMatrix()
        {
            LocalToWorldMatrix = MyMatrix4x4.TRS(localPosition, LocalRotation, localScale);

            localEulerAngles = LocalRotation.EulerAngles;
            
        }

        private void UpdateWorldToLocalMatrix()
        {
            if (parent != null)
            {
                position = new Vec3(parent.Rotation * Vec3.Scale(localPosition, parent.lossyScale) + parent.position);
                
                Rotation = parent.Rotation * LocalRotation;
                
                lossyScale = Vec3.Scale(parent.lossyScale, localScale);
            }
            else
            {
                lossyScale = localScale;
                
                WorldToLocalMatrix = LocalToWorldMatrix.inverse;
            }
            
            eulerAngles = Rotation.EulerAngles;
            
            UpdateChildrens();
        }

        private void UpdateMatrix()
        {
            UpdateLocalToWorldMatrix();
            UpdateWorldToLocalMatrix();
        }

        private void UpdateChildrens()
        {
            foreach (MyTransform child in childrens)
            {
                if (child != null)
                {
                    child.UpdateWorldToLocalMatrix();
                }
            }
        }
    }
}