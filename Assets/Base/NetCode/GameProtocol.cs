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

    public class GameProtocol : NetProtocol
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
            GameNetPack pack = NetPackPool<GameNetPack>.New();
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
            NetPackPool<GameNetPack>.Reclaim(pack as GameNetPack);
            return true;
        }
    }

    // ֻ�ܱ���һ�̷߳���
    // �˴����Ǹ���body�������ڴ���䣬�磺����32��64��128��256��512��1024��2048��4096 �����ڴ��pool[7]��
    // ��Ӧ��Ӳ���Ͱ��߼���
    public class GameNetPack : INetPack
    {
        public uint connID;
        public uint seq;
        public ushort msgID;
        public byte flag;
        public byte[] body;
        public const int LeftSize = sizeof(uint) + sizeof(uint) + sizeof(ushort) + sizeof(byte);

        public int GetByteSize()
        {
            if (body == null)
                return LeftSize;
            else
                return body.Length + LeftSize;
        }
        public void Serialize(NetByteBuf buf)
        {
            buf.WriteUInt(connID);
            buf.WriteUInt(seq);
            buf.WriteUShort(msgID);
            buf.WriteByte(flag);
            buf.WriteBytes(body);
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
            body = new byte[packLen - LeftSize];
            buf.ReadBytes(body, 0, packLen - LeftSize);
        }

        public bool MergePack(ref byte[] mergeCache, ref int mergeCacheSize)
        {
            // �ϲ��ְ�
            if (mergeCache != null)
            {
                if (body != null)
                {
                    byte[] result = new byte[mergeCache.Length + body.Length];
                    mergeCache.CopyTo(result, 0);
                    body.CopyTo(result, mergeCache.Length);
                    body = result;
                }
                else
                    body = mergeCache;
            }

            if ((flag & (byte)0x04) != 0) // ��ǰΪ�ְ�
            {
                mergeCache = body;
                return true;
            }
            mergeCache = null;
            return false;
        }

        public bool SplitPack(int SubPackageSize, List<INetPack> splitPackList)
        {
            if (SubPackageSize <= 0 || ((body.Length + SubPackageSize - 1) / SubPackageSize) <= 1)
                return false;

            int count = 0;
            while (true)
            {
                var splitRet = NetPackPool<GameNetPack>.New();
                splitRet.connID = connID;
                splitRet.msgID = msgID;
                splitRet.seq = seq;
                if ((count + 1) * SubPackageSize >= body.Length)
                {
                    byte[] temp = new byte[body.Length - count * SubPackageSize];
                    System.Array.Copy(body, count * SubPackageSize, temp, 0, body.Length - count * SubPackageSize);
                    splitRet.flag = flag;
                    splitRet.body = temp;
                    break;
                }
                else
                {
                    byte[] temp = new byte[SubPackageSize];
                    System.Array.Copy(body, count * SubPackageSize, temp, 0, SubPackageSize);
                    splitRet.body = temp;
                    splitRet.flag = (byte)(flag | 0x04);
                }
                count++;

                splitPackList.Add(splitRet);
            }
            return true;
        }

        public void Reset()
        {

        }

    }

}

