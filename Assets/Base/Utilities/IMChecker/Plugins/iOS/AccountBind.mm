
#import "IMCheckerAlertViewController.h"
#import <AccountKit/AKDeviceInfo.h>

extern "C" const char* GetChain(void* key)
{
	NSString * nskey = [[NSString alloc] initWithCString:(const char*)key]; 
	NSString *account = [AKDeviceInfo stringForKey:nskey];
	return[account UTF8String];
}