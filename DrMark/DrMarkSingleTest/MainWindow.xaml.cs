﻿using AxMMEditx64Lib;
using AxMMIOx64Lib;
using AxMMLensCalx64Lib;
using AxMMMarkx64Lib;
using AxMMStatusx64Lib;
using DRsoft.Runtime.Core.XcMarkCard;
using System.Windows;
using System.Windows.Forms.Integration;

namespace DrMarkSingleTest;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    System.Windows.Controls.Panel panels;

    DrMarkAdaptor drMarkAdaptor = new DrMarkAdaptor();

    public MainWindow()
    {
        InitializeComponent();

        panels = DrmarkShow;
        ThreadExitis();
        drMarkAdaptor.InitiaPanel(panels, 1);

        this.Loaded += MainWindow_Loaded;
    }

    public void ThreadExitis(string threadName = "DRMark")
    {
        System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcessesByName(threadName);
        if (processList.Any())
        {
            foreach (var process in processList)
            {
                process.Kill(); //结束进程 
            }
        }
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        drMarkAdaptor.InitialExt();
        Task.Run(async ()=>
        {
            while (true)
            {
                try
                {
                    var ret = drMarkAdaptor.LaserOff(1);
                    ret = drMarkAdaptor.LaserOff(2);

                    ret = drMarkAdaptor.JumpToStartPos(1);

                    ret = drMarkAdaptor.JumpToStartPos(2);

                    ret = drMarkAdaptor.StopAutomation(1);

                    ret = drMarkAdaptor.StopAutomation(2);

                    ret = drMarkAdaptor.StartAutomation(1);

                    ret = drMarkAdaptor.StartAutomation(1);

                    ret = drMarkAdaptor.SetMatrix(1,1,1,1);

                    ret = drMarkAdaptor.SetMatrix(2,2,2,2);
                }
                catch (Exception ex)
                {

                }
                await Task.Delay(1000);
            }
        });
    }
}