using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trose_extract_vfs
{
    struct chunkInfo
    {
        public ushort fileNameLength;
        public string filePath;
        public uint fileOffset;
        public uint fileSize;
    };
}
