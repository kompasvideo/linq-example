//Написать консольную программу на Net для решения задачи, с использованием LINQ
//    - уникальное количество пользователей (userid)
//    - уникальное количество организаций (orgid)
//    - уникальное количество пользователей - сотрудников организаций
//    - уникальное количество департаментов (DepartmentId)
//    - уникальное количество пользователей относящихся к департаменту

class Program
{
    class TestData
    {
        public int Id;
        public int UserId;
        public int OrgId;
        public int DepartmentId;
    };

    static readonly List<TestData> Data = new List<TestData>
    {
        new TestData { Id = 1, UserId = 1 },
        new TestData { Id = 1, UserId = 2 },
        new TestData { Id = 1, UserId = 3 },
        new TestData { Id = 1, UserId = 3 },
        new TestData { Id = 2, UserId = 1 },
        new TestData { Id = 2, UserId = 2 },
        new TestData { Id = 2, UserId = 2 },
        new TestData { Id = 3, UserId = 1 },
        new TestData { Id = 3, UserId = 2 },
        new TestData { Id = 3, UserId = 2 },
        new TestData { Id = 1, UserId = 1, OrgId = 1 },
        new TestData { Id = 1, UserId = 2, OrgId = 2 },
        new TestData { Id = 1, UserId = 3, OrgId = 3 },
        new TestData { Id = 1, UserId = 3, OrgId = 3 },
        new TestData { Id = 2, UserId = 1, OrgId = 4 },
        new TestData { Id = 2, UserId = 1, OrgId = 2 },
        new TestData { Id = 2, UserId = 1, OrgId = 3 },
        new TestData { Id = 2, UserId = 2, OrgId = 2 },
        new TestData { Id = 2, UserId = 2, OrgId = 2 },
        new TestData { Id = 3, UserId = 1, OrgId = 3 },
        new TestData { Id = 3, UserId = 2, OrgId = 3 },
        new TestData { Id = 3, UserId = 2, OrgId = 3 },
        new TestData { Id = 3, UserId = 2, OrgId = 4 },
        new TestData { Id = 1, UserId = 11, OrgId = 11, DepartmentId = 1 },
        new TestData { Id = 1, UserId = 12, OrgId = 12, DepartmentId = 2 },
        new TestData { Id = 1, UserId = 13, OrgId = 13, DepartmentId = 3 },
        new TestData { Id = 2, UserId = 11, OrgId = 14, DepartmentId = 3 },
        new TestData { Id = 2, UserId = 12, OrgId = 12, DepartmentId = 4 },
        new TestData { Id = 3, UserId = 11, OrgId = 13, DepartmentId = 4 },
        new TestData { Id = 3, UserId = 12, OrgId = 13, DepartmentId = 4 }
    };

    class UserIdComparer : IEqualityComparer<TestData>
    {
        public bool Equals(TestData? x, TestData? y)
        {
            return Equals(x?.UserId, y?.UserId);
        }

        public int GetHashCode(TestData obj)
        {
            return obj.UserId;
        }
    }

    class OrgIdComparer : IEqualityComparer<TestData>
    {
        public bool Equals(TestData? x, TestData? y)
        {
            if (x.OrgId == 0 & y.OrgId == 1) 
                return true;
            return Equals(x?.OrgId, y?.OrgId);
        }

        public int GetHashCode(TestData obj)
        {
            if (obj.OrgId == 0) return 1;
            return obj.OrgId;
        }
    }

    class UserOrgIdComparer : IEqualityComparer<TestData>
    {
        public bool Equals(TestData? x, TestData? y)
        {
            if (x.OrgId == 0 & y.OrgId == 1) 
                return true;
            return Equals(x?.UserId * 47 + x.OrgId, y?.UserId * 47 + y.OrgId);
        }

        public int GetHashCode(TestData obj)
        {
            if (obj.OrgId == 0) return obj.UserId * 47 + 1;
            return obj.UserId * 47 + obj.OrgId;
        }
    }

    public static void Main(string[] args)
    {
        var userIds = Data.Distinct(new UserIdComparer());
        Console.WriteLine(userIds.Count());
        Console.WriteLine(Data.Distinct(new OrgIdComparer()).Count());
        Console.WriteLine(Data.Distinct(new UserOrgIdComparer()).Count());
        
    }
}