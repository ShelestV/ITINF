import numpy as np
import pandas as pd

from helpers import do_pca
from helpers import k_means
from helpers import mean_shift
from helpers import mini_batch_k_means

train_label = pd.read_csv("/content/train_label_csv")
train_label.rename(columns = { "S": "actual_labels" }, inplace=True)
train_label = train_label["actual_labels"]
train_set = pd.read_csv("/content/train_set.csv")
train_set.set_axis([i for i in range(0, 784)], axis=1, inplace=True)

train_set.isnull().sum().isnull().sum(axis=0)

X_282features = train_set.loc[:, train_set.std() > 0.3]
zero_columns = []
for i in range(0, len(train_set.columns)):
    if train_set[i].sum() == 0:
        zero_columns.append(i)
print('number of zero columns: ', len(zero_columns))
train_set.drop(train_set.columns[zero_columns], axis=1, inplace=True)

X = train_set[:1000]
y_true = train_label[:1000]

k = len(np.unique(train_label))
print('k: ', k)

pca, X_pca = do_pca(59, X)
print('number of features: ', pca.n_components_)

pca2, train_set_pca = do_pca(59, train_set)

# without pca
k_means(X_pca, y_true, k)
mini_batch_k_means(X, y_true, k)

# with pca
k_means(X_pca, y_true, k)
mini_batch_k_means(X_pca, y_true, k)

mini_batch_k_means(train_set, train_label, k)

mini_batch_k_means(train_set, train_label, 10)
mini_batch_k_means(train_set, train_label, 16)
mini_batch_k_means(train_set, train_label, 32)
mini_batch_k_means(train_set, train_label, 128)
mini_batch_k_means(train_set, train_label, 256)

# without pca
mean_shift(X, y_true, 0.2)
mean_shift(X, y_true, 0.5)
mean_shift(X, y_true, 0.7)

# with pca
mean_shift(X_pca, y_true, 0.2)
mean_shift(X_pca, y_true, 0.5)
mean_shift(X_pca, y_true, 0.7)

pca2, X_pca2 = do_pca(2, X) # with 2 dimension

# with pca2
mean_shift(X_pca2, y_true, 0.1)
mean_shift(X_pca2, y_true, 0.3)

mean_shift(train_set_pca[:20000], train_label[:20000], 0.2)

mean_shift(train_set_pca[:10000], train_label[:10000], 0.5)
mean_shift(train_set_pca[:10000], train_label[:10000], 0.1)
