namespace Ticketing.Domain.Models.Reports;

public class EdutionReportDto
{
    public string CourseTitle { get; set; }
    public string OrganizationName { get; set; }
    public string Teachername { get; set; }
    public string ClassRoomType { get; set; }
    public string ClassDurationHours { get; set; }
    public string Venue { get; set; }
    public string StartClassDate { get; set; }
    public string EndClassDate { get; set; }
    public string StartClassTime { get; set; }
    public string EndClassTime { get; set; }
}
public class Person
{
    public string FullName { get; set; }
    public string PersonalCode { get; set; }
    public string DepartmentTitle { get; set; }
}
public static class StaticEducationData
{
    public static EdutionReportDto EducationReport => new()
    {
        CourseTitle = "/آموزش مبانی برق صنعتی آموزش مبانی برق صنعتی آموزش مبانی برق صنعتی آموزش مبانی برق صنعتی آموزش مبانی برق صنعتی",
        OrganizationName = "شرکت توسعه فناوری نیرو",
        Teachername = "مهندس محمدی",
        ClassRoomType = "حضوری - کارگاه برق",
        ClassDurationHours = "40 ساعت",
        Venue = "کارگاه شماره ۲ - طبقه همکف",
        StartClassDate = "1404/07/10",
        EndClassDate = "1404/07/15",
        StartClassTime = "08:30",
        EndClassTime = "12:30"
    };

    public static List<Person> Participants =>
        [
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
            new Person { FullName = "علی رضایی علی رضایی علی رضایی علی رضایی", PersonalCode = "P-1001", DepartmentTitle = "تعمیرات مکانیک" },
            new Person { FullName = "رضا احمدی رضا احمدی رضا احمدی رضا احمدی", PersonalCode = "P-1002", DepartmentTitle = "واحد برق صنعتی" },
            new Person { FullName = "سارا محمدی سارا محمدی سارا محمدی سارا محمدی", PersonalCode = "P-1003", DepartmentTitle = "کنترل کیفیت" },
            new Person { FullName = "حمید نادری حمید نادری حمید نادری حمید نادری", PersonalCode = "P-1004", DepartmentTitle = "انبار قطعات" },
            new Person { FullName = "مریم کریمی مریم کریمی مریم کریمی مریم کریمی", PersonalCode = "P-1005", DepartmentTitle = "منابع انسانی" },
    ];
}