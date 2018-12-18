#!/usr/bin/python
# coding: utf-8

import sys
import dlib
import cv2

# mp4動画で物体検出と物体内の特徴点検出を確認
# ./checkVideo.py video.mp4 landmark.svm landmark.dat

detector = dlib.simple_object_detector(sys.argv[2])
cam = cv2.VideoCapture(sys.argv[1])
predictor = dlib.shape_predictor(sys.argv[3])

color_green = (0,255,0)
line_width = 3
while True:
    ret_val, img = cam.read()
    h,w=img.shape[:2]
    size=(w/2,h/2)
    img=cv2.resize(img,size)
    rgb_image = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    dets = detector(rgb_image)
    for det in dets:
        cv2.rectangle(img,(det.left(), det.top()), (det.right(), det.bottom()), color_green, line_width)
        shape = predictor(img, det)
        for s in range(shape.num_parts):
            cv2.circle(img,(shape.part(s).x,shape.part(s).y),2, color_green)
    cv2.imshow('my webcam', img)
    if cv2.waitKey(1) == 27:
        break  # esc to quit
cv2.destroyAllWindows()
