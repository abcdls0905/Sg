using Entitas;

namespace Game
{
    [Game]
    public class HpComponent : IComponent
    {
        // 1.血量相关
        public bool hpChg;
        public int hp;
        public int maxhp;
        public int oldhp;
        public int oldmaxhp;

        // 2.存活相关
        public bool liveChg;
        public bool isDead;
        public int energy;

        // 3.虚弱状态相关
        public bool weakChg;
        public bool isWeak;
        public int weakHp;
        public int maxWeakHp;

        // 4.虚弱治疗状态
        public bool treatChg;
        public bool isBeTreating;

        // 5.治疗状态
        public bool recoveryChg;
        public int recovery;

        // 6.护盾
        public float shield;
        public float maxShield;
        public AkEffectType lastShieldEffect;
        public void Reset()
        {
            hpChg = false;
            hp = 100;
            maxhp = 100;
            oldhp = 100;
            oldmaxhp = 100;

            shield = 0;
            maxShield = 0;
            maxWeakHp = 0;

            liveChg = false;
            isDead = false;
            energy = 0;

            weakChg = false;
            isWeak = false;

            treatChg = false;
            isBeTreating = false;

            weakHp = 0;
            recovery = 0;
            lastShieldEffect = AkEffectType.None;
        }
    }
}