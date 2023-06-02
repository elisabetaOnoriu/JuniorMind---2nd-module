namespace Delegates
{
    internal abstract class SourceSorter<TElement>
    {
        internal abstract void GenerateKeys(List<TElement> elements);

        internal abstract int CompareKeys(int index1, int index2);

        internal int[] Sort(List<TElement> elements)
        {
            GenerateKeys(elements);
            int[] map = new int[elements.Count];
            for (int i = 0; i < elements.Count; i++)
            {
                map[i] = i;
            }

            InsertionSort(map);
            return map;
        }

        void InsertionSort(int[] map)
        {
            for (int i = 1; i < map.Length; i++)
            {
                for (int j = i; j > 0 && CompareKeys(map[j - 1], map[j]) == 1; j--)
                {
                    (map[j - 1], map[j]) = (map[j], map[j - 1]);
                }
            }
        }
    }
}