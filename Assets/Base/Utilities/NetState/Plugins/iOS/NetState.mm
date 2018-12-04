#include <ifaddrs.h>  
#include <sys/socket.h>  
#include <net/if.h>  
#include <string.h>

//流量统计功能
uint64_t netiBytes = 0;  
uint64_t netoBytes = 0;
uint64_t netiBytesOld = 0;  
uint64_t netoBytesOld = 0; 

extern "C" void UpdateNet()
{
    struct ifaddrs *ifa_list = 0, *ifa;  
      
    if (getifaddrs(&ifa_list) == -1)   
    {  
        return;  
    }  
      
    netiBytes = 0;  
      
    netoBytes = 0;  
      
    for (ifa = ifa_list; ifa; ifa = ifa->ifa_next)   
    {  
          
        if (AF_LINK != ifa->ifa_addr->sa_family)  
            continue;  
          
        if (!(ifa->ifa_flags & IFF_UP) && !(ifa->ifa_flags & IFF_RUNNING))  
            continue;  
          
        if (ifa->ifa_data == NULL || ifa->ifa_name == NULL)  
            continue;  
          
        //en0 is wifi, pdp_ip0 is WWAN
        if (strncmp(ifa->ifa_name, "en0", 3) == 0 || strncmp(ifa->ifa_name, "pdp_ip0", 7) == 0)    
        {  
            struct if_data *if_data = (struct if_data *)ifa->ifa_data;  
              
            netiBytes += if_data->ifi_ibytes;  
              
            netoBytes += if_data->ifi_obytes;  
        }     
    }  
      
    freeifaddrs(ifa_list);  
}  

extern "C" void Clear()
{
    UpdateNet();
    netiBytesOld = netiBytes;
    netoBytesOld = netoBytes;
}

//获取接受的数据
extern "C" double GetReceive()
{
    return (double)(netiBytes - netiBytesOld);
}

//获取发送的数据
extern "C" double GetSend()
{
    return (double)(netoBytes - netoBytesOld);
}

//获取发送和接收的总数据
extern "C" double GetAll()
{
    return GetReceive() + GetSend();
}
  


