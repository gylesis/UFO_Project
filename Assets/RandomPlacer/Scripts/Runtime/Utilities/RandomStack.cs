namespace RandomPlacer.Utilities
{
	public class RandomStack<T>
	{
		private readonly T[] _items;
		private int _index;

		public RandomStack(T[] items)
		{
			_items = items;
			_index = 0;
		}

		public T GetNext()
		{
			_index %= _items.Length;

			return _items[_index++];
		}
	}
}
