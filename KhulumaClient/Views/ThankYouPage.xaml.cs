﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KhulumaClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThankYouPage : ContentPage
    {
        public ThankYouPage()
        {
            InitializeComponent();

            thankyouImage.Aspect = Aspect.AspectFit;
            thankyouImage.Source = ImageSource.FromResource("KhulumaClient.thankyoupage.png");
        }
    }
}