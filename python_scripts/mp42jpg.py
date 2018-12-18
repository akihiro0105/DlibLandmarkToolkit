#!/usr/bin/python
# coding: utf-8

import sys
import os
import cv2

# mp4ファイルをjpg連番画像でvideoフォルダに出力(mp4ファイルを同じ階層)
# 出力時にfps/5に間引いて連番名で出力
# ./mp42jpg.py video.mp4

count=0
cap=cv2.VideoCapture(sys.argv[1])
root=os.path.splitext(sys.argv[1])
path=root[0]
os.mkdir(path)

while True:
    ret,frame=cap.read()
    if ret==True:
        count+=1
        if count%5==0:
        #frame=frame.transpose(1,0,2)[::-1] #-90度回転
        #frame=frame.transpose(1,0,2)[:,::-1] #90度回転
            cv2.imwrite(path+'/'+str("{0:05d}".format(count))+'.jpg',frame)
    else:
        break

