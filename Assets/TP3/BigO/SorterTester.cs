using System;
using System.Collections.Generic;
using UnityEngine;

namespace TP3
{
    public enum Sorts
    {
        Bitonic,
        Selection,
        CocktailShaker,
        Quick,
        RadixLSD,
        Shell,
        Bogo,
        RadixMSD,
        Intro,
        Adaptive,
        Bubble,
        Gnome,
        Merge,
        Heap,
        Insertion
    }

    public class SorterTester : MonoBehaviour
    {
        [SerializeField] private List<int> list = new List<int>();
        [SerializeField] private Sorts sort;

        private void Start()
        {
            switch (sort)
            {
                case Sorts.Bitonic:
                    TestBitonic();
                    break;
                case Sorts.Selection:
                    TestSelection();
                    break;
                case Sorts.CocktailShaker:
                    TestCocktailShaker();
                    break;
                case Sorts.Quick:
                    TestQuick();
                    break;
                case Sorts.RadixLSD:
                    TestRadixLSD();
                    break;
                case Sorts.Shell:
                    TestShell();
                    break;
                case Sorts.Bogo:
                    TestBogo();
                    break;
                case Sorts.RadixMSD:
                    TestRadixMSD();
                    break;
                case Sorts.Intro:
                    TestIntro();
                    break;
                case Sorts.Adaptive:
                    TestAdaptive();
                    break;
                case Sorts.Bubble:
                    TestBubble();
                    break;
                case Sorts.Gnome:
                    TestGnome();
                    break;
                case Sorts.Merge:
                    TestMerge();
                    break;
                case Sorts.Heap:
                    TestHeap();
                    break;
                case Sorts.Insertion:
                    TestInsertion();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TestBitonic()
        {
            Sorter<int>.BitonicSort(list);
        }

        private void TestSelection()
        {
            Sorter<int>.SelectionSort(list);
        }

        private void TestCocktailShaker()
        {
            Sorter<int>.CocktailShakerSort(list);
        }

        private void TestQuick()
        {
            Sorter<int>.QuickSort(list);
        }

        private void TestRadixLSD()
        {
            throw new NotImplementedException();
        }

        private void TestShell()
        {
            Sorter<int>.ShellSort(list);
        }

        private void TestBogo()
        {
            Sorter<int>.BogoSort(list);
        }

        private void TestRadixMSD()
        {
            throw new NotImplementedException();
        }

        private void TestIntro()
        {
            Sorter<int>.IntroSort(list);
        }

        private void TestAdaptive()
        {
            throw new NotImplementedException();
        }

        private void TestBubble()
        {
            Sorter<int>.BubbleSort(list);
        }

        private void TestGnome()
        {
            Sorter<int>.GnomeSort(list);
        }

        private void TestMerge()
        {
            Sorter<int>.MergeSort(list);
        }

        private void TestHeap()
        {
            Sorter<int>.HeapSort(list);
        }

        private void TestInsertion()
        {
            Sorter<int>.InsertionSort(list);
        }
    }
}
