IMChecker用于完成企业版发布到外网的APP用户检查

>>>>用途说明<<<<
1、如果没有安装火星的用户，则需要安装火星
2、如果火星没有登陆或者超时的用户，则需要重新登陆


>>>>Unity3D中需要配置以下内容<<<<
1、在MAC环境下，选中IMChecker/Plugins/iOS/目录下的IMChecker.mm，然后在Inspector面板中，钩选依赖项 Security
2、AccountKit.framework 由于是放在IMChecker/Plugins/iOS/ 下面的，会被XCode自动识别，无需特殊处理

>>>>xcode项目需要配置以下内容<<<<

1、info.plist需要增加KEY LSApplicationQueriesSchemes
2、修改Key的值类型为Array
3、为数组添加一个元素  calltoimccQYB

4、在info.plist同级（项目根目录）新增一个entitlement.plist文件
5、打开文件贴上下面的代码

<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>keychain-access-groups</key>
	<array>
		<string>$(AppIdentifierPrefix)com.jiajiu.AKDeviceInfo</string>
	</array>
</dict>
</plist>


6、在Code Signing中把Entitlement处填上 entitlement 用于指向上一步中新建的文件

7、开启Keychain Sharing
注：由于Keychain Sharing需要识别Team账号。 所以在内网无法直接通过Capabilities界面的选项开启。
需要手工修改xcodeproj下面的proejct.settings文件 在TargetAttributes中加入
DevelopmentTeam = MDDB52YB8L;
SystemCapabilities = {
	com.apple.Keychain = {
		enabled = 1;
	};
};

8、检查entitlement.plist，如果内容与步骤5中不一致，重新复制粘贴一遍。 要确保里面只有 $(AppIdentifierPrefix)com.jiajiu.AKDeviceInfo
9、保存所有的文件修改，重新打开xcode项目
