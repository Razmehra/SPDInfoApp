﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPageStudentInfo : TabbedPage
    {
        public TabPageStudentInfo ()
        {
            InitializeComponent();

            Children.Add(new EntryPage());
            Children.Add(new StudentFeedback());

        }
    }
}