#import "IMCheckerAlertViewController.h"
#import <UIKit/UIKit.h>

@implementation IMCheckerAlertViewController
{
    int _checkTag;
}

-(void)showAlertViewInstallIM
{
	_checkTag = 1;
	UIAlertView* pAlertView=[[UIAlertView alloc]initWithTitle:@"未安装企业版CC"
						message:@"您的设备未安装火星企业版，是否安装？"
						delegate:self
						cancelButtonTitle:nil
						otherButtonTitles:nil];
    [pAlertView addButtonWithTitle:@"否，并退出"];
    [pAlertView addButtonWithTitle:@"下载"];
    [pAlertView show];
    //[pAlertView release];
}

-(void)showAlertLoginIM
{
	_checkTag = 0;
	UIAlertView* pAlertView=[[UIAlertView alloc]initWithTitle:@"游戏需要激活"
						message:@"您太长时间没有登陆企业版CC,需要登陆激活！"
						delegate:self
						cancelButtonTitle:nil
						otherButtonTitles:nil];
    [pAlertView addButtonWithTitle:@"退出"];
    [pAlertView addButtonWithTitle:@"登陆IM激活"];
    [pAlertView show];
    //[pAlertView release];
}

-(void)showAlertCantDownloadIMCC
{
    _checkTag = 0;
    UIAlertView* pAlertView=[[UIAlertView alloc]initWithTitle:@"不能进入下载页面"
                                                      message:@"不能进行下载，无法跳转到下载页面！"
                                                     delegate:self
                                            cancelButtonTitle:nil
                                            otherButtonTitles:nil];
    [pAlertView addButtonWithTitle:@"退出"];
    [pAlertView addButtonWithTitle:@"重试"];
    [pAlertView show];
    //[pAlertView release];
}

-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    if (_checkTag == 0) {
        if (buttonIndex == 0) {
            exit(0);
        }
        else if (buttonIndex == 1)
        {
            UIApplication* application = [UIApplication sharedApplication];
            NSURL *url = [NSURL URLWithString:@"calltoimccQYB://"];
            NSLog(@"Jump to CC:%@",url);
            if (url && [application canOpenURL:url]) {
                [application openURL:url];
            }
            else{
                [self showAlertViewInstallIM];
            }
        }
    }
    else if (_checkTag == 1)
    {
        if (buttonIndex == 0) {
            exit(0);
        }
        else if (buttonIndex == 1){
            _checkTag = 0;
            UIApplication *application = [UIApplication sharedApplication];
            NSURL *url = [NSURL URLWithString:@"http://i.2980.com"];
            NSLog(@"Ready to download CC:%@",url);
            if (url && [application canOpenURL:url]) {
                [application openURL:url];
            }
            else{
                _checkTag = 1;
                [self showAlertCantDownloadIMCC];
            }
        }
    }
}

@end