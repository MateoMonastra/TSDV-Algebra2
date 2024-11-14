using System;
using System.Collections.Generic;
using UnityEngine;

namespace TP3
{
    public enum Methods
    {
        All,
        Any,
        Contains,
        Distinct,
        ElementAt,
        Except,
        First,
        Last,
        Intersect,
        Count,
        SequenceEqual,
        Single,
        SkipWhile,
        Union,
        Where
    }

    public class GraphsTester : MonoBehaviour
    {
        [SerializeField] private List<int> source1 = new List<int>();
        [SerializeField] private List<int> source2 = new List<int>();
        [SerializeField] private Methods method;
        [SerializeField] private int index;
    
        private void Start()
        {
            switch (method)
            {
                case Methods.All:
                    TestAll();
                    break;
                case Methods.Any:
                    TestAny();
                    break;
                case Methods.Contains:
                    TestContains();
                    break;
                case Methods.Distinct:
                    TestDistinct();
                    break;
                case Methods.ElementAt:
                    TestElementAt();
                    break;
                case Methods.Except:
                    TestExcept();
                    break;
                case Methods.First:
                    TestFirst();
                    break;
                case Methods.Last:
                    TestLast();
                    break;
                case Methods.Intersect:
                    TestIntersect();
                    break;
                case Methods.Count:
                    TestCount();
                    break;
                case Methods.SequenceEqual:
                    TestSequenceEqual();
                    break;
                case Methods.Single:
                    TestSingle();
                    break;
                case Methods.SkipWhile:
                    TestSkipWhile();
                    break;
                case Methods.Union:
                    TestUnion();
                    break;
                case Methods.Where:
                    TestWhere();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TestAll()
        {
            Debug.Log($"All are {index.ToString()}: {GraphMethods.All(source1, i => i == index)}");
        }
        private void TestAny()
        {
            Debug.Log($"Any is {index.ToString()}: {GraphMethods.Any(source1, i => i == index)}");
        }


        private void TestContains()
        {
        }

        private void TestDistinct()
        {
        }

        private void TestElementAt()
        {
        }

        private void TestExcept()
        {
        }

        private void TestFirst()
        {
        }

        private void TestLast()
        {
        }

        private void TestIntersect()
        {
        }

        private void TestCount()
        {
        }

        private void TestSequenceEqual()
        {
        }

        private void TestSingle()
        {
        }

        private void TestSkipWhile()
        {
        }

        private void TestUnion()
        {
        }

        private void TestWhere()
        {
        }
    }
}