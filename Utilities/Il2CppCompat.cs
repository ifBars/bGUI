using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if IL2CPP
using Il2CppInterop.Runtime;
#endif

namespace bGUI.Utilities
{
    internal static class Il2CppCompat
    {
        public static UnityAction ToUnityAction(Action action)
        {
#if IL2CPP
            return DelegateSupport.ConvertDelegate<UnityAction>(action)!;
#else
            return new UnityAction(action);
#endif
        }

        public static UnityAction<T> ToUnityAction<T>(Action<T> action)
        {
#if IL2CPP
            return DelegateSupport.ConvertDelegate<UnityAction<T>>(action)!;
#else
            return new UnityAction<T>(action);
#endif
        }

        public static void RemoveLayoutGroups(GameObject gameObject)
        {
            RemoveComponent(gameObject.GetComponent<HorizontalLayoutGroup>());
            RemoveComponent(gameObject.GetComponent<VerticalLayoutGroup>());
            RemoveComponent(gameObject.GetComponent<GridLayoutGroup>());
        }

        public static bool HasEventSystem()
        {
#if IL2CPP
            return UnityEngine.Object.FindObjectOfType(Il2CppType.Of<EventSystem>()) != null;
#else
            return UnityEngine.Object.FindObjectOfType<EventSystem>() != null;
#endif
        }

        private static void RemoveComponent(Component? component)
        {
            if (component == null)
            {
                return;
            }

            UnityEngine.Object.DestroyImmediate(component);
        }
    }
}
