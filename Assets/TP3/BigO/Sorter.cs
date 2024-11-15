using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;


namespace TP3
{
    public static class Sorter<T> where T : IComparable<T>
    {
        #region SortingMethods

        // Algoritmo Bogo Sort:
        //Memory: O(n!)
        // Ordena la lista aleatoriamente hasta que esté en el orden correcto.
        public static void BogoSort(List<T> list)
        {
            Debug.Log("BogoSort - Lista inicial: " + string.Join(", ", list));


            while (!IsSorted(list, 0, list.Count - 1, 0))
            {
                Shuffle(list);
            }

            Debug.Log("BogoSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Bubble Sort:
        //Memory: O(n^2)
        // Recorre la lista repetidamente, moviendo los elementos más grandes al final de la lista.
        public static void BubbleSort(List<T> list)
        {
            Debug.Log("BubbleSort - Lista inicial: " + string.Join(", ", list));

            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (Compare(list[j], list[j + 1]) > 0)
                    {
                        Swap(list, j, j + 1);
                    }
                }
            }

            Debug.Log("BubbleSort - Lista ordenada: " + string.Join(", ", list));
        }


        // Algoritmo Selection Sort:
        //Memory: O(n^2)
        // Selecciona el elemento más pequeño y lo coloca al inicio de la lista, repitiendo este proceso para
        // cada posición de la lista hasta que esté completamente ordenada.
        public static void SelectionSort(List<T> list)
        {
            Debug.Log("SelectionSort - Lista inicial: " + string.Join(", ", list));

            for (int i = 0; i < list.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (Compare(list[j], list[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                Swap(list, i, minIndex);
            }

            Debug.Log("SelectionSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Gnome Sort:
        //Memory: O(n^2)
        // Similar a la ordenación por inserción, pero realiza intercambios de elementos y retrocede
        // si encuentra un desorden.
        public static void GnomeSort(List<T> list)
        {
            Debug.Log("GnomeSort - Lista inicial: " + string.Join(", ", list));

            int index = 0;
            while (index < list.Count)
            {
                if (index == 0 || Compare(list[index], list[index - 1]) >= 0)
                {
                    index++;
                }
                else
                {
                    Swap(list, index, index - 1);
                    index--;
                }
            }

            Debug.Log("GnomeSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Cocktail Shaker Sort:
        //Memory: O(n^2)
        // Es una variación del Bubble Sort. Ordena en ambas direcciones alternadamente en cada pasada.
        // Mueve los elementos más grandes al final y los más pequeños al inicio de la lista en cada ciclo.
        public static void CocktailShakerSort(List<T> list)
        {
            Debug.Log("CocktailShakerSort - Lista inicial: " + string.Join(", ", list));

            bool swapped = true;
            int start = 0;
            int end = list.Count - 1;

            while (swapped)
            {
                swapped = false;
                for (int i = start; i < end; i++)
                {
                    if (Compare(list[i], list[i + 1]) > 0)
                    {
                        Swap(list, i, i + 1);
                        swapped = true;
                    }
                }

                if (!swapped) break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if (Compare(list[i], list[i + 1]) > 0)
                    {
                        Swap(list, i, i + 1);
                        swapped = true;
                    }
                }

                start++;
            }

            Debug.Log("CocktailShakerSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Insertion Sort:
        //Memory: O(n^2)
        // Toma cada elemento de la lista y lo inserta en su posición correcta en una sublista ordenada al inicio.
        public static void InsertionSort<T>(List<T> list, int left, int right) where T : IComparable<T>
        {
            Debug.Log("InsertionSort - Lista inicial: " + string.Join(", ", list));

            for (int i = left + 1; i <= right; i++)
            {
                T key = list[i];
                int j = i - 1;
                while (j >= left && list[j].CompareTo(key) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }

            Debug.Log("InsertionSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Shell Sort:
        // Ordena la lista utilizando intervalos decrecientes (gap) para reducir el número de comparaciones.
        // A medida que los intervalos se reducen, el algoritmo aproxima un ordenamiento tipo Insertion Sort
        // en sublistas cada vez más grandes hasta que el intervalo es 1.
        public static void ShellSort(List<T> list)
        {
            Debug.Log("ShellSort - Lista inicial: " + string.Join(", ", list));

            int n = list.Count;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = list[i];
                    int j = i;


                    while (j >= gap && Compare(list[j - gap], temp) > 0)
                    {
                        list[j] = list[j - gap];
                        j -= gap;
                    }

                    list[j] = temp;
                }
            }

            Debug.Log("ShellSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Merge Sort:
        //Memory: O(n log n)
        // Divide la lista en mitades y ordena recursivamente cada mitad. Luego, fusiona las dos mitades ordenadas.
        public static void MergeSort(List<T> list)
        {
            Debug.Log("MergeSort - Lista inicial: " + string.Join(", ", list));
            var sortedList = MergeSortRecursive(list);
            list.Clear();
            list.AddRange(sortedList);
            Debug.Log("MergeSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static List<T> MergeSortRecursive(List<T> list)
        {
            if (list.Count <= 1)
                return list;

            int mid = list.Count / 2;
            List<T> left = MergeSortRecursive(list.GetRange(0, mid));
            List<T> right = MergeSortRecursive(list.GetRange(mid, list.Count - mid));

            return Merge(left, right);
        }
        
        private static List<T> Merge(List<T> left, List<T> right)
        {
            List<T> result = new List<T>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (Compare(left[i], right[j]) <= 0)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }

            result.AddRange(left.GetRange(i, left.Count - i));
            result.AddRange(right.GetRange(j, right.Count - j));

            return result;
        }


        // Algoritmo Heap Sort:
        //Memory: O(n log n)
        public static void HeapSort<T>(List<T> list, int left, int right) where T : IComparable<T>
        {
            Debug.Log("HeapSort - Lista inicial: " + string.Join(", ", list));
            int size = right - left + 1;


            for (int i = left + size / 2 - 1; i >= left; i--)
                Heapify(list, size, i, left);


            for (int i = right; i > left; i--)
            {
                Swap(list, left, i);
                Heapify(list, i - left, left, left);
            }

            Debug.Log("HeapSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void Heapify<T>(List<T> list, int size, int root, int offset) where T : IComparable<T>
        {
            int largest = root;
            int leftChild = 2 * (root - offset) + 1 + offset;
            int rightChild = 2 * (root - offset) + 2 + offset;

            if (leftChild < offset + size && list[leftChild].CompareTo(list[largest]) > 0)
                largest = leftChild;

            if (rightChild < offset + size && list[rightChild].CompareTo(list[largest]) > 0)
                largest = rightChild;

            if (largest != root)
            {
                Swap(list, root, largest);
                Heapify(list, size, largest, offset);
            }
        }


        // Algoritmo Bitonic Sort
        // Funciona solo para tamaños que sean potencias de 2.
        // Memory: O(n log^2 n)
        public static void BitonicSort<T>(List<T> list) where T : IComparable<T>
        {
            Debug.Log("BitonicSort - Lista inicial: " + string.Join(", ", list));
            BitonicSortRecursive(list, 0, list.Count, true);
            Debug.Log("BitonicSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void BitonicSortRecursive<T>(List<T> list, int from, int count, bool ascending)
            where T : IComparable<T>
        {
            if (count <= 1) return;

            int halfCount = count / 2;


            BitonicSortRecursive(list, from, halfCount, true);

            BitonicSortRecursive(list, from + halfCount, halfCount, false);

            BitonicMerge(list, from, count, ascending);
        }

        private static void BitonicMerge<T>(List<T> list, int from, int count, bool ascending) where T : IComparable<T>
        {
            if (count <= 1) return;

            int halfCount = count / 2;

            for (int i = from; i < from + halfCount; i++)
            {
                if ((ascending && list[i].CompareTo(list[i + halfCount]) > 0) ||
                    (!ascending && list[i].CompareTo(list[i + halfCount]) < 0))
                {
                    Swap(list, i, i + halfCount);
                }
            }

            BitonicMerge(list, from, halfCount, ascending);
            BitonicMerge(list, from + halfCount, halfCount, ascending);
        }


        // Algoritmo Quick Sort:
        // Memory: O(n^2) en el peor caso, pero O(n log n) en el promedio
        // Divide la lista en sublistas en torno a un pivote, de manera que los elementos menores
        // están antes del pivote y los mayores después. Luego, ordena recursivamente cada sublista.
        public static void QuickSort<T>(List<T> list) where T : IComparable<T>
        {
            Debug.Log("QuickSort - Lista inicial: " + string.Join(", ", list));
            QuickSortRecursive(list, 0, list.Count - 1);
            Debug.Log("QuickSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void QuickSortRecursive<T>(List<T> list, int left, int right) where T : IComparable<T>
        {
            if (left >= right) return;

            int pivotIndex = Partition(list, left, right);
            QuickSortRecursive(list, left, pivotIndex - 1);
            QuickSortRecursive(list, pivotIndex + 1, right);
        }


        // Algoritmo Intro Sort:
        // Comienza usando QuickSort y cambia a HeapSort si la profundidad de recursión excede el límite permitido,
        // evitando el mal rendimiento de QuickSort en sus peores casos. Usa Insertion Sort para listas pequeñas.
        public static void IntroSort<T>(List<T> list) where T : IComparable<T>
        {
            Debug.Log("IntroSort - Lista inicial: " + string.Join(", ", list));

            int depthLimit = 2 * (int)Mathf.Log(list.Count);
            IntroSortRecursive(list, 0, list.Count - 1, depthLimit);

            Debug.Log("IntroSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void IntroSortRecursive<T>(List<T> list, int left, int right, int depthLimit)
            where T : IComparable<T>
        {
            int size = right - left + 1;

            if (size < 16)
            {
                InsertionSort(list, left, right);
                return;
            }

            if (depthLimit == 0)
            {
                HeapSort(list, left, right);
                return;
            }

            int pivot = Partition(list, left, right);
            IntroSortRecursive(list, left, pivot - 1, depthLimit - 1);
            IntroSortRecursive(list, pivot + 1, right, depthLimit - 1);
        }

        public static void AdaptiveSort(List<T> list)
        {
            Debug.Log("AdaptiveMergeSort - Lista inicial: " + string.Join(", ", list));
            RecursiveAdaptiveMergeSort(list, 0, list.Count - 1);
            Debug.Log("AdaptiveMergeSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void RecursiveAdaptiveMergeSort<T>(List<T> list, int from, int to) where T : IComparable<T>
        {
            if (from >= to) return;

            int middle = from + (to - from) / 2;

            if (IsSorted(list, from, to, 0))
            {
                Inverse(list, from, to);
            }
            else
            {
                if (IsSorted(list, from, middle, 0))
                    Inverse(list, from, middle);
                if (IsSorted(list, middle + 1, to, 0))
                    Inverse(list, middle + 1, to);
            }

            if (!IsSorted(list, from, to, 1))
            {
                if (!IsSorted(list, from, middle, 1))
                    RecursiveAdaptiveMergeSort(list, from, middle);

                if (!IsSorted(list, middle + 1, to, 1))
                    RecursiveAdaptiveMergeSort(list, middle + 1, to);

                AdaptiveMerge(list, from, middle, to);
            }
        }

        private static void AdaptiveMerge<T>(List<T> list, int from, int middle, int to) where T : IComparable<T>
        {
            int left = from, right = middle + 1;
            
            List<T> temp = new List<T>();

            while (left <= middle && right <= to)
            {
                if (list[left].CompareTo(list[right]) <= 0)
                    temp.Add(list[left++]);
                else
                    temp.Add(list[right++]);
            }

            while (left <= middle)
                temp.Add(list[left++]);

            while (right <= to)
                temp.Add(list[right++]);

            for (int i = 0; i < temp.Count; i++)
            {
                list[from + i] = temp[i];
            }
        }


        public static void RadixLSDSort(List<T> list)
        {
            Debug.Log("RadixLSDSort - Lista inicial: " + string.Join(", ", list));
            List<uint> ints = GetIntsFromT(list);
            uint biggestNum = ints[0];
            for (int i = 1; i < ints.Count; i++)
            {
                if (ints[i] > biggestNum)
                    biggestNum = ints[i];
            }

            int maxDigits = GetNumberOfDigits(Convert.ToInt32(biggestNum));

            uint digitMultiplier = 10;
            List<int>[] buckets = new List<int>[10];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            for (int i = 0; i < maxDigits; i++)
            {
                for (int j = 0; j < ints.Count; j++)
                {
                    uint digitResult = ints[j] / (digitMultiplier / 10) % 10;
                    buckets[digitResult].Add(j);
                }

                List<T> auxList = CloneList(list);
                List<uint> auxNumbers = Sorter<uint>.CloneList(ints);

                list.Clear();
                ints.Clear();

                for (int bucketIndex = 0; bucketIndex < buckets.Length; bucketIndex++)
                {
                    for (int j = 0; j < buckets[bucketIndex].Count; j++)
                    {
                        list.Add(auxList[buckets[bucketIndex][j]]);
                        ints.Add(auxNumbers[buckets[bucketIndex][j]]);
                    }
                }

                digitMultiplier *= 10;
                for (int j = 0; j < buckets.Length; j++)
                    buckets[j].Clear();
                Debug.Log("RadixLSDSort - Lista ordenada: " + string.Join(", ", list));
            }
        }


        public static void RadixMSDSort(List<T> list)
        {
            Debug.Log("RadixMSDSort - Lista inicial: " + string.Join(", ", list));

            List<uint> ints = GetIntsFromT(list);

            uint biggestNum = ints[0];

            for (int i = 1; i < ints.Count; i++)
            {
                if (ints[i] > biggestNum)
                    biggestNum = ints[i];
            }

            int maxDigits = GetNumberOfDigits(Convert.ToInt32(biggestNum));

            RecursiveRadixMSD(list, ints, 0, list.Count - 1, maxDigits);

            Debug.Log("RadixMSDSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void RecursiveRadixMSD(List<T> list, List<uint> ints, int from, int to, int digit)
        {
            if (to <= from)
            {
                return;
            }

            List<int>[] buckets = new List<int>[10];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            for (int j = from; j <= to; j++)
            {
                int digitResult = (int)(ints[j] / Mathf.Pow(10, digit - 1) % 10);
                buckets[digitResult].Add(j);
            }

            List<T> auxList = CloneList(list);
            List<uint> auxNumbers = Sorter<uint>.CloneList(ints);

            int iterator = from;
            for (int bucketIndex = 0; bucketIndex < buckets.Length; bucketIndex++)
            {
                for (int i = 0; i < buckets[bucketIndex].Count; i++, iterator++)
                {
                    auxList[iterator] = list[buckets[bucketIndex][i]];
                    auxNumbers[iterator] = ints[buckets[bucketIndex][i]];
                }
            }

            for (int i = from; i <= to; i++)
            {
                list[i] = auxList[i];
                ints[i] = auxNumbers[i];
            }

            int prevPos = from;

            foreach (var element in buckets)
            {
                if (element.Count < 1)
                    continue;

                RecursiveRadixMSD(list, ints, prevPos, prevPos + element.Count - 1, digit - 1);
                prevPos += element.Count;
            }
        }

        #endregion

        #region Utilities

        private static bool IsSorted<T>(List<T> list, int from, int to, int direction) where T : IComparable<T>
        {
            for (int i = from; i < to; i++)
            {
                if ((direction == 0 && list[i].CompareTo(list[i + 1]) > 0) ||
                    (direction == 1 && list[i].CompareTo(list[i + 1]) < 0))
                    return false;
            }

            return true;
        }

        public static void Shuffle(List<T> list)
        {
            T aux;
            for (int i = 0; i < list.Count; i++)
            {
                var randomIndex = Random.Range(0, list.Count);
                aux = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = aux;
            }
        }

        // Método para intercambiar dos elementos en la lista
        private static void Swap<T>(List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }

        private static int Compare(T a, T b)
        {
            return a.CompareTo(b);
        }

        // Divide la lista y coloca el pivote en su posición correcta, separando los elementos
        // menores y mayores alrededor de él.
        private static int Partition<T>(List<T> list, int left, int right) where T : IComparable<T>
        {
            T pivotValue = list[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (list[j].CompareTo(pivotValue) < 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, right);
            return i + 1;
        }

        private static void Inverse<T>(List<T> list, int from, int to)
        {
            while (from < to)
            {
                Swap(list, from, to);
                from++;
                to--;
            }
        }

        private static uint GetIntFromBitArray(List<uint> bits)
        {
            uint result = 0;
            for (int i = bits.Count - 1; i >= 0; i--)
            {
                result *= 2;
                result += bits[i];
            }

            return result;
        }

        private static List<uint> GetIntsFromT(List<T> list)
        {
            List<BitArray> bitArrays = new List<BitArray>();

            List<uint> ints = new List<uint>();
            for (int i = 0; i < list.Count; i++)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, list[i]);
                bitArrays.Add(new BitArray(ms.ToArray()));
            }

            for (int i = 0; i < list.Count; i++)
            {
                List<uint> bits = new List<uint>();
                for (int j = bitArrays[i].Count - 40; j < bitArrays[i].Count - 8; j++)
                {
                    bits.Add(Convert.ToUInt32(bitArrays[i][j]));
                }

                ints.Add(GetIntFromBitArray(bits));
            }

            return ints;
        }

        private static int GetNumberOfDigits(int number)
        {
            if (number < 10)
                return 1;

            return 1 + GetNumberOfDigits(number / 10);
        }

        private static List<T> CloneList(List<T> list)
        {
            List<T> auxList = new List<T>();
            for (int k = 0; k < list.Count; k++)
            {
                auxList.Add(list[k]);
            }

            return auxList;
        }

        #endregion
    }
}