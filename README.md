# Image processing project - C# Bitmap from scratch 

Date : ~ 03/2021

## Introduction 

The main concept of this project was to recreate a part of the Bitmap class in C#. 

This class allows representing an image as a grid of pixels. It is part of the System.Drawing namespace and is used to manipulate bitmap images, i.e., images that are stored as matrices of pixels.

The Bitmap class provides methods for loading and saving images from files, as well as for drawing images on graphics surfaces. It also allows manipulating individual pixels, which enables modifying the colors and values of the pixels of the image.

![exemple_bitmap](https://user-images.githubusercontent.com/115212826/224155919-3931c543-eefb-46e5-8916-fc9045d3996c.png)

## How to compile

The folder contains the .sln of my code. In the Visual Studio project file, there are 2 pictures at bitmap format (.bmp) in « debug » folder (coco.bmp & lena.bmp). All the images that we want to modify should be in this folder, we also have to be sure that they are not « read-only ».

To compile, we can execute "FinalProject.exe" in the Debug folder. A menu appears in the console with a little interface where we can type numbers to select a picture and apply stuff to it. 

