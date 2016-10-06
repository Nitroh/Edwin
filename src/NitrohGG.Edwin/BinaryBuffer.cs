using System;
using System.Collections.Generic;
using System.Linq;

namespace NitrohGG.Edwin
{
    //TODO: Linq?
    internal class BinaryBuffer
    {
        private readonly List<byte> _data;
        private int _position;

        private int AvailableByteCount => _data.Count - _position;

        public BinaryBuffer(IEnumerable<byte> data)
        {
            _data = data.ToList();
            _position = 0;
        }

        public void Seek(int position)
        {
            if (position < 0) _position = 0;
            else if (position >= _data.Count) _position = _data.Count - 1;
            else _position = position;
        }

        private IEnumerable<byte> ProcessBytes(int size, bool reverse = false, bool moveCursor = true)
        {
            var result = new List<byte>();
            if (AvailableByteCount < size) return result;

            for (var index = _position; index < _position + size; index++)
            {
                result.Add(_data[index]);
            }
            if (moveCursor) _position += size;
            if (reverse) result.Reverse();
            return result;
        }

        private string ProcessStringBytes(bool moveCursor = true)
        {
            var result = string.Empty;
            var count = 0;
            while (count < AvailableByteCount)
            {
                var currChar = (char)_data[_position + count];
                count++;
                if (currChar == '\0') break;
                result += currChar;
            }
            if (moveCursor) _position += count;
            return result;
        }

        private byte[] GetBytesInternal(int size, bool reverse) => ProcessBytes(size, reverse).ToArray();
        private byte[] PeekBytesInternal(int size, bool reverse) => ProcessBytes(size, reverse, false).ToArray();

        public IEnumerable<byte> GetBytes(int size) => GetBytesInternal(size, false);
        public IEnumerable<byte> PeekBytes(int size) => PeekBytesInternal(size, false);

        public int GetInt() => BitConverter.ToInt32(GetBytesInternal(4, BitConverter.IsLittleEndian), 0);
        public int PeekInt() => BitConverter.ToInt32(PeekBytesInternal(4, BitConverter.IsLittleEndian), 0);

        public uint GetUInt() => BitConverter.ToUInt32(GetBytesInternal(4, BitConverter.IsLittleEndian), 0);
        public uint PeekUInt() => BitConverter.ToUInt32(PeekBytesInternal(4, BitConverter.IsLittleEndian), 0);

        public short GetShort() => BitConverter.ToInt16(GetBytesInternal(2, BitConverter.IsLittleEndian), 0);
        public short PeekShort() => BitConverter.ToInt16(PeekBytesInternal(2, BitConverter.IsLittleEndian), 0);

        public long GetLong() => BitConverter.ToInt64(GetBytesInternal(8, BitConverter.IsLittleEndian), 0);
        public long PeekLong() => BitConverter.ToInt64(PeekBytesInternal(8, BitConverter.IsLittleEndian), 0);

        public string GetString() => ProcessStringBytes();
        public string PeekString() => ProcessStringBytes(false);
    }
}
