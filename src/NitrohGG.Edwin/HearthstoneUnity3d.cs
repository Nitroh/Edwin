using System.Collections.Generic;
using System.IO;
using System.Linq;
using LZ4;

namespace NitrohGG.Edwin
{
    internal class HearthstoneUnity3d
    {
        //Lz4net, recommended by lz4.org
        //https://github.com/MiloszKrajewski/lz4net

        private readonly string _fileName;
        private BinaryBuffer _buffer;

        private const string Signature = "UnityFS";
        
        //https://github.com/ata4/disunity/blob/6c1c3215419faaca427fa8d1e960f13cde76e766/disunity-core/src/main/java/info/ata4/junity/bundle/BundleHeader.java
        //lower 6 bits are compression type, 3 is LZ4
        private const int CompressionTypeFlagBits = 0x3F;
        private const int CompressionTypeLz4 = 3;

        public HearthstoneUnity3d(string fileName)
        {
            _fileName = fileName;
            _buffer = null;
            if (!File.Exists(_fileName)) return;

            var fileBytes = new List<byte>();
            using (var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    var readerPos = 0;
                    var readerLength = binaryReader.BaseStream.Length;
                    while (readerPos < readerLength)
                    {
                        fileBytes.Add(binaryReader.ReadByte());
                        readerPos++;
                    }
                }
            }
            if(fileBytes.Count > 0) _buffer = new BinaryBuffer(fileBytes);
        }

        public List<HearthstoneAsset> LoadAssets()
        {
            var results = new List<HearthstoneAsset>();


        }

        private bool ProcessBase()
        {
            var signature = _buffer.GetString();
            if (signature != Signature) return false;

            var formatVersion = _buffer.GetInt();
            var unityVersion = _buffer.GetString();
            var genVersion = _buffer.GetString();
            var fileSize = _buffer.GetLong();
            var compressedSize = _buffer.GetUInt();
            var decompressedSize = _buffer.GetUInt();
            var flags = _buffer.GetUInt();
            var compressionType = flags & CompressionTypeFlagBits;
            if (compressionType != CompressionTypeLz4) return false;

            var compressedBytes = _buffer.GetBytes((int) compressedSize).ToList();
            var decompressedBytes= new byte[decompressedSize];
            var decompressResult = LZ4Codec.Decode(compressedBytes.ToArray(), 0, compressedBytes.Count, decompressedBytes, 0, decompressedBytes.Length);
            if (decompressResult != decompressedBytes.Length) return false;

            var dataBuffer = new BinaryBuffer(decompressedBytes);
            var guid = dataBuffer.GetBytes(16);

            var blockCount = dataBuffer.GetInt();
            for (var index = 0; index < blockCount; index++)
            {
                var blockSize1 = dataBuffer.GetInt();
                var blockSize2 = dataBuffer.GetInt();
                var blockFlags = dataBuffer.GetShort();
            }

            var nodeCount = dataBuffer.GetInt();
            for (var index = 0; index < nodeCount; index++)
            {
                var nodeOffset = dataBuffer.GetLong();
                var nodeSize = dataBuffer.GetLong();
                var nodeStatus = dataBuffer.GetInt();
                var nodeName = dataBuffer.GetString();
                var nodeBasePosition = dataBuffer.PeekInt();

            }
        }
    }
}
