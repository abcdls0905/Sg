#import "Reachability.h"
#import "NativeHelp.h"
#import <CoreTelephony/CTCarrier.h>
#import <CoreTelephony/CTTelephonyNetworkInfo.h>


@implementation NativeHelp:NSObject
+(NSString *)GetNetType {
    NSString *state = [[NSString alloc]init];
    state = @"no";
    Reachability *reachability = [Reachability reachabilityForInternetConnection];
    [reachability startNotifier];

    NetworkStatus status = [reachability currentReachabilityStatus];

    if(status == NotReachable)
    {
       state = @"no";
        //No internet
    }
    else if (status == ReachableViaWiFi)
    {
        state = @"wifi";
        //WiFi
    }
    else if (status == ReachableViaWWAN)
    {
        state = @"wwan";


        //connection type
        CTTelephonyNetworkInfo *netinfo = [[CTTelephonyNetworkInfo alloc] init];
        //_carrier = [[netinfo subscriberCellularProvider] carrierName];

        if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyGPRS]) 
        {
            state = @"2g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyEdge]) 
        {
            state = @"2g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyWCDMA]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyHSDPA]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyHSUPA]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyCDMA1x]) 
        {
            state = @"2g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyCDMAEVDORev0])
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyCDMAEVDORevA]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyCDMAEVDORevB]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyeHRPD]) 
        {
            state = @"3g";
        } else if ([netinfo.currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyLTE]) 
        {
            state = @"4g";
        }

    }
    return state;
}
@end
extern "C"{
  //获取网络类型
  const char * GetNetTypeI(void)
  {
    NSString* type = [NativeHelp GetNetType];
    return [type UTF8String];
  }
  
}
