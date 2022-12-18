import keras 
from keras.datasets import fashion_mnist 
import numpy as np 
import matplotlib.pyplot as plt

(x_train, y_train), (x_test, y_test) = fashion_mnist.load_data()
classes = {
    0: 'T-shirt/top',
    1: 'Trouser',
    2: 'Pullover',
    3: 'Dress',
    4: 'Coat',
    5: 'Sandal',
    6: 'Shirt',
    7: 'Sneaker',
    8: 'Bag',
    9: 'Ankle boot'
}

def plot_image(X, y=None):
    if y is None:
        y = 'unknown'
    else:
        y = classes[y]
    plt.title('Label is {label}'.format(label=y))
    plt.imshow(X, cmap='gray')
    plt.show()
    
    plot_image(x_train[0], y_train[0])
    plot_image(x_train[1], y_train[1])
    plot_image(x_train[2], y_train[2])
    plot_image(x_train[3], y_train[3])

x_train_prep = x_train / 255
x_test_prep = x_test / 255 
x_train_prep_1d = x_train_prep.reshape(-1, 28 * 28)
x_test_prep_1d = x_test_prep.reshape(-1, 28 * 28) 
x_train_prep_3d = x_train_prep.reshape(-1, 28, 28, 1)
x_test_prep_3d = x_test_prep.reshape(-1, 28, 28, 1) 

from sklearn.model_selection import GridSearchCV, cross_val_score 
from sklearn.metrics import confusion_matrix 
import heapq 

def find_example(model, x, y, true_class, predicted_class): 
    y_true = y
    y_pred = model.predict(x)
    found_index = None
    for index, (current_y_true, current_y_pred) in enumerate(zip(y_true, y_pred)): 
        if current_y_true == true_class and current_y_pred == predicted_class:
            found_index = index
            break
    return found_index 

def plot_example(model, x, y, true_class, predicted_class, value=None): 
    index = find_example(model, x, y, true_class, predicted_class)
    print('True class:', classes[true_class])
    print('Predicted class:', classes[predicted_class])
    if value is not None: 
        print('Misclassified', value, 'times')
        if index is not None: 
            plt.imshow(x_test_prep[index])
            plt.show()
            print('')

def analyze_model(model, x, y, inspect_n=10):
    y_pred = model.predict(x)
    conf_matrix = confusion_matrix(y, y_pred)
    print('Confusion matrix:')
    print(conf_matrix)
    print('')
    for _ in range(10): 
        conf_matrix[_][_] = 0
        conf_matrix_flat = conf_matrix.reshape(-1, 1)
        biggest_indices = heapq.nlargest(inspect_n, range(len(conf_matrix_flat)), conf_matrix_flat.take)
        biggest_indices = np.unravel_index(biggest_indices, conf_matrix.shape)
        highest_values = conf_matrix[biggest_indices]
        for x_index, y_index, value in zip(biggest_indices[0], biggest_indices[1], highest_values):
            plot_example(model, x, y, x_index, y_index, value) 

from sklearn.naive_bayes import GaussianNB 

naive_bayes = GaussianNB()
print(np.mean(cross_val_score(estimator=naive_bayes, cv=4, scoring='accuracy', X=x_train_prep_1d, y=y_train))) 
naive_bayes.fit(x_train_prep_1d, y_train) 

analyze_model(naive_bayes, x_test_prep_1d, y_test)