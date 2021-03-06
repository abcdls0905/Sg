//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.AIComponent aI { get { return (Game.AIComponent)GetComponent(GameComponentsLookup.AI); } }
    public bool hasAI { get { return HasComponent(GameComponentsLookup.AI); } }

    public Game.AIComponent AddAI() {
        var index = GameComponentsLookup.AI;
        var component = CreateComponent<Game.AIComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveAI() {
        RemoveComponent(GameComponentsLookup.AI);
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

    static Entitas.IMatcher<GameEntity> _matcherAI;

    public static Entitas.IMatcher<GameEntity> AI {
        get {
            if (_matcherAI == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AI);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAI = matcher;
            }

            return _matcherAI;
        }
    }
}
