## Create a local registry for minikube
folow: 
https://minikube.sigs.k8s.io/docs/handbook/registry/

detail:
```minikube start --driver=hyperv --memory=8192 --insecure-registry "10.0.0.0/24"```
```docker run --rm -it --network=host alpine ash -c "apk add socat && socat TCP-LISTEN:5000,reuseaddr,fork TCP:$(minikube ip):5000"```
```docker context use default
```