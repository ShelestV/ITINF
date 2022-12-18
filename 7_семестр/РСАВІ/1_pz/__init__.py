import numpy as np 
import matplotlib.pyplot as plt 
import pandas as pd 
import seaborn as sns
from keras.datasets import mnist 

from helpers import naive_bayes
 
(x_train, y_train), (x_test, y_test) = mnist.load_data() 
x_train=x_train.reshape(60000,784) 
x_test=x_test.reshape(10000,784) 
data=np.zeros([70000,784]) 
data[:60000,:]=x_train; 
data[60000:,:]=x_test 
label=np.zeros(70000) 
label[:60000]=y_train; 
label[60000:]=y_test 
     
#Call Naive Bayes Function
(digit_accuracy,overall_accuracy,matrix,mean_v,var_v) = naive_bayes(data,label)

#Print All class Accuracy
digit=['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']
naive_df = pd.DataFrame(list(zip(digit, digit_accuracy)), columns = ['Digit','Digit Accuracy'])
print('Digit (Individual Class) Accuracy of the Samples:')
naive_df

#Print Overall Accuracy
print('Overall Accuracy of Naive Bayes Model: ' + str(overall_accuracy))
overall_accuracy

#Print Mean of each Class in 28*28 Form
plt.figure(figsize=(16,8))
for i in range(mean_v.shape[0]):
    plt.subplot(2,5, i+1)
    img = mean_v[i].reshape(28,28)
    plt.imshow(img, cmap="Greys")
    plt.xlabel('Image of digit ' + str(i) + ' Mean', fontsize=12)

#Print Variance of each Class in 28*28 Form 
plt.figure(figsize=(16,8)) 
for i in range(var_v.shape[0]):
    plt.subplot(2,5, i+1)
    img = var_v[i].reshape(28,28)
    plt.imshow(img, cmap="Greys")
    plt.xlabel('Image of digit ' + str(i) + ' Variance', fontsize=12)  

#Print Confusion Matrix
plt.figure(figsize=(18,10))
uniform_data = np.random.rand(10, 12)
ax = sns.heatmap(matrix, annot=True, cmap="YlGnBu")
bottom,top = ax.get_ylim()
ax.set_ylim(bottom + 0.5, top - 0.5)
plt.xlabel('True class value')
plt.ylabel('Predicted class value')