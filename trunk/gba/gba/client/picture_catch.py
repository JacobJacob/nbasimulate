
import Image
import sys
import os

DIR = 'e://pictures/'
NEW_DIR = 'e://pictures/new'

def cut_image(file, i):
    im = Image.open(file)
    im2 = Image.new('RGB', (95, 95))
    x  = 419
    y = 262
    z = x + 95
    w = y + 95
    regoin = (x, y , z, w)
    print regoin
    box = im.crop(regoin)
    im2.paste(box, (0 , 0 , 95 , 95))
    im2.save(os.path.join(NEW_DIR, '%s.jpg' % i))

if __name__ == '__main__':
    
    names = os.listdir(DIR)
    #print names
    i = 1
    for name in names:
        name = os.path.join(DIR, name)
        if os.path.isfile(name):
            i += 1
            cut_image(name, i)