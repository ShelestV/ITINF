import time
import numpy as np

from sklearn import metrics
from sklearn.cluster import KMeans
from sklearn.decomposition import PCA
from sklearn.cluster import MiniBatchKMeans
from sklearn.preprocessing import StandardScaler
from sklearn.cluster import MeanShift, estimate_bandwidth

def eval_metrics(description, n_clusters, y_true, y_pred):
    print(description)
    print('Number of clusters: {}'.format(n_clusters))
    print('Accuracy: {:.2f}'.format(metrics.accuracy_score(y_true, y_pred)))
    print('Rand Index: {:.2f}'.format(metrics.rand_score(y_true, y_pred)))
    print('----------------------')

def get_accuracy(y_true, y_pred):
    return metrics.accuracy_score(y_true, y_pred)

def map_cluster(y_clusters, num_clusters):
    """
    Parameters:
        y, a 1-D Numpy array containing true class numbers (integers between 0 and 10)
        y_cluster, a 1-D Numpy array containing cluster numbers corresponding to each element of y
        
    Returns:
        y_prod, a 1-D Numpy array mapping clusters to most common class in y
    """

    y_pred = np.zeros(y_clusters.shape).astype(np.int32)
    map_of_clusters = np.zeros(num_clusters).astype(np.int32) * -1

    for cluster in range(0, num_clusters):
        count = np.bincount(y[y_clusters == cluster], minlength=10)
        map_of_clusters[cluster] = np.argmax(count)

    y_pred = map_of_clusters[y_clusters]
    return y_pred

def get_n_clusters(model):
    labels = model.labels_
    return len(np.unique(labels))

def k_means(X, y_true, n_clusters):
    k_clusting = KMeans(n_clusters=n_clusters, random_state=0)
    t1 = time.time()
    k_clusting.fit(X)
    t2 = time.time()
    print('time: ', t2 - t1)
    relabel = map_cluster(k_clusting.labels_, y_true, n_clusters)
    eval_metrics('k-means clustering', n_clusters, y_true, relabel)

def mini_batch_k_means(X, y_true, n_clusters):
    mb_clustering = MiniBatchKMeans(n_clusters=n_clusters, random_state=0, batch_size=6)
    t1 = time.time()
    mb_clustering.fit(X)
    t2 = time.time()
    print('time: ', t2 - t1)
    relabel = map_cluster(mb_clustering.labels_, y_true, n_clusters)
    eval_metrics('mini batch k-means clustering', n_clusters, y_true, relabel)

def mean_shift(X, y_true, quantile):
    bandwidth = estimate_bandwidth(X, quantile=quantile, n_samples=(len(X)//3))
    m_clustering = MeanShift(bandwidth=bandwidth)
    t1 = time.time()
    m_clustering.fit(X)
    t2 = time.time()
    print('time: ', t2 - t1)
    n = get_n_clusters(m_clustering)
    relabel = map_cluster(m_clustering.labels_, y_true, n)
    eval_metrics('mean-shift clustering - quantile: ' + str(quantile) + ' - bandwidth: ' + str(bandwidth), n, y_true, relabel)

def do_pca(n_components, data):
    X = StandardScaler().fit_transform(data)
    pca = PCA(n_components=n_components)
    X_pca = pca.fit_transform(X)
    return pca, X_pca

