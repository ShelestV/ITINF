import time
import numpy as np
import matplotlib.pyplot as plt

from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn import datasets
from sklearn.preprocessing import StandardScaler
from sklearn.datasets import fetch_openml
from sklearn.utils import check_random_state

t0 = time.time()
train_samples = 5000

X, y = fetch_openml('mnist_784', version=1, return_X_y=True)
print(X.shape, y.shape)

fig, ax = plt.subplots(nrows=2, ncols=3, sharex='all', sharey='all')
ax = ax.flatten()
for i in range(6):
    img = X.loc[i].to_numpy().reshape(28, 28)
    ax[i].matshow(img)
plt.show()

random_state = check_random_state(0)
permutation = random_state.permutation(X.shape[0])

X = X.loc[permutation].to_numpy()
y = y[permutation]
X = X.reshape((X.shape[0], -1))

X_train, X_test, y_train, y_test = train_test_split(X, y, train_size=train_samples, test_size=10000)

scaler=StandardScaler()
X_train = scaler.fit_transform(X_train)
X_test = scaler.transform(X_test)
  
clf = LogisticRegression(C=50./train_samples, penalty='l1', solver='saga', tol=0.1)
clf.fit(X_train,y_train)
score = clf.score(X_test,y_test)

sparsity = np.mean(clf.coef_ == 0) * 100
print("Sparsity with L1 penalty: 	%.2f%%" % sparsity)
print("Test score with L1 penalty: 	%.4f" % score)

coef = clf.coef_.copy()
print(coef.shape)
plt.figure(figsize=(10, 5))
scale = np.abs(coef).max()

for i in range(10): 
    l1_plot = plt.subplot(2, 5, i + 1)
    l1_plot.imshow(coef[i].reshape(28, 28), interpolation='nearest', cmap=plt.cm.RdBu, vmin=-scale, vmax=scale)
    l1_plot.set_xticks(())
    l1_plot.set_yticks(())
    l1_plot.set_xlabel('Class %i' % i)

plt.suptitle('Classification vector for...') 
run_time = time.time() - t0 
print('Example run in 	%.3f	 s' % run_time)
plt.show() 
