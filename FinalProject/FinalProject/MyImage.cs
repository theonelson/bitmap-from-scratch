using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    //The class 'MyImage' permits the conversion of a BMP image to
    //a sequence of bytes, or a sequence of bytes to an BMP image.
    //It includes all the image processing methodes

    class MyImage
    {
        //---------------------------Set up of the pivot format-------------------------------------------------------
        //ATTRIBUTES
        private byte[] Header = new byte[54];

        private int size_image;
        private byte[] tab_size_image = new byte[4]; //bytes that describes the image size

        private int offset;
        private byte[] tab_offset = new byte[4]; //bytes that describes the offset

        private int size_header;
        private byte[] tab_size_header = new byte[4]; //bytes that describes the header size

        private int width_image;
        private byte[] tab_width_image = new byte[4]; //bytes that describes the image width

        private int height_image;
        private byte[] tab_height_image = new byte[4]; //bytes that describes the image height

        private int number_bits_per_color;
        private byte[] tab_number_bits_per_color = new byte[2];

        private int size_file;
        private byte[] tab_size_file = new byte[4];

        private string type_image;

        private Pixel[,] matrix_image;


        private byte[] header = new byte[54];

        //CONSTRUCTOR
        /// <summary public MyImage>
        /// The constructor analyzes the header and its different components, then convert it and 
        /// puts it in a bytes table. 
        /// </summary>
        /// <param name="myfile"> lit un fichier et le transforme en instance de classe MyImage</param>

        public MyImage(string myfile)
        {
            byte[] Myfile = File.ReadAllBytes(myfile); //création d'un tableau (Myfile) contenant toutes les informations de l'image
            type_image = "";

            for (int i = 0; i < 2; i++)
            {
                char format = (char)Myfile[i];
                type_image += format;
            }

            for (int i = 2; i < 6; i++)
            {
                tab_size_file[i - 2] = Myfile[i];
                header[i] = Myfile[i];
            }
            size_file = Convert_Endian_To_Int(tab_size_file);

            for (int i = 6; i < 10; i++)
            {
                header[i] = Myfile[i];
            }

            for (int i = 10; i < 14; i++)
            {
                tab_offset[i - 10] = Myfile[i];
                header[i] = Myfile[i];
            }
            offset = Convert_Endian_To_Int(tab_offset);

            for (int i = 14; i < 18; i++)
            {
                tab_size_header[i - 14] = Myfile[i];
                header[i] = Myfile[i];
            }
            size_header = Convert_Endian_To_Int(tab_size_header);

            for (int i = 18; i < 22; i++)
            {
                tab_width_image[i - 18] = Myfile[i];
                header[i] = Myfile[i];
            }
            width_image = Convert_Endian_To_Int(tab_width_image);

            for (int i = 22; i < 26; i++)
            {
                tab_height_image[i - 22] = Myfile[i];
            }
            height_image = Convert_Endian_To_Int(tab_height_image);

            for (int i = 26; i < 28; i++)
            {
                header[i] = Myfile[i];
            }

            for (int i = 28; i < 30; i++)
            {
                tab_number_bits_per_color[i - 28] = Myfile[i];
                header[i] = Myfile[i];
            }
            number_bits_per_color = Convert_Endian_To_Int(tab_number_bits_per_color);

            for (int i = 30; i < 34; i++)
            {
                header[i] = Myfile[i];
            }

            for (int i = 34; i < 38; i++)
            {
                tab_size_image[i - 34] = Myfile[i];
                header[i] = Myfile[i];
            }
            size_image = Convert_Endian_To_Int(tab_size_image);

            for (int i = 38; i < 54; i++)
            {
                header[i] = Myfile[i];
            }

            //We just ordered our header by defining all the components in int format
            //Now we are going to create a pixels matrix that contains the content of the image 
            //The numbers in the header gives us the itensity of the RGB color (red, green and blue)
            //This boucle ends when we have set up all the pixels. 

            matrix_image = new Pixel[height_image, width_image];
            int value = 54; //Numbers before 54 were the informations in the header

            for (int i = 0; i < height_image; i++)
            {
                for (int j = 0; j < width_image; j++)
                {
                    matrix_image[i, j] = new Pixel(Myfile[value], Myfile[value + 1], Myfile[value + 2]);
                    value += 3; //Each pixel contains 3 bits                             
                }
            }
        }


        //----------------------------------------  METHODS  ----------------------------------------------------------------

        /// <summary>
        /// Convert_Endian_To_Int permits to convert a sequence of bytes in 'little Endian' format to an other sequence of 
        /// bytes in integer numbers format. 
        /// </summary>
        /// We use the 'Pow' method : returns a specified number to the specified power (2^8 = 256 to the 'i'th).
        /// <param name="tab"></param>
        /// <returns> The methods returns an integer converted from the sequence of bytes.</returns>
        public int Convert_Endian_To_Int(byte[] tab)
        {
            int integer_conversion = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                integer_conversion += tab[i] * ((int)Math.Pow(256, i));
            }
            return integer_conversion;
        }


        /// <summary>
        /// From_Image_To_File takes an instance of MyImage and turns it in binary file that respects the structure of the file .bmp
        /// </summary>
        /// <param name="file"></param>

        public void From_Image_To_File(string file)
        {

            byte[] table = new byte[size_file];


            //components of the image format .bmp (the same for all the processed images)
            table[0] = 66; //B
            table[1] = 77; //M


            for (int i = 2; i < 6; i++) //image size
            {
                table[i] = tab_size_file[i - 2];
            }

            for (int i = 6; i < 10; i++) //bits in stock
            {
                table[i] = 0;
            }

            for (int i = 10; i < 14; i++) //offset
            {
                table[i] = tab_offset[i - 10];
            }

            for (int i = 14; i < 18; i++) //header size
            {
                table[i] = tab_size_header[i - 14];
            }

            for (int i = 18; i < 22; i++) //width
            {
                table[i] = tab_width_image[i - 18];
            }

            for (int i = 22; i < 26; i++) //height
            {
                table[i] = tab_height_image[i - 22];
            }

            table[26] = 01;

            table[27] = 0;

            for (int i = 28; i < 30; i++) //number of bits per color
            {
                table[i] = tab_number_bits_per_color[i - 28];
            }

            for (int i = 30; i < 34; i++)
            {
                table[i] = 0;
            }

            for (int i = 34; i < 38; i++)
            {
                table[i] = tab_size_image[i - 34];
            }

            for (int i = 38; i < 54; i++)
            {
                table[i] = 0;
            }

            int index = 54;

            //we make a loop to attribute the colors, we use 'Pixel' class to do this. 
            for (int i = 0; i < matrix_image.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_image.GetLength(1); j++)
                {
                    table[index] = Convert.ToByte(matrix_image[i, j].Blue);
                    table[index + 1] = Convert.ToByte(matrix_image[i, j].Green);
                    table[index + 2] = Convert.ToByte(matrix_image[i, j].Red);
                    index += 3;
                }
            }

            File.WriteAllBytes(file, table);
        }

        /// <summary>
        /// The method Convert_Int_To_Endian permits to convert an integer to a sequence of bits in 'Little Endian' format
        /// The little Endian format is the sum of the multipliers of the powers of 256 by integers
        /// To do that, we stock the integer we want to convert, we find the smallest power of 256 which exceeds this integer
        /// then, we do a loop that goes from this power 256 to 0
        /// then, we divide the integer by this power. We obtain the quotient, that we put in the first box of our table
        /// (we are now in little Endian format)
        /// Then, we take the rest of the euclidean division, then we do the same calculous (we do not forget to increment the power)
        /// We use the pow method as for the previous Convert_Endian_To_Int method.      
        /// </summary>
        /// <param name="val"> integer that we convert to a sequence of bits in Little Endian format </param>
        /// <returns>A table composed of a sequence of bits in Little Endian format (int numbers)</returns>
        public byte[] Convert_Int_To_Endian(int val, int size)
        {
            byte[] tab = new byte[size];
            int rest = val % 256;
            tab[0] = (byte)rest;
            val = val - rest;
            for (int i = size - 1; i > 0; i--)
            {
                int q = val / ((int)Math.Pow(256, i));
                tab[i] = (byte)q;
                val = val - ((int)Math.Pow(256, i) * q);
            }
            return tab;
        }





        //------------------------------------ image processing methods --------------------------------------

        //METHODS ABOUT COLORS
        //we have 3 methods :  Creation_Grey_Shades() / Creation_Black_White() / Inversion_Colors()


        /// <summary>
        /// This method turns a colored image into a grey shades image. 
        /// We go across the each pixel of the matrix_image and we modify the value of each color 
        /// using Grey_Shades() method in the Pixel class
        /// </summary>
        public void Creation_Grey_Shades()
        {
            for (int ligne = matrix_image.GetLength(0) - 1; ligne >= 0; ligne--)
            {
                for (int colonne = 0; colonne < matrix_image.GetLength(1); colonne++)
                {
                    matrix_image[ligne, colonne].Grey_Shades();
                }
            }
        }

        /// <summary>
        /// The method Creation_Black_White() allows to turn a colored image to an image excusively composed by white (255 255 255) pixels
        /// or black (0 0 0) pixels. 
        /// To know which pixel should become black or white, we use a base value that we compare with the medium value of the sum of the 3 pixels
        /// (we do 3*255/2)
        /// </summary>
        public void Creation_Black_White()
        {
            int seuil = (3 * 255) / 2;
            for (int row = matrix_image.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < matrix_image.GetLength(1); column++)
                {
                    int somme = matrix_image[row, column].Red + matrix_image[row, column].Green + matrix_image[row, column].Blue;

                    //CASE BLACK
                    if (somme <= seuil)
                    {
                        matrix_image[row, column].Red = 0;
                        matrix_image[row, column].Green = 0;
                        matrix_image[row, column].Blue = 0;
                    }
                    //CASE WHITE
                    else
                    {
                        matrix_image[row, column].Red = 255;
                        matrix_image[row, column].Green = 255;
                        matrix_image[row, column].Blue = 255;
                    }

                }
            }
        }

        /// <summary>
        /// The method Inversion_Colors() allows to invert the colorsof the image
        /// We enter an image and the methods returns a matrix where each pixel is (255 - [color value])
        /// </summary>
        public void Inversion_Colors()
        {
            for (int row = matrix_image.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < matrix_image.GetLength(1); column++)
                {
                    matrix_image[row, column].Inv_Colors();
                }
            }
        }


        //METHODS ABOUT IMAGE SIZE


        /// <summary>
        /// The method Expend_Image allows to make an image bigger by increasing the number of pixels
        /// (the methods do not increase the resolution). It consits in modifying the dimensions of the
        /// image depending on a (int) number we enter inside. 
        /// 
        /// For each slot, we duplicate the pixel for every unit of the number entered. 
        /// (ex : if the number is "2" the image size is multiplied by 2 -> the pixel in position [0,0] will 
        /// be in [0,0],[0,1],[1,0] and [1,1] in the new image). 
        /// 
        /// To do that, we create a new matrix that will be the returned image (its dimensions depends on the number that we entered before) ,  
        /// we go through the former image (matrix_image). 
        /// In the 2nd loop : We introduce an other loop (to go through the new matrix)
        /// To finish, we associate to each cell the pixel of the former image (currently chosen by the first loop)
        /// </summary>
        public void Expand_Image(int factor_increase)
        {
            Pixel[,] Image_returned = new Pixel[height_image * factor_increase, width_image * factor_increase];

            for (int i = 0; i < matrix_image.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_image.GetLength(1); j++)
                {
                    Image_returned[i * factor_increase, j * factor_increase] = matrix_image[i, j];

                    for (int h = 1; h < factor_increase; h++)
                    {
                        for (int k = 1; k < factor_increase; k++)
                        {
                            Image_returned[i * factor_increase, j * factor_increase + k] = matrix_image[i, j];
                            Image_returned[i * factor_increase + k, j * factor_increase] = matrix_image[i, j];
                            Image_returned[i * factor_increase + k, j * factor_increase + k] = matrix_image[i, j];

                            Image_returned[i * factor_increase + h, j * factor_increase + k] = matrix_image[i, j];
                            Image_returned[i * factor_increase + k, j * factor_increase + h] = matrix_image[i, j];
                        }
                    }
                }
            }
            matrix_image = Image_returned;
            //mise à jour des informations de l'image pour le header
            height_image = height_image * factor_increase;
            width_image = width_image * factor_increase;

            tab_height_image = Convert_Int_To_Endian(height_image, 4);
            tab_width_image = Convert_Int_To_Endian(width_image, 4);

            size_image = height_image * width_image * 3;
            tab_size_image = Convert_Int_To_Endian(size_image, 4);

            size_file = size_image + 54;
            tab_size_file = Convert_Int_To_Endian(size_file, 4);

        }

        /// <summary>
        /// The method Mini_Dimension() find the smallest dimension of the image 
        /// </summary>
        /// <returns>The smallest dimension of the image</returns>
        public int Mini_Dimension()
        {
            if (height_image > width_image)
            {
                return width_image;
            }
            else
            {
                return height_image;
            }
        }

        /// <summary>
        /// The method Reduce_Image() allows to reduce an image (it is the opposite of Expand_Image()
        /// We have to modify the image dimensions depending on the int number that we entered before. 
        /// 
        /// How to create a pixel in the image returned :
        /// We start from the up-left side of matrix_image, we take a number of pixels
        /// This number is a n*n matrix which have the size of the entered number. 
        /// We calculate the medium itensity of the square for each RGB color
        /// we put this value in the new matrix (image_returned)
        /// 
        /// How to create the image returned : 
        /// We divide the height of the picture by the number entered before. 
        /// 
        /// If this number and the height are not multiples :
        /// We have a problem because of the rest, so we add a row/column to the image returned
        /// In this case, height = height (first image) + number entered before - rest
        /// We divide this new height by the number (this height is called height_image_returned)
        /// </summary>
        public int Reduce_Image(int factor_reduce)
        {

            //We check if factor_reduce is possible, which means if the height and the witdh
            //can be devided by this number 
            int counter = 0;
            
            //calculates the number of same dividers 
            for (int i = 1; i < Mini_Dimension(); i++)
            {
                if (height_image % i == 0 && width_image % i == 0)
                {
                    counter++;
                }
            }
            int[] common_dividers = new int[counter];
            counter = 0;

            //enters all the dividers in a table
            for (int i = 1; i < Mini_Dimension(); i++)
            {
                if (height_image % i == 0 && width_image % i == 0)
                {
                    common_dividers[counter] = i;
                    counter++;
                }
            }
            bool end = false;

            //compares each divider with the entered number 
            while (end != true)
            {
                for (int i = 0; i < common_dividers.Length; i++)
                {
                    if (factor_reduce == common_dividers[i])
                    {
                        end = true;
                    }
                }
                if (end == false)
                {
                    Console.Clear();       
                    Console.WriteLine("The chosen factor_reduce is not a common divider if the height and the width. Pleaser enter a new coefficient.");
                    factor_reduce = Convert.ToInt32(Console.ReadLine());
                }
            }

            //Creation of the new matrix
            Pixel[,] Image_returned = new Pixel[height_image / factor_reduce, width_image / factor_reduce];
            int sum_pixel_blue = 0;
            int sum_pixel_green = 0;
            int sum_pixel_red = 0;
            for (int i = 0; i < Image_returned.GetLength(0); i++)
            {
                for (int j = 0; j < Image_returned.GetLength(1); j++)
                {
                    for (int h = i * factor_reduce; h < i * factor_reduce + factor_reduce; h++)
                    {
                        for (int k = j * factor_reduce; k < j * factor_reduce + factor_reduce; k++)
                        {
                            sum_pixel_blue += matrix_image[h, k].Blue;
                            sum_pixel_green += matrix_image[h, k].Green;
                            sum_pixel_red += matrix_image[h, k].Red;
                        }
                    }
                    sum_pixel_blue = sum_pixel_blue / (factor_reduce * factor_reduce);
                    sum_pixel_green = sum_pixel_green / (factor_reduce * factor_reduce);
                    sum_pixel_red = sum_pixel_red / (factor_reduce * factor_reduce);

                    Image_returned[i, j] = new Pixel((byte)(sum_pixel_blue), (byte)(sum_pixel_green), (byte)(sum_pixel_red));

                    sum_pixel_blue = 0;
                    sum_pixel_green = 0;
                    sum_pixel_red = 0;
                }
            }
            matrix_image = Image_returned;
            //Update of the image informations in the header
            height_image = height_image / factor_reduce;
            width_image = width_image / factor_reduce;

            tab_height_image = Convert_Int_To_Endian(height_image, 4);
            tab_width_image = Convert_Int_To_Endian(width_image, 4);

            size_image = height_image * width_image * 3;
            tab_size_image = Convert_Int_To_Endian(size_image, 4);

            size_file = size_image + 54;
            tab_size_file = Convert_Int_To_Endian(size_file, 4);

            return factor_reduce;
        }


        /// <summary>
        /// The method Crop() asks a % and returns the image with edges cut to the size of this %.*
        /// First, we calculate the new dimensions and we creatte the new matrix
        /// We only go through the surface that represents the remaining part of the image
        /// we attributes to each new pixel the value of the old pixels inside the surface. 
        /// </summary>
        /// <param name="percent"></param>

        public void Crop(int percent)
        {
            //calculates the new dimensions regarding on the % entered
            int height2 = Convert.ToInt32((height_image * percent) / 100);
            int width2 = Convert.ToInt32((width_image * percent / 100));

            Pixel[,] new_mat = new Pixel[height2, width2];
            for (int row = (height_image - height2) / 2; row < (height_image + height2) / 2; row++)
            {
                for (int column = (width_image - width2) / 2; column < (width_image + width2) / 2; column++)
                {
                    new_mat[row - (height_image - height2) / 2, column - (width_image - width2) / 2] = matrix_image[row, column];
                }
            }
            matrix_image = new_mat;
            //Update of the image informations in the header           
            height_image = height2;
            width_image = width2;

            tab_height_image = Convert_Int_To_Endian(height_image, 4);
            tab_width_image = Convert_Int_To_Endian(width_image, 4);

            size_image = height_image * width_image * 3;
            tab_size_image = Convert_Int_To_Endian(size_image, 4);

            size_file = size_image + 54;
            tab_size_file = Convert_Int_To_Endian(size_file, 4);
        }

        //METHOD Mirror

        
        /// <summary>
        /// The method Mirror() returns the symmetrical image of the beginning, according to 2 axes : horizontal and vertical
        /// </summary>
        /// <param name="x">chose the direction of the symmetry (horizontal or vertical)</param>
        public void Mirror(string x)
        {
            Pixel[,] Image_returned = new Pixel[height_image, width_image];

            if (x == "h")
            {
                for (int colonne = 0; colonne < height_image; colonne++)
                {
                    for (int ligne = 0; ligne < width_image; ligne++)
                    {
                        Image_returned[colonne, ligne] = matrix_image[colonne, width_image - ligne - 1];
                    }
                }
                matrix_image = Image_returned;
            }
            else if (x == "v")
            {
                for (int ligne = 0; ligne < width_image; ligne++)
                {
                    for (int colonne = 0; colonne < height_image; colonne++)
                    {
                        Image_returned[colonne, ligne] = matrix_image[height_image - colonne - 1, ligne];
                    }
                }
                matrix_image = Image_returned;
            }
            else
            {
                Console.WriteLine("Wrong entry, please type 'h' or 'v'. ");
            }
        }



        //------------------------- Filters : convolution matrix (kernel) -----------------------
        //detection of the contours - strenghtening of the edges - gaussian blur - relief 


        /// <summary>
        /// The medoth Convultion() returns an image with the chosen filter applied
        /// To do that, we need to enter a convolution matrix that corresponds to a filter
        /// (ex : [1,1,1],[1,1,1],[1,1,1] is the matrix which applies blur filter)
        /// working : we isolate each 3x3 (or more) pixels bloc, it forms a little matrix that we multiply with
        /// the convolution matrix. 
        /// </summary>
        /// <param name="conv"></param>

        public void Convolution(double[,] conv)
        {
            Pixel[,] new_mat = new Pixel[height_image, width_image]; //creation of our new matrix           
            for (int i = 0; i < new_mat.GetLength(0); i++)
            {
                for (int j = 0; j < new_mat.GetLength(1); j++)
                {
                    new_mat[i, j] = matrix_image[i, j];
                }
            }

            double sumR = 0;
            double sumB = 0;
            double sumG = 0;
            double sum_conv = 0;

            //calcul of the sum of the elements in the convolution matrix 
            //in order to divide/multiply it if the value is too big/small 
            for (int i = 0; i < conv.GetLength(0); i++)
            {
                for (int j = 0; j < conv.GetLength(1); j++)
                {
                    sum_conv += conv[i, j];
                }
            }
            if (sum_conv == 0)
            {
                sum_conv = 1;
            }
            if (sum_conv == 1)
            {
                sum_conv = 2;
            }

            //we go through the matrix image
            for (int ligne = (conv.GetLength(0) - 1); ligne < new_mat.GetLength(0) - (conv.GetLength(0) - 1); ligne++)
            {
                for (int colonne = (conv.GetLength(1) - 1); colonne < new_mat.GetLength(1) - (conv.GetLength(1) - 1); colonne++)
                {

                    //Product of the convolution matrix and the matrix image around the chosen pixel                  
                    for (int a = 0; a < conv.GetLength(0); a++)
                    {
                        for (int b = 0; b < conv.GetLength(1); b++)
                        {
                            sumB += conv[a, b] * matrix_image[a + ligne, b + colonne].Blue;
                            sumG += conv[a, b] * matrix_image[a + ligne, b + colonne].Green;
                            sumR += conv[a, b] * matrix_image[a + ligne, b + colonne].Red;

                        }
                    }

                    sumB = Convert.ToInt32(sumB / sum_conv);
                    sumG = Convert.ToInt32(sumG / sum_conv);
                    sumR = Convert.ToInt32(sumR / sum_conv);

                    //prevent the pixels from exceeding the color values
                    if (sumR < 1)
                    {
                        sumR = 0;
                    }
                    if (sumG < 1)
                    {
                        sumG = 0;
                    }
                    if (sumB < 1)
                    {
                        sumB = 0;
                    }

                    if (sumR > 255)
                    {
                        sumR = 255;
                    }
                    if (sumB > 255)
                    {
                        sumB = 255;
                    }
                    if (sumG > 255)
                    {
                        sumG = 255;
                    }
                    //we fill the new matrix with the result of the matrix product
                    new_mat[ligne, colonne] = new Pixel((byte)sumB, (byte)sumG, (byte)sumR);

                }
            }

            matrix_image = new_mat;

        }





        //END of the class 'MyImage'

    }
}