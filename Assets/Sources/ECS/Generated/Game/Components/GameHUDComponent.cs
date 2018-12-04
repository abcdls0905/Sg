//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public Game.HUDComponent hUD { get { return SingleEntity.hUD; } }
    public bool hasHUD { get { return SingleEntity.hasHUD; } }

    public Game.HUDComponent AddHUD() {
        var entity = SingleEntity;
        var component = entity.AddHUD();
        return component;
    }

    public void RemoveHUD() {
        SingleEntity.RemoveHUD();
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

    public Game.HUDComponent hUD { get { return (Game.HUDComponent)GetComponent(GameComponentsLookup.HUD); } }
    public bool hasHUD { get { return HasComponent(GameComponentsLookup.HUD); } }

    public Game.HUDComponent AddHUD() {
        var index = GameComponentsLookup.HUD;
        var component = CreateComponent<Game.HUDComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveHUD() {
        RemoveComponent(GameComponentsLookup.HUD);
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

    static Entitas.IMatcher<GameEntity> _matcherHUD;

    public static Entitas.IMatcher<GameEntity> HUD {
        get {
            if (_matcherHUD == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HUD);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHUD = matcher;
            }

            return _matcherHUD;
        }
    }
}
