using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TgaReader
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    tgareader <filename.tga>");
                return -1;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    tgareader <filename.tga>");
                Console.WriteLine();
                Console.WriteLine($"\"{args[0]}\" not found.");
                return -1;
            }

            TgaHeader header;

            using (FileStream stream = new FileStream(args[0], FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    header = new TgaHeader();
                    header.IdLength = reader.ReadByte();
                    header.ColorMapType = reader.ReadByte();
                    header.ImageType = reader.ReadByte();

                    header.ColorMapSpecification.FirstEntryIndex = reader.ReadUInt16();
                    header.ColorMapSpecification.ColorMapLength = reader.ReadUInt16();
                    header.ColorMapSpecification.ColorMapEntrySize = reader.ReadByte();


                    header.ImageSpecification.XOrigin = reader.ReadUInt16();
                    header.ImageSpecification.YOrigin = reader.ReadUInt16();
                    header.ImageSpecification.ImageWidth = reader.ReadUInt16();
                    header.ImageSpecification.ImageHeight = reader.ReadUInt16();
                    header.ImageSpecification.PixelDepth = reader.ReadByte();
                    header.ImageSpecification.ImageDescriptor = reader.ReadByte();

                    header.ColorMapData.TgaColors =  new TgaColor[256];
                    for (int i = 0; i < 256; i++)
                    {
                        header.ColorMapData.TgaColors[i].Blue = reader.ReadByte();
                        header.ColorMapData.TgaColors[i].Green = reader.ReadByte();
                        header.ColorMapData.TgaColors[i].Red = reader.ReadByte();
                    }
                }
            }

            Console.WriteLine("TGA Info:");
            Console.WriteLine();
            Console.WriteLine($"  ColorMapLength:    {header.ColorMapSpecification.ColorMapLength}");
            Console.WriteLine($"  ColorMapEntrySize: {header.ColorMapSpecification.ColorMapEntrySize}");
            Console.WriteLine($"  XOrigin:           {header.ImageSpecification.XOrigin}");
            Console.WriteLine($"  YOrigin:           {header.ImageSpecification.YOrigin}");
            Console.WriteLine($"  ImageWidth:        {header.ImageSpecification.ImageWidth}");
            Console.WriteLine($"  ImageHeight:       {header.ImageSpecification.ImageHeight}");
            Console.WriteLine();
            Console.WriteLine("  Palette:");
            for (var i = 0; i < header.ColorMapData.TgaColors.Length; i++)
            {
                var color = header.ColorMapData.TgaColors[i];
                Console.WriteLine($"    {i:D3} - {color.Red:D3}, {color.Green:D3}, {color.Blue:D3}");
            }

            return 0;
        }
    }
}
