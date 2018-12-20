//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.LimitComponent limit { get { return (Game.LimitComponent)GetComponent(GameComponentsLookup.Limit); } }
    public bool hasLimit { get { return HasComponent(GameComponentsLookup.Limit); } }

    public Game.LimitComponent AddLimit() {
        var index = GameComponentsLookup.Limit;
        var component = CreateComponent<Game.LimitComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveLimit() {
        RemoveComponent(GameComponentsLookup.Limit);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherLimit;

    public static Entitas.IMatcher<GameEntity> Limit {
        get {
            if (_matcherLimit == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Limit);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLimit = matcher;
            }

            return _matcherLimit;
        }
    }
}