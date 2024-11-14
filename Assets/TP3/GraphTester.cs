using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private int numTest;
    
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
            Debug.Log($"All are {numTest.ToString()}: {GraphMethods.All(source1, i => i == numTest)}");
        }
        private void TestAny()
        {
            Debug.Log($"Any is {numTest.ToString()}: {GraphMethods.Any(source1, i => i == numTest)}");
        }


        private void TestContains()
        {
            Debug.Log($"Contains {numTest.ToString()}: {GraphMethods.Contains(source1, numTest)}");
        }

        private void TestDistinct()
        {
            var distinctInts = GraphMethods.ToList(GraphMethods.Distinct(source1));
            
            var logText = "Distincts: ";
            
            foreach (var element in distinctInts)
            {
                logText += $"{element}, ";
            }

            Debug.Log(logText);
        }

        private void TestElementAt()
        {
            Debug.Log($"Element at: {numTest}: {GraphMethods.ElementAt(source1, numTest)}");
        }

        private void TestExcept()
        {
            var exceptsList = GraphMethods.ToList(GraphMethods.Except(source1, source2));
            
            string logText = "Excepts: ";
            
            foreach (var element in exceptsList)
            {
                logText += $"{element}, ";
            }

            Debug.Log(logText);
        }

        private void TestFirst()
        {
            Debug.Log($"First equal value: {GraphMethods.First(source1, i => i == numTest)}");
        }

        private void TestLast()
        {
            Debug.Log($"Last equal value: {GraphMethods.Last(source1, i => i == numTest)}");
        }

        private void TestIntersect()
        {
            var intersectList = GraphMethods.ToList(GraphMethods.Intersect(source1, source2));
            
            string logText = "Intersects: ";
            
            foreach (var element in intersectList)
            {
                logText += $"{element}, ";
            }

            Debug.Log(logText);
        }

        private void TestCount()
        {
            Debug.Log($"Count of {numTest}: {GraphMethods.Count(source1, i => i == numTest)}");
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