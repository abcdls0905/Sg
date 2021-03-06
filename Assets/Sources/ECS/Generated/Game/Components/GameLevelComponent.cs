//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public Game.LevelComponent level { get { return SingleEntity.level; } }
    public bool hasLevel { get { return SingleEntity.hasLevel; } }

    public Game.LevelComponent AddLevel() {
        var entity = SingleEntity;
        var component = entity.AddLevel();
        return component;
    }

    public void RemoveLevel() {
        SingleEntity.RemoveLevel();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.LevelComponent level { get { return (Game.LevelComponent)GetComponent(GameComponentsLookup.Level); } }
    public bool hasLevel { get { return HasComponent(GameComponentsLookup.Level); } }

    public Game.LevelComponent AddLevel() {
        var index = GameComponentsLookup.Level;
        var component = CreateComponent<Game.LevelComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveLevel() {
        RemoveComponent(GameComponentsLookup.Level);
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

    static Entitas.IMatcher<GameEntity> _matcherLevel;

    public static Entitas.IMatcher<GameEntity> Level {
        get {
            if (_matcherLevel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Level);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLevel = matcher;
            }

            return _matcherLevel;
        }
    }
}
