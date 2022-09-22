using System;
using System.Collections.Generic;

namespace ORMandDapperExercise
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        void InsertDepartment(string newDepartmentName);
    }
}
