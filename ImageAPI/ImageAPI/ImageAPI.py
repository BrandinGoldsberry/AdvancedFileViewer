from os import listdir
from PIL import Image
import os

images = listdir("C:/Users/Wesley Monk/Documents/Quarter 4 Classes/Intro Software Projects/ImageManipulation/")
dir1 = "C:/Users/Wesley Monk/Documents/Quarter 4 Classes/Intro Software Projects/ImageManipulation/"
counter = 0

for element in images:
    counter += 1
    im = Image.open(dir1 + element)
    out = im.rotate(180)
    #out = out.crop((25,0,225,200))
    out.thumbnail((75,75))
    im = out.convert("L")
    SaveImage(dir1)

def LoadImage(File):
    None

def SaveImage(Path):
    try:
        os.makedirs(Path)
    except FileExistsError:
        pass
    im.save(Path + "pic" + str(counter) + ".png", "PNG")

def RGBRecolor():
    None

def GrayscaleRecolor():
    None