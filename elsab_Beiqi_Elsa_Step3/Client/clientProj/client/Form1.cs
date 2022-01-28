using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;

            int portNum;
            if (textBox_Username.Text != "" && Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    button_mySweets.Enabled = true;
                    button_disconnect.Enabled = true;
                    button_block.Enabled = true;
                    button_Sweet.Enabled = true;
                    button_delete.Enabled = true;
                    button_request.Enabled = true;
                    button_request_followed.Enabled = true;
                    button_followedUsers.Enabled = true;
                    button_followingUsers.Enabled = true;
                    button1.Enabled = true;
                    button_follow.Enabled = true;
                    connected = true;
                    // Send username as message
                    string username = textBox_Username.Text;

                    Byte[] buffer = Encoding.Default.GetBytes(username);

                    clientSocket.Send(buffer);

                    Thread recieveThread = new Thread(Receive2); // Reciever of data from the server. 
                    recieveThread.Start(); // Don't forget to start the thread.

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                if(textBox_Username.Text == "")
                {
                    logs.AppendText("Please enter a username!\n");
                }
                else
                {
                    logs.AppendText("Check the port\n");
                }
            }
        }

        private void Receive2()
        {
            while (connected) // clientProj
            {
                try
                {
                    Byte[] buffer1 = new Byte[128];
                    // if client socket is live
                    if (SocketConnected(clientSocket))
                    {
                        clientSocket.Receive(buffer1); // Shouldn't be able to do this.
                    }
                    else
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_disconnect.Enabled = false;
                        button_connect.Enabled = true;
                        button_follow.Enabled = false;
                        button_block.Enabled = false;
                        button_Sweet.Enabled = false;
                        button_delete.Enabled = false;
                        button_request.Enabled = false;
                        button_request_followed.Enabled = false;
                        button_followedUsers.Enabled = false;
                        button_followingUsers.Enabled = false;
                        button1.Enabled = false;
                        clientSocket.Close();
                        connected = false;
                    }
                    string incomingMessage = Encoding.Default.GetString(buffer1);
                    incomingMessage = incomingMessage.Trim('\0');
                    Console.WriteLine(incomingMessage);
                    if (incomingMessage != "")
                    {
                        logs.AppendText(incomingMessage + "\n");
                    }
                }
                catch
                {

                    System.Console.WriteLine("There is an error here.");
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {


        }

        private void logs_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void sweet_Button_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Sweet_Click(object sender, EventArgs e)
        {
            string sweet = textBox_Sweet.Text;
            if (sweet != "" && sweet.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(sweet);
                clientSocket.Send(buffer);
                textBox_Sweet.Text = "";
            }
        }

        private void textBox_ip_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_request_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button_request_followed_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "";
                Byte[] request_followed_Buffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st_v3rsion_2");
                clientSocket.Send(request_followed_Buffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st_users");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button_follow_Click(object sender, EventArgs e)
        {

            string username = textBox_follow_username.Text;
            try
            {
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_follow");
                clientSocket.Send(requestBuffer);
                requestBuffer = Encoding.Default.GetBytes(username);
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button_block_Click(object sender, EventArgs e)
        {
            string username = textBox_block_username.Text;
            try
            {
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_block");
                clientSocket.Send(requestBuffer);
                requestBuffer = Encoding.Default.GetBytes(username);
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            clientSocket.Close();
            button_disconnect.Enabled = false;
            button_connect.Enabled = true;
            button_follow.Enabled = false;
            button_block.Enabled = false;
            button_Sweet.Enabled = false;
            button_delete.Enabled = false;
            button_request.Enabled = false;
            button_request_followed.Enabled = false;
            button_followedUsers.Enabled = false;
            button_followingUsers.Enabled = false;
            button1.Enabled = false;

            logs.AppendText("You have successfully disconnected from the server.\n");
        }

        private void button_followedUsers_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "You have requested users you have followed.\n";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st_followed_users");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the followed users!\n");
            }
        }

        private void button_followingUsers_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "You have requested users who follow you.\n";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st_following_users");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the following users!\n");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string id = textBox_SweetID.Text;
            if(id != "")
            try
            {
                textBox_SweetID.Text = "";
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_delete_sweet");
                clientSocket.Send(requestBuffer);
                requestBuffer = Encoding.Default.GetBytes(id);
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the sweets!\n");
            }
        }

        private void button_mySweets_Click(object sender, EventArgs e)
        {
            try
            {
                logs.Text = "";
                Byte[] requestBuffer = Encoding.Default.GetBytes("s0_call3d_h1dd3n_r3qu3st_mySweets");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving your sweets!\n");
            }
        }
    }
}
