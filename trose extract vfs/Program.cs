



using System.Text;
using trose_extract_vfs;


Console.WriteLine("Path to rose:");
string rosePath = Console.ReadLine();

StreamReader dataStructureStream = new StreamReader(rosePath+"\\data.idx");
StreamReader dataStream = new StreamReader(rosePath+"\\rose.vfs");

string outPutFolder = "out";

if (Directory.Exists(outPutFolder) == false)
    Directory.CreateDirectory(outPutFolder);
else
    Console.WriteLine("out exist");

//34
int moveOffset = 1;
byte[] buffer = new byte[255];





while (!dataStructureStream.EndOfStream)
{
    chunkInfo fileInfo = new chunkInfo();

   if(moveOffset==1)
   moveOffset= (int)dataStructureStream.BaseStream.Seek(0x231d65, SeekOrigin.Begin);
    dataStructureStream.BaseStream.Read(buffer, 0, 2);
    ushort filePathTextSize = BitConverter.ToUInt16(buffer,0);
    fileInfo.fileNameLength = filePathTextSize;

    dataStructureStream.BaseStream.Read(buffer, 0, filePathTextSize);
    string path = Encoding.ASCII.GetString(buffer, 0, filePathTextSize);
    fileInfo.filePath = path;

    dataStructureStream.BaseStream.Read(buffer, 0, 8);
    fileInfo.fileOffset = BitConverter.ToUInt32(buffer, 0);
    fileInfo.fileSize = BitConverter.ToUInt32(buffer, 4);

    Console.WriteLine($"Path: {fileInfo.filePath}\nFile offset:{fileInfo.fileOffset.ToString("X")}\nFile size:{fileInfo.fileSize}");
    dataStructureStream.BaseStream.Read(buffer, 0, 12);

        if(fileInfo.filePath.LastIndexOf("/")!=-1)
        Directory.CreateDirectory(fileInfo.filePath.Substring(0, fileInfo.filePath.LastIndexOf("/")));
        FileStream file = File.Create(fileInfo.filePath);

        byte[] fileRaw = new byte[fileInfo.fileSize];
        dataStream.BaseStream.Seek(fileInfo.fileOffset, SeekOrigin.Begin);
        dataStream.BaseStream.Read(fileRaw, 0, fileRaw.Length);
        file.Write(fileRaw, 0, fileRaw.Length);
        file.Flush();
        file.Close();
    
}


