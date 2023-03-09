using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    //The class Pixel permits to use pixels encoded in 3 bits (blue, red, green)
    //The itensity of each color goes from 0 to 255, then make a color with the others
    //sub-pixels by additive synthesis 

    class Pixel
    {
        //ATTRIBUTES
        private byte red;
        private byte green;
        private byte blue;

        //CONSTRUCTOR
        public Pixel(byte blue1, byte green1, byte red1)
        {
            this.red = red1;
            this.green = green1;
            this.blue = blue1;
        }

        //PROPERTIES
        public byte Red
        {
            get
            {
                return this.red;
            }
            set
            {
                this.red = value;
            }
        }
        public byte Green
        {
            get
            {
                return this.green;
            }
            set
            {
                this.green = value;
            }
        }
        public byte Blue
        {
            get
            {
                return this.blue;
            }
            set
            {
                this.blue = value;
            }
        }


        //METHODS

        public void Addition_Pixel(Pixel additionneur)
        {
            red += additionneur.Red;
            green += additionneur.Green;
            blue += additionneur.Blue;
        }
        public void Multiply_Values(int multiplicateur)
        {
            red = (byte)(red * multiplicateur);
            green = (byte)(green * multiplicateur);
            blue = (byte)(blue * multiplicateur);
        }
        public void Divide_Values(int divider)
        {
            if (divider == 0)
            {
                if (red < 0)
                    red = 0;
                if (red > 255)
                    red = 255;

                if (green < 0)
                    green = 0;
                if (green > 255)
                    green = 255;

                if (blue < 0)
                    blue = 0;
                if (blue > 255)
                    blue = 255;
            }
            else
            {
                red = (byte)(red / divider);
                green = (byte)(green / divider);
                blue = (byte)(blue / divider);
            }
        }

        /// <summary>
        /// The method Grey_SHades() permits to modify colors in grey shades. To do that, we use the average value of the itensities of each color. 
        /// </summary>
        public void Grey_Shades()
        {
            byte greyed_color = (byte)((red + green + blue) / 3);
            red = greyed_color;
            green = greyed_color;
            blue = greyed_color;
        }

        /// <summary>
        /// The method Inv_Colors inverts the color of a pixel (255 - value of the pixel)
        /// </summary>

        public void Inv_Colors()
        {
            byte color_invR = (byte)(255 - red);
            byte color_InvG = (byte)(255 - green);
            byte color_invB = (byte)(255 - blue);
            red = color_invR;
            green = color_InvG;
            blue = color_invB;

        }
    }
}