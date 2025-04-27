using SimpleCleanArch.Tests.Api;

namespace SimpleCleanArch.Tests.Other;

[Collection("Test.Api.ManyToManyTest")]
public class ManyToManyTest : BaseControllerTest
{
    protected override async Task CleanDatabase(AppDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM employees;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM projects;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM employees_projects;");
    }

    [Fact]
    public async Task EmployeesProjects()
    {
        var context = await CreateContext();
        var employee = new EmployeeSchema()
        {
            Name = "fulano",
            Document = "ful-123",
        };
        var project1 = new ProjectSchema()
        {
            Name = "my project",
        };
        var project2 = new ProjectSchema()
        {
            Name = "my project 2",
        };
        employee.Projects.Add(project1);
        employee.Projects.Add(project2);
        project1.Employees.Add(employee);
        project2.Employees.Add(employee);
        await context.Employees.AddAsync(employee);
        await context.Projects.AddAsync(project1);
        await context.Projects.AddAsync(project2);
        await context.SaveChangesAsync();
        var savedEmployee = await context.Employees.Include("Projects").FirstOrDefaultAsync(e => e.Id == employee.Id);
        Assert.NotNull(savedEmployee);
        Assert.Equal(2, savedEmployee.Projects.Count);
        var savedProject1 = await context.Projects.Include("Employees").FirstOrDefaultAsync(p => p.Id == project1.Id);
        Assert.NotNull(savedProject1);
        Assert.Single(savedProject1.Employees);
        var savedProject2 = await context.Projects.Include("Employees").FirstOrDefaultAsync(p => p.Id == project2.Id);
        Assert.NotNull(savedProject2);
        Assert.Single(savedProject2.Employees);
    }
}