using System;
public class MessageHandlerAttribute : Attribute
{
    public ushort MessageID;
    public Type MessageType;
    public MessageHandlerAttribute(ushort InMessageID, Type InMessageType)
    {
        MessageID = InMessageID;
        MessageType = InMessageType;
    }

    public MessageHandlerAttribute(Pb.MSG InMessageID, Type InMessageType)
    {
        MessageID = (ushort)InMessageID;
        MessageType = InMessageType;
    }

    public MessageHandlerAttribute(Gatewaypb.MSG InMessageID, Type InMessageType)
    {
        MessageID = (ushort)InMessageID;
        MessageType = InMessageType;
    }
}
