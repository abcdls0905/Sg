# 功能模块：

Charge:支付相关

	 逗库文档路径:
	http://dokuwiki.oa.com/doku.php?id=all_design:public:%E6%89%8B%E6%B8%B8%E5%85%AC%E7%94%A8%E8%AE%BE%E7%BD%AE:%E6%89%8B%E6%B8%B8%E5%85%85%E5%80%BC%E7%9B%B8%E5%85%B3
	
	基本接口:
	IAPCharge.BuyProduct:购买商品
		product:商品字符串
	IAPCharge.CallBack:购买商品回调

WebView：手机网页相关

	基本接口：
	WebView.LoadUrl：加载URL
		url：链接
		js：网页打开js脚本

IMChecker：火星相关

	基本接口：
	IMChecker.NeedCheckIMLogin：检测CC是否安装
	AccountBind.GetKeyChain：获取CCkeychain
		
		已用key
		["dytestaccount"] = "dytestaccount", ：cc账号
	 	["ccaccount"]     = "check_duoyi_account", ：ccGM权限

####EasyAB：AB加载库相关
* CDirectory: unity路径的封装，方便获取路径
* GameTimer:	一个定时器脚本，指定时间触发
* Map: 一个优化版的Dictionary
* EngineHelper: 提供很多数据类型（链表，LRU容器，二叉堆，Index池，固定长度队列）


CustomConnection：修复iOS网络缓存问题
HttpRequest：网络请求相关
Keychain：iOS keychain
CRC32：CRC
ClassSearch：C#类搜索
GamePool：通用对象池
LogListener：Log监听
MonoSingleton：Mono脚本单例
Singleton：单例
Yielders：Yield优化