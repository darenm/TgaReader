using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TgaReader
{
    /*
    Image ID length (field 1)

    0–255 The number of bytes that the image ID field consists of. The image ID field can contain any information, but it is common for it to contain the date and time the image was created or a serial number.

    As of version 2.0 of the TGA spec, the date and time the image was created is catered for in the extension area.

    Color map type (field 2)

    has the value:

        0 if image file contains no color map
        1 if present
        2–127 reserved by Truevision
        128–255 available for developer use

    Image type (field 3)

    is enumerated in the lower three bits, with the fourth bit as a flag for RLE. Some possible values are:

        0 no image data is present
        1 uncompressed color-mapped image
        2 uncompressed true-color image
        3 uncompressed black-and-white (grayscale) image
        9 run-length encoded color-mapped image
        10 run-length encoded true-color image
        11 run-length encoded black-and-white (grayscale) image
        Image type 1 and 9: Depending on the Pixel Depth value, image data representation is an 8, 15, or 16 bit index into a 
            color map that defines the color of the pixel. Image type 2 and 10: The image data is a direct representation of the pixel color. 
            For a Pixel Depth of 15 and 16 bit, each pixel is stored with 5 bits per color. If the pixel depth is 16 bits, the topmost bit is 
            reserved for transparency. For a pixel depth of 24 bits, each pixel is stored with 8 bits per color. A 32-bit pixel depth defines 
            an additional 8-bit alpha channel. Image type 3 and 11: The image data is a direct representation of grayscale data. The pixel 
            depth is 8 bits for images of this type.

    Color map specification (field 4)

    has three subfields:

    First entry index (2 bytes): index of first color map entry that is included in the file
    Color map length (2 bytes): number of entries of the color map that are included in the file
    Color map entry size (1 byte): number of bits per pixel
    In case that not the entire color map is actually used by the image, a non-zero first entry index allows to store only a required part of the color map in the file.

    Image specification (field 5)

    has six subfields:

    X-origin (2 bytes): absolute coordinate of lower-left corner for displays where origin is at the lower left
    Y-origin (2 bytes): as for X-origin
    Image width (2 bytes): width in pixels
    Image height (2 bytes): height in pixels
    Pixel depth (1 byte): bits per pixel
    Image descriptor (1 byte): bits 3-0 give the alpha channel depth, bits 5-4 give direction
     */
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TgaHeader
    {
        public byte IdLength;
        public byte ColorMapType;
        public byte ImageType;
        public ColorMapSpec ColorMapSpecification;
        public ImageSpec ImageSpecification;
        public TgaColorMap ColorMapData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ColorMapSpec
    {

        public UInt16 FirstEntryIndex;
        public UInt16 ColorMapLength;
        public byte ColorMapEntrySize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ImageSpec
    {
        public UInt16 XOrigin;
        public UInt16 YOrigin;
        public UInt16 ImageWidth;
        public UInt16 ImageHeight;
        public byte PixelDepth;
        public byte ImageDescriptor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TgaColorMap
    {
        public TgaColor[] TgaColors;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TgaColor
    {
        public byte Blue;
        public byte Green;
        public byte Red;
    }

}
