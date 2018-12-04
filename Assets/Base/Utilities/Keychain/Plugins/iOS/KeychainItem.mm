#import "SSKeychain.h"
#define ServiceName @"com.duoyi.x6"

extern "C" const char* GetKeyChain(void* key)
{
	NSString *nskey = [[NSString alloc] initWithUTF8String:(const char*)key];
	NSString *value = [SSKeychain passwordForService:ServiceName account:nskey];
	return [value UTF8String];
}

extern "C" void SaveKeyChain(void* key, void* value)
{
	NSString * nskey = [[NSString alloc] initWithUTF8String:(const char*)key];
	NSString * nsvalue = [[NSString alloc] initWithUTF8String:(const char*)value];
	[SSKeychain setPassword:nsvalue forService:ServiceName account:nskey];
}

extern "C" void DeleteKeyChain(void* key)
{
    NSString * nskey = [[NSString alloc] initWithUTF8String:(const char*)key];
    [SSKeychain deletePasswordForService:ServiceName account:nskey];
}

// 自定义service
extern "C" const char* GetKeyChain2(void* key, void* service)
{
	NSString *nsservice = [[NSString alloc] initWithUTF8String:(const char*)service];
	NSString *nskey = [[NSString alloc] initWithUTF8String:(const char*)key];
	NSString *value = [SSKeychain passwordForService:nsservice account:nskey];
	return [value UTF8String];
}