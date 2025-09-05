using System.Collections.Generic;
using bGUI.Core.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace bGUI.Components
{
	/// <summary>
	/// Lightweight container for a list of Toggle elements with simple vertical layout.
	/// </summary>
	public class ToggleGroupWrapper : UIElementBase
	{
		private readonly List<ToggleWrapper> _toggles = new List<ToggleWrapper>();
		private VerticalLayoutGroup _layout = null!;
		private ContentSizeFitter _fitter = null!;

		public IReadOnlyList<ToggleWrapper> Toggles => _toggles;

		public ToggleGroupWrapper(Transform? parent, string name = "ToggleGroup") : base(parent, name)
		{
			_rectTransform.sizeDelta = new Vector2(260f, 220f);
			_layout = _gameObject.AddComponent<VerticalLayoutGroup>();
			_layout.spacing = 8f; // base spacing; each item sets its own height
			_layout.childAlignment = TextAnchor.UpperLeft;
			_layout.childForceExpandWidth = true;
			_layout.childForceExpandHeight = false;
			_layout.padding = new RectOffset(12, 12, 12, 12); // Add padding around edges

			_fitter = _gameObject.AddComponent<ContentSizeFitter>();
			_fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		}

		public ToggleWrapper AddToggle(string label, bool isOn = false)
		{
			var t = new ToggleWrapper(_rectTransform, "Toggle", label, isOn);
			// Let layout control size; ensure sensible width and height
			t.RectTransform.anchorMin = new Vector2(0f, 1f);
			t.RectTransform.anchorMax = new Vector2(1f, 1f);
			t.RectTransform.pivot = new Vector2(0f, 1f);
			t.RectTransform.sizeDelta = new Vector2(0f, 32f);
			var le = t.GameObject.AddComponent<LayoutElement>();
			le.minHeight = 28f;
			le.preferredHeight = 32f;
			
			// Improve toggle colors for better visibility
			t.SetColors(
				new Color(0.25f, 0.25f, 0.3f, 1f),
				new Color(0.35f, 0.35f, 0.4f, 1f),
				new Color(0.2f, 0.2f, 0.25f, 1f),
				new Color(0.5f, 0.5f, 0.5f, 0.5f)
			);
			
			// Set label color for better contrast
			t.SetLabelColor(Color.white);
			
			_toggles.Add(t);
			return t;
		}
	}
}


