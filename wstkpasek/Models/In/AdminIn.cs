using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.TrainingModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace wstkpasek.Models.In
{
  public class AdminIn
  {
    private string resetPasswordEmail = "";
    private string resetPasswordName = "";
    private string resetPasswordLastName = "";
    private string trainingUser = "";
    private string trainingName = "";
    private string seriesUser = "";
    private string seriesExerciseName = "";
    private string exerciseUser = "";
    private string exerciseName = "";
    private string exercisePart = "";
    private string scheduleTrainingName = "";
    private string scheduleTrainingUser = "";
    private string scheduleSeriesName = "";
    private string scheduleSeriesPart = "";
    private string scheduleSeriesUser = "";
    private string scheduleExerciseName = "";
    private string scheduleExerciseUser = "";
    private string scheduleExercisePart = "";

    public DateTime ScheduleTrainingStartDate { get; set; }
    public DateTime ScheduleTrainingEndDate { get; set; }
    public bool ScheduleTrainingFinish { get; set; } = false;
    public DateTime ScheduleSeriesStartDate { get; set; }
    public DateTime ScheduleSeriesEndDate { get; set; }
    public bool ScheduleSeriesFinish { get; set; } = false;
    public bool ScheduleExerciseStarted { get; set; } = false;
    public DateTime ScheduleExericeStartDate { get; set; }
    public DateTime ScheduleExerciseEndDate { get; set; }
    public string TrainingName
    {
      get => this.trainingName; 
      set
      {
        if (value != null) trainingName = value;
        else this.trainingName = "";
      }
    }
    public string ResetPasswordEmail
    {
      get => resetPasswordEmail; 
      set
      {
        if (value != null) resetPasswordEmail = value;
        else this.resetPasswordEmail = "";
      }
    }
    public string ResetPasswordName
    {
      get => resetPasswordName; 
      set
      {
        if (value != null) resetPasswordName = value;
        else this.resetPasswordName = "";
      }
    }
    public string ResetPasswordLastName
    {
      get => resetPasswordLastName; 
      set
      {
        if (value != null) resetPasswordLastName = value;
        else this.resetPasswordLastName = "";
      }
    }
    public string TrainingUser
    {
      get => trainingUser; 
      set
      {
        if (value != null) trainingUser = value;
        else this.trainingUser = "";
      }
    }
    public string SeriesUser
    {
      get => seriesUser; 
      set
      {
        if (value != null) seriesUser = value;
        else this.seriesUser = "";
      }
    }
    public string SeriesExerciseName
    {
      get => seriesExerciseName; 
      set
      {
        if (value != null) seriesExerciseName = value;
        else this.seriesExerciseName = "";
      }
    }
    public string ExerciseUser
    {
      get => exerciseUser; 
      set
      {
        if (value != null) exerciseUser = value;
        else this.exerciseUser = "";
      }
    }
    public string ExerciseName
    {
      get => exerciseName; 
      set
      {
        if (value != null) exerciseName = value;
        else this.exerciseName = "";
      }
    }
    public string ExercisePart
    {
      get => exercisePart; 
      set
      {
        if (value != null) exercisePart = value;
        else this.exercisePart = "";
      }
    }
    public string ScheduleTrainingName
    {
      get => scheduleTrainingName; 
      set
      {
        if (value != null) scheduleTrainingName = value;
        else this.scheduleTrainingName = "";
      }
    }
    public string ScheduleTrainingUser
    {
      get => scheduleTrainingUser; 
      set
      {
        if (value != null) scheduleTrainingUser = value;
        else this.scheduleTrainingUser = "";
      }
    }
    public string ScheduleSeriesName
    {
      get => scheduleSeriesName; 
      set
      {
        if (value != null) scheduleSeriesName = value;
        else this.scheduleSeriesName = "";
      }
    }
    public string ScheduleSeriesPart
    {
      get => scheduleSeriesName; 
      set
      {
        if (value != null) scheduleSeriesPart = value;
        else this.scheduleSeriesPart = "";
      }
    }
    public string ScheduleSeriesUser
    {
      get => scheduleSeriesUser; 
      set
      {
        if (value != null) scheduleSeriesUser = value;
        else this.scheduleSeriesUser = "";
      }
    }
    public string ScheduleExerciseName
    {
      get => scheduleExerciseName; 
      set
      {
        if (value != null) scheduleExerciseName = value;
        else this.scheduleExerciseName = "";
      }
    }
    public string ScheduleExerciseUser
    {
      get => scheduleExerciseUser; 
      set
      {
        if (value != null) scheduleExerciseUser = value;
        else this.scheduleExerciseUser = "";
      }
    }
    public string ScheduleExercisePart
    {
      get => scheduleExercisePart; 
      set
      {
        if (value != null) scheduleExercisePart = value;
        else this.scheduleExercisePart = "";
      }
    }
  }
}
