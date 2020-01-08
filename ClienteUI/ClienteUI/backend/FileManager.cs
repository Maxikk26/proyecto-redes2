﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClienteUI.backend
{
    public class FileManager
    {
        private Socket socket;
        private string path = @"C:\pruebas";

        public FileManager(Socket _socket)
        {
            socket = _socket;
        }

        public void Upload(string fullPath, string name)
        {
            byte[] fileNameByte = Encoding.ASCII.GetBytes(name);
            byte[] fileData = File.ReadAllBytes(fullPath);
            double x = 4 + fileNameByte.Length + fileData.Length;
            int y = Convert.ToInt32(x);
            socket.Send(BitConverter.GetBytes(y));
            Thread.Sleep(3500);
            byte[] clientData = new byte[y];
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);
            socket.Send(clientData);
            
        }

        public void Download()
        {
            byte[] len = new byte[1024 * 5000];
            socket.Receive(len);
            byte[] clientData = new byte[BitConverter.ToInt32(len,0)];
            decimal bytesReceived;
            bytesReceived = socket.Receive(clientData);
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            BinaryWriter bWrite = new BinaryWriter(File.OpenWrite(path + @"\" + fileName));
            bWrite.Write(clientData, 4 + fileNameLen, Convert.ToInt32(bytesReceived) - 4 - fileNameLen);
            bWrite.Close();

        }

        public string ReceivedData()
        {
            byte[] dataRecieved = new byte[1024];
            socket.Receive(dataRecieved);
            string response = Encoding.ASCII.GetString(dataRecieved);
            return response;
        }


    }
}
