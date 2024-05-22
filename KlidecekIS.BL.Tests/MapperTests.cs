using AutoMapper;
using KlidecekIS.BL.Mappers;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using Xunit;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class MapperTests
{
    [Fact]
    public void MapperConfigurationIsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<StudentMapperProfile>();
            cfg.AddProfile<RoomMapperProfile>();
            cfg.AddProfile<ActivityMapperProfile>();
            cfg.AddProfile<SubjectMapperProfile>();
            cfg.AddProfile<GradeMapperProfile>();
            cfg.AddProfile<StudentSubjectMapperProfile>();
        });

        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void MapperStudenEntityToStudentDetailModel()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<StudentMapperProfile>(); });

        var mapper = config.CreateMapper();
        var studentEntity = new StudentEntity()
        {
            Id = Guid.NewGuid(),
            Name = "David",
            Surname = "Klidečka",
            ImageUrl = string.Empty
        };
        
        var studentDetailModel = mapper.Map<StudentDetailModel>(studentEntity);
        Assert.Equal(studentEntity.Id, studentDetailModel.Id);
        Assert.Equal(studentEntity.Name, studentDetailModel.Name);
        Assert.Equal(studentEntity.Surname, studentDetailModel.Surname);
        Assert.Equal(studentEntity.ImageUrl, studentDetailModel.ImageUrl);
    }
    
    [Fact]
    public void MapperStudentDetailModelToStudentEntity()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<StudentMapperProfile>(); });

        var mapper = config.CreateMapper();
        var studentDetailModel = new StudentDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "David",
            Surname = "Klidečka",
            ImageUrl = string.Empty
        };
        
        var studentEntity = mapper.Map<StudentEntity>(studentDetailModel);
        Assert.Equal(studentDetailModel.Id, studentEntity.Id);
        Assert.Equal(studentDetailModel.Name, studentEntity.Name);
        Assert.Equal(studentDetailModel.Surname, studentEntity.Surname);
        Assert.Equal(studentDetailModel.ImageUrl, studentEntity.ImageUrl);
    }
    
    [Fact]
    public void MapperStudentEntityToStudentListModel()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<StudentMapperProfile>(); });

        var mapper = config.CreateMapper();
        var studentEntity = new StudentEntity()
        {
            Id = Guid.NewGuid(),
            Name = "David",
            Surname = "Klidečka",
            ImageUrl = string.Empty
        };
        
        var studentListModel = mapper.Map<StudentListModel>(studentEntity);
        Assert.Equal(studentEntity.Id, studentListModel.Id);
        Assert.Equal(studentEntity.Name, studentListModel.Name);
        Assert.Equal(studentEntity.Surname, studentListModel.Surname);
        Assert.Equal(studentEntity.ImageUrl, studentListModel.ImageUrl);
    }
    
    [Fact]
    public void MapperStudentListModelToStudentEntity()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<StudentMapperProfile>(); });

        var mapper = config.CreateMapper();
        var studentListModel = new StudentListModel()
        {
            Id = Guid.NewGuid(),
            Name = "David",
            Surname = "Klidečka",
            ImageUrl = string.Empty
        };
        
        var studentEntity = mapper.Map<StudentEntity>(studentListModel);
        Assert.Equal(studentListModel.Id, studentEntity.Id);
        Assert.Equal(studentListModel.Name, studentEntity.Name);
        Assert.Equal(studentListModel.Surname, studentEntity.Surname);
        Assert.Equal(studentListModel.ImageUrl, studentEntity.ImageUrl);
    }
    
    [Fact]
    public void MapperGradeEntityToGradeListModel()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<GradeMapperProfile>(); });

        var mapper = config.CreateMapper();
        var gradeEntity = new GradeEntity()
        {
            Id = Guid.NewGuid(),
            Score = 10.0,
            Note = "Good job",
            ActivityId = Guid.NewGuid(),
            StudentId = Guid.NewGuid()
        };
        
        var gradeListModel = mapper.Map<GradeListModel>(gradeEntity);
        Assert.Equal(gradeEntity.Id, gradeListModel.Id);
        Assert.Equal(gradeEntity.Score, gradeListModel.Score);
        Assert.Equal(gradeEntity.Note, gradeListModel.Note);
    }
    
    [Fact]
    public void MapperGradeListModelToGradeEntity()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<GradeMapperProfile>(); });

        var mapper = config.CreateMapper();
        var gradeListModel = new GradeListModel()
        {
            Id = Guid.NewGuid(),
            Score = 10.0,
            Note = "Good job",
            ActivityId = Guid.NewGuid(),
            StudentId = Guid.NewGuid()
        };
        
        var gradeEntity = mapper.Map<GradeEntity>(gradeListModel);
        Assert.Equal(gradeListModel.Id, gradeEntity.Id);
        Assert.Equal(gradeListModel.Score, gradeEntity.Score);
        Assert.Equal(gradeListModel.Note, gradeEntity.Note);
    }
    
    [Fact]
    public void MapperGradeDetailModelToGradeEntity()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<GradeMapperProfile>(); });

        var mapper = config.CreateMapper();
        var gradeDetailModel = new GradeDetailModel()
        {
            Id = Guid.NewGuid(),
            Score = 10.0,
            Note = "Good job",
            ActivityId = Guid.NewGuid(),
            StudentId = Guid.NewGuid()
        };
        
        var gradeEntity = mapper.Map<GradeEntity>(gradeDetailModel);
        Assert.Equal(gradeDetailModel.Id, gradeEntity.Id);
        Assert.Equal(gradeDetailModel.Score, gradeEntity.Score);
        Assert.Equal(gradeDetailModel.Note, gradeEntity.Note);
        Assert.Equal(gradeDetailModel.ActivityId, gradeEntity.ActivityId);
        Assert.Equal(gradeDetailModel.StudentId, gradeEntity.StudentId);
    }
    
    [Fact]
    public void MapperGradeEntityToGradeDetailModel()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<GradeMapperProfile>(); });

        var mapper = config.CreateMapper();
        var gradeEntity = new GradeEntity()
        {
            Id = Guid.NewGuid(),
            Score = 10.0,
            Note = "Good job",
            ActivityId = Guid.NewGuid(),
            StudentId = Guid.NewGuid()
        };
        
        var gradeDetailModel = mapper.Map<GradeDetailModel>(gradeEntity);
        Assert.Equal(gradeEntity.Id, gradeDetailModel.Id);
        Assert.Equal(gradeEntity.Score, gradeDetailModel.Score);
        Assert.Equal(gradeEntity.Note, gradeDetailModel.Note);
        Assert.Equal(gradeEntity.ActivityId, gradeDetailModel.ActivityId);
        Assert.Equal(gradeEntity.StudentId, gradeDetailModel.StudentId);
    }
}