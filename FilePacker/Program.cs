﻿using System;
using System.Dynamic;
using System.IO;
using System.Text;
class Program
{
    public static string dataString = "";
    public static string progVer = "1.0";
    public static string[] pubArgs;
    public static string[] orgFolders;
    public static bool folderMode = false;
    public static FileFolder[] fileFolders;
    public static string[] charTable = { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "W", "X" };
    public static List<string> thingery;
    public static string[] tempCharTable;
    public class FileFolder()
    {
        public string[] path;
    }
    public static string ProcessFolderName(string folderName, string path)
    {

        bool addstuff = false;
        string[] splittedPath = path.Split(new string[] { "\\" }, StringSplitOptions.None);
        string result = "";
        for (int i = 0; i < splittedPath.Length; i++)
        {
            if (splittedPath[i] == folderName)
            {
                addstuff = true;
            }
            if (addstuff)
            {

                result += splittedPath[i] + "\\";
            }

        }
        return result;
    }
    public static void Main(string[] args)
    {
        //Console.WriteLine("Tool made by greensoupdev");
        /*string[] hi = { "patchbuilderv-1-3-3"};
        Unpack(hi);*/
        try
        {
            string[] urls = new string[0];
            pubArgs = args;
            if (args.Length != 0)
            {
                Console.WriteLine("Welcome to the File Packer v" + progVer + "!!\nThis tool packs your files.\nBy greensoupdev.\n");
                int thing = 0;
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-u")
                    {
                        continue;
                    }
                    else if (args[i] == "-p")
                    {
                        continue;
                    }
                    else
                    {
                        thing++;
                    }
                }
                urls = new string[thing];
                orgFolders = new string[thing];
                fileFolders = new FileFolder[thing];
                for (int i = 0; i < thing; i++)
                {
                    FileAttributes attr = File.GetAttributes(args[i]);



                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        folderMode = true;
                        System.IO.DirectoryInfo di = new DirectoryInfo(args[i]);

                        orgFolders[i] = args[i];
                        FileInfo[] TXTFiles = di.GetFiles("*.*", SearchOption.AllDirectories);
                        var dirName = new DirectoryInfo(args[i]).Name;

                        if (TXTFiles.Length != 0)
                        {
                            urls = new string[TXTFiles.Length];
                            fileFolders[i] = new FileFolder();
                            fileFolders[i].path = new string[TXTFiles.Length];
                            for (int k = 0; k < TXTFiles.Length; k++)
                            {
                                try
                                {

                                    fileFolders[i].path[k] = ProcessFolderName(dirName, TXTFiles[k].DirectoryName);
                                    urls[k] = TXTFiles[k].FullName;
                                }
                                catch (IOException)
                                {
                                    continue;
                                }
                            }
                        }

                    }
                    else
                    {
                        urls[i] = args[i];

                    }


                }



                if (Path.GetExtension(args[0]) == ".pkf" || args[args.Length - 1] == "-u")
                {
                    Unpack(urls);
                    Console.WriteLine("Files Unpacked!");
                }
                else if (args[args.Length - 1] == "-p")
                {
                    Pack(urls);
                    Console.WriteLine("Files Packed!");
                }
                else
                {
                    Console.WriteLine("What do you want to do? (Pack: 1 / Unpack: 2)");
                    string response = Console.ReadLine()?.Trim();

                    switch (response)
                    {
                        case "1":
                            Pack(urls);
                            Console.WriteLine("Files Packed!");
                            break;

                        case "2":
                            Unpack(urls);
                            Console.WriteLine("Files Unpacked!");
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Welcome to the File Packer v" + progVer + "!!\nThis tool packs your files.\nBy greensoupdev.\n\nIf you want to pack or unpack stuff, you can just grab the files or folders to this program and boom.");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
        Console.WriteLine("Press any key to exit..");
        string end = Console.ReadLine();
    }
   public static void Pack(string[] paths)
{

    if (folderMode)
    {

        for (int k = 0; k < orgFolders.Length; k++)
        {

            for (int i = 0; i < paths.Length; i++)
            {
                string passCode = "";
                thingery = new List<string>();
                tempCharTable = new string[charTable.Length];

                for (int h = 0; h < charTable.Length; h++)
                {
                    thingery.Add(charTable[h]);
                }
                Random rad = new Random();
                string parapa = "";
                for (int h = 0; h < charTable.Length; h++)
                {
                    int ahhhh = rad.Next(0, thingery.Count - 1);
                    tempCharTable[h] = thingery[ahhhh];
                    if (h == charTable.Length - 1)
                        passCode += thingery[ahhhh] + "Z";
                    else
                        passCode += thingery[ahhhh] + charTable[rad.Next(0, charTable.Length - 1)];
                    parapa += thingery[ahhhh];
                    thingery.RemoveAt(ahhhh);


                }
               


                if (!File.Exists(paths[i]))
                {
                    Console.WriteLine("File not found.");
                    continue;
                }
                Random random = new Random();
                Console.WriteLine("Packing: " + paths[i]);
                byte[] bytes = File.ReadAllBytes(paths[i]);
                string filename = Path.GetFileName(paths[i]);
                string thinng = BitConverter.ToString(bytes).Replace("-", "").Replace("0", tempCharTable[0]).Replace("1", tempCharTable[1]).Replace("2", tempCharTable[2]).Replace("3", tempCharTable[3]).Replace("4", tempCharTable[4]).Replace("5", tempCharTable[5]).Replace("6", tempCharTable[6]).Replace("7", tempCharTable[7]).Replace("8", tempCharTable[8]).Replace("9", tempCharTable[9]).Replace("A", tempCharTable[10]).Replace("B", tempCharTable[11]).Replace("C", tempCharTable[12]).Replace("D", tempCharTable[13]).Replace("E", tempCharTable[14]).Replace("F", tempCharTable[15]);
                dataString += "[HEADER]\n" + fileFolders[k].path[i] + filename + "\n" + thinng.Insert(random.Next(0, thinng.Length), "Z" + passCode) + "\n";
                File.WriteAllText("test.txt", BitConverter.ToString(bytes));
            }
            if (dataString != "")
            {
                File.WriteAllText(Path.GetDirectoryName(orgFolders[k]) + "\\" + Path.GetDirectoryName(orgFolders[k]).Split(new string[] { "\\" }, StringSplitOptions.None)[Path.GetDirectoryName(orgFolders[k]).Split(new string[] { "\\" }, StringSplitOptions.None).Length - 1] + "_PACKED.pkf", dataString);
                Console.WriteLine(Path.GetDirectoryName(orgFolders[k]) + "\\" + Path.GetDirectoryName(orgFolders[k]).Split(new string[] { "\\" }, StringSplitOptions.None)[Path.GetDirectoryName(orgFolders[k]).Split(new string[] { "\\" }, StringSplitOptions.None).Length - 1] + "_PACKED.pkf");
            }
        }
    }
    else
    {
        for (int i = 0; i < paths.Length; i++)
        {

            string passCode = "";
            thingery = new List<string>();
            tempCharTable = new string[charTable.Length];

            for (int h = 0; h < charTable.Length; h++)
            {
                thingery.Add(charTable[h]);
            }
            Random rad = new Random();
            string parapa = "";
            for (int h = 0; h < charTable.Length; h++)
            {
                int ahhhh = rad.Next(0, thingery.Count - 1);
                tempCharTable[h] = thingery[ahhhh];
                if (h == charTable.Length - 1)
                    passCode += thingery[ahhhh] + "Z";
                else
                    passCode += thingery[ahhhh] + charTable[rad.Next(0, charTable.Length - 1)];
                parapa += thingery[ahhhh];
                thingery.RemoveAt(ahhhh);


            }
           


            if (!File.Exists(paths[i]))
            {
                Console.WriteLine("File not found.");
                continue;
            }
            Random random = new Random();
            Console.WriteLine("Packing: " + paths[i]);
            byte[] bytes = File.ReadAllBytes(paths[i]);
            string filename = Path.GetFileName(paths[i]);
            string thinng = BitConverter.ToString(bytes).Replace("-", "").Replace("0", tempCharTable[0]).Replace("1", tempCharTable[1]).Replace("2", tempCharTable[2]).Replace("3", tempCharTable[3]).Replace("4", tempCharTable[4]).Replace("5", tempCharTable[5]).Replace("6", tempCharTable[6]).Replace("7", tempCharTable[7]).Replace("8", tempCharTable[8]).Replace("9", tempCharTable[9]).Replace("A", tempCharTable[10]).Replace("B", tempCharTable[11]).Replace("C", tempCharTable[12]).Replace("D", tempCharTable[13]).Replace("E", tempCharTable[14]).Replace("F", tempCharTable[15]);
            dataString += "[HEADER]\n" + filename + "\n" + thinng.Insert(random.Next(0, thinng.Length), "Z" + passCode) + "\n";

        }
        if (dataString != "")
        {
            File.WriteAllText(Path.GetDirectoryName(paths[0]) + "\\" + Path.GetDirectoryName(paths[0]).Split(new string[] { "\\" }, StringSplitOptions.None)[Path.GetDirectoryName(paths[0]).Split(new string[] { "\\" }, StringSplitOptions.None).Length - 1] + "_PACKED.pkf", dataString);
            Console.WriteLine(Path.GetDirectoryName(paths[0]) + "\\" + Path.GetDirectoryName(paths[0]).Split(new string[] { "\\" }, StringSplitOptions.None)[Path.GetDirectoryName(paths[0]).Split(new string[] { "\\" }, StringSplitOptions.None).Length - 1] + "_PACKED.pkf");
        }
    }
}

    public static void Unpack(string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (!File.Exists(paths[i]))
            {
                Console.WriteLine("File not found.");
                continue;
            }
            if (Path.GetExtension(paths[i]) != ".pkf")
            {
                Console.WriteLine("This is not a packed file.");
                continue;
            }
            Console.WriteLine("Unpacking: " + paths[i]);
            string[] file = File.ReadAllLines(paths[i]);
            int filesNumber = 0;
            for (int j = 0; j < file.Length; j++)
            {

                if (file[j] == "[HEADER]")
                {
                    filesNumber++;

                    string fileName = file[j + 1];
                    string[] locations = fileName.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string fileLocation = "";
                    for (int l = 0; l < locations.Length - 1; l++)
                    {
                        fileLocation += locations[l] + "\\";
                    }
                    string thingango = file[j + 2].Split(new string[] { "Z" }, StringSplitOptions.None)[1];
                    string passCode = "";
                    for (int l = 0; l < 32; l += 2)
                    {
                        passCode += thingango.ToCharArray()[l];
                    }
                    string[] passString = new string[passCode.ToCharArray().Length];
                    for (int l = 0; l < passCode.ToCharArray().Length; l++)
                    {
                        passString[l] = passCode.ToCharArray()[l] + "";
                    }

                    string[] diddy = file[j + 2].Replace(passString[0], "0").Replace(passString[1], "1").Replace(passString[2], "2").Replace(passString[3], "3").Replace(passString[4], "4").Replace(passString[5], "5").Replace(passString[6], "6").Replace(passString[7], "7").Replace(passString[8], "8").Replace(passString[9], "9").Replace(passString[10], "A").Replace(passString[11], "B").Replace(passString[12], "C").Replace(passString[13], "D").Replace(passString[14], "E").Replace(passString[15], "F").Split(new string[] { "Z" }, StringSplitOptions.None);

                    byte[] result = FromHex(diddy[0] + diddy[2]);
                    string folderName = "";
                    for (int k = 0; k < Path.GetFileName(paths[i]).Split(new string[] { "." }, StringSplitOptions.None).Length - 1; k++)
                    {
                        if (i == Path.GetFileName(paths[i]).Split(new string[] { "." }, StringSplitOptions.None).Length - 1)
                            folderName += Path.GetFileName(paths[i]).Split(new string[] { "." }, StringSplitOptions.None)[k];
                        else
                            folderName += Path.GetFileName(paths[i]).Split(new string[] { "." }, StringSplitOptions.None)[k] + ".";
                    }
                    Console.WriteLine(Path.GetDirectoryName(paths[i]) + "\\" + folderName + "\\" + fileLocation);
                    Directory.CreateDirectory(Path.GetDirectoryName(paths[i]) + "\\" + folderName + "\\" + fileLocation);
                    File.WriteAllBytes(Path.GetDirectoryName(paths[i]) + "\\" + folderName + "\\" + fileName, result);
                    Console.WriteLine("Unpacked: " + folderName + fileName);
                }
            }
        }
    }
    public static byte[] FromHex(string hex)
    {
        try
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
    }


}