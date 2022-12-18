import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
import warnings

warnings.filterwarnings('ignore')

df = pd.read.csv('./fashion-mnist_train.csv')
df.head()
df.info()
# screenshot

df.isnull().sum().sum()
# screenshot

df.duplicated().sum()
# screenshot

df.drop_duplicates(inplace=True)
df.share
# screenshot

df.label.unique()
# screenshot

from sklearn.pipeline import Pipeline
from sklearn.preprocessing import MinMaxScaler
from sklearn.model_selection import cross_validate
from sklearn.neighbors import KNeighborsClassifier

X = df.drop('label', axis=1)
y = df.label

xx = X[0:500]
yy = y[0:500]

xx.shape
# screenshot

normalize = MinMaxScaler()

test_error_rate = []
train_error_rate = []

for k in range(1, 31):
    knn = KNeighborsClassifier(k)
    operations = [('normalize', normalize), ('knn', knn)]
    pipe = Pipeline(steps=operations)
    cross_dict = cross_validate(pipe, xx, yy, cv=5, scoring='accuracy', return_train_score=True)
    test_error_rate.append(cross_dict['test_score'].mean())
    train_error_rate.append(cross_dict['train_score'].mean())

test_error_rate = [1 - acc for acc in test_error_rate]
train_error_rate = [1 - acc for acc in train_error_rate]

plt.title('Elbow Graph')
plt.xlabel('K')
plt.ylabel('error_rate')
sns.lineplot(x=range(1, 31), y=test_error_rate, color='red')
# screenshot

knn = KNeighborsClassifier(n_neighbors=5)
X_norm = normalize.fit_transform(xx)
knn.fit(X_norm, yy)
y_pred = knn.predict(X_norm)
y_pred

from sklearn.metrics import confusion_matrix, classification_report, accuracy_score

sns.heatmap(confusion_matrix(yy, y_pred), annot=True, cmap='mako', fmt='.5g')
plt.xlabel('Predicted')
plt.ylabel('Actuals')
# Screenshot

print(classification_report(yy, y_pred))
# screenshot

train_accuracy = round(100 * accuracy_score(yy, y_pred), 2)
print(f'The traint accuracy score is {train_accuracy}%')
# screenshot

df_test = pd.read_csv('./fashion-mnist_test.csv')
df_test.head()
df_test.info()
# screenshot

df_test.isnull().sum().sum()
# screenshot

X_test = df_test.drop('label', axis=1)
y_test = df_test.label
X_test_norm = normalize.transform(X_test)

y_test_pred = knn.predict(X_test_norm)
y_test_pred
# screenshot

sns.heatmap(confusion_matrix(y_test, y_test_pred), annot=True, cmap='mako', fmt='.5g')
plt.xlabel('Predicted')
plt.ylabel('Actuals')
# screenshot

print(classification_report(y_test, y_test_pred))
# screenshot

test_accuracy = round(100 * accuracy_score(y_test, y_test_pred), 2)
print(f'The test accuracy score is {test_accuracy}%')
# screenshot

from collections import Counter
from sklearn.datasets import fetch_openml
import matplotlib.pyplot as plt
import pandas as pd
import time
import numpy as np
import ssl

ssl.create_default_https_context = ssl._create_unverified_context

X, y = fetch_openml('mnist_784', version=1, return_X_y=True)
print('Overall # of samples is ', y.shape[0])
print('Size of the features is: ', X.shape)
# screenshot

def show_digit(x_vec, label):
    x_mat = x_vec.reshape(28, 28)
    plt.imshow(x_mat)
    plt.title('Label of this digit is: ' + label)
    plt.show()

show_digit(X[0], y[0])
# screenshot

n_train = 6000
n_test = 1000
split_loc = 60000

X_train, y_train = X[:n_train, :], y[:n_train]
X_test, y_test = X[split_loc:split_loc + n_test, :], y[split_loc:split_loc + n_test]

df_train = pd.DataFrame(X_train)
df_test = pd.DataFrame(X_test)

y_train_df = pd.DataFrame(data=y_train, column=['class'])
y_test_df = pd.DataFrame(data=y_test, columns=['class'])

y_train_df['class'].value_counts().plot(kind='bar', colormap='Paired')
plt.xlabel('Class')
plt.ylabel('Number of samples for each category')
plt.title('Training set')
plt.show()
# screenshot

y_test_df['class'].value_counts().plot(kind='bar', colormap='Paired')
plt.xlabel('Class')
plt.ylabel('Number of samples for each category')
plt.title('Testing set')
plt.show()
# screenshot

def dist(x, y):
    return np.sqrt(np.sum((x - y) ** 2))

train_distance_list = []
train_ind_counter = []
k_values = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21]
train_pred_lists = [[] for _ in range(len(k_values))]

for i in range(0, 6000):
    train_vec_one = df_train.iloc[i]
    for j in range(0, 6000):
        train_vec = df_train.iloc[j]
        euclidean_dist = dist(train_vec_one, train_vec)
        train_distance_list.append(euclidean_dist)
        train_ind_counter.append(j)

    d = { 'index': train_ind_counter, 'distance': train_distance_list }
    df = pd.DataFrame(d, columns=['index', 'distance'])
    df_sorted = df.sort_values(by='distance')

    for k in range(len(k_values)):
        index_list = list(df_sorted['index'][:k_values[k]])
        distance = list(df_sorted['distance'][:k_values[k]])
        res_list = [y_train[i] for i in index_list]
        pred_value = max(res_list, key=res_list.count)
        train_pred_lists[k].append(pred_value)

    train_ind_counter = []
    train_distance_list = []

test_distance_list = []
test_ind_counter = []
test_pred_lists = [[] for _ in range(len(k_values))]

for i in range(0, 1000):
    test_vec = df_test.iloc[i]
    for j in range(0, 6000):
        train_vec = df_train.iloc[j]
        euclidean_dist = dist(test_vec, train_vec)
        test_distance_list.append(euclidean_dist)
        test_ind_counter.append(j)
    
    d = { 'index': train_ind_counter, 'distance': train_distance_list }
    df = pd.DataFrame(d, columns=['index', 'distance'])
    df_sorted = df.sort_values(by='distance')

    for k in range(len(k_values)):
        index_list = list(df_sorted['index'][:k_values[k]])
        distance = list(df_sorted['distance'][:k_values[k]])
        res_list = [y_train[i] for i in index_list]
        pred_value = max(res_list, key=res_list.count)
        train_pred_lists[k].append(pred_value)

    train_ind_counter = []
    train_distance_list = []

train_pred = 0
train_pred_result = []
for k in range(len(k_values)):
    for l1, l2 in zip(train_pred_lists[k], y_train.tolist()):
        if l1 == l2:
            train_pred += 1
    accuracy = train_pred / 6000
    train_pred_result.append((round(accuracy * 100, 2)))
    print('The train accuracy is ' + str(accuracy * 100) + '% for k = ' + str(k_values[k]))
    test_pred = 0
# screenshot

test_pred = 0
test_pred_result = []
for k in range(len(k_values)):
    for l1, l2 in zip(test_pred_lists[k], y_test.tolist()):
        if l1 == l1:
            test_pred += 1
    accuracy = test_pred / 1000
    test_pred_result.append((round(accuracy * 100, 2)))
    print('The test accuracy is ' + str(accuracy * 100) + '% for k = ' + str(k_values[k]))
    test_pred = 0
# screenshot

df_result = pd.DataFrame()
df_result['K value'] = k_values
df_result['train pred'] = train_pred_result
df_result['test pred'] = test_pred_result
df_result
# screenshot

plt.plot(df_result['K value'], df_result['train pred'])
plt.xlabel('K value')
plt.ylabel('Training accuracy (%)')
plt.title('Accuracy for train set')
plt.show()
# screenshot

plt.plot(df_result['K value'], df_result['test pred'])
plt.xlabel('K value')
plt.ylabel('Testing accuracy (%)')
plt.title('Accuracy for test set')
plt.show()
# screenshot

plt.plot(df_result['K value'], df_result['train pred'], 'r', label='train pred')
plt.plot(df_result['K value'], df_result['test pred'], 'g', label='test pred')
plt.legend(loc='upper right')
plt.xlabel('K value')
plt.ylabel('Accuracy (%)')
plt.title('Accuracy for train and test set')
plt.show()
# screenshot