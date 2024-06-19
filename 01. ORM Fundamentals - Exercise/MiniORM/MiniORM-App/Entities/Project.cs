﻿namespace MiniORM.App.Entities;

using MiniORM.App.Entities;
using System.ComponentModel.DataAnnotations;

public class Project
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }

    public ICollection<EmployeeProject> EmployeeProjects { get; set; }
}