using UnityEngine;
using bGUI.Core.Factory;

namespace bGUI.Components
{
	/// <summary>
	/// Fluent builder for a toggle group container.
	/// </summary>
	public class ToggleGroupBuilder
	{
		private readonly ToggleGroupWrapper _group;
		private readonly bool _usePooling;

		public ToggleGroupBuilder(Transform? parent, bool usePooling = false)
		{
			_usePooling = usePooling;
			_group = UIFactory.Instance.CreateToggleGroup(parent, "ToggleGroup", _usePooling);
		}

		// Backward compatibility constructor
		public ToggleGroupBuilder(Transform? parent)
		{
			_usePooling = false;
			_group = UIFactory.Instance.CreateToggleGroup(parent, "ToggleGroup", _usePooling);
		}

		public ToggleGroupBuilder SetSize(float width, float height)
		{
			_group.RectTransform.sizeDelta = new Vector2(width, height);
			return this;
		}

		public ToggleGroupBuilder SetPosition(float x, float y)
		{
			_group.RectTransform.anchoredPosition = new Vector2(x, y);
			return this;
		}

		public ToggleGroupBuilder SetAnchor(float anchorX, float anchorY)
		{
			_group.RectTransform.anchorMin = _group.RectTransform.anchorMax = new Vector2(anchorX, anchorY);
			return this;
		}

		public ToggleGroupBuilder SetPivot(float pivotX, float pivotY)
		{
			_group.RectTransform.pivot = new Vector2(pivotX, pivotY);
			return this;
		}

		public ToggleWrapper AddToggle(string label, bool isOn = false)
		{
			return _group.AddToggle(label, isOn);
		}

		public ToggleGroupWrapper Build() => _group;
	}
}


