using System.Collections.Generic;
using System.Linq;
using HCPJ3.Tools.Extensions;

namespace HCPJ3.Tools
{
    /// <summary>
    /// Collection for pseudo random generation.
    /// Items are shuffle and picked in order to avoid duplicate picks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomList<T>
    {
        private readonly List<T> _items;
        private int _index;

        public RandomList(IEnumerable<T> items)
        {
            _items = items.ToList();
            Reset();
        }

        public T Next()
        {
            T item = _items[_index];
            _index += 1;

            if (_index >= _items.Count)
            {
                Reset();
            }

            return item;
        }

        private void Reset()
        {
            _index = 0;
            _items.Shuffle();
        }
    }
}
