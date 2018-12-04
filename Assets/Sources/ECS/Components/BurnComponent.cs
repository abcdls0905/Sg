using Entitas;
using UnityEngine;

namespace Game
{
    public enum AkBurnState
    {
        Ak_None = 0,
        Ak_Stable,
        Ak_Danger,
    }


    public class BurnStateMachine : StateMachine
    {

    }

    public class BurnStateBase : IState
    {
        public Color srcColor = new Color(0, 0, 0, 1);
        public Color stableColor = new Color(0.35f, 0.35f, 0.35f, 1);
        public Color dangerColor = new Color(0.85f, 0.85f, 0.85f, 1);
        public GameEntity entity;
        protected BurnStateMachine parent;
        public BurnStateBase(BurnStateMachine parent, GameEntity entity)
        {
            this.parent = parent;
            this.entity = entity;
        }
        public string name
        {
            get;
            set;
        }
        public virtual bool OnStateEnterCheck() { return true; }
        public virtual bool OnStateLeaveCheck() { return true; }
        public virtual void OnStateEnter() { }
        public virtual void OnStateLeave() { }
        public virtual void OnStateOverride() { }
        public virtual void OnStateResume() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedExecute() { }
    }

    public class BurnStableState : BurnStateBase
    {
        public BurnStableState(BurnStateMachine parent, GameEntity entity) : base(parent, entity) { }
        public override void OnStateEnter()
        {
            entity.burn.eBurnState = AkBurnState.Ak_Stable;
            //entity.animation.animator.SetTrigger("stable");
            string model = BattleManager.Instance.eColorType == AkColorType.AK_TWO ? "Prefabs/Effect/cube_ignited_01b" : "Prefabs/Effect/cube_ignited_01a";
            Util.ChangeEntityModel(entity, model);
            string star = "Prefabs/Effect/stableeffect";
            EffectComponent effectComp = Contexts.Instance.game.effect;
            if (!effectComp.ContainEffect(entity.iD.value, star))
            {
                EffectData effect = new EffectData();
                effect.targetId = entity.iD.value;
                effect.type = AkEffectType.Ak_Follow;
                effect.name = star;
                effect.position = entity.transform.position;
                effectComp.effects.Add(effect);
            }
        }
    }

    public class BurnDangerState : BurnStateBase
    {
        public BurnDangerState(BurnStateMachine parent, GameEntity entity) : base(parent, entity) { }
        public override void OnStateEnter()
        {
            entity.burn.eBurnState = AkBurnState.Ak_Danger;
            //entity.animation.animator.SetTrigger("danger");
            string model = BattleManager.Instance.eColorType == AkColorType.AK_TWO ? "Prefabs/Effect/cube_ignited_02b" : "Prefabs/Effect/cube_ignited_02a";
            Util.ChangeEntityModel(entity, model);
        }
    }

    public class BurnNormalState : BurnStateBase
    {
        public BurnNormalState(BurnStateMachine parent, GameEntity entity) : base(parent, entity) { }
        public override void OnStateEnter()
        {
//             entity.burn.eBurnState = AkBurnState.Ak_None;
//             MaterialPropertyBlock propBlock = entity.view.propBlock;
//             entity.view.SetViewProperty("_EmissionColor", srcColor);
        }

    }

    public class BurnComponent : IComponent
    {
        public AkBurnState eBurnState;
        public float burnTime;
        public BurnStateMachine burnStateMachine = new BurnStateMachine();
        public void Reset()
        {
            eBurnState = AkBurnState.Ak_None;
            burnTime = 0;
            burnStateMachine = new BurnStateMachine();
        }
    }
}