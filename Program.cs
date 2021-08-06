using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using SewLib;
using System.IO.Ports;



class Testing
{

    SewDevice myDevice;
    SerialPort comPort;
    public Testing()
    {
        //InitializeComponent();
         
        //comPort = new SerialPort("/dev/rfcomm0",115200,Parity.None,8,StopBits.One);
        //comPort.Close();
        comPort = new SerialPort("/dev/rfcomm0",115200);
        //comPort.Open();

        myDevice = new SewDevice(ref comPort);
        //this.listBox1.Enabled = false;
        //this.listBox1.Items.Clear();
        myDevice.OnSamplesReceived += new ReceivedSamplesHandler(ReceiveData);

    }
    //private void btstart_Click(object sender, EventArgs e)
    private void btstart_Click()
    {
        eErrorClassOpcodes reply = myDevice.StartStreaming();
        Console.WriteLine(reply);
        if (reply == eErrorClassOpcodes.Ok)
        {
            // ok device is connected and streaming is started
        }
        else
        {
            // check which error occurred and do something
        }
        
    }
    //private void btstop_click(object sender, eventargs e)
    private void btstop_Click()
    {   
        eErrorClassOpcodes reply = myDevice.StopStreaming();
        if (reply == eErrorClassOpcodes.Ok)
        {
            
            // ok device is connected and streaming is started
        }
        else
        {
            // check which error occurred and do something
        }
        
    }

    private void close_port()
    {
        comPort.Close();
    }

    private void open_port()
    {
        comPort.Open();
    }
    // used to receive the data
    public void ReceiveData(object sender, SewDeviceEventArgs e)
    //public void ReceiveData(SewDeviceEventArgs e)

    {
        Console.WriteLine("herererer");
        foreach (SewChannelStream channelstream in e.samplesList)
        {
            switch (channelstream.channel)
            {
                case eChannelNumb.MagX:
                    foreach (SewSample sample in channelstream.samples)
                    {
                       
                        Console.WriteLine(sample.sample);
                    }
                    break;
                case eChannelNumb.RespPiezo:
                    foreach (SewSample sample in channelstream.samples)
                    {
                        // do something here with each samples
                        // from this channel
                    }
                    break;
                //
                // add here all the channel to be monitored
                //
                default:
                    break;
            }
        }
    }
    
    static void Main(string[] args)
    {
       
        Testing acquire = new Testing();
       
        
        acquire.btstart_Click();
        //form.ReceiveData();
        //SewDeviceEventArgs channel list = new SewDeviceExportEventArgs();
        //acquire.ReceiveData();
        Console.WriteLine("yo");
        acquire.comPort.Close();
        acquire.comPort.Open();
        //acquire.btstop_Click();
        //Console.Read();
        
        
    }


}
