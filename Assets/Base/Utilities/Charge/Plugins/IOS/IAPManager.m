//
//  IAPManager.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

#import "IAPManager.h"


#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
#if defined(__cplusplus)
}
#endif


@implementation IAPManager

-(void) attachObserver{
    NSLog(@"AttachObserver");
	//UnitySendMessage("IAPCharge", "IOSToU", "attachObserver!!!!!!");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

-(BOOL) CanMakePayment{
    return [SKPaymentQueue canMakePayments];
}

-(void) requestProductData:(NSString *)productIdentifiers{
    NSArray *idArray = [productIdentifiers componentsSeparatedByString:@"\t"];
    NSSet *idSet = [NSSet setWithArray:idArray];
    [self sendRequest:idSet];
}

-(void)sendRequest:(NSSet *)idSet{
    SKProductsRequest *request = [[SKProductsRequest alloc] initWithProductIdentifiers:idSet];
    request.delegate = self;
    [request start];
}

-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response{
    NSArray *products = response.products;
    
	//UnitySendMessage("IAPCharge", "IOSToU", "productsRequest");
    for (SKProduct *p in products) {
        UnitySendMessage("IAPCharge", "ShowProductList", [[self productInfo:p] UTF8String]);
    }
    
    for(NSString *invalidProductId in response.invalidProductIdentifiers){
        NSLog(@"Invalid product id:%@",invalidProductId);
		NSString* Error = [NSString stringWithFormat:@"Invalid product id:%@", invalidProductId];
		//UnitySendMessage("IAPCharge", "IOSToU", [Error UTF8String]);
    }
    
    //[request autorelease];
}

-(void)buyRequest:(NSString *)productIdentifier{
    SKPayment *payment = [SKPayment paymentWithProductIdentifier:productIdentifier];
    [[SKPaymentQueue defaultQueue] addPayment:payment];
}

-(void)dealloc{ 
	[[SKPaymentQueue defaultQueue] removeTransactionObserver:self];//解除监听 
}

-(NSString *)productInfo:(SKProduct *)product{
    NSArray *info = [NSArray arrayWithObjects:product.localizedTitle,product.localizedDescription,product.price,product.productIdentifier, nil];
    
    return [info componentsJoinedByString:@"\t"];
}

-(NSString *)transactionInfo:(SKPaymentTransaction *)transaction{
    
    //return [self encode:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    //return [[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSASCIIStringEncoding];
	NSString *result = [[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSUTF8StringEncoding]; 
	return result;
}

-(NSString *)encode:(const uint8_t *)input length:(NSInteger) length{
    static char table[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    
    NSMutableData *data = [NSMutableData dataWithLength:((length+2)/3)*4];
    uint8_t *output = (uint8_t *)data.mutableBytes;
    
    for(NSInteger i=0; i<length; i+=3){
        NSInteger value = 0;
        for (NSInteger j= i; j<(i+3); j++) {
            value<<=8;
            
            if(j<length){
                value |=(0xff & input[j]);
            }
        }
        
        NSInteger index = (i/3)*4;
        output[index + 0] = table[(value>>18) & 0x3f];
        output[index + 1] = table[(value>>12) & 0x3f];
        output[index + 2] = (i+1)<length ? table[(value>>6) & 0x3f] : '=';
        output[index + 3] = (i+2)<length ? table[(value>>0) & 0x3f] : '=';
    }
    
    return [[NSString alloc] initWithData:data encoding:NSASCIIStringEncoding];
}

-(void) provideContent:(SKPaymentTransaction *)transaction{
    UnitySendMessage("IAPCharge", "ProvideContent", [[self transactionInfo:transaction] UTF8String]);
}

-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions{
    for (SKPaymentTransaction *transaction in transactions) 
	{
        switch (transaction.transactionState) 
		{
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
			case  SKPaymentTransactionStateDeferred:
				break;
            default:
                break;
        }
    }
}

-(void) completeTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Comblete transaction : %@",transaction.transactionIdentifier);
    [self provideContent:transaction];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

-(void) failedTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Failed transaction : %@",transaction.transactionIdentifier);
    
	NSString* ErrorCode = [NSString stringWithFormat:@"ErrorCode:%d", transaction.error.code];
	//UnitySendMessage("IAPCharge", "IOSToU", [ErrorCode UTF8String]);
    if (transaction.error.code != SKErrorPaymentCancelled) {
        NSLog(@"!Cancelled");
    }
	
	if (transaction.error.localizedDescription != nil)
	{
		//UnitySendMessage("IAPCharge", "IOSToU", "localizedDescription:");
		//UnitySendMessage("IAPCharge", "IOSToU", [transaction.error.localizedDescription UTF8String]);
	}
	if (transaction.error.localizedFailureReason != nil)
	{
		//UnitySendMessage("IAPCharge", "IOSToU", "localizedFailureReason:");
		//UnitySendMessage("IAPCharge", "IOSToU", [transaction.error.localizedFailureReason UTF8String]);
	}
	if (transaction.error.domain != nil)
	{
		//UnitySendMessage("IAPCharge", "IOSToU", "domain:");
		//UnitySendMessage("IAPCharge", "IOSToU", [transaction.error.domain UTF8String]);
	}
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

-(void) restoreTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Restore transaction : %@",transaction.transactionIdentifier);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}


@end
