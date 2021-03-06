//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.GroundItemComponent groundItem { get { return (Game.GroundItemComponent)GetComponent(GameComponentsLookup.GroundItem); } }
    public bool hasGroundItem { get { return HasComponent(GameComponentsLookup.GroundItem); } }

    public Game.GroundItemComponent AddGroundItem() {
        var index = GameComponentsLookup.GroundItem;
        var component = CreateComponent<Game.GroundItemComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveGroundItem() {
        RemoveComponent(GameComponentsLookup.GroundItem);
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

    static Entitas.IMatcher<GameEntity> _matcherGroundItem;

    public static Entitas.IMatcher<GameEntity> GroundItem {
        get {
            if (_matcherGroundItem == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GroundItem);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGroundItem = matcher;
            }

            return _matcherGroundItem;
        }
    }
}
