#!/usr/bin/python
# coding: utf-8

import sys
import os
import glob
import cv2

# フォルダ内のjpg画像サイズを1/2に変更(出力名は連番)
# ./resize.py <画像フォルダ> <出力フォルダ>

os.mkdir(sys.argv[2])
count=0
for f in glob.glob(os.path.join(sys.argv[1],"*.jpg")):
    img=cv2.imread(f)
    h,w=img.shape[:2]
    size=(w/2,h/2)
    himg=cv2.resize(img,size)
    count+=1
    cv2.imwrite(sys.argv[2]+'/'+str("{0:05d}".format(count))+'.jpg',himg)
