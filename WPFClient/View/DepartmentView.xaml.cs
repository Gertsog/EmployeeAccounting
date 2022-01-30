﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : Window
    {
        private DepartmentVM vm;
        public DepartmentView(DepartmentVM vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
    }
}
