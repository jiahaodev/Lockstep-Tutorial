/****************************************************
    文件：Server.cs
    作者：JiahaoWu
    邮箱: jiahaodev@163.ccom
    日期：2020/03/03      
    功能：负责“接收、分发”网络消息
         * 玩家连接请求--》进入房间
         * 玩家断开连接--》离开房间
         * 战斗消息更新--》游戏更新（game.update）
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Lockstep.Game;
using Lockstep.Logging;
using Lockstep.Network;
using Lockstep.Util;
using NetMsg.Common;

namespace Lockstep.FakeServer {
    public class Server : IMessageDispatcher {
        //network
        public static IPEndPoint serverIpPoint = NetworkUtil.ToIPEndPoint("127.0.0.1", 10083);
        private NetOuterProxy _netProxy = new NetOuterProxy();

        //update
        private const double UpdateInterval = NetworkDefine.UPDATE_DELTATIME /1000.0f; //frame rate = 30
        private DateTime _lastUpdateTimeStamp;
        private DateTime _startUpTimeStamp;
        private double _deltaTime;
        private double _timeSinceStartUp;

        //user mgr 
        private Game _game;
        private Dictionary<long, Player> _id2Player = new Dictionary<long, Player>();

        //id
        private static int _idCounter = 0;
        private int _curCount = 0;


        public void Start(){
            _netProxy.MessageDispatcher = this;
            _netProxy.MessagePacker = MessagePacker.Instance;
            _netProxy.Awake(NetworkProtocol.TCP, serverIpPoint);
            _startUpTimeStamp = _lastUpdateTimeStamp = DateTime.Now;
        }

        //接收消息，并分发
        public void Dispatch(Session session, Packet packet){
            ushort opcode = packet.Opcode();
            if (opcode == 39) { 
                int i = 0;
            }

            var message = session.Network.MessagePacker.DeserializeFrom(opcode, packet.Bytes, Packet.Index,
                packet.Length - Packet.Index);
            OnNetMsg(session, opcode, message as BaseMsg);
        }

        //消息处理
        void OnNetMsg(Session session, ushort opcode, BaseMsg msg){
            var type = (EMsgSC) opcode;
            switch (type) {
                //login
                // case EMsgSC.L2C_JoinRoomResult: 
                case EMsgSC.C2L_JoinRoom:
                    OnPlayerConnect(session, msg);
                    return;
                case EMsgSC.C2L_LeaveRoom:
                    OnPlayerQuit(session, msg);
                    return;
                //room
            }
            var player = session.GetBindInfo<Player>();
            _game?.OnNetMsg(player, opcode, msg);
        }

        //检测“帧间隔”是否已经达到；更新。
        public void Update(){
            var now = DateTime.Now;
            _deltaTime = (now - _lastUpdateTimeStamp).TotalSeconds;
            if (_deltaTime > UpdateInterval) {
                _lastUpdateTimeStamp = now;
                _timeSinceStartUp = (now - _startUpTimeStamp).TotalSeconds;
                DoUpdate();
            }
        }

        //驱动游戏逻辑更新
        public void DoUpdate(){
            //check frame inputs
            var fDeltaTime = (float) _deltaTime;
            var fTimeSinceStartUp = (float) _timeSinceStartUp;
            _game?.DoUpdate(fDeltaTime);
        }

        //处理玩家加入房间请求
        void OnPlayerConnect(Session session, BaseMsg message){
            //TODO load from db
            var info = new Player();
            info.UserId = _idCounter++;
            info.PeerTcp = session;
            info.PeerUdp = session;
            _id2Player[info.UserId] = info;
            session.BindInfo = info;
            _curCount++;
            if (_curCount >= Game.MaxPlayerCount) {
                //TODO temp code
                _game = new Game();
                var players = new Player[_curCount];
                int i = 0;
                foreach (var player in _id2Player.Values) {
                    player.LocalId = (byte) i;
                    player.Game = _game;
                    players[i] = player;
                    i++;
                }
                _game.DoStart(0, 0, 0, players, "123");
            }

            Debug.Log("OnPlayerConnect count:" + _curCount + " ");
        }


        //处理玩家退出房间请求
        void OnPlayerQuit(Session session, BaseMsg message){
            var player = session.GetBindInfo<Player>();
            if (player == null)
                return;
            _curCount--;
            Debug.Log("OnPlayerQuit count:" + _curCount);
            _id2Player.Remove(player.UserId);
            if (_curCount == 0) {
                _game = null;
            }
        }
    }
}