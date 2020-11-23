using Miracle.FileZilla.Api;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;
using Session = WinSCP.Session;

namespace ConexionSftp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = "sftp.maitsa.com",
                UserName = "idrtext",                
                SshHostKeyFingerprint = null,
                //RUTA DEL ARCHIVO DE LA KEY PARA CONEXION AL SFTOP
                SshPrivateKeyPath = @"C:\NOVOTIC\CLIENTES\IDRTEXT\SFTP MAITSA\private-key.ppk",

            };

            using (Session session = new Session())
            {
                session.ScanFingerprint(sessionOptions,"SHA-256");

                sessionOptions.SshHostKeyFingerprint= session.ScanFingerprint(sessionOptions, "SHA-256");
                Console.WriteLine(session.ScanFingerprint(sessionOptions, "SHA-256"));

                session.Open(sessionOptions);


              
                string xmlLogPath = session.XmlLogPath;
                object p = xmlLogPath;

                Console.WriteLine(p.ToString());

                TransferOptions oTransferOptions = new TransferOptions();
                oTransferOptions.TransferMode = WinSCP.TransferMode.Binary;

                TransferOperationResult transferResult;

                transferResult = session.GetFiles("/sftp-maitsa/customs-broker-integration/api/prod/idrtext/processed/PO626/*", @"C:\NOVOTIC\CLIENTES\IDRTEXT\SFTP MAITSA\", false, oTransferOptions);

                transferResult.Check();

                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
