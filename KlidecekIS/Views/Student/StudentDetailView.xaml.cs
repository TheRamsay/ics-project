using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlidecekIS.ViewModels;

namespace KlidecekIS.Views.Student;

public partial class StudentDetailView
{
    public StudentDetailView(StudentDetailViewModel viewModel): base(viewModel)
    {
        InitializeComponent();
    }
}