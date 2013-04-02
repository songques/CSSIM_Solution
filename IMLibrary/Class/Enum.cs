using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 与服务端的通信协议 
    /// </summary>
    public enum ProtocolServer
    {
        /// <summary>
        /// 客户端向服务器发送UDP包以检测是否允许UDP通信
        /// </summary>
        DetectingIsUDPCom = 30, 
        /// <summary>
        /// 检测是否FullConeNat
        /// </summary>
        DetectingIsFullConeNat = 31, 
        /// <summary>
        ///  检测是否Symmetric
        /// </summary>
        DetectingIsSymmetric = 32,
        /// <summary>
        /// 检测是否RestrictedCone
        /// </summary>
        DetectingIsRestrictedCone = 33,
        /// <summary>
        /// 检测是否PortRestrictedCone
        /// </summary>
        DetectingIsPortRestrictedCone = 34,
        /// <summary>
        /// 用户登录请求
        /// </summary>
        BeginLogin = 35, 
        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        GetOnlineUsers = 36,
        /// <summary>
        /// 获取组织机构部门基本信息
        /// </summary>
        GetDepsBaseInfo = 37, 
        /// <summary>
        /// 获取用户所有联系人基本信息
        /// </summary>
        GetUsersBaseInfo = 38, 
        /// <summary>
        /// 修改用户密码
        /// </summary>
        ChangePassword = 39, 
        /// <summary>
        /// 转发消息
        /// </summary>
        TransmitMsg = 40,
        /// <summary>
        /// 保存群发通知
        /// </summary>
        SaveNoticeMsg = 41,
        /// <summary>
        /// 发送群发通知
        /// </summary>
        SendNoticeMsg = 42,
        /// <summary>
        /// 删除离线消息
        /// </summary>
        DelMsg = 43,
        /// <summary>
        /// 获取指定用户的资料
        /// </summary>
        GetUserData = 44,
        /// <summary>
        /// 创建群组
        /// </summary>
        CreateGroup = 45,
       /// <summary>
       /// 获取指定群组的基本信息
       /// </summary>
        GetGroupBaseInfo = 46,
        /// <summary>
        /// 服务器收到WebService消息
        /// </summary>
        WebServiceMsg = 47,
        /// <summary>
        /// 客户端更新在线状态
        /// </summary>
        UpdateOnlineState = 48,
        /// <summary>
        /// 用户离线
        /// </summary>
        UserDisOnline = 49,
        /// <summary>
        /// 用户发出UDP打洞申请
        /// </summary>
        UserUDPPenetrateRequest = 50,
        /// <summary>
        /// 获得OA待办、待阅
        /// </summary>
        GetOA_ToDo = 100,
        /// <summary>
        /// 获得财务系统待办、待阅
        /// </summary>
        GetTMS_ToDo = 101,
        /// <summary>
        /// 获得人力系统待办、待阅
        /// </summary>
        GetHR_ToDo = 102,
    }

    /// <summary>
    /// 与客户端的通信协议
    /// </summary>
    public enum ProtocolClient
    {
        /// <summary>
        /// 服务器端告之客户端允许UDP通信
        /// </summary>
        IsUDPCom,
        /// <summary>
        /// 客户端是FullConeNat
        /// </summary>
        IsFullConeNat,
        /// <summary>
        /// 客户端是Symmetric
        /// </summary>
        IsSymmetric,
        /// <summary>
        /// 客户端是RestrictedCone
        /// </summary>
        IsRestrictedCone,
        /// <summary>
        /// 客户端是PortRestrictedCone
        /// </summary>
        IsPortRestrictedCone,
        /// <summary>
        /// 帐号有异地登录
        /// </summary>
        ElseLogin,
        /// <summary>
        /// 客户端登录成功
        /// </summary>
        LoginSuccessful,
        /// <summary>
        /// 客户端密码错误
        /// </summary>
        LoginPasswordError,
        /// <summary>
        /// 服务器通知有用户上线
        /// </summary>
        UserOnline,
        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        GetOnlineUsers,
        /// <summary>
        /// 获取组织机构部门基本信息
        /// </summary>
        GetDepsBaseInfo,
        /// <summary>
        /// 获取用户所有联系人基本信息
        /// </summary>
        GetUsersBaseInfo,
        /// <summary>
        /// 修改用户密码成功
        /// </summary>
        ChangePasswordSuccessful,
        /// <summary>
        /// 收到对话消息
        /// </summary>
        ChatMsg,
        /// <summary>
        /// 对话消息成功发送
        /// </summary>
        ChatMsgSendSuccessful,
        /// <summary>
        /// 收到群发通知
        /// </summary>
        NoticeMsg, 
        /// <summary>
        /// 收到刚才发送的服务器的群发通知的ID
        /// </summary>
        NoticeMsgID,
        /// <summary>
        /// 获取指定用户的资料
        /// </summary>
        GetUserData,
        /// <summary>
        /// 创建群组
        /// </summary>
        CreateGroupSuccessful,
        /// <summary>
        /// 创建群组超出最大限制
        /// </summary>
        CreateGroupOut,
        /// <summary>
        /// 获得所有群组ID编号集
        /// </summary>
        GetGroupsIDs,
        /// <summary>
        /// 获取指定群组的基本信息
        /// </summary>
        GetGroupBaseInfo,
        /// <summary>
        /// 收到群组对话消息
        /// </summary>
        GroupChat,
        /// <summary>
        /// 获得OA待办、待阅
        /// </summary>
        GetOA_ToDo,
        /// <summary>
        /// 获得财务系统待办、待阅
        /// </summary>
        GetTMS_ToDo,
        /// <summary>
        /// 获得人力系统待办、待阅
        /// </summary>
        GetHR_ToDo,
        /// <summary>
        /// 客户端更新在线状态成功
        /// </summary>
        UpdateOnlineStateSuccessful,
        /// <summary>
        /// 用户离线
        /// </summary>
        UserDisOnline,

        /// <summary>
        /// 用户文件传输请求
        /// </summary>
        FileTransmitRequest,
        /// <summary>
        /// 用户取消文件传输
        /// </summary>
        FileTransmitCancel,
        /// <summary>
        /// 获取对方文件传输中转服务ID号
        /// </summary>
        FileTransmitGetOppositeID,
        /// <summary>
        /// 获取对方文件传输套接字的UDP端口
        /// </summary>
        FileTransmitGetUDPPort,

        /// <summary>
        /// 用户图片文件传输请求
        /// </summary>
        ImageTransmitRequest,
        /// <summary>
        /// 用户图片取消文件传输
        /// </summary>
        ImageTransmitCancel,
        /// <summary>
        /// 获取对方图片文件传输中转服务ID号
        /// </summary>
        ImageTransmitGetOppositeID,
        /// <summary>
        /// 获取对方图片文件传输套接字的UDP端口
        /// </summary>
        ImageTransmitGetUDPPort,

        /// <summary>
        /// 用户请求音视频对话
        /// </summary>
        AVRequest,
        /// <summary>
        /// 用户取消音视频对话
        /// </summary>
        AVCancel,
        /// <summary>
        /// 获取对方视频传输服务中转ID
        /// </summary>
        AVGetOppositeID,
        /// <summary>
        /// 获取对方视频传输套接字的UDP端口
        /// </summary>
        AVGetUDPPort,

        /// <summary>
        /// 用户发出UDP打洞申请
        /// </summary>
        UserUDPPenetrateRequest,
        /// <summary>
        /// 用户UDP打洞成功
        /// </summary>
        UserUDPPenetrateSuccess,
    }


    /// <summary>
    /// 客户端Nat类型
    /// </summary>
    public enum NatClass
    {
        /// <summary>
        /// UDP未知NAT
        /// </summary>
        UdpUnkonw=0,
        /// <summary>
        /// 全双工UDP NAT
        /// </summary>
        FullCone = 1,
        /// <summary>
        /// 注册锥形NAT
        /// </summary>
        RestrictedCone = 2,
        /// <summary>
        /// 端口注册锥形NAT
        /// </summary>
        PortRestrictedCone = 3,
        /// <summary>
        /// 对称NAT
        /// </summary>
        Symmetic = 4,
        /// <summary>
        /// TCP联连
        /// </summary>
        Tcp = 5,
        /// <summary>
        /// 无任何联接
        /// </summary>
        None=6,
    }


    /// <summary>
    /// 通信传输通道所采用的通信方式
    /// </summary>
    public enum NetCommunicationClass
    {
        /// <summary>
        /// 局域网UDP通信
        /// </summary>
        LanUDP,
        /// <summary>
        /// 广域网UDP直接通信
        /// </summary>
        WanNoProxyUDP,
        /// <summary>
        /// 广域网UDP中转通信
        /// </summary>
        WanProxyUDP,
        /// <summary>
        /// TCP中转通信
        /// </summary>
        TCP,
        /// <summary>
        /// 广域网TCP直联通信
        /// </summary>
        WanTCP,
        /// <summary>
        /// 服务器TCP中转通信
        /// </summary>
        ServerTCP,
        /// <summary>
        /// 局域网TCP直联通信
        /// </summary>
        LanTCP,
        /// <summary>
        /// 表示没有建立通信
        /// </summary>
        None,
    }


    /// <summary>
    /// 文件传输通信协议 
    /// </summary>
    public enum ProtocolFileTransmit
    {
        /// <summary>
        /// 获得对方传输的文件数据包
        /// </summary>
        GetFileBlock,
        /// <summary>
        /// 对方发送的局域网握手数据
        /// </summary>
        HandshakeLAN,
        /// <summary>
        /// 对方发送的广域网握手数据
        /// </summary>
        HandshakeWAN,
        /// <summary>
        /// 对方通知收到自己发送的局域网握手数据
        /// </summary>
        IsOppositeRecSelfWanUDPData,
        /// <summary>
        /// 获取代理ID号
        /// </summary>
        GetFileTransmitProxyID,
        /// <summary>
        /// 文件传输
        /// </summary>
        FileTransmit,
        /// <summary>
        /// 开始文件传输
        /// </summary>
        BeginTransmit,
        /// <summary>
        /// 文件传输完成
        /// </summary>
        FileTranstmitOver,
        /// <summary>
        /// 对方通知收到自己发送的局域网握手数据
        /// </summary>
        IsOppositeRecSelfLanUDPData,
        /// <summary>
        /// 客户端获取广域网文件传输套接字的IP与端口信息
        /// </summary>
        GetUDPWANInfo,
    }

  

    /// <summary>
    /// 音视频通信协议 
    /// </summary>
    public enum ProtocolAVTransmit
    {
        /// <summary>
        /// 获得对方传输的视频数据包
        /// </summary>
        GetVideoData,
        /// <summary>
        /// 获得对方传输的音频数据包
        /// </summary>
        GetAudioData,
        /// <summary>
        /// 获取代理ID号
        /// </summary>
        GetAVTransmitProxyID,
        /// <summary>
        /// 音视频传输
        /// </summary>
        AVTransmit,
        /// <summary>
        /// 开始传输数据
        /// </summary>
        BeginTransmit, 
        /// <summary>
        /// 传输完成
        /// </summary>
        AVTranstmitOver, 
        /// <summary>
        /// 对方发送的局域网握手数据
        /// </summary>
        HandshakeLAN, 
        /// <summary>
        /// 对方通知收到自己发送的局域网握手数据
        /// </summary>
        IsOppositeRecSelfLanUDPData, 
        /// <summary>
        /// 对方发送的广域网握手数据
        /// </summary>
        HandshakeWAN,
        /// <summary>
        /// 对方通知收到自己发送的局域网握手数据
        /// </summary>
        IsOppositeRecSelfWanUDPData,
        /// <summary>
        /// 客户端获取广域网传输套接字的IP与端口信息
        /// </summary>
        GetUDPWANInfo,
        /// <summary>
        /// 获得对方视频图像头信息 
        /// </summary>
        GetBITMAPINFOHEADER,
    }
}
