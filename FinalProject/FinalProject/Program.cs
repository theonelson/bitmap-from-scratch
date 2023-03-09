using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FinalProject
{

    class Program
    {
        /// <summary>
        /// The class Program contains the main function with a menu to run every function.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            ConsoleKeyInfo cki;
            Console.WindowHeight = 35;
            Console.WindowWidth = 100;
            do
            {
                Console.Clear();


                Console.WriteLine("  Menu - Enter the number that corresponds to the request :\n"
                                 + "\n"
                                 + "    1. Modify a picture (color/size/mirror)\n"
                                 + "    2. Apply a filter (outlines/edge enhancement/blur/relief)\n"
                                 + "\n"
                                 + "    Enter the number that corresponds to the request ");
          
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("\n" + "Type the name of the file (Ex : coco.bmp) :  ");
                            string file = Console.ReadLine();
                            MyImage chosen_image = new MyImage(file);


                            Console.WriteLine("\n" + "     Modifying a picture :\n"
                                + "\n"
                                + "     Color : \n"
                                + "     1. Grey shades\n"
                                + "     2. Black and white\n"
                                + "     3. reverse colors\n"
                                + "\n" + "      Size : \n"
                                + "     4. Enlarge\n"
                                + "     5. Reduce\n"
                                + "     6. Crop\n"
                                + "\n" + "     7. Mirror effect \n");

                            int inside_choice = Convert.ToInt32(Console.ReadLine());
                            if (inside_choice == 1)
                            {
                                chosen_image.Creation_Grey_Shades();
                             
                                chosen_image.From_Image_To_File(file + "_Grey_shades.bmp");
                                Console.WriteLine("success");
                            }
                            if (inside_choice == 2)
                            {
                                chosen_image.Creation_Black_White();
                                chosen_image.From_Image_To_File(file + "_Black_and_white.bmp");
                                Console.WriteLine("success");
                            }
                            if (inside_choice == 3)
                            {
                                chosen_image.Inversion_Colors();
                                chosen_image.From_Image_To_File(file + "_Reversed_colors.bmp");
                                Console.WriteLine("success");
                            }
                            if (inside_choice == 4)
                            {
                                Console.Write("Type the number that will multiply the image size (ex : 2) : ");
                                int factor_size1 = Convert.ToInt32(Console.ReadLine());
                                chosen_image.Expand_Image(factor_size1);
                                chosen_image.From_Image_To_File(file + "_Bigger.bmp");
                                Console.WriteLine("success");
                            }
                            if (inside_choice == 5)
                            {
                                Console.Write("Type the number that will divide the image size : (ex : 4) ");
                                int factor_size2 = Convert.ToInt32(Console.ReadLine());
                                chosen_image.Reduce_Image(factor_size2);
                                chosen_image.From_Image_To_File(file + "_Smaller.bmp");
                                Console.WriteLine("success");
                            }
                            if (inside_choice == 6)
                            {
                                Console.Write("Type the size of the new image in % of the old one (Ex : 75) : ");
                                int porcent = Convert.ToInt32(Console.ReadLine());
                                chosen_image.Crop(porcent);
                                chosen_image.From_Image_To_File(file + "_Cropped.bmp");
                                Console.WriteLine("success");                           
                            }
                            if (inside_choice == 7)
                            {
                                Console.Write("chose the horizontal 'h' or vertical 'v' axis (Ex : h) : ");
                                string axis = Console.ReadLine();
                                chosen_image.Mirror(axis);
                                chosen_image.From_Image_To_File(file + "_Mirror.bmp");
                                Console.WriteLine("success");
                            }
                            else
                            {
                                Console.WriteLine("Enter a number between 1 and 7");
                            }
                            break;

                        }
                    case 2:
                        {
                            Console.Write("Type the name of the file (Ex : coco.bmp) :  ");
                            string file = Console.ReadLine();
                            MyImage image_filtered = new MyImage(file);


                            Console.WriteLine("     Filter to apply :\n"
                                + "\n"
                                + "     1. Blur\n"
                                + "     2. Relief\n"
                                + "     3. Detection of the outlines\n"
                                + "     4. strenghtening of the outlines\n"
                                + "     5. contrast\n");

                            int chosen_number = Convert.ToInt32(Console.ReadLine());
                            if (chosen_number == 1)
                            {
                                double[,] blur3 = new double[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                                image_filtered.Convolution(blur3);
                                image_filtered.From_Image_To_File(file + "_Blurred.bmp");
                                Console.WriteLine("success");
                            }
                            if (chosen_number == 2)
                            {
                                double[,] relief = new double[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                                image_filtered.Convolution(relief);
                                image_filtered.From_Image_To_File(file + "_Relief.bmp");
                                Console.WriteLine("success");
                            }
                            if (chosen_number == 3)
                            {
                                double[,] detection_outlines = new double[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                                image_filtered.Convolution(detection_outlines);
                                image_filtered.From_Image_To_File(file + "_Detection_outlines.bmp");
                                Console.WriteLine("success");
                            }
                            if (chosen_number == 4)
                            {
                                double[,] strengthening_outlines = new double[3, 3] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                                image_filtered.Convolution(strengthening_outlines);
                                image_filtered.From_Image_To_File(file + "_Strenghtening_outlines.bmp");
                                Console.Write("success");
                            }
                            if (chosen_number == 5)
                            {
                                double[,] contrast = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                                image_filtered.Convolution(contrast);
                                image_filtered.From_Image_To_File(file + "_Contrast.bmp");
                                Console.WriteLine("success");
                            }
                            else
                            {
                                Console.WriteLine("Please type a number between 1 and 5");
                            }
                            break;
                        }


                   

                }
                Console.WriteLine("Close the window to end the program.");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);

            Console.Read();
            //End of Program class
        }
    }
}