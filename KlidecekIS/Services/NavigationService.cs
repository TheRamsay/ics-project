using KlidecekIS.Models;
using KlidecekIS.Services.Interfaces;
using KlidecekIS.ViewModels;
using KlidecekIS.ViewModels.Activity;
using KlidecekIS.Views.Grade;
using KlidecekIS.Views.Student;
using KlidecekIS.Views.Subject;

namespace KlidecekIS.Services;

public class NavigationService: INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//home/students", typeof(StudentListView), typeof(StudentListViewModel)),
        new("//home/students/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        new("//home/students/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("//home/students/edit/enroll", typeof(StudentSubjectEditView), typeof(StudentSubjectEditViewModel)),
        new("//home/students/edit/grade", typeof(GradeEditView), typeof(GradeEditViewModel)),

        new("//home/subject", typeof(SubjectListView), typeof(SubjectListViewModel)),
        new("//home/subject/detail", typeof(SubjectDetailView), typeof(SubjectDetailViewModel)),
        new("//home/subject/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
        new("//home/subject/edit/enroll", typeof(SubjectActivityEditView), typeof(SubjectActivityEditViewModel)),


        new("//home/grade", typeof(GradeListView), typeof(GradeListViewModel)),
        new("//home/grade/detail", typeof(GradeDetailView), typeof(GradeDetailViewModel)),
        new("//home/grade/edit", typeof(GradeEditView), typeof(GradeEditViewModel)),

    };
    
    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}