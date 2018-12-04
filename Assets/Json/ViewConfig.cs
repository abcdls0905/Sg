using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJson
{
    [Serializable]
    public class ViewConfig
    {
        public int viewRange;
        public int noAimViewRange;
        public int inCarViewRange;
        public int inCarCastShadowRange;

        public float upperViewHeight;
        public float lowerViewHeight;

        public float shadowThreshold;
        public float castShadowHeight;
        public float shadowOffset;
        public float fadeStretchFactor;

        public float canLockAlpha;
        public float lockExposeAlpha;
        public float lockExposeDis;

        public float wallShadowAngle;
        public float lowWallShadowAngle;

        public float noWindowShadowRange;
        public float windowShadowAlpha;

        public int inGrassValue;
        public float grassHideAlpha;
        public float lowGrassHideAlpha;
        public float grassHideSpeedLimit;
        public float grassHideTime;
        public float grassSneakTime;
        public float grassHideSpeed;
        public float grassExposeSpeed;

        public float exposeShadowAlpha;
        public float exposeDuration;
        public float exposeRecoverDuration;

        public float smokeAlpha;
        public int smokeInnerRange;
        public int smokeOuterRange;
    }
}