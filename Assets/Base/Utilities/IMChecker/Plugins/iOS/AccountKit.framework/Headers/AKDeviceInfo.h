//
//  AKDeviceInfo.h
//  AccountKit
//  注意,本framework依赖于Security.framework,要使用必须导入Security.framework
//  Created by duoyi on 13-9-6.
//  Copyright (c) 2013年 Guangzhou Duo Yi Network Technology. All rights reserved.
//

#import <Foundation/Foundation.h>

extern NSString * const AKDeviceInfoErrorDomain;
extern NSString * const AKDeviceInfoNotification;

/*! @breif 多益标识设置唯一值公共模块:为了确定取值在不同应用之间都相同,需要按以下方法设置
 *  @1、在引用项目创建一个Entitlements.plist文件
 *  @2、plist的内容如下:
 *  @3、将该plist的路径要配置在 Project->build setting->Code Signing Entitlements里
 *      小技巧:点击Code Signing Entitlements弹出输入框后,将文本直接拖到输入框便可
 *
 <plist version="1.0">
 <dict>
 <key>keychain-access-groups</key>
 <array>
 <string>$(AppIdentifierPrefix)com.duoyi.AKDeviceInfo</string>
 </array>
 </dict>
 </plist>
 *
 *  注意:
 *  所有应用配置的keychain-access-groups都必须包含
 *  com.duoyi.AKDeviceInfo
 */
@interface AKDeviceInfo : NSObject

#pragma mark - 设备唯一标识

/**
 *  获取设备的唯一标识
 *
 *  @return NSString
 */
+ (NSString *)identifier;

/**
 *  设置设备唯一标识,如果标识已经存在,则设置失败;若非特殊情况不应该调用此方法
 *
 *  @param identifier 唯一标识的值
 */
+ (void)setIdentifier:(NSString *)identifier;

/**
 *  设置设备唯一标识,若非特殊情况不应该调用此方法
 *
 *  @param value          唯一标识的值
 *  @param updateExisting 是true,则会覆盖原来的值
 */
+ (void)setIdentifier:(NSString *)value updateExisting: (BOOL) updateExisting;

/**
 *  设置获取设备唯一标识,内网机可以调用[AKDeviceInfo setURLString:@"http://192.168.189.150:804"]
 *
 *  @param urlString 设置获取唯一标识服务口的host
 */
+ (void)setURLString:(NSString *)urlString;

//! @breif 从服务器中下载唯一标识并保存,注意通过监听AKDeviceInfoNotification来获取返回, notifcation.object是NSString时,表示结果,为NSError时,表示错误
+ (void)download;


#pragma mark - 应用共享持久化保存

/*! @breif 将指定的文本保存到keychain中
 *  @param string 被保存的文本内容
 *  @param key    文本的键
 */
+ (void)setString:(NSString *)string forKey:(NSString *)key;


/*! @breif 获取指定键的文本
 *  @param key 键名
 *  @return 对应key的string
 */
+ (NSString *)stringForKey:(NSString *)key;

/*! @breif 删除指定串的文本
 *  @param key 要删除的键名
 */
+ (void)removeStringForKey:(NSString *)key;

@end
