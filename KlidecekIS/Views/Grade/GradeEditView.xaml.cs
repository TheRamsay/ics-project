using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlidecekIS.ViewModels;

namespace KlidecekIS.Views.Grade;

public partial class GradeEditView
{
    public GradeEditView(GradeEditViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }
}