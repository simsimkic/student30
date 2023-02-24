﻿using System.Windows.Input;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToggleSwitch
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        public Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        public Thickness RightSide = new Thickness(0, 0, -39, 0);
        public SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        public SolidColorBrush On = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        private bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool Toggled1 { get => Toggled; set => Toggled = value; }

    }
}
