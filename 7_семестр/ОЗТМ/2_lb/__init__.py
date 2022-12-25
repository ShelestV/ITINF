import numpy
import cv2
from graphics import *

def create_pixel(win, x, y):
    pt = Point(x, y)
    pt.draw(win)

def find_code(min_x, max_x, min_y, max_y, x, y):
    code = 0

    coef1 = ((x - min_x) >> 27) & 1
    coef2 = ((max_x - x) >> 27) & 1
    coef3 = ((y - min_y) >> 27) & 1
    coef4 = ((max_y - y) >> 27) & 1

    return code | coef1 | (coef2 << 1) | (coef3 << 2) | (coef4 << 3)

def clamp(min_x, max_x, min_y, max_y, x0, y0, x1, y1):
    while(True):
        code1 = find_code(min_x, max_x, min_y, max_y, x0, y0)
        code2 = find_code(min_x, max_x, min_y, max_y, x1, y1)

        if (code1 | code2) == 0:
            return (True, x0, y0, x1, y1)
        
        if (code1 & code2) != 0:
            return (False, -1, -1, -1, -1)
        
        if code1 == 0:
            (x0, x1) = (x1, x0)
            (y0, y1) = (y1, y0)
            (code1, code2) = (code2, code1)

        coef = 0

        if x0 != x1:
            coef = (y1 - y0) / (x1 - x0)

        if code1 & 1:
            y0 += coef * (min_x - x0)
            x0 = min_x
        elif code1 & 2:
            y0 += coef * (max_x - x0)
            x0 = max_x
        elif code1 & 4:
            if x0 != x1:
                x0 += (min_y - y0) / coef
            y_0 = min_y
        elif code1 & 8:
            if x0 != x1:
                x0 += (max_y - y0) / coef
            y0 = max_y
        
        x0 = round(x0)
        y0 = round(y0)
        x1 = round(x1)
        y1 = round(y1)

x1 = 200
y1 = 320
x2 = 280
y2 = 100

x_diff = abs(x2 - x1)
y_diff = abs(y2 - y1)
coef = y_diff / float(x_diff)
x, y = x1, y1

window = GraphWin('line', 500, 500)

if coef > 1:
    x_diff, y_diff = y_diff, x_diff
    x, y = y, x
    x1, y1 = y1, x1
    x2, y2 = y2, x2
    
p = 2 * y_diff - x_diff
create_pixel(window, x, y)
for _ in range(2, x_diff):
    if p > 0:
        y = y + 1 if y < y2 else y - 1
        p = p + 2 * (y_diff - x_diff)
    else:
        p = p + 2 * y_diff
    x = x + 1 if x < x2 else x - 1
    
    create_pixel(window, x, y)

_, prepared_image = cv2.threshold(cv2.imread('./2_lb/headphones.jpg'), 200, 255, cv2.THRESH_BINARY_INV)
image = prepared_image.copy()
rows, columns = prepared_image.shape[:2]
mask = numpy.zeros((rows + 2, columns + 2), numpy.uint8)
cv2.floodFill(image, mask, (0, 0), (255, 255, 255))
cv2.imshow('Full Mask', image)
cv2.waitKey(0)

lines = [
    (250, 100, 400, 250), 
    (100, 180, 400, 180), 
    (320, 250, 250, 330), 
    (300, 400, 250, 120), 
    (50, 300, 200, 200)
]
colors = ["blue", "green", "red", "yellow", "purple"]

x_min = y_min = 150
x_max = y_max = 350
size = 500

window1 = GraphWin("Initial", size, size)
window2 = GraphWin("Clipped", size, size)

rectangle = Polygon(Point(x_min, y_min), Point(x_min, y_max), Point(x_max, y_max), Point(x_max, y_min))
rectangle.setOutline("black")
rectangle.setWidth(5)
rectangle.draw(window1)
polygon = rectangle.clone()
polygon.draw(window2)
for line, color in zip(lines, colors):
    (x0, y0, x1, y1) = line
    line1 = Line(Point(x0, y0), Point(x1, y1))
    line1.setOutline(color)
    line1.setWidth(5)
    line1.draw(window1)
    (exist, x0_2, y0_2, x1_2, y1_2) = clamp(x_min, x_max, y_min, y_max, x0, y0, x1, y1)
    if(exist):
        line2 = Line(Point(x0_2,y0_2), Point(x1_2,y1_2))
        line2.setOutline(color)
        line2.setWidth(5)
        line2.draw(window2)
