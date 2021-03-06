//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.BurnComponent burn { get { return (Game.BurnComponent)GetComponent(GameComponentsLookup.Burn); } }
    public bool hasBurn { get { return HasComponent(GameComponentsLookup.Burn); } }

    public Game.BurnComponent AddBurn() {
        var index = GameComponentsLookup.Burn;
        var component = CreateComponent<Game.BurnComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveBurn() {
        RemoveComponent(GameComponentsLookup.Burn);
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

    static Entitas.IMatcher<GameEntity> _matcherBurn;

    public static Entitas.IMatcher<GameEntity> Burn {
        get {
            if (_matcherBurn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Burn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBurn = matcher;
            }

            return _matcherBurn;
        }
    }
}
