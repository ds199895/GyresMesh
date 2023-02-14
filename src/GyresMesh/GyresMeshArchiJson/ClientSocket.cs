using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
namespace Hsy.GyresMeshArchiJson
{
  public class ClientSocket
  {
    IPAddress ip;
    int port;
    public Socket socket;
    byte[] result = new byte[1024];
    public String msg = "";
    public List<String> msgList = new List<string>();

    public ClientSocket(String address,int port)
    {

      ip = IPAddress.Parse("127.0.0.1");//本地IP地址
      this.port = port;
      socket = new Socket(
          AddressFamily.InterNetwork,
          SocketType.Stream,
          ProtocolType.Tcp);

    }
    public Status Connect()
    { 

      try
      {
        socket.Connect(new IPEndPoint(ip, this.port)); //配置服务器IP与端口 ，并且尝试连接
        Console.WriteLine("Connect Success");
        return Status.Connected;
      }
      catch
      {
        Console.WriteLine("Connect error");
        return Status.Disconnected;
      }

      int receiveLength = socket.Receive(result);//接收回复，成功则说明已经接通
      
    }

    /// <summary>
    /// 开始接收消息
    /// </summary>
    public void StartReceive()
    {
      //异步接收数据 参数1：接收信息载体，参数2：从0开始，参数3：信息长度，参数4：目前为空，参数5：回调函数，参数6：回调函数需要用到的参数
      //接收到的消息将在回调函数中进行处理
      socket.BeginReceive(result, 0, result.Length, SocketFlags.None, ReceiveCallback, null);
    }

    /// <summary>
    /// 接收消息的回调函数
    /// </summary>
    /// <param name="iar">BeginReceive方法得到结果即为回调函数的参数</param>
    public void ReceiveCallback(IAsyncResult iar)
    {
      //1，结束接收消息后，会返回接收到的消息长度
      int len = socket.EndReceive(iar);
      //2，判断消息是否被正确完整接收
      //当接收消息过程中发生中断，EndReceive一直处于接收状态，返回的消息长度为0，及消息未传输成功
      if (len == 0)
      {
        return;
      }
      //3，对接收的消息进行解析
      #region 简单消息示例，接收到的是字符串信息
      //将字节数组中的信息转换成字符串，并用UTF-8进行编码

      Console.WriteLine("receive length: "+len);
      Debug.Log("receive length: " + len);
      string str = Encoding.UTF8.GetString(result, 0, len);
      string con = str.Substring(0, str.Length - 3);

      if (str.Substring(str.Length - 3) != "end")
      {
        msg += str;
      }
      else
      {
        msg += con;
        Debug.Log("msg content:" + con);
        Console.WriteLine("msg content:" + con);
        msgList.Insert(0,msg);
        msg = "";
      }
      
      //输出消息
      Console.WriteLine(con);
      Debug.Log(str);

      #endregion
      //4，循环接收消息并对消息进行处理（第一条数据解析完后，对后面服务器发送的消息进行处理）
      StartReceive();
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    public void Send()
    {
      socket.Send(Encoding.UTF8.GetBytes("你好！"));
      socket.Send(Encoding.UTF8.GetBytes("end"));
    }




    public void Send(String msg)
    {
      if (!socket.Connected)
      {
        socket.Connect(new IPEndPoint(ip, this.port));
      }

      byte[] result = new byte[1024];
      string sendMsg = msg;
      socket.Send(Encoding.ASCII.GetBytes(sendMsg));//传送信息
      socket.Send(Encoding.ASCII.GetBytes("end"));//传送信息
      Console.WriteLine("msg: " + sendMsg + " 发送成功");
      //int receiveLength = socket.Receive(result);//接收回复，成功则说明已经接通
      //string recvmsg = Encoding.UTF8.GetString(result, 0, receiveLength);
      //Console.WriteLine("从服务器接收信息: " + recvmsg);
      //clientSocket.Disconnect(true);
    }
    public string Receive()
    {
      if (!socket.Connected)
      {
        socket.Connect(new IPEndPoint(ip, this.port));
      }
      byte[] result = new byte[1024];
      string sendMsg = "receive";
      socket.Send(Encoding.ASCII.GetBytes(sendMsg));//传送信息
      socket.Send(Encoding.ASCII.GetBytes("end"));//传送信息
      Console.WriteLine("msg: " + sendMsg + " 发送成功");
      int receiveLength = socket.Receive(result);//接收回复，成功则说明已经接通
      string recvmsg = Encoding.UTF8.GetString(result, 0, receiveLength);
      Console.WriteLine("从服务器接收信息: " + recvmsg);
      //clientSocket.Disconnect(true);
      return recvmsg;
    }

    public void End()
    {
      socket.Close();
    }
  }

  public enum Status
  {
    Disconnected = 0,
    Connected =1

  }
}
