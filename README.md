# HTTP files transfer speed test 

Before starting the HTTP server, make sure to reserve a URL namespace so that the program can run in non-admin mode. 
For example, in Command prompt(admin mode), 

```sh 
netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user 
# if the server's ip addr is 192.168.3.59 and we are listening on port 5000, then 
netsh http add urlacl url=http://192.168.3.59:5000/ user=\Everyone