using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> clientNames = new List<string>();
        private static Mutex mutex = new Mutex(); //mutex for sweets.txt
        private static Mutex followDatabaseMutex = new Mutex();
        private static Mutex blockDatabaseMutex = new Mutex();


        bool terminating = false;
        bool listening = false;
        int counter = 0;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            if (!File.Exists(@"sweets.txt"))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(@"sweets.txt"))
                {
                    sw.WriteLine(counter.ToString());
                }
            }
            else
            {
                mutex.WaitOne();
                string[] sweets = System.IO.File.ReadAllLines(@"sweets.txt");
                counter = Int32.Parse(sweets[0]);
                mutex.ReleaseMutex();
            }
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;
                textBox_message.Enabled = true;
                button_send.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();

                    logs.AppendText("A client is trying to connect.\n");

                    Thread checkThread = new Thread(() => Check(newClient)); // checking username
                    checkThread.Start();

                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private bool checkUsername(string username)
        {
            //string dir = Directory.GetCurrentDirectory(); put user-db.txt here
            string[] usernames = System.IO.File.ReadAllLines(@"user-db.txt");
            if (Array.Find(usernames, element => element == username) == username)          //todo, check logged in users here
                return true;
            else
                return false;
        }

        private void storeSweet(string username, string sweet)
        {
            mutex.WaitOne();
            using (StreamWriter sw = File.AppendText(@"sweets.txt"))
            {
                sw.WriteLine(username + " ( " + counter.ToString() + " ) [ " + DateTime.Now.ToString() + " ] : " + sweet);
            }
            counter++;
            mutex.ReleaseMutex();

        }

        private void sendSweets(Socket thisClient, string username)
        {

            if (File.Exists(@"sweets.txt"))
            {
                bool isSent = false;
                bool isBlocked = false;
                Byte[] buffer = new Byte[128];
                //send sweets
                string[] sweets = System.IO.File.ReadAllLines(@"sweets.txt");

                foreach (string sweet in sweets.Skip(1))
                {
                    isSent = false;
                    isBlocked = false;
                    string userOfSweet = sweet.Split(new char[] { ' ' })[0];
                    if (username != userOfSweet)
                    {

                        if (File.Exists("blockDatabase.txt"))
                        {
                            string[] blockDatabase = System.IO.File.ReadAllLines(@"blockDatabase.txt");
                            foreach (string line in blockDatabase)
                            {
                                string[] line2 = line.Split(new char[] { ' ' });

                                if (line2[0] == ("@" + userOfSweet))
                                {
                                    if (!line2.Contains(username)) //if the line of the @user contains the name of user to be followed
                                    {
                                        isSent = true;
                                        buffer = Encoding.Default.GetBytes(sweet);
                                        thisClient.Send(buffer);
                                    }
                                    else
                                    {
                                        isBlocked = true;
                                    }

                                }

                            }
                            if (!isBlocked && !isSent)
                            {
                                buffer = Encoding.Default.GetBytes(sweet);
                                thisClient.Send(buffer);
                            }
                        }
                        else
                        {
                            buffer = Encoding.Default.GetBytes(sweet);
                            thisClient.Send(buffer);
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }

                logs.AppendText("Sweets are sent to " + username + ".\n");
            }
        }
        private void sendUsers(Socket thisClient, string username)
        {
            if (File.Exists(@"user-db.txt"))
            {
                Byte[] buffer = new Byte[128];
                //send sweets
                string[] users_names = System.IO.File.ReadAllLines(@"user-db.txt");

                foreach (string name in users_names.Skip(1))
                {

                    if (username != name)
                    {
                        buffer = Encoding.Default.GetBytes(name);
                        thisClient.Send(buffer);
                        System.Threading.Thread.Sleep(1);
                    }
                }
                logs.AppendText("User names are sent to " + username + ".\n");
            }
        }

        private void sendFollowingUsers(Socket thisClient, string username)
        {
            bool isAnyFollower = false;
            string followingUsers = "";
            if (File.Exists("followDatabase.txt"))
            {
                string[] followDatabase = System.IO.File.ReadAllLines(@"followDatabase.txt");
                foreach (string line in followDatabase)
                {
                    string[] line2 = line.Split(new char[] { ' ' });
                    string fullUsername = "@" + username;
                    if (line2[0] != fullUsername && line2.Contains(username)) // If this line contains username AND the line is not belong to them, return the username.
                    {
                        isAnyFollower = true;
                        followingUsers = line2[0];
                        Byte[] buffer = new Byte[128];
                        buffer = Encoding.Default.GetBytes(followingUsers);
                        thisClient.Send(buffer);
                        System.Threading.Thread.Sleep(1);
                    }
                }
                if (!isAnyFollower)
                {
                    Byte[] buffer = new Byte[128];
                    buffer = Encoding.Default.GetBytes("No one is following you :( ..... yet :)");
                    thisClient.Send(buffer);
                }
                logs.AppendText("Users that follows " + username + " are sent to user.\n");
            }
        }

        private void followUser(Socket thisClient, string username, string followedUser)
        {
            bool userExist = false, followYourself = false, followDataExist = false;
            bool blocked = false;
            if (File.Exists(@"user-db.txt"))
            {
                string[] users_names = System.IO.File.ReadAllLines(@"user-db.txt");
                if (followedUser == username)
                {
                    followYourself = true;
                }
                else
                {
                    foreach (string name in users_names)
                    {
                        if (followedUser == name)
                        {
                            userExist = true;
                        }
                    }
                }
                if (userExist && File.Exists("blockDatabase.txt"))
                {
                    string[] blockDatabase = System.IO.File.ReadAllLines(@"blockDatabase.txt");
                    foreach (string line in blockDatabase)
                    {
                        string[] line2 = line.Split(new char[] { ' ' });

                        if (line2[0] == ("@" + followedUser))
                        {
                            if (line2.Contains(username)) //if the line of the @user contains the name of user to be followed
                            {
                                blocked = true;
                                logs.AppendText(username + " cannot follow " + followedUser + ". " + username + " is blocked .\n");
                                Byte[] buffer = Encoding.Default.GetBytes("You cannot follow " + followedUser + ", you are blocked" + ".\n");
                                thisClient.Send(buffer);
                                break;
                            }
                        }
                    }
                }
                if (followYourself)
                {
                    logs.AppendText(username + " Tried to follow itself .\n");
                    Byte[] buffer = Encoding.Default.GetBytes("You can not follow yourself. \n");
                    thisClient.Send(buffer);
                }
                else if (!userExist)
                {
                    logs.AppendText("There is no such a user called " + followedUser + ", none is followed.\n");
                    Byte[] buffer = Encoding.Default.GetBytes("There is no such a user called " + followedUser + ", none is followed.\n");
                    thisClient.Send(buffer);
                }
            }
            followDatabaseMutex.WaitOne();
            if (!followYourself && userExist && !blocked)
            {
                if (!File.Exists(@"followDatabase.txt"))
                {
                    using (StreamWriter sw = File.CreateText(@"followDatabase.txt")) //if it the first time of follow database txt
                    {
                        Byte[] buffer = Encoding.Default.GetBytes("You followed " + followedUser + ".\n");
                        thisClient.Send(buffer);
                        sw.WriteLine("@" + username + " " + followedUser); //the format is as follows: @userX name1 name2 name3(users that userX follows)
                    }
                }
                else
                {
                    string editedLine = ""; //to modify the line
                    int counter = 0;
                    string[] followDatabase = System.IO.File.ReadAllLines(@"followDatabase.txt");
                    foreach (string line in followDatabase)
                    {
                        string[] line2 = line.Split(new char[] { ' ' });

                        if (line2[0] == ("@" + username))
                        {
                            followDataExist = true;
                            int noOfFollowing = line2.Length;
                            if (line2.Contains(followedUser)) //if the line of the @user contains the name of user to be followed
                            {
                                logs.AppendText(username + " already follows " + followedUser + ".\n");
                                Byte[] buffer = Encoding.Default.GetBytes("You are already following " + followedUser + ".\n");
                                thisClient.Send(buffer);
                            }
                            else
                            {
                                Byte[] buffer = Encoding.Default.GetBytes("You followed " + followedUser + ".\n");
                                thisClient.Send(buffer);
                                editedLine = line;
                                editedLine += " " + followedUser; //just adding new followed username to the end of the corresponding line
                                logs.AppendText(editedLine + ".\n");
                                break;
                            }
                        }
                        counter++;

                    }
                    if (!followDataExist) //if there is not already a line, attributed to the user, first time we write @user ...
                    {
                        File.AppendAllText("followDatabase.txt", ("\n" + "@" + username + " " + followedUser));
                        Byte[] buffer = Encoding.Default.GetBytes("You followed " + followedUser + ".\n");
                        thisClient.Send(buffer);
                    }
                    else if (editedLine != "") //if there is already a line, just replacing the line with edited line
                    {
                        followDatabase[counter] = editedLine;
                        File.WriteAllLines("followDatabase.txt", followDatabase);
                    }
                }
            }
            followDatabaseMutex.ReleaseMutex();

        }
        private void blockUser(Socket thisClient, string username, string blockedUser)
        {
            bool userExist = false, blockYourself = false, blockDataExist = false;
            if (File.Exists(@"user-db.txt"))
            {
                string[] users_names = System.IO.File.ReadAllLines(@"user-db.txt");
                if (blockedUser == username)
                {
                    blockYourself = true;
                }
                else
                {
                    foreach (string name in users_names)
                    {
                        if (blockedUser == name)
                        {
                            userExist = true;
                        }
                    }
                }
                if (blockYourself)
                {
                    logs.AppendText(username + " Tried to block itself .\n");
                    Byte[] buffer = Encoding.Default.GetBytes("You can not block yourself. \n");
                    thisClient.Send(buffer);
                }
                else if (!userExist)
                {
                    logs.AppendText("There is no such a user called " + blockedUser + ", none is blocked.\n");
                    Byte[] buffer = Encoding.Default.GetBytes("There is no such a user called " + blockedUser + ", none is blocked.\n");
                    thisClient.Send(buffer);
                }
            }
            blockDatabaseMutex.WaitOne();
            if (!blockYourself && userExist)
            {
                if (!File.Exists(@"blockDatabase.txt"))
                {
                    using (StreamWriter sw = File.CreateText(@"blockDatabase.txt")) //if it the first time of follow database txt
                    {
                        Byte[] buffer = Encoding.Default.GetBytes("You blocked " + blockedUser + ".\n");
                        thisClient.Send(buffer);
                        sw.WriteLine("@" + username + " " + blockedUser); //the format is as follows: @userX name1 name2 name3(users that userX follows)
                    }
                }
                else
                {
                    string editedLine = ""; //to modify the line
                    int counter = 0;
                    string[] blockDatabase = System.IO.File.ReadAllLines(@"blockDatabase.txt");
                    foreach (string line in blockDatabase)
                    {
                        string[] line2 = line.Split(new char[] { ' ' });

                        if (line2[0] == ("@" + username))
                        {
                            blockDataExist = true;
                            int noOfBlocked = line2.Length;
                            if (line2.Contains(blockedUser)) //if the line of the @user contains the name of user to be followed
                            {
                                logs.AppendText(username + " already blocked " + blockedUser + ".\n");
                                Byte[] buffer = Encoding.Default.GetBytes("You have already blocked " + blockedUser + ".\n");
                                thisClient.Send(buffer);
                            }
                            else
                            {
                                Byte[] buffer = Encoding.Default.GetBytes("You blocked " + blockedUser + ".\n");
                                thisClient.Send(buffer);
                                editedLine = line;
                                editedLine += " " + blockedUser; //just adding new followed username to the end of the corresponding line
                                logs.AppendText(editedLine + ".\n");
                                break;
                            }
                        }
                        counter++;

                    }
                    if (!blockDataExist) //if there is not already a line, attributed to the user, first time we write @user ...
                    {
                        File.AppendAllText("blockDatabase.txt", ("\n" + "@" + username + " " + blockedUser));
                        Byte[] buffer = Encoding.Default.GetBytes("You blocked " + blockedUser + ".\n");
                        thisClient.Send(buffer);
                    }
                    else if (editedLine != "") //if there is already a line, just replacing the line with edited line
                    {
                        blockDatabase[counter] = editedLine;
                        File.WriteAllLines("blockDatabase.txt", blockDatabase);
                    }
                }
                updateFollowDatabase(thisClient, username, blockedUser);
            }
            blockDatabaseMutex.ReleaseMutex();
        }

        private void sendFollowedUsers(Socket thisClient, string username)
        {
            bool isAnyFollowed = false;
            string followedUsers = "";
            if (File.Exists("followDatabase.txt"))
            {
                string[] followDatabase = System.IO.File.ReadAllLines(@"followDatabase.txt");
                foreach (string line in followDatabase)
                {
                    string[] line2 = line.Split(new char[] { ' ' });
                    string fullUsername = "@" + username;

                    if (line2[0] == fullUsername)
                    {
                        Byte[] buffer = new Byte[128];
                        foreach (string user in line2.Skip(1))
                        {
                            isAnyFollowed = true;
                            buffer = Encoding.Default.GetBytes(user);
                            thisClient.Send(buffer);
                            System.Threading.Thread.Sleep(1);
                        }
                        
                    }
                }
                if (!isAnyFollowed)
                {
                    Byte[] buffer = new Byte[128];
                    buffer = Encoding.Default.GetBytes("You are not following anybody :( ..... yet :)");
                    thisClient.Send(buffer);
                }
                logs.AppendText("Followed users are sent to " + username + ".\n");
            }
        }

        private void updateFollowDatabase(Socket thisClient, string username, string blockedUser)
        {
            string editedLine = ""; //to modify the line
            int counter = 0;
            bool followDataExist = false;
            string followedUsers = "";

            string[] followDatabase = System.IO.File.ReadAllLines(@"followDatabase.txt");

            using (StreamWriter sw = File.CreateText(@"followDatabase.txt")) //if it the first time of follow database txt
            {
                sw.WriteLine(""); //the format is as follows: @userX name1 name2 name3(users that userX follows)
            }

            foreach (string line in followDatabase)
            {
                if (line != "")
                {
                    string[] line2 = line.Split(new char[] { ' ' });
                    if (line2[0] == ("@" + blockedUser))
                    {
                        followedUsers = line; //spotting the corresponding line, including the followed users names 
                        if (followedUsers.Contains(username))
                        {
                            editedLine = line.Replace(" " + username, "");
                            using (StreamWriter sw = File.AppendText(@"followDatabase.txt"))
                            {
                                sw.WriteLine(editedLine);
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(@"followDatabase.txt"))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }

        private void sendSweetsFromFollowed(Socket thisClient, string username)
        {
            string followedUsers = "";
            if (File.Exists("followDatabase.txt"))
            {
                string[] followDatabase = System.IO.File.ReadAllLines(@"followDatabase.txt");
                foreach (string line in followDatabase)
                {
                    string[] line2 = line.Split(new char[] { ' ' });
                    if (line2[0] == ("@" + username))
                    {
                        followedUsers = line; //spotting the corresponding line, including the followed users names 
                    }
                }

                if (File.Exists(@"sweets.txt"))
                {
                    Byte[] buffer = new Byte[128];
                    //send sweets
                    string[] sweets = System.IO.File.ReadAllLines(@"sweets.txt");

                    foreach (string sweet in sweets.Skip(1))
                    {
                        string userOfSweet = sweet.Split(new char[] { ' ' })[0];
                        if (username != userOfSweet && followedUsers.Contains(userOfSweet))
                        //going througha all sweets in database and matching the followedUsers name 
                        {
                            buffer = Encoding.Default.GetBytes(sweet);
                            thisClient.Send(buffer);
                            System.Threading.Thread.Sleep(1);
                        }
                    }
                    logs.AppendText("Sweets from followed users are sent to " + username + ".\n");
                }
            }
        }

        private void sendMySweets(Socket thisClient, string username)
        {
            if (File.Exists(@"sweets.txt"))
            {
                Byte[] buffer = new Byte[128];
                //send sweets
                string[] sweets = System.IO.File.ReadAllLines(@"sweets.txt");

                foreach (string sweet in sweets.Skip(1))
                {
                    string userOfSweet = sweet.Split(new char[] { ' ' })[0];
                    if (username == userOfSweet)
                    {
                        buffer = Encoding.Default.GetBytes(sweet);
                        thisClient.Send(buffer);
                        System.Threading.Thread.Sleep(1);
                    }
                }
                logs.AppendText("Sweets of " + username + " are sent to themselves.\n");
            }
        }
        private void Check(Socket thisClient) // updated
        {
            bool trying = true;
            string username = "A client";
            while (trying && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);

                    username = Encoding.Default.GetString(buffer);
                    username = username.Substring(0, username.IndexOf("\0"));
                    logs.AppendText("A client entered their username as; " + username + "\n");
                    if (clientNames.Contains(username))
                    {
                        logs.AppendText("The user " + username + " has already been connected. Therefore client does not connected to server.\n");
                        Byte[] buffer2 = Encoding.Default.GetBytes("This username is currently connected to the server.");
                        thisClient.Send(buffer2);
                        thisClient.Close();
                        trying = false;
                        break;
                    }
                    else if (checkUsername(username))
                    {
                        clientSockets.Add(thisClient);
                        clientNames.Add(username);
                        Byte[] buffer2 = Encoding.Default.GetBytes("You have successfully connected to the server.");
                        thisClient.Send(buffer2);
                        logs.AppendText(username + " has successfully connected.\n");
                        Thread receiveThread = new Thread(() => Receive(thisClient, username)); // updated
                        receiveThread.Start();

                        trying = false;
                    }
                    else
                    {
                        logs.AppendText("There is no such a user, client disconnected. \n");
                        Byte[] buffer2 = Encoding.Default.GetBytes("Username does not exist.");
                        thisClient.Send(buffer2);
                        thisClient.Close();
                        trying = false;
                        break;
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText(username + " has disconnected\n");
                    }
                    thisClient.Close();
                    trying = false;
                }
            }
        }

        private void deleteSweet(Socket thisClient, string username, string sweetID)
        {
            int counter = 0;
            bool isDeleted = false;
            mutex.WaitOne();
            if (File.Exists(@"sweets.txt"))
            {
                string[] sweetDatabase = System.IO.File.ReadAllLines(@"sweets.txt");
                foreach (string line in sweetDatabase.Skip(1))
                {
                    counter++;
                    if (line != "")
                    {
                        string[] line2 = line.Split(new char[] { ' ' });
                        if (line2[2] == sweetID && line2[0] == username)
                        {
                            sweetDatabase[counter] = "";
                            File.Delete(@"sweets.txt");
                            File.WriteAllLines(@"sweets.txt", sweetDatabase);
                            Byte[] buffer = Encoding.Default.GetBytes("You have deleted your sweet with id: " + sweetID + ".\n");
                            thisClient.Send(buffer);
                            isDeleted = true;
                            break;
                        }

                    }
                }
                if (!isDeleted)
                {
                    Byte[] buffer = Encoding.Default.GetBytes("You don't have a sweet with id: " + sweetID + ".\n");
                    thisClient.Send(buffer);
                }

            }
            mutex.ReleaseMutex();
        }

        private void Receive(Socket thisClient, string username) // updated
        {
            bool connected = true;
            bool followRequest = false;
            bool blockRequest = false;
            bool deleteSweetRequest = false;
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (followRequest)
                    {
                        logs.AppendText(username + " wanted to follow " + incomingMessage + "\n");
                        Thread followUserThread = new Thread(() => followUser(thisClient, username, incomingMessage));
                        followUserThread.Start();
                        followRequest = false;
                    }
                    else if (blockRequest)
                    {
                        logs.AppendText(username + " wanted to block " + incomingMessage + "\n");
                        Thread blockUserThread = new Thread(() => blockUser(thisClient, username, incomingMessage));
                        blockUserThread.Start();
                        blockRequest = false;
                    }
                    else if (deleteSweetRequest)
                    {
                        logs.AppendText(username + " wanted to delete their sweet with id:" + incomingMessage + "\n");
                        Thread deleteSweetThread = new Thread(() => deleteSweet(thisClient, username, incomingMessage));
                        deleteSweetThread.Start();
                        deleteSweetRequest = false;
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st")
                    {
                        Thread sendSweetsThread = new Thread(() => sendSweets(thisClient, username));
                        sendSweetsThread.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st_v3rsion_2")
                    {
                        Thread sendSweetsFromFollowedUsersThread = new Thread(() => sendSweetsFromFollowed(thisClient, username));
                        sendSweetsFromFollowedUsersThread.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st_users")
                    {
                        Thread sendUserNames = new Thread(() => sendUsers(thisClient, username));
                        sendUserNames.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st_followed_users")
                    {
                        Thread sendFollowedUsersNames = new Thread(() => sendFollowedUsers(thisClient, username));
                        sendFollowedUsersNames.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st_following_users")
                    {
                        Thread sendFollowingUsersNames = new Thread(() => sendFollowingUsers(thisClient, username));
                        sendFollowingUsersNames.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_follow")
                    {
                        followRequest = true;
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_block")
                    {
                        blockRequest = true;
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_r3qu3st_mySweets")
                    {
                        Thread sendSweetsThread = new Thread(() => sendMySweets(thisClient, username));
                        sendSweetsThread.Start();
                    }
                    else if (incomingMessage == "s0_call3d_h1dd3n_delete_sweet")
                    {
                        deleteSweetRequest = true;
                    }
                    else
                    {

                        buffer = Encoding.Default.GetBytes("You sent a sweet!");
                        thisClient.Send(buffer);
                        logs.AppendText(username + " has sent a sweet \n");
                        Thread storeSweetThread = new Thread(() => storeSweet(username, incomingMessage));
                        storeSweetThread.Start();
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText(username + " has disconnected.\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    clientNames.Remove(username);
                    connected = false;
                }
            }

        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            string[] sweetsUpdate = System.IO.File.ReadAllLines(@"sweets.txt");
            sweetsUpdate[0] = counter.ToString();
            File.WriteAllLines(@"sweets.txt", sweetsUpdate);
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;
            if (message != "" && message.Length <= 128)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                foreach (Socket client in clientSockets)
                {
                    try
                    {
                        client.Send(buffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }

                }
            }
        }

        private void logs_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
