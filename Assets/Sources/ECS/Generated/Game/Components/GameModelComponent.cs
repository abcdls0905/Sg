//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.ModelComponent model { get { return (Game.ModelComponent)GetComponent(GameComponentsLookup.Model); } }
    public bool hasModel { get { return HasComponent(GameComponentsLookup.Model); } }

    public Game.ModelComponent AddModel() {
        var index = GameComponentsLookup.Model;
        var component = CreateComponent<Game.ModelComponent>(index);

        AddComponent(index, component);
        return component;
    }

    public void RemoveModel() {
        RemoveComponent(GameComponentsLookup.Model);
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

    static Entitas.IMatcher<GameEntity> _matcherModel;

    public static Entitas.IMatcher<GameEntity> Model {
        get {
            if (_matcherModel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Model);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherModel = matcher;
            }

            return _matcherModel;
        }
    }
}
