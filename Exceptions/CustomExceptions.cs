namespace DDD.APP.Exceptions;

public class EmployeeAlreadyExistsException() : Exception("Сотрудник с таким именем уже существует.");

public class EmployeeNotFoundException() : Exception("Сотрудник не найден.");