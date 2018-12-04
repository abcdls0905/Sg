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
    public class PlatformConfig 
    {
        public static  string productGate = "m2sw"; // 产品代号
        public static  GameNet gameNet = GameNet.OUTTER_TEST;
    }
}
