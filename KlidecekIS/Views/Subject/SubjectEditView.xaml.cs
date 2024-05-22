using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlidecekIS.ViewModels;

namespace KlidecekIS.Views.Subject;

public partial class SubjectEditView
{
    public SubjectEditView(SubjectEditViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }
}