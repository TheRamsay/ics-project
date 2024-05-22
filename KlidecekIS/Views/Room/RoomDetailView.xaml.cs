using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlidecekIS.ViewModels;

namespace KlidecekIS.Views.Room;

public partial class RoomDetailView
{
    public RoomDetailView(RoomDetailViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }
}