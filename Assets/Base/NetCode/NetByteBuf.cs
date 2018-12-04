using System;
using System.Text;

namespace GameNetwork
{
    public class NetByteBuf
    {
        private int len;
        private byte[] data;
        private int readerIndex;
        private int writerIndex;
        private int markReader;
        private int markWriter;

        public NetByteBuf(int capacity)
        {
            len = capacity;
            data = new byte[len];
            Clear();
        }

        public int Capacity()
        {
            return len;
        }

        public NetByteBuf Capacity(int nc)
        {
            if (nc > len)
            {
                byte[] old = data;
                data = new byte[nc];
                Array.Copy(old, data, len);
                len = nc;
            }
            return this;
        }

        public NetByteBuf Clear()
        {
            readerIndex = 0;
            writerIndex = 0;
            markReader = 0;
            markWriter = 0;
            return this;
        }

        public NetByteBuf Copy()
        {
            NetByteBuf item = new NetByteBuf(len);
            Array.Copy(this.data, item.data, len);
            item.readerIndex = readerIndex;
            item.writerIndex = writerIndex;
            item.markReader = markReader;
            item.markWriter = markWriter;
            return item;
        }

        public NetByteBuf MarkReaderIndex()
        {
            markReader = readerIndex;
            return this;
        }

        public NetByteBuf MarkWriterIndex()
        {
            markWriter = writerIndex;
            return this;
        }

        public int MaxWritableBytes()
        {
            return len - writerIndex;
        }

        public byte ReadByte()
        {
            if (readerIndex + 1 <= writerIndex)
            {
                byte ret = data[readerIndex++];
                return ret;
            }
            return (byte)0;
        }

        public int ReadInt()
        {
            if (readerIndex + 4 <= writerIndex)
            {
                unchecked
                {
                    int ret = (int)(((data[readerIndex++]) << 24) & 0xff000000);
                    ret |= (((data[readerIndex++]) << 16) & 0x00ff0000);
                    ret |= (((data[readerIndex++]) << 8) & 0x0000ff00);
                    ret |= (((data[readerIndex++])) & 0x000000ff);
                    return ret;
                }
            }
            return 0;
        }

        public uint ReadUInt()
        {
            if (readerIndex + 4 <= writerIndex)
            {
                unchecked
                {
                    int ret = (int)(((data[readerIndex++]) << 24) & 0xff000000);
                    ret |= (((data[readerIndex++]) << 16) & 0x00ff0000);
                    ret |= (((data[readerIndex++]) << 8) & 0x0000ff00);
                    ret |= (((data[readerIndex++])) & 0x000000ff);
                    return (uint)ret;
                }
            }
            return 0;
        }

        public short ReadShort()
        {
            if (readerIndex + 2 <= writerIndex)
            {
                int h = data[readerIndex++];
                int l = data[readerIndex++] & 0x000000ff;
                int len = ((h << 8) & 0x0000ff00) | (l);
                return (short)len;
            }
            return 0;
        }

        public ushort ReadUShort()
        {
            if (readerIndex + 2 <= writerIndex)
            {
                int h = data[readerIndex++];
                int l = data[readerIndex++] & 0x000000ff;
                int len = ((h << 8) & 0x0000ff00) | (l);
                return (ushort)len;
            }
            return 0;
        }

        public void ReadBytes(byte[] buffer, int offset, int count)
        {
            if (readerIndex + count <= writerIndex)
            {
                Array.Copy(data, readerIndex, buffer, offset, count);
                readerIndex += count;
            }
        }

        public int ReadableBytes()
        {
            return writerIndex - readerIndex;
        }

        public int ReaderIndex
        {
            get
            {
                return readerIndex;
            }
            set
            {
                if (readerIndex <= writerIndex)
                {
                    readerIndex = value;
                }
            }
        }

        public NetByteBuf ResetReaderIndex()
        {
            if (markReader <= writerIndex)
            {
                this.readerIndex = markReader;
            }
            return this;
        }
        public NetByteBuf ResetWriterIndex()
        {
            if (markWriter >= readerIndex)
            {
                writerIndex = markWriter;
            }
            return this;
        }

        public int WritableBytes()
        {
            return len - writerIndex;
        }

        public NetByteBuf WriteByte(byte value)
        {
            this.Capacity(writerIndex + 1);
            this.data[writerIndex++] = value;
            return this;
        }

        public NetByteBuf WriteInt(int value)
        {
            Capacity(writerIndex + 4);
            data[writerIndex++] = (byte)((value >> 24) & 0xff);
            data[writerIndex++] = (byte)((value >> 16) & 0xff);
            data[writerIndex++] = (byte)((value >> 8) & 0xff);
            data[writerIndex++] = (byte)(value & 0xff);
            return this;
        }

        public NetByteBuf WriteUInt(uint value)
        {
            Capacity(writerIndex + 4);
            data[writerIndex++] = (byte)((value >> 24) & 0xff);
            data[writerIndex++] = (byte)((value >> 16) & 0xff);
            data[writerIndex++] = (byte)((value >> 8) & 0xff);
            data[writerIndex++] = (byte)(value & 0xff);
            return this;
        }

        public NetByteBuf WriteShort(short value)
        {
            Capacity(writerIndex + 2);
            data[writerIndex++] = (byte)((value >> 8) & 0xff);
            data[writerIndex++] = (byte)(value & 0xff);
            return this;
        }

        public NetByteBuf WriteUShort(ushort value)
        {
            Capacity(writerIndex + 2);
            data[writerIndex++] = (byte)((value >> 8) & 0xff);
            data[writerIndex++] = (byte)(value & 0xff);
            return this;
        }

        public NetByteBuf WriteBytes(NetByteBuf src)
        {
            int sum = src.writerIndex - src.readerIndex;
            Capacity(writerIndex + sum);
            if (sum > 0)
            {
                Array.Copy(src.data, src.readerIndex, data, writerIndex, sum);
                writerIndex += sum;
                src.readerIndex += sum;
            }
            return this;
        }

        public NetByteBuf WriteBytes(NetByteBuf src, int len)
        {
            if (len > 0)
            {
                Capacity(writerIndex + len);
                Array.Copy(src.data, src.readerIndex, data, writerIndex, len);
                writerIndex += len;
                src.readerIndex += len;
            }
            return this;
        }

        public NetByteBuf WriteBytes(byte[] src)
        {
            int sum = src.Length;
            Capacity(writerIndex + sum);
            if (sum > 0)
            {
                Array.Copy(src, 0, data, writerIndex, sum);
                writerIndex += sum;
            }
            return this;
        }

        public NetByteBuf WriteBytes(byte[] src, int off, int len)
        {
            int sum = len;
            if (sum > 0)
            {
                Capacity(writerIndex + sum);
                Array.Copy(src, off, data, writerIndex, sum);
                writerIndex += sum;
            }
            return this;
        }

        public int WriterIndex
        {
            get
            {
                return writerIndex;
            }
            set
            {
                if (writerIndex >= readerIndex && writerIndex <= len)
                {
                    writerIndex = value;
                }
            }
        }

        public byte[] Raw
        {
            get
            {
                return data;
            }
        }

        public void ResetBuffer()
        {
            markReader = 0;
            markWriter = 0;
            int rest = writerIndex - readerIndex;
            // 拷贝
            if (rest > 0)
            {
                Buffer.BlockCopy(data, readerIndex, data, 0, rest);
            }
            else
                rest = 0;
            readerIndex = 0;
            writerIndex = rest;
        }
    }
}

