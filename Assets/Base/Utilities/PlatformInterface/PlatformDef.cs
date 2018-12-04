using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using GameUtil;


namespace GamePlatform
{

    public enum GameNet
    {
        INNER_TEST,//内网, 或内测
        OUTTER_TEST,//外网 或外测
        OUTER_GAME//公网，正式游戏的
    }

}