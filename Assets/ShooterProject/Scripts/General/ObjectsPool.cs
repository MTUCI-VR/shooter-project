using System.Collections.Generic;
using UnityEngine;
namespace ShooterProject.Scripts.General
{
	public class ObjectsPool<T> where T: MonoBehaviour
	{
		#region Fields

		private List<T> _objects;

		private bool _autoExpand;
		private bool _activeByDefault;
		private Transform _container;
		private T _objectPrefab;
		private bool _initialized;

		#endregion

		#region Properties

		public int MaxSize { get; }

		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="maxSize">Максимальный размер пула</param>
		/// <param name="autoExpand">Использовать ли авторасширение</param>
		/// <param name="activeByDefault">Параметр active объектов при инициализации или добавлении в пул</param>
		/// <param name="prefab">Префаб объекта</param>
		/// <param name="container">Контейнер пула</param>
		public ObjectsPool(int maxSize, bool autoExpand,bool activeByDefault, T prefab, Transform container)
		{
			MaxSize = maxSize;
			_autoExpand = autoExpand;
			_activeByDefault = activeByDefault;
			_objectPrefab = prefab;
			_container = container;

			InitPool();
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Проверяет наличие свободного элемента и возвращает его
		/// </summary>
		/// <param name="element">Свободный элемент пула</param>
		/// <returns>Возвращает true если удалось найти свободный элемент, иначе возвращает false</returns>
		public bool TryGetFreeElement(out T element)
		{
			if (!_initialized)
				throw new System.Exception($"Пул объектов типа {typeof(T)} не был инициализирован");
			foreach (var poolObject in _objects)
			{
				if (poolObject.gameObject.activeSelf)
				{
					element = poolObject;
					return true;
				}
			}

			if (_autoExpand)
			{
				element = AddObject();
				return true;
			}

			element = null;
			return false;

		}

		#endregion

		#region Private Methods

		private void InitPool()
		{
			_objects = new List<T>();
			for (var i = 0; i < MaxSize; i++)
			{
				AddObject();
			}
			_initialized = true;
		}
		private T AddObject()
		{
			var newObject = GameObject.Instantiate(_objectPrefab, _container);
			newObject.gameObject.SetActive(_activeByDefault);
			_objects.Add(newObject);
			return newObject;
		}

		#endregion
	}
}
