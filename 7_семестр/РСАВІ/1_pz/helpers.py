import numpy as np

def naive_bayes(data,label):     
    n_s, n_f = data.shape
    classes = np.unique(label)
    n_c = len(classes)
    total_data = np.zeros([n_s, n_f+1])
    total_data[:,:-1] = data
    total_data[:,-1] = label
    np.random.shuffle(total_data)
    trainX = total_data[:60000,:]
    np.random.shuffle(trainX)
    testX = total_data[60000:,:]
    np.random.shuffle(testX)
    testX_c = testX[:,:-1]
    testX_l = testX[:,-1]
    mean_v = np.zeros([n_c,n_f])
    var_v = np.zeros([n_c,n_f])  
    c_prob = []
    confusion_matrix = np.zeros([n_c,n_c])
    d_acc = []
         
    for c in classes:
        trainX_c = trainX[trainX[:,-1]==c]   
        #Filter samples for each class
        trainX_c = trainX_c[:,:-1]
        c_prob.append(len(trainX_c)/len(trainX))
        mean_v[int(c),:] = trainX_c.mean(axis=0)
        var_v[int(c),:] = trainX_c.var(axis=0)
        var_v = var_v + 1000
        count = 0
        for i in range(testX.shape[0]):
            lists = []
            #Empty list to store probability of all class for ith sample feature  
            for j in range(n_c):
                numerator = np.exp(-((testX_c[i]-mean_v[j])**2)/(2*var_v[j]))
                denominator = np.sqrt(2*np.pi*(var_v[j]))
                prob_xc = numerator/denominator
                ratio = np.sum(np.log(prob_xc))
                lists.append(ratio)
            pred = lists.index(max(lists))
            if pred == testX_l[i]:  
                count = count+1  
                confusion_matrix[int(testX_l[i])][int(testX_l[i])]=confusion_matrix[int(testX_l[i])][int(testX_l[i])]+1 
            else:             
                for k in range(n_c):                 
                    if pred == k:                     
                        confusion_matrix[int(testX_l[k])][int(testX_l[i])]=confusion_matrix[int(testX_l[k])][int(testX_l[i])]+1                          
                        for l in classes: 
                            check = testX[testX[:,-1]==l]          
                            a = (confusion_matrix[int(l)][int(l)])/check.shape[0]         
                            d_acc.append(a)    
        o_acc = count/testX.shape[0]  
    return(d_acc, o_acc, confusion_matrix, mean_v, var_v) 