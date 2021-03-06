//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public Game.ComboComponent combo { get { return SingleEntity.combo; } }
    public bool hasCombo { get { return SingleEntity.hasCombo; } }

    public Game.ComboComponent AddCombo() {
        var entity = SingleEntity;
        var component = entity.AddCombo();
        return component;
    }

    public void RemoveCombo() {
        SingleEntity.RemoveCombo();
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

    public Game.ComboComponent combo { get { return (Game.ComboComponent)GetComponent(GameComponentsLookup.Combo); } }
    public bool hasCombo { get { return HasComponent(GameComponentsLookup.Combo); } }

    public Game.ComboComponent AddCombo() {
        var index = GameComponentsLookup.Combo;
        var component = CreateComponent<Game.ComboComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveCombo() {
        RemoveComponent(GameComponentsLookup.Combo);
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

    static Entitas.IMatcher<GameEntity> _matcherCombo;

    public static Entitas.IMatcher<GameEntity> Combo {
        get {
            if (_matcherCombo == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Combo);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCombo = matcher;
            }

            return _matcherCombo;
        }
    }
}
