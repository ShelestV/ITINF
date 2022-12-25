import cv2 as cv
import numpy as np
import math

def generate_walsh_matrix(number): 
    walsh_matrix = [[1]]
    number = math.floor(math.log(number, 2))
    for _ in range(0, number):
        walsh_matrix = double_matrix(walsh_matrix)
    return walsh_matrix

def double_matrix(matrix):
    matrix_length = len(matrix)
    output_matrix = np.zeros((matrix_length * 2, matrix_length * 2), int)
    for i in range(0, matrix_length):
        for j in range(0, matrix_length):
            output_matrix[i][j] = matrix[i][j]
            output_matrix[i + matrix_length][j] = matrix[i][j]
            output_matrix[i][j + matrix_length] = matrix[i][j]
            output_matrix[i + matrix_length][j + matrix_length] = -matrix[i][j]
    return output_matrix

def multiply_matrix_by_number(matrix, number):
    matrix_length = len(matrix)
    output_matrix = np.zeros((matrix_length, matrix_length), float)
    for i in range(0, matrix_length):
        for j in range(0, matrix_length):
            output_matrix[i][j] = matrix[i][j] * number
    return output_matrix

def convert_matrix_values_to_int(matrix):
    matrix_length = len(matrix)
    output_matrix = np.zeros((matrix_length, matrix_length), int)
    for i in range(0, matrix_length):
        for j in range(0, matrix_length):
            output_matrix[i][j] = math.floor(matrix[i][j])
    return output_matrix

def start_script(path_to_image):
    image = cv.imread(path_to_image)
    image_gray = cv.cvtColor(image, cv.COLOR_BGR2GRAY)
    walsh_size = 32
    walsh_matrix = generate_walsh_matrix(walsh_size)
    number = 1 / (walsh_size * walsh_size)
    transformation = multiply_matrix_by_number(np.dot(walsh_matrix, np.dot(image_gray, walsh_matrix)), number)
    revert_transformation = convert_matrix_values_to_int(np.dot(walsh_matrix, np.dot(transformation, walsh_matrix))).astype('uint8')
    revertImage = cv.cvtColor(revert_transformation, cv.COLOR_GRAY2BGR) 
    image_size = (512, 512)
    cv.imshow("initial", cv.resize(image_gray, image_size, interpolation = cv.INTER_AREA))
    cv.imshow("transformed", cv.resize(transformation, image_size, interpolation = cv.INTER_AREA))
    cv.imshow("reverted", cv.resize(revertImage, image_size, interpolation = cv.INTER_AREA))
    cv.waitKey(0)
    cv.destroyAllWindows()  

start_script("C:\\Users\\ShelestV\\Pictures\\Saved Pictures\\tired emotion.jpg")
