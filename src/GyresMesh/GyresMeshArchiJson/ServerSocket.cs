using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
namespace Hsy.GyresMeshArchiJson
{
  public class ServerSocket
  {
    static Socket serverSocket;
    static Socket clientSocket;
    private static byte[] result = new byte[1024];
    public  string backmsg = "";
    public String msg = "";
    public List<string> msgList = new List<string>();
    IPAddress ip;
    int port;
    public ServerSocket(String address,int port)
    {
      ip = IPAddress.Parse(address);
      serverSocket = new Socket(
          AddressFamily.InterNetwork,
          SocketType.Stream,
          ProtocolType.Tcp);
      this.port = port;
      serverSocket.Bind(new IPEndPoint(ip, port));  //绑定IP地址：端口  
      serverSocket.Listen(10);    //最多10个连接请求  
      Console.WriteLine("creat service {0} success",
          serverSocket.LocalEndPoint.ToString());

      StartAccept();
    }


    /// <summary>
    /// 开始应答客户端
    /// </summary>
    public void StartAccept()
    {
      //异步应答客户端
      serverSocket.BeginAccept(AcceptCallback, null);
    }

    /// <summary>
    /// 开始接收消息
    /// </summary>
    /// <param name="client">应答的客户端套接字</param>
    public void StartReceive(Socket client)
    {
      //将应答的客户端作为参数传给回调函数
      client.BeginReceive(result, 0, result.Length, SocketFlags.None, ReceiveCallback, client);
    }

    /// <summary>
    /// 应答的回调函数
    /// </summary>
    /// <param name="iar"></param>
    public void AcceptCallback(IAsyncResult iar)
    {
      //1，结束应答，获得应答的客户端的套接字（服务器端将会有多个Socket，每一个Socket对应的是其响应的客户端）
      //一对一通信，client指的是应答Socket，而非客户端本身，该Socket属于服务器端，由服务器产生，针对请求响应的客户端
      clientSocket = serverSocket.EndAccept(iar);
      //2，应答Socket接收消息
      StartReceive(clientSocket);
      //3，处理完一个，继续处理，继续应答
      StartAccept();
    }

    public void ReceiveCallback(IAsyncResult iar)
    {
      //1，从回调函数结果中获取传递过来的参数，即获取传递过来的应答客户端对象
      Socket client = iar.AsyncState as Socket;
      //2，获取接收到信息的长度
      int len = client.EndReceive(iar);
      //3，判断信息是否成功接收
      if (len == 0)
      {
        return;
      }
      try
      {
        Debug.Log("receive length: " + len);
      }
      catch (Exception e)
      {

      }

      Console.WriteLine("receive length: "+len);
      //4，解析信息并处理
      string str = Encoding.UTF8.GetString(result, 0, len);

      string con = str.Substring(0, str.Length - 3);
      string msgCombine = "";
      if (str.Substring(str.Length - 3) != "end")
      {
        msg += str;
      }
      else
      {
        msg += con;
        try
        {
          Debug.Log("msg content:" + con);
        }
        catch (Exception e)
        {

        }
        
        Console.WriteLine("msg content:" + con);
        msgCombine = msg;
        msg = "";
      }
      Console.WriteLine(con);
      try
      {
        Debug.Log(con);
      }
      catch (Exception e)
      {

      }
      if (msgCombine == "receive")
      {

        //发送数据
        string message = "send outcome: " + backmsg;
        byte[] data = Encoding.UTF8.GetBytes(message);//对字符串做编码，得到一个字符串的字节数组
        client.Send(data);
        Console.WriteLine("向客户端发送了一条数据： " + message);
      }
      else
      {
        Console.WriteLine("msgCombine: " + msgCombine);
        msgList.Insert(0, msgCombine);
        string message = "服务器已收到消息";
        byte[] data = Encoding.UTF8.GetBytes(message);//对字符串做编码，得到一个字符串的字节数组
        client.Send(data);
        Console.WriteLine("向客户端发送了一条数据：" + message);
      }
      //5，继续接收来自客户端的信息
      StartReceive(client);
    }

    public void Start()
    {
      Thread myThread = new Thread(ListenClientConnect);
      myThread.Start();
    }






    public void Send(string message)
    {
      //byte[] data = Encoding.UTF8.GetBytes(message);//对字符串做编码，得到一个字符串的字节数组
      //clientSocket.Send(data);
      //Console.WriteLine("向客户端发送了一条数据" + message);
      //SendMessage(clientSocket, message);
      try
      {
        SendMessage(message + "end");
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.ToString());
       
      }
        

    }

    // 监听客户端是否连接  
    private void ListenClientConnect()
    {
      while (true)
      {
        clientSocket = serverSocket.Accept();
        //clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
        Thread receiveThread = new Thread(ReceiveMessage);
        receiveThread.Start(clientSocket);
      }
    }



    private void SendMessage(object message)
    {
      Socket myClientSocket = (Socket)clientSocket;

      byte[] data = Encoding.UTF8.GetBytes((string)message);//对字符串做编码，得到一个字符串的字节数组
      myClientSocket.Send(data);
      Console.WriteLine("向客户端发送了一条数据" + message);
    }

    //开启线程接收数据
    private void ReceiveMessage(object clientSocket)
    {
      Socket myClientSocket = (Socket)clientSocket;
      while (true)
      {
        try
        {
          string msg = "";
          string msgCombine = "";
          int receiveNumber = 0;
          while (msg != "end")
          {
            receiveNumber = myClientSocket.Receive(result);
            Console.WriteLine("receive Length: " + receiveNumber);
            string recvmsg = Encoding.UTF8.GetString(result, 0, receiveNumber);
            //Console.WriteLine("client say : {0} ", recvmsg);
            msg = recvmsg.Substring(recvmsg.Length - 3);
            //Print("msg: "+ msg);
            if (msg == "end")
            {
              msgCombine += recvmsg.Substring(0, recvmsg.Length - 3);
            }
            else
            {
              msgCombine += recvmsg;
            }
          }
          Console.WriteLine("msg length: " + msgCombine.Length);
          Console.WriteLine("msg content: " + msgCombine);
          //接收数据


          if (msgCombine == "receive")
          {

            //发送数据
            string message = "send outcome: " + backmsg;
            byte[] data = Encoding.UTF8.GetBytes(message);//对字符串做编码，得到一个字符串的字节数组
            myClientSocket.Send(data);
            Console.WriteLine("向客户端发送了一条数据： " + message);
          }
          else
          {
            msgList.Insert(0,msgCombine);
            string message = "服务器已收到消息";
            byte[] data = Encoding.UTF8.GetBytes(message);//对字符串做编码，得到一个字符串的字节数组
            myClientSocket.Send(data);
            Console.WriteLine("向客户端发送了一条数据：" + message);
          }
          //break;

        }
        catch (Exception ex)
        {
          Console.WriteLine(" A client break");
          if (myClientSocket.Connected)
          {
            myClientSocket.Shutdown(SocketShutdown.Both);
          }

          break;
        }
      }
    }

  }
}
