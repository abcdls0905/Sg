using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;
using Game;

public sealed partial class GameContext
{
    private Stack<IComponent> GetComponentPool(int index)
    {
        var componentPool = componentPools[index];
        if (componentPool == null)
        {
            componentPool = new Stack<IComponent>();
            componentPools[index] = componentPool;
        }

        return componentPool;
    }

    public T NewComponent<T>() where T : IComponent, new()
    {
        var id = GameComponentsLookup.componentTypesDic[typeof(T)];
        var componentPool = GetComponentPool(id);
        return componentPool.Count > 0 ? (T)componentPool.Pop() : new T();
    }

    public void AddComponent(IComponent comp)
    {
        SingleEntity.AddComponent(comp);
    }

    public void RemoveComponent<T>()
    {
        SingleEntity.RemoveComponent<T>();
    }

    public T GetComponent<T>() where T : IComponent
    {
        return SingleEntity.GetComponent<T>();
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return SingleEntity.HasComponent<T>();
    }
}

public sealed partial class GameEntity
{
    public void AddComponent(IComponent comp)
    {
        var id = GameComponentsLookup.componentTypesDic[comp.GetType()];
        AddComponent(id, comp);
    }

    public void RemoveComponent<T>()
    {
        var id = GameComponentsLookup.componentTypesDic[typeof(T)];
        RemoveComponent(id);
    }

    public T GetComponent<T>() where T : IComponent
    {
        var id = GameComponentsLookup.componentTypesDic[typeof(T)];
        return (T)GetComponent(id);
    }

    public bool HasComponent<T>() where T : IComponent
    {
        var id = GameComponentsLookup.componentTypesDic[typeof(T)];
        return HasComponent(id);
    }

    public override void Destroy()
    {
        if (hasPlayer)
            Util.RemovePlayer(this);
        else if (hasBox)
            Util.RemoveBox(this);
        base.Destroy();
    }
}

public sealed partial class GameMatcher
{
    static Dictionary<int, Entitas.Matcher<GameEntity>> _matchers = new Dictionary<int, Entitas.Matcher<GameEntity>>();
    public static Entitas.Matcher<GameEntity> GetMatcher<T>() where T : IComponent
    {
        var id = GameComponentsLookup.componentTypesDic[typeof(T)];
        Entitas.Matcher<GameEntity> matcher;
        if (!_matchers.TryGetValue(id, out matcher))
        {
            matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(id);
            matcher.componentNames = GameComponentsLookup.componentNames;
            _matchers.Add(id, matcher);
        }
        return matcher;
    }

}

namespace Entitas
{
    public partial class Matcher<TEntity>
    {
        public static IAllOfMatcher<TEntity> AllOf(params Type[] indices)
        {
            int[] indicesInt = new int[indices.Length];
            for (int i = 0; i < indices.Length; ++i)
            {
                var id = GameComponentsLookup.componentTypesDic[indices[i]];
                indicesInt[i] = id;
            }
            return AllOf(indicesInt);
        }
    }
}