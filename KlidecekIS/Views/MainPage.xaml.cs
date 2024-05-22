using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlidecekIS.Services.Interfaces;
using KlidecekIS.ViewModels;
using KlidecekIS.Views;

namespace KlidecekIS.Views;

public partial class MainPage
{
    public MainPage(MainPageViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
