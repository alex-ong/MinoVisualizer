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

    public void OnApplicationQuit()
    {
        stopped = true;        
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

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(IPAddress.Any, port);

            // Start listening for client requests.
            server.Start();



            // Enter the listening loop.
            while (!stopped)
            {
                // Perform a blocking call to accept requests.
                TcpClient client = server.AcceptTcpClient();

                //Process each client in a new thread.
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
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

    private void HandleClient(object clientObject)
    {
        TcpClient client = (TcpClient)clientObject;
        client.NoDelay = true;
        client.ReceiveTimeout = 500;
        client.SendTimeout = 500;

        // Buffer for reading data
        Byte[] bytes = new Byte[4096]; //1 megabyte should do it.
        List<byte> currentBuffer = new List<byte>();
        String data = null;
        StringBuilder sb = new StringBuilder();
        sb.Length = 0; //clears it

        // Get a stream object for reading and writing
        NetworkStream stream = client.GetStream();
        int header = -1;
        int bytesRead;
        try
        {
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
                else
                {
                    break;
                }
            }
        }
        catch (SocketException e)
        {
            Debug.Log(string.Format("SocketException: {0}", e));
        }
        finally
        {
            // Shutdown and end connection
            client.Close();
        }

    }
}