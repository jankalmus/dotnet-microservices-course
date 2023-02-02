**Docker build:**

    docker build --rm -t username/tagname .

    docker run -p ext_port:int_port tag 

    docker ps 

    docker push “imgname” 

**Host file:**

    sudo code /etc/hosts

**Kubernetes:**

    kubectl -f apply *.yaml

    kubectl get pods 

    kubectl get deployments 

    kubectl get storageclass

    kubectl delete deployment “deployment-name” 

    kubectl rollout restart deployment “deployment name”

    kubectl create secret generic ?? —from-literal=

**Terminology:**

K8S object

Pod

Node

Cluster

Persistence Volume Claim

Persistence Volume

Storage Class