using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TP3
{
    public static class Sorter<T> where T : IComparable<T>
    {
        private static int _iterationCount = 0;
        private static int _comparissonCount = 0;

        #region SortingMethods

        // Algoritmo Bitonic Sort:
        public static void BitonicSort(List<T> list)
        {
            Debug.Log("BitonicSort - Lista inicial: " + string.Join(", ", list));
            BitonicSortRecursive(list, true);
            Debug.Log("BitonicSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Método auxiliar recursivo para BitonicSort sin rango específico.
        private static void BitonicSortRecursive(List<T> list, bool ascending)
        {
            if (list.Count <= 1) return;

            int k = list.Count / 2;
            List<T> leftList = list.GetRange(0, k);
            List<T> rightList = list.GetRange(k, list.Count - k);

            BitonicSortRecursive(leftList, true);
            BitonicSortRecursive(rightList, false);
            BitonicMerge(list, ascending);
        }

        // Fusión bitónica de dos subsecuencias sin rango específico.
        private static void BitonicMerge(List<T> list, bool ascending)
        {
            if (list.Count <= 1) return;

            int k = list.Count / 2;
            for (int i = 0; i < k; i++)
            {
                if ((ascending && Compare(list[i], list[i + k]) > 0) ||
                    (!ascending && Compare(list[i], list[i + k]) < 0))
                {
                    Swap(list, i, i + k);
                }
            }

            List<T> leftList = list.GetRange(0, k);
            List<T> rightList = list.GetRange(k, list.Count - k);

            BitonicMerge(leftList, ascending);
            BitonicMerge(rightList, ascending);

            list.Clear();
            list.AddRange(leftList);
            list.AddRange(rightList);
        }


        // Algoritmo Selection Sort:
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


        // Algoritmo Cocktail Shaker Sort:
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

        // Algoritmo Quick Sort:
        // Divide la lista en sublistas en torno a un pivote, de manera que los elementos menores
        // están antes del pivote y los mayores después. Luego, ordena recursivamente cada sublista.
        public static void QuickSort(List<T> list)
        {
            Debug.Log("QuickSort - Lista inicial: " + string.Join(", ", list));
            QuickSortRecursive(list);
            Debug.Log("QuickSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Método auxiliar recursivo para QuickSort sin parámetros de rango.
        private static void QuickSortRecursive(List<T> list)
        {
            if (list.Count <= 1) return;

            int pivot = Partition(list);
            List<T> leftList = list.GetRange(0, pivot);
            List<T> rightList = list.GetRange(pivot + 1, list.Count - pivot - 1);

            QuickSortRecursive(leftList);
            QuickSortRecursive(rightList);

            list.Clear();
            list.AddRange(leftList);
            list.Add(list[pivot]);
            list.AddRange(rightList);
        }

        private static void RadixLSDSort(List<T> list)
        {
            Debug.Log("Implementation needed");
        }

        // Algoritmo Shell Sort:
        // Ordena la lista utilizando intervalos decrecientes (gap) para reducir el número de comparaciones.
        // A medida que los intervalos se reducen, el algoritmo aproxima un ordenamiento tipo Insertion Sort
        // en sublistas cada vez más grandes hasta que el intervalo es 1.
        public static void ShellSort(List<T> list)
        {
            Debug.Log("ShellSort - Lista inicial: " + string.Join(", ", list));

            int n = list.Count;
            // Comienza con un intervalo grande y lo reduce progresivamente.
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Aplica Insertion Sort en las sublistas definidas por el intervalo actual.
                for (int i = gap; i < n; i++)
                {
                    T temp = list[i];
                    int j = i;

                    // Desplaza los elementos mayores al valor temporal hacia adelante.
                    while (j >= gap && Compare(list[j - gap], temp) > 0)
                    {
                        list[j] = list[j - gap];
                        j -= gap;
                    }

                    list[j] = temp; // Coloca el elemento temporal en su posición final.
                }
            }

            Debug.Log("ShellSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Algoritmo Bogo Sort:
        // Ordena la lista aleatoriamente hasta que esté en el orden correcto.
        public static void BogoSort(List<T> list)
        {
            Debug.Log("BogoSort - Lista inicial: " + string.Join(", ", list));

            // Repite hasta que la lista esté ordenada.
            while (!IsSorted(list))
            {
                Shuffle(list); // Mezcla aleatoriamente la lista.
            }

            Debug.Log("BogoSort - Lista ordenada: " + string.Join(", ", list));
        }

        private static void RadixMSDSort(List<T> list)
        {
            Debug.Log("Implementation needed");
        }

        // Algoritmo Intro Sort:
        // Comienza usando QuickSort y cambia a HeapSort si la profundidad de recursión excede el límite permitido,
        // evitando el mal rendimiento de QuickSort en sus peores casos. Usa Insertion Sort para listas pequeñas.
        public static void IntroSort(List<T> list)
        {
            Debug.Log("IntroSort - Lista inicial: " + string.Join(", ", list));

            int depthLimit = 2 * (int)Mathf.Log(list.Count); // Define el límite de recursión.
            IntroSortRecursive(list, depthLimit);

            Debug.Log("IntroSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Método auxiliar recursivo para IntroSort sin parámetros de rango.
        private static void IntroSortRecursive(List<T> list, int depthLimit)
        {
            if (list.Count < 16)
            {
                InsertionSort(list); // Usa Insertion Sort para listas pequeñas.
                return;
            }

            if (depthLimit == 0)
            {
                HeapSort(list); // Cambia a HeapSort si la profundidad es demasiado alta.
                return;
            }

            int pivot = Partition(list);
            List<T> leftList = list.GetRange(0, pivot);
            List<T> rightList = list.GetRange(pivot + 1, list.Count - pivot - 1);

            IntroSortRecursive(leftList, depthLimit - 1);
            IntroSortRecursive(rightList, depthLimit - 1);
            list.Clear();
            list.AddRange(leftList);
            list.Add(list[pivot]);
            list.AddRange(rightList);
        }

        private static void AdaptiveSort(List<T> list)
        {
            Debug.Log("Implementation needed");
        }

        // Algoritmo Bubble Sort:
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

        // Algoritmo Gnome Sort:
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

        // Algoritmo Merge Sort:
        // Divide la lista en mitades y ordena recursivamente cada mitad. Luego, fusiona las dos mitades ordenadas.
        public static void MergeSort(List<T> list)
        {
            Debug.Log("MergeSort - Lista inicial: " + string.Join(", ", list));
            var sortedList = MergeSortRecursive(list);
            list.Clear();
            list.AddRange(sortedList);
            Debug.Log("MergeSort - Lista ordenada: " + string.Join(", ", list));
        }


        // Algoritmo Heap Sort:
        public static void HeapSort(List<T> list)
        {
            Debug.Log("HeapSort - Lista inicial: " + string.Join(", ", list));

            for (int i = list.Count / 2 - 1; i >= 0; i--)
            {
                Heapify(list, list.Count, i);
            }

            for (int i = list.Count - 1; i > 0; i--)
            {
                Swap(list, 0, i);
                Heapify(list, i, 0);
            }

            Debug.Log("HeapSort - Lista ordenada: " + string.Join(", ", list));
        }

        // Método auxiliar Heapify para HeapSort.
        private static void Heapify(List<T> list, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && Compare(list[left], list[largest]) > 0)
                largest = left;

            if (right < n && Compare(list[right], list[largest]) > 0)
                largest = right;

            if (largest != i)
            {
                Swap(list, i, largest);
                Heapify(list, n, largest);
            }
        }

        // Divide la lista y coloca el pivote en su posición correcta, separando los elementos
        // menores y mayores alrededor de él.
        private static int Partition(List<T> list)
        {
            T pivot = list[list.Count - 1];
            int i = -1;
            for (int j = 0; j < list.Count - 1; j++)
            {
                if (Compare(list[j], pivot) <= 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, list.Count - 1);
            return i + 1;
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

        // Fusiona dos listas ordenadas en una lista completamente ordenada.
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

        // Algoritmo Insertion Sort:
        // Toma cada elemento de la lista y lo inserta en su posición correcta en una sublista ordenada al inicio.
        public static void InsertionSort(List<T> list)
        {
            Debug.Log("InsertionSort - Lista inicial: " + string.Join(", ", list));

            for (int i = 1; i < list.Count; i++)
            {
                T key = list[i];
                int j = i - 1;
                while (j >= 0 && Compare(list[j], key) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }

            Debug.Log("InsertionSort - Lista ordenada: " + string.Join(", ", list));
        }

        #endregion

        #region Utilities

        public static bool IsSorted(List<T> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i - 1].CompareTo(list[i]) > 0)
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

        private static void Swap(List<T> list, int firstIndex, int secondIndex)
        {
            (list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);
        }

        private static int Compare(T a, T b)
        {
            return a.CompareTo(b);
        }

        #endregion
    }
}