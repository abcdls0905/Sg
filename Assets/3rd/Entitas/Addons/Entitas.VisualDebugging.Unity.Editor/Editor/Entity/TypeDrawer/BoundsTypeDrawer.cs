using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor {

    public class BoundsTypeDrawer : ITypeDrawer {

        public bool HandlesType(Type type) {
            return type == typeof(Bounds);
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {
            return EditorGUILayout.BoundsField(memberName, (Bounds)value);
        }
    }
}
