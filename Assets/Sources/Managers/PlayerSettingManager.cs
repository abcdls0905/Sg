using GameUtil;
using System;

namespace Game
{
    public enum PlayerSetting
    {
        StaticLeftJoystic   = 1,
        CanOpenUmbrella     = 1 << 1,
        VehicleButtonMode   = 1 << 2,
        PickRecommand       = 1 << 3,
        WeaponRecommandMode = 1 << 4,
        VoiceMic            = 1 << 5,
        VoicePlayTeam       = 1 << 6,
        VoiceTeamBattle     = 1 << 7,
        AimBtnOperateMode   = 1 << 8,
        TeamPlayerAddMode   = 1 << 9,
        CameraTrackMode     = 1 << 10,
    }

    public enum PlayerGuide
    {
        NewPlayer    = 1,
        PickItem     = 1 << 1,
        UseDrug      = 1 << 2,
        FastRun      = 1 << 3,
        RotateCamera = 1 << 4,
        JumpAirPlane = 1 << 5,
        FastFallDown = 1 << 6,
        MatchTeammate= 1 << 7,
        DropItems    = 1 << 8,
        FullBackpack = 1 << 9,
        OpenDoor     = 1 << 10,
        OpenMap      = 1 << 11,
        MarkPoint    = 1 << 12,
        SearchEnv    = 1 << 13,
        SingleAI     = 1 << 14,
    }

    [XLua.LuaCallCSharp]
    class PlayerSettingManager : Singleton<PlayerSettingManager>
    {
        private ulong setting;
        private ulong tutorial;
        public void SetSetting(ulong setting, ulong tutorial)
        {
            this.setting = setting;
            this.tutorial = tutorial;
        }

        public bool IsNeedGuide(PlayerGuide type)
        {
            return (tutorial & (ulong)type) <= 0;
        }

        public void CloseGuide(PlayerGuide type)
        {
            if(IsNeedGuide(type))
            {
                tutorial = tutorial | (ulong)type;
                SendSetting();
            }
        }

        public void SetPlayerSetting(bool open, PlayerSetting type)
        {
            if (open)
                setting = setting | (ulong)type;
            else
                setting = setting & ~(ulong)type;
            SendSetting();
        }

        public bool CheckPlayerSetting(PlayerSetting value)
        {
            return (setting & (ulong)value) > 0;
        }

        public void SendSetting()
        {
            GameLua.LuaManager.Instance.PushLuaEvent(GameLua.LuaEventType.LE_PlayerSetting, setting, tutorial);
        }

        public void Destroy()
        {

        }
    }
}
