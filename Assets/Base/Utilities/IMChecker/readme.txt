IMChecker���������ҵ�淢����������APP�û����

>>>>��;˵��<<<<
1�����û�а�װ���ǵ��û�������Ҫ��װ����
2���������û�е�½���߳�ʱ���û�������Ҫ���µ�½


>>>>Unity3D����Ҫ������������<<<<
1����MAC�����£�ѡ��IMChecker/Plugins/iOS/Ŀ¼�µ�IMChecker.mm��Ȼ����Inspector����У���ѡ������ Security
2��AccountKit.framework �����Ƿ���IMChecker/Plugins/iOS/ ����ģ��ᱻXCode�Զ�ʶ���������⴦��

>>>>xcode��Ŀ��Ҫ������������<<<<

1��info.plist��Ҫ����KEY LSApplicationQueriesSchemes
2���޸�Key��ֵ����ΪArray
3��Ϊ�������һ��Ԫ��  calltoimccQYB

4����info.plistͬ������Ŀ��Ŀ¼������һ��entitlement.plist�ļ�
5�����ļ���������Ĵ���

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


6����Code Signing�а�Entitlement������ entitlement ����ָ����һ�����½����ļ�

7������Keychain Sharing
ע������Keychain Sharing��Ҫʶ��Team�˺š� �����������޷�ֱ��ͨ��Capabilities�����ѡ�����
��Ҫ�ֹ��޸�xcodeproj�����proejct.settings�ļ� ��TargetAttributes�м���
DevelopmentTeam = MDDB52YB8L;
SystemCapabilities = {
	com.apple.Keychain = {
		enabled = 1;
	};
};

8�����entitlement.plist����������벽��5�в�һ�£����¸���ճ��һ�顣 Ҫȷ������ֻ�� $(AppIdentifierPrefix)com.jiajiu.AKDeviceInfo
9���������е��ļ��޸ģ����´�xcode��Ŀ
