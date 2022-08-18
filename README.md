# MicService

#### 介绍
.net core 3.1 微服务Demo，其中包含Consul服务注册与发现，Polly弹性和瞬态故障处理库，Ocelot网关，IdentityServer4 授权中心

#### 软件架构
软件架构说明

#### 使用说明

本地调试

启动webapi

dotnet MicServiceWebApi.dll --urls="http://*:5726" --ip="127.0.0.1" --port=5726

dotnet MicServiceWebApi.dll --urls="http://*:5727" --ip="127.0.0.1" --port=5727

dotnet MicServiceWebApi.dll --urls="http://*:5728" --ip="127.0.0.1" --port=5728

如果出现Unexpected response, status code BadGateway:
检查可能是VPN导致的网关错误。


 docker run -d --name=node1 --restart=always \ -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \ -p 8300:8300 \ -p 8301:8301 \ -p 8301:8301/udp \ -p 8302:8302/udp \ -p 8302:8302 \ -p 8400:8400 \ -p 8500:8500 \ -p 8600:8600 \ -h node1 \ consul agent -server -bind=0.0.0.0 -bootstrap-expect=3 -node=node1 \ -data-dir=/tmp/data-dir -client 0.0.0.0 -ui 

 Windows DockerDesktop
 docker run -d --name=server1 -p 8500:8500 --restart=always consul:latest agent -server -bootstrap-expect=3 -ui -node=server1 -bind='0.0.0.0' -client='0.0.0.0'
 docker run -d --name=server1 -p 8500:8500 -e 'CONSUL_LOCAL_CONFIG={\"skip_leave_on_interrupt\": true}' --restart=always consul:latest agent -server -bootstrap-expect=3 -ui -node=server1 -bind='0.0.0.0' -client='0.0.0.0'
 -- 构建Cousul集群
 172.17.0.2 是服务端IP地址
 -- Server
 docker run -d --name=server1 --restart=always -p 8300:8300 -p 8301:8301 -p 8301:8301/udp -p 8302:8302/udp -p 8302:8302 -p 8400:8400 -p 8500:8500 -p 8600:8600 -e 'CONSUL_LOCAL_CONFIG={\"skip_leave_on_interrupt\": true}' consul:latest agent -server -bootstrap-expect=3 -data-dir=/tmp/data-dir -node=server1 -bind='0.0.0.0' -client='0.0.0.0' -ui
 docker run -d --name=server2 --restart=always -p 9300:8300 -p 9301:8301 -p 9301:8301/udp -p 9302:8302/udp -p 9302:8302 -p 9400:8400 -p 9500:8500 -p 9600:8600 consul:latest agent -server -bind='0.0.0.0' -join='172.17.0.2' -data-dir=/tmp/data-dir -node=server2 -client='0.0.0.0' -ui
 docker run -d --name=server3 --restart=always -p 10300:8300 -p 10301:8301 -p 10301:8301/udp -p 10302:8302/udp -p 10302:8302 -p 10400:8400 -p 10500:8500 -p 10600:8600 consul:latest agent -server -bind='0.0.0.0' -join='172.17.0.2' -data-dir=/tmp/data-dir -node=server3 -client='0.0.0.0' -ui
 -- Client
 docker run -d --name=client1 --restart=always -p 11300:8300 -p 11301:8301 -p 11301:8301/udp -p 11302:8302/udp -p 11302:8302 -p 11400:8400 -p 11500:8500 -p 11600:8600 consul:latest agent -bind='0.0.0.0' -retry-join='172.17.0.2' -data-dir=/tmp/data-dir -node=client1 -client='0.0.0.0' -ui


 Linux Docker
 docker run -d --name=server1 --restart=always -p 8300:8300 -p 8301:8301 -p 8301:8301/udp -p 8302:8302/udp -p 8302:8302 -p 8400:8400 -p 8500:8500 -p 8600:8600 -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' consul:latest agent -server -bootstrap-expect=3 -data-dir=/tmp/data-dir -node=server1 -bind='0.0.0.0' -client='0.0.0.0' -ui
 docker run -d --name=server2 --restart=always -p 9300:8300 -p 9301:8301 -p 9301:8301/udp -p 9302:8302/udp -p 9302:8302 -p 9400:8400 -p 9500:8500 -p 9600:8600 consul:latest agent -server -bind='0.0.0.0' -join='47.96.153.205' -data-dir=/tmp/data-dir -node=server2 -client='0.0.0.0' -ui
 docker run -d --name=server3 --restart=always -p 10300:8300 -p 10301:8301 -p 10301:8301/udp -p 10302:8302/udp -p 10302:8302 -p 10400:8400 -p 10500:8500 -p 10600:8600 consul:latest agent -server -bind='0.0.0.0' -join='47.96.153.205' -data-dir=/tmp/data-dir -node=server3 -client='0.0.0.0' -ui
 docker run -d --name=client1 --restart=always -p 11300:8300 -p 11301:8301 -p 11301:8301/udp -p 11302:8302/udp -p 11302:8302 -p 11400:8400 -p 11500:8500 -p 11600:8600 consul:latest agent -bind='0.0.0.0' -retry-join='47.96.153.205' -data-dir=/tmp/data-dir -node=client1 -client='0.0.0.0' -ui

 -- 查看日志
 docker logs -f server1

 -- 查看节点
 docker exec -t server1 consul members

 -- 查看主从信息
 docker exec -t server1 consul operator raft list-peers

 -- 构建webapi镜像
 docker build -t micservice:dev-1.0 .
 docker build -t api49 -f Dockerfile .

 -- 运行webapi镜像
 docker run -d -p 5726:80 --name=micservice1.0 micservice:dev-1.0
 docker run -it -p 5726:80 api49

 Docker Compose
 下载
curl -L https://get.daocloud.io/docker/compose/releases/download/1.25.0/docker-compose-`uname -s`-`uname -m` > /usr/local/bin/docker-compose

授权
chmod +x /usr/local/bin/docker-compose

docker-compose
docker-compose –version
docker-compose stop

运行dockercompose，进入目录
docker-compose up 

使用up命令后，自动生成镜像，自动运行容器，相当于一个构建运行脚本

1、拉取Registrator的镜像
docker pull gliderlabs/registrator:latest
2、启动Registrator节点
docker run -d --name=registrator -v /var/run/docker.sock:/tmp/docker.sock --net=host gliderlabs/registrator -ip="47.96.153.205" consul://47.96.153.205:8500
--net指定为host表明使用主机模式。 -ip用于指定宿主机的IP地址，用于健康检查的通信地址。
consul://47.96.153.205:8500: 使用Consul作为服务注册表，指定具体的Consul通信地址进行服务注册和
注销（注意：8500是Consul对外暴露的HTTP通信端口）。
查看 Registrator 的容器进程启动日志：
Registrator 在启动过程完成了以下几步操作：
1. 查看Consul数据中心的leader节点，作为服务注册表；
2. 同步当前宿主机的启用容器，以及所有的服务端口；
3. 分别将各个容器发布的服务地址/端口注册到Consul的服务注册列表。

-- Ocelet网关配置

本地运行

dotnet OcelotGateway.dll --urls="http://*:6299" --ip="127.0.0.1" --port=6299

Dockerfile 挂载

docker build -t gateway421 -f Dockerfile .

docker run -itd -p 6399:80  gateway421 

docker exec it did /bin/bash

docker run -itd -p 6299:80 -v /opt/micservice_ocelot/conf/configuration.json:/app/configuration.json gateway421 



路由冲突加权重Priority


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


