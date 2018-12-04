#import "IMCheckerAlertViewController.h"
#import <AccountKit/AKDeviceInfo.h>

IMCheckerAlertViewController* g_checkImDelegate = [[IMCheckerAlertViewController alloc] init];

BOOL iCanOpenURL(NSString* url)
{
    return [[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:url]];
}

extern "C" void iCheckIMLogin(float expireDay)
{
    //如果没有检测到cc，则提示安装
    if (iCanOpenURL(@"calltoimccQYB://") == FALSE) {
        [g_checkImDelegate showAlertViewInstallIM];
        return;
    }
    
    //进行登陆检查
    do{
        NSString *nsaccount = [AKDeviceInfo stringForKey:@"check_duoyi_account"];
        NSString *nslogintime = [AKDeviceInfo stringForKey:@"check_duoyi_login_time"];
        
        if(nsaccount == nil || [nsaccount length] == 0){
            break;
        }
        
        NSLog(@"CC acount is %@",nsaccount);
        if (nslogintime == nil || [nslogintime length] == 0) {
            break;
        }
        
        long timevalue = (long)[nslogintime longLongValue];
        long curtimevalue = [[NSDate date] timeIntervalSince1970];
        NSLog(@"system time is %ld",curtimevalue);
        //如果大于了过期时间，则还是要提示重新登陆
        if(curtimevalue > timevalue + expireDay * 86400){
            break;
        }
        return;
    }while (0);
    
    [g_checkImDelegate showAlertLoginIM];
}