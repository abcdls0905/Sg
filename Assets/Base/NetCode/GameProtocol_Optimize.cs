using System.Collections.Generic;

namespace GameNetwork
{
    /*
uint16 HeadLen 	����buff����
uint32 ConnID   ����������ID
uint32 Seq      ����Number
uint16 MsgID    ��ϢID
byte   flag     TCP/UDP��ʶ
bytes  Body     ��Ϣ���أ�Protobuf�ȣ�
 * */

    public class GameProtocol_Optimize : NetProtocol
    {
        const int HeadLen = 2;

        public INetPack ReadPack(NetByteBuf buf)
        {
            buf.MarkReaderIndex();
            if (buf.ReadableBytes() < HeadLen)
            {
                buf.ResetReaderIndex();
                return null;
            }
            ushort packLen = (ushort)buf.ReadUShort();
            if (buf.ReadableBytes() < packLen)
            {
                buf.ResetReaderIndex();
                return null;
            }
            var pack = NetPackPool<GameNetPack_Optimize>.New();
            pack.Deserialize(buf, packLen);
            return pack;
        }

        public bool WritePack(INetPack pack, NetByteBuf buf)
        {
            buf.MarkWriterIndex();
            if (buf.WritableBytes() < HeadLen)
            {
                buf.ResetWriterIndex();
                return false;
            }
            ushort packLen = (ushort)pack.GetByteSize();
            buf.WriteUShort(packLen);
            if (buf.WritableBytes() < packLen)
            {
                buf.ResetWriterIndex();
                return false;
            }
            pack.Serialize(buf);
            NetPackPool<GameNetPack_Optimize>.Reclaim(pack as GameNetPack_Optimize);
            return true;
        }
    }
    // ֻ�ܱ���һ�̷߳���
    // �˴����Ǹ���body�������ڴ���䣬�磺����32��64��128��256��512��1024��2048��4096 �����ڴ��pool[7]��
    // ��Ӧ��Ӳ���Ͱ��߼���
    public class GameNetPack_Optimize : INetPack
    {
        public uint connID;
        public uint seq;
        public ushort msgID;
        public byte flag;
        public byte[] body;
        public int bodySize;
        public const int LeftSize = sizeof(uint) + sizeof(uint) + sizeof(ushort) + sizeof(byte);

        public int GetByteSize()
        {
            if (body == null)
                return LeftSize;
            else
                return bodySize + LeftSize;
        }
        public void Serialize(NetByteBuf buf)
        {
            buf.WriteUInt(connID);
            buf.WriteUInt(seq);
            buf.WriteUShort(msgID);
            buf.WriteByte(flag);
            buf.WriteBytes(body, 0, bodySize);
        }
        public void Deserialize(NetByteBuf buf, ushort packLen)
        {
            if (LeftSize > packLen)
            {
                // Э�����ʧ��, �����˰�, ���б�Ҫ��Ҫ�Ͽ������������
                buf.ReaderIndex += packLen;
                return;
            }
            connID = buf.ReadUInt();
            seq = buf.ReadUInt();
            msgID = buf.ReadUShort();
            flag = buf.ReadByte();
            body = BytesCache.New(packLen - LeftSize);
            buf.ReadBytes(body, 0, packLen - LeftSize);
            bodySize = packLen - LeftSize;
        }

        public bool MergePack(ref byte[] mergeCache, ref int mergeCacheSize)
        {
            // �ϲ��ְ�
            if (mergeCacheSize != 0)
            {
                if (bodySize != 0)
                {
                    byte[] result = BytesCache.New(mergeCacheSize + bodySize);
                    System.Array.Copy(mergeCache, 0, result, 0, mergeCacheSize);
                    System.Array.Copy(body, 0, result, mergeCacheSize, bodySize);

                    BytesCache.Reclaim(body);
                    body = result;
                    bodySize = mergeCacheSize + bodySize;
                }
                else
                {
                    body = mergeCache;
                    bodySize = mergeCacheSize;
                }
            }

            if ((flag & (byte)0x04) != 0) // ��ǰΪ�ְ�
            {
                mergeCache = body;
                mergeCacheSize = bodySize;

                body = null;
                NetPackPool<GameNetPack_Optimize>.Reclaim(this); // ����
                return true;
            }
            else
            {
                if (mergeCache != null)
                    BytesCache.Reclaim(mergeCache);
                mergeCache = null;
                mergeCacheSize = 0;
                return false;
            }
        }

        public bool SplitPack(int SubPackageSize, List<INetPack> splitPackList)
        {
            if (SubPackageSize <= 0 || ((body.Length + SubPackageSize - 1) / SubPackageSize) <= 1)
                return false;

            int count = 0;
            while (true)
            {
                var splitRet = NetPackPool<GameNetPack_Optimize>.New();
                splitRet.connID = connID;
                splitRet.msgID = msgID;
                splitRet.seq = seq;
                if ((count + 1) * SubPackageSize >= bodySize)
                {
                    byte[] temp = BytesCache.New(bodySize - count * SubPackageSize);
                    System.Array.Copy(body, count * SubPackageSize, temp, 0, bodySize - count * SubPackageSize);
                    splitRet.flag = flag;
                    splitRet.body = temp;
                    splitRet.bodySize = bodySize - count * SubPackageSize;
                    break;
                }
                else
                {
                    byte[] temp = BytesCache.New(SubPackageSize);
                    System.Array.Copy(body, count * SubPackageSize, temp, 0, SubPackageSize);
                    splitRet.flag = (byte)(flag | 0x04);
                    splitRet.body = temp;
                    splitRet.bodySize = SubPackageSize;
                }
                count++;

                splitPackList.Add(splitRet);
            }
            return true;
        }

        public void Reset()
        {
            if(body != null)
                BytesCache.Reclaim(body);
            body = null;
            bodySize = 0;
            msgID = 0;
            seq = 0;
            flag = 0;
        }
    }
}