using Entitas;
using System.Collections.Generic;
using UnityEngine;

//计算公式 
//属性 = (基础值*∏(基础加成)+∑(附加值))*(1+∑(附加比例))+∑(绝对值)

namespace Game
{
    public class BaseEPA
    {
        public float basis = 1;
        public float ext = 0;
        public float per = 0;
        public float abs = 0;

        public void Reset()
        {
            basis = 1;
            ext = 0;
            per = 0;
            abs = 0;
        }
    }
    public class BaseValue
    {
        float baseValue = 0; 
        float ext = 0;
        float per = 0;
        float abs = 0;
        float revise = 0; //修正值
        public delegate void OnChgFnCallback(float value);
        public OnChgFnCallback OnChgFn = null;
        int lockRevise = 0;
        public Dictionary<int, BaseEPA> bonus;
        float minVal = float.MinValue;
        float maxVal = float.MaxValue;
        public BaseValue(float baseValue)
        {
            bonus = new Dictionary<int, BaseEPA>();
            SetBase(baseValue);
        }

        //基础值
        public float Base()
        {
            return baseValue;
        }
        //最终值
        public float Value()
        {
            return revise;
        }
        //基础属性变更
        public void SetBase(float val)
        {
            if (baseValue == val) return;
            baseValue = val;
            ReCalc();
        }

        //设置值域
        public void SetValueMinMax(float min, float max)
        {
            minVal = min;
            maxVal = max;
            if(minVal > maxVal)
            {
                float tmp = minVal;
                minVal = maxVal;
                maxVal = tmp;
            }
            ReCalc();
        }
        //获取所有的设置情况
        public BaseEPA GetBonus(int flag)
        {
            BaseEPA epa;
            if (!bonus.TryGetValue(flag, out epa))
            {
                epa = new BaseEPA();
                bonus.Add(flag, epa);
            }
            return epa;
        }
        //设置所有的设置情况
        public void SetBonus(int flag, float ext, float per, float abs)
        {
            BaseEPA epa = GetBonus(flag);
            if (epa.ext == ext && epa.per == per && epa.abs == abs)
                return;
            epa.ext = ext;
            epa.per = per;
            epa.abs = abs;
            ReCalc();
        }

        public void SetBonusBasis(int flag, float basis)
        {
            BaseEPA epa = GetBonus(flag);
            if (epa.basis == basis)
                return;
            epa.basis = basis;
            ReCalc();
        }
        public void AddBonus(int flag, float ext, float per, float abs)
        {
            BaseEPA epa = GetBonus(flag);
            epa.ext += ext;
            epa.per += per;
            epa.abs += abs;
            ReCalc();
        }

        public void RemoveBonus(int flag)
        {
            BaseEPA epa;
            if (bonus.TryGetValue(flag, out epa))
            {
                epa.Reset();
                ReCalc();
            }
        }

        public void RemoveAllBonus()
        {
            foreach (var item in bonus)
                item.Value.Reset();
            ReCalc();
        }
        public void ReCalc()
        {
            ext = 0;
            per = 0;
            abs = 0;
            float baseReal = baseValue;
            // 遍历次序不影响结果(加减法理论上)
            foreach (var item in bonus)
            {
                ext = ext + item.Value.ext;
                per = per + item.Value.per;
                abs = abs + item.Value.abs;
                baseReal *= item.Value.basis;
            }
            //锁住
            if(lockRevise > 0) return;
            revise = (baseReal + ext) * (1 + per) + abs;
            //最小值最大值限制
            revise = Mathf.Clamp(revise, minVal, maxVal);
            //回调
            if (OnChgFn != null)
                OnChgFn(revise);
        }
        public void LockRevise(bool isLock)
        {
            if (lockRevise == 0 && !isLock) return;
            lockRevise = isLock ? lockRevise + 1 : lockRevise - 1;
            if(lockRevise <= 0)
            {
                lockRevise = 0;
                ReCalc();
            }
        }

        public bool IsLockRevise()
        {
            return lockRevise > 0;
        }
    }
}