using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameUtil
{
    public class IAPCharge : MonoSingleton<IAPCharge>
    {
        public delegate void ProvideCallBack(string s);
        public static ProvideCallBack CallBack;

#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void TestMsg();//测试信息发送
    
        [DllImport("__Internal")]
        private static extern void TestSendString(string s);//测试发送字符串
    
        [DllImport("__Internal")]
        private static extern void TestGetString();//测试接收字符串
    
        [DllImport("__Internal")]
        private static extern void InitIAPManager();//初始化
    
        [DllImport("__Internal")]
        private static extern bool IsProductAvailable();//判断是否可以购买
    
        [DllImport("__Internal")]
        private static extern void RequstProductInfo(string s);//获取商品信息
    
        [DllImport("__Internal",  EntryPoint = "BuyProduct")]
        private static extern void _BuyProduct(string s);//购买商品
#endif

        //测试从xcode接收到的字符串
        public void IOSToU(string s)
        {
            Debug.Log("[IOSToU MsgFrom ios]" + s);
        }

        //购买商品回调
        public void ProvideContent(string s)
        {
            Debug.Log("[ProvideContent MsgFrom ios]proivideContent : " + s);
            if (CallBack != null)
                CallBack(s);
        }

        // Use this for initialization
        public override void Init()
        {
#if UNITY_IPHONE
            InitIAPManager();
#endif
        }

        //购买商品
        public static void BuyProduct(string product)
        {
            if (product == null || product.Length == 0)
                return;
#if UNITY_IPHONE
            if (!IsProductAvailable())
                return;
//             Debug.Log("BuyProduct:"+product);
//             RequstProductInfo(product);
            _BuyProduct(product);
#endif
        }

        public static void BuyTest(string product)
        {
#if UNITY_IPHONE
            if (!IsProductAvailable())
                return;
             Debug.Log("BuyTest:"+product);
            RequstProductInfo(product);
#endif
        }

    }
}