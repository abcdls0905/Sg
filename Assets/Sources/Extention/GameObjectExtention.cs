using UnityEngine;

public static class GameObjectExtension
{
    // Unity�������Ż���GetComponent���������gc
    // mono�ڴ���л����GC
    public static T MakeSureComponent<T>(this GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
            comp = obj.AddComponent<T>();
        return comp;
    }
}