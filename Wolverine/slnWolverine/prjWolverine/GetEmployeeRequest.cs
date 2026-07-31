namespace prjWolverine;

public record GetEmployeeRequest(int EmployeeIde);
public record GetEmployeeResponse(int EmployeeId, string EmployeeName, DateTime EmployeeDOB);