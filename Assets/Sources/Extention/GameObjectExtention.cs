using UnityEngine;

public static class GameObjectExtension
{
    // Unity进行了优化，GetComponent并不会产生gc
    // mono内存持有会产生GC
    public static T MakeSureComponent<T>(this GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
            comp = obj.AddComponent<T>();
        return comp;
    }
}