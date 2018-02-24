using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using SimpleJSON;

public class MyTcpListener : MonoBehaviour
{
    public int port = 9999;
    public string ip = "127.0.0.1";
    private Thread t;
    private bool stopped = false;
    private Queue<JSONNode> toRead = new Queue<JSONNode>();

    public JSONNode getObject()
    {
        JSONNode result = null;
        lock (toRead)
        {
            if (toRead.Count > 0)
            {
                result = toRead.Dequeue();
            }
        }
        return result;
    }

    private void AddObject(JSONNode toQueue)
    {
        lock (toRead)
        {
            toRead.Enqueue(toQueue);
        }
    }

    public void Start()
    {
        t = new Thread(StartServer);
        t.IsBackground = true;
        t.Start();
    }

    public void OnDestroy()
    {
        stopped = true;
    }

    private void copyData(List<byte> longArray, byte[] buffer, int count)
    {
        for (int i = 0; i < count; i++)
        {
            longArray.Add(buffer[i]);
        }
    }

    public void StartServer()
    {
        TcpListener server = null;

        try
        {

            Int32 port = this.port;
            IPAddress localAddr = IPAddress.Parse(this.ip);

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[2048]; //1 megabyte should do it.
            List<byte> currentBuffer = new List<byte>();
            String data = null;
            StringBuilder sb = new StringBuilder();
            sb.Length = 0; //clears it

            // Enter the listening loop.
            while (!stopped)
            {
                Debug.Log("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                client.NoDelay = true;
                Debug.Log("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                int header = -1;
                int bytesRead;

                // Loop to receive all the data sent by the client.
                while (!stopped)
                {
                    if (stream.CanRead)
                    {
                        bytesRead = stream.Read(bytes, 0, bytes.Length);
                        copyData(currentBuffer, bytes, bytesRead);
                        if (header == -1)
                        {
                            if (currentBuffer.Count >= 4)
                            {
                                header = BitConverter.ToInt32(currentBuffer.GetRange(0, 4).ToArray(), 0);
                                currentBuffer.RemoveRange(0, 4);
                            }
                        } 

                        if (header != -1 && currentBuffer.Count >= header)
                        {
                            byte[] finalString = currentBuffer.GetRange(0, header).ToArray();
                            currentBuffer.RemoveRange(0, header);
                            data = System.Text.Encoding.UTF8.GetString(finalString, 0, finalString.Length);
                            JSONNode obj = SimpleJSON.JSONObject.Parse(data);
                            AddObject(obj);
                            header = -1;
                        }
                    
                    }
                }

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Debug.Log(string.Format("SocketException: {0}", e));
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }


        Debug.Log("\nHit enter to continue...");
    }
}