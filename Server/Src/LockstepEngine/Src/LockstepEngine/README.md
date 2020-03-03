帧同步引擎（LockstepEngine）
https://github.com/JiepengTan/LockstepEngine


# Math 【数学库】

## BaseType
LFloat(定点数的基础)
LVector2、LVector2Int、 LVector3、LVector3Int
LAxis2D、LAxis3D
LMatrix33
LQuaternion
LRect

## LUT - 查找表
Acos、Asin、Atan2、SinCos

通过查找表，一方面能够提高运算性能；另一方面，也可以保证计算结果为唯一性。
【结果，从一开始就定义好了。】

## 其他
数学函数、拓展方法

-------------------------------------------------

# Logging 【日志】

## Log等级 （LogSeverity）
   枚举，都是2^n 。 保证每一位的唯一性
## LogEventArgs （Log参数类型）
	日志等级 + 消息实体
## Logger 
   根据“日志等级”决定是否输出日志
## Debug
   封装了对Logger的使用
## BaseLogger
   并非Logger的基类？？
   封装了对Debug的使用


-------------------------------------------------

   
#Serializaition 【序列化】

## 序列化

## 反序列化

## TODO（了解更多细节）

-------------------------------------------------


# Network【网络】

## 环形消息缓冲区

## 消息接口定义（IMessage）

## 消息通道定义（AChannel）
   发送、接收 消息

## 消息打包/解包接口（IMessagePackager）

## 消息分发接口（IMessageDispatcher）

## 网络代理（NetworkProxy）

## TODO（了解更多细节）

-------------------------------------------------


# NetMsg.Common【网络消息】

## UDP支持

-------------------------------------------------


# PathFinding【寻路】

## BSP

## NavMesh

-------------------------------------------------


# Collision2D【2D碰撞检测】

## TODO
项目中暂时没有使用，后续研究


-------------------------------------------------

# ECS.ECDefine
未见使用



-------------------------------------------------

# ECS.Common

## TODO（了解更多细节）


-------------------------------------------------

# BehaviourTree【行为树】

## TODO（了解更多细节）


-------------------------------------------------

# Util【各种工具】

## JsonUtil

## NetworkHelper

## PathUtil

## CoroutineHelper

## ECS

## BackUpUtil

## LTimer

## Pool

## ReflectionUtility

## Profiler




